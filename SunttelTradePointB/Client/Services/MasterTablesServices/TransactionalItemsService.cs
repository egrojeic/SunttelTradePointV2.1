using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;


namespace SunttelTradePointB.Client.Services.MasterTablesServices
{
    public class TransactionalItemsService : ITransactionalItems
    {
        #region Property
        public string? transactionalItemId { get; set; }
        public string? Page { get; set; }
        public TransactionalItem? TransactionalItems { get; set; }
        public Box? BoxSelectedItems { get; set; }
        public SeasonBusiness? SeasonBusinessSelectedItems { get; set; }
        public ConceptGroup? ConceptGroupSelectedItems { get; set; }
        public TransactionalItemStatus? ConceptStatusSelectedItems { get; set; }
        public TransactionalItemType? ConceptTransactionalItemType { get; set; }
        public PackingSpecs? ConceptTransactionalItemPackingSpecs { get; set; }
        public enum UploadingFileType
        {
            TransactionalItemImage,
            EntityImage
        }
        public string Host { get { return "https://localhost:7186/uploads/transactionalItemsImages/"; } }
        #endregion Property

        private readonly HttpClient _httpClient;
        public IWebAssemblyHostEnvironment environment { get; set; }
        List<TransactionalItem>? transactionalItemsList;
        List<Concept> conceptList;
        List<ConceptGroup> conceptGroupsList;
        List<ProductModel> ProductModelList;
        List<Box> boxsList;
        List<TransactItemImage> transactItemImagesList;
        List<TransactionalItemQualityPair> transactionalItemQualityPairsList;
        List<PackingSpecs> packingSpecs;
        List<TransactionalItemProcessStep> processSteps;
        List<TransactionalItemTag> transactionalItemTags;
        List<Box> boxes;
        List<SeasonBusiness> seasonBusiness;
        List<TransactionalItemType> transactionalItemTypes;
        List<TransactionalItemStatus> transactionalItemStatuss;
        public TransactionalItemsService(HttpClient httpClient, IWebAssemblyHostEnvironment environment)
        {
            _httpClient = httpClient;
            this.environment = environment;
        }
        public async Task<List<TransactionalItem>> GetTransactionalItemsList(int? page = 1, int? perPage = 10, string? nameLike = null, string? className = null, string? Code = null, bool forceRefresh = false)
        {
            string namteToFind = nameLike != null ? nameLike : "";

            try
            {
                transactionalItemsList = await _httpClient.GetFromJsonAsync<List<TransactionalItem>>($"api/TransactionalItems/GetTransactionalItems?page=1&perPage=10&filterName={namteToFind}");
                page = page == 0 ? 1 : page;

                if ((transactionalItemsList.Count() - page) >= perPage) transactionalItemsList.ToList().GetRange((int)page, (int)perPage);
                if ((transactionalItemsList.Count() - page) < perPage) transactionalItemsList.GetRange((int)page, (transactionalItemsList.Count() - 1));
                return transactionalItemsList != null ? transactionalItemsList : new List<TransactionalItem>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }
        public async Task<TransactionalItem> GetTransactionalItemById(string? transactionalItemId = null)
        {
            transactionalItemId = transactionalItemId != null ? transactionalItemId : "";
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var transactionalItem = await _httpClient.GetFromJsonAsync<TransactionalItem>($"api/TransactionalItems/GetTransactionalItemById?userId={userId}&ipAddress={ipAddress}&filterName={transactionalItemId}");

                return transactionalItem != null ? transactionalItem : new TransactionalItem();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }
        public async Task<List<Concept>> GetSelectorListEntityActors(string? nameLike = null, string roleName = null)
        {
            string namteToFind = nameLike != null ? nameLike : "";

            try
            {

                conceptList = await _httpClient.GetFromJsonAsync<List<Concept>>($"api/ConceptsSelector/GetSelectorListEntityActor?filterString={namteToFind}&roleName={roleName}");
                return conceptList != null ? conceptList : new List<Concept>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }
        public async Task<List<ProductModel>> GetSelectorListEntityProductModel(string? transactionalItemId = null)
        {

            try
            {

                ProductModelList = await _httpClient.GetFromJsonAsync<List<ProductModel>>($"/api/ConceptsSelector/GetSelectorListTransactionalItemModels?transactionalItemId={transactionalItemId}");
                return ProductModelList != null ? ProductModelList : new List<ProductModel>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }
        public async Task<List<Box>> GetSelectorListEntityBoxToSale()
        {
            try
            {

                boxsList = await _httpClient.GetFromJsonAsync<List<Box>>($"​/api​/ConceptsSelector​/GetSelectorListBoxes");

                return boxsList != null ? boxsList : new List<Box>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }
        public async Task<List<Box>> GetSelectorListEntityBoxToBuy()
        {

            try
            {

                boxsList = await _httpClient.GetFromJsonAsync<List<Box>>($"api/ConceptsSelector/GetSelectorListBoxes");

                return boxsList;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }

        //---
        public async Task<List<Box>> GetBoxes()
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {

                boxsList = await _httpClient.GetFromJsonAsync<List<Box>>($"api/TransactionalItems/GetTransactionalItemById?userId={userId}&ipAddress={ipAddress}&filterName={transactionalItemId}");

                return boxsList;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }
        public async Task<List<SeasonBusiness>> GetSelectorListSeasonBusiness(string? nameLike = null)

        {
            string namteToFind = nameLike != null ? nameLike : "";

            try
            {
                return await _httpClient.GetFromJsonAsync<List<SeasonBusiness>>($"api/ConceptsSelector/GetSelectorListSeasonBusiness?filterString={namteToFind}");

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }
        public async Task<List<TransactionalItemTag>> GetSelectorListTag(string? transactionalItemId = null)

        {
            transactionalItemId = transactionalItemId != null ? transactionalItemId : "";
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                transactionalItemTags = await _httpClient.GetFromJsonAsync<List<TransactionalItemTag>>($"api/TransactionalItems/GetTransactionalItemDetailsTags?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}");
                return transactionalItemTags != null ? transactionalItemTags : new List<TransactionalItemTag>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }


        }
        public async Task<List<ConceptGroup>> GetSelectorListTransactionalItemGroups(string? nameLike = null)

        {
            string namteToFind = nameLike != null ? nameLike : "";

            try
            {
                conceptGroupsList = await _httpClient.GetFromJsonAsync<List<ConceptGroup>>($"api/ConceptsSelector/GetSelectorListTransactionalItemGroups?filterCondition={namteToFind}");
                return conceptGroupsList != null ? conceptGroupsList.Where(g => g.Id != "").Take(20).ToList() : new List<ConceptGroup>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }


        }

        public async Task<List<ConceptGroup>> GetTransactionalItemGroups(string? nameLike = null)

        {
            string namteToFind = nameLike != null ? nameLike : "";
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                conceptGroupsList = await _httpClient.GetFromJsonAsync<List<ConceptGroup>>($"api/TransactionalItemsRelatedConcepts/GetTransactionalItemGroups?userId={userId}&ipAddress={ipAddress}&filterCondition={namteToFind}");
                return conceptGroupsList != null ? conceptGroupsList.Where(g => g.Id != "").Take(20).ToList() : new List<ConceptGroup>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }


        }

        public async Task<List<TransactItemImage>> GetTransactionalItemDetailsPathImages(string? transactionalItemId = null)
        {
            transactionalItemId = transactionalItemId != null ? transactionalItemId : "";
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                transactItemImagesList = await _httpClient.GetFromJsonAsync<List<TransactItemImage>>($"api/TransactionalItems/GetTransactionalItemDetailsPathImages?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}");
                return transactItemImagesList != null ? transactItemImagesList : new List<TransactItemImage>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }


        }
        public async Task<List<PackingSpecs>> GetTransactionalItemDetailsPackingRecipe(string? transactionalItemId = null)

        {
            transactionalItemId = transactionalItemId != null ? transactionalItemId : "";
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                packingSpecs = await _httpClient.GetFromJsonAsync<List<PackingSpecs>>($"api/TransactionalItems/GetTransactionalItemDetailsPackingRecipe?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}");
                return packingSpecs != null ? packingSpecs : new List<PackingSpecs>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }


        }
        public async Task<List<TransactionalItemProcessStep>> GetTransactionalItemDetailsProductionSpecs(string? transactionalItemId = null)
        {
            transactionalItemId = transactionalItemId != null ? transactionalItemId : "";
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                processSteps = await _httpClient.GetFromJsonAsync<List<TransactionalItemProcessStep>>($"api/TransactionalItems/GetTransactionalItemDetailsProductionSpecs?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}");
                return packingSpecs != null ? processSteps : new List<TransactionalItemProcessStep>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }


        }
        public async Task<List<TransactionalItemQualityPair>> GetTransactionalItemDetailsQualityParameters(string? transactionalItemId = null)

        {
            transactionalItemId = transactionalItemId != null ? transactionalItemId : "";
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                transactionalItemQualityPairsList = await _httpClient.GetFromJsonAsync<List<TransactionalItemQualityPair>>($"api/TransactionalItems/GetTransactionalItemDetailsQualityParameters?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}");
                return transactItemImagesList != null ? transactionalItemQualityPairsList : new List<TransactionalItemQualityPair>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }
        public async Task<List<Box>> GetBoxGetBoxTable( string? nameLike = null)
        {
            transactionalItemId = transactionalItemId != null ? transactionalItemId : "";
            nameLike = nameLike != null ? nameLike : "";
            try
            {
                boxes = await _httpClient.GetFromJsonAsync<List<Box>>($"api/TransactionalItemsRelatedConcepts/GetBoxTable");
                return boxes != null ? boxes.Where(s => s.Name.ToLower().Contains(nameLike.ToLower())).ToList(): new List<Box>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }
        public async Task<Box> GetBox(string? boxID)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            boxID = boxID != null ? boxID : "";
            try
            {
                var Boxes = await _httpClient.GetFromJsonAsync<Box>($"api/TransactionalItemsRelatedConcepts/GetBox?userId={userId}&ipAddress={ipAddress}&boxID={boxID}");
                return Boxes != null ? Boxes : new Box();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }
        public async Task<List<SeasonBusiness>> GetSeasonsTable(int? page = 1, int? perPage = 10, string? nameLike = null)
        {

            try
            {
                seasonBusiness = await _httpClient.GetFromJsonAsync<List<SeasonBusiness>>($"api/TransactionalItemsRelatedConcepts/GetSeasonsTable");
                page = page == 0 ? 1 : page;
                if (!nameLike.ToLower().Contains("all") && !nameLike.ToLower().Contains("todo"))
                {
                    seasonBusiness = seasonBusiness = seasonBusiness != null ? seasonBusiness.Where(s => s.Name.ToLower().Contains(nameLike.ToLower())).ToList() : new List<SeasonBusiness>();

                    if ((seasonBusiness.Count() - page) >= perPage) seasonBusiness.ToList().GetRange((int)page, (int)perPage);
                    if ((seasonBusiness.Count() - page) < perPage) seasonBusiness.GetRange((int)page, (seasonBusiness.Count() - 1));
                }


                return seasonBusiness;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }

        //---
        public async Task<List<SeasonBusiness>> GetSeasons(string? filterCondition = null)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                seasonBusiness = await _httpClient.GetFromJsonAsync<List<SeasonBusiness>>($"api/TransactionalItemsRelatedConcepts/GetSeasons?userId={userId}&ipAddress={ipAddress}&filterCondition={filterCondition}");
                seasonBusiness = seasonBusiness = seasonBusiness != null ? seasonBusiness.Where(s => s.Name.ToLower().Contains(filterCondition.ToLower())).ToList() : new List<SeasonBusiness>();
                return seasonBusiness;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }
        public async Task<SeasonBusiness> GetSeason(string seasonId)
        {
            seasonId = seasonId != null ? seasonId : "";
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var Boxes = await _httpClient.GetFromJsonAsync<SeasonBusiness>($"api/TransactionalItemsRelatedConcepts/GetSeason?userId={userId}&ipAddress={ipAddress}&seasonId={seasonId}");
                return Boxes != null ? Boxes : new SeasonBusiness();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }
        public async Task<List<TransactionalItemType>> GetSelectorListTransactionalItemTypes(string? nameLike = null)
        {
            nameLike = nameLike != null ? nameLike : "";
            try
            {
                transactionalItemTypes = await _httpClient.GetFromJsonAsync<List<TransactionalItemType>>($"api/ConceptsSelector/GetSelectorListTransactionalItemTypes");
                return transactionalItemTypes != null ? transactionalItemTypes.Where(s => s.Name.ToLower().Contains(nameLike.ToLower())).ToList() : new List<TransactionalItemType>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;

            }

        }
        public async Task<List<TransactionalItemType>> GetTransactionalItemType(string? transactionalItemTypeId = null)
        {
            transactionalItemTypeId = transactionalItemTypeId != null ? transactionalItemTypeId : "";
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var transactionalItemType = await _httpClient.GetFromJsonAsync<List<TransactionalItemType>>($"api/TransactionalItems/GetTransactionalItemType?userId={userId}&ipAddress={ipAddress}&transactionalItemTypeId={transactionalItemTypeId}");
                return transactionalItemType != null ? transactionalItemType : new List<TransactionalItemType>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }
        public async Task<List<TransactionalItemStatus>> GetTransactionalStatusesTable(string? nameLike = null)
        {
            nameLike = nameLike != null ? nameLike : "";

            try
            {
                transactionalItemStatuss = await _httpClient.GetFromJsonAsync<List<TransactionalItemStatus>>($"api/TransactionalItemsRelatedConcepts/GetTransactionalStatusesTable");
                return transactionalItemStatuss != null ? transactionalItemStatuss.Where(s => s.Name.ToLower().Contains(nameLike.ToLower())).ToList() : new List<TransactionalItemStatus>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }
        public async Task<bool> SaveProductPackingSpec(string transactionalItemId, PackingSpecs packingSpecs)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var resul = await _httpClient.PostAsJsonAsync<PackingSpecs>($"api/TransactionalItems/SaveProductPackingSpecs?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}", packingSpecs);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return false;

            }

        }
        public async Task<bool> SaveTags(string transactionalItemId, TransactionalItemTag transactionalItemTag)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var resul = await _httpClient.PostAsJsonAsync<TransactionalItemTag>($"api/TransactionalItems/SaveTags?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}", transactionalItemTag);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return false;

            }

        }
        public async Task<bool> SaveProductionSpecs(string transactionalItemId, TransactionalItemProcessStep transactionalItemProcessStep)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var resul = await _httpClient.PostAsJsonAsync<TransactionalItemProcessStep>($"api/TransactionalItems/SaveProductionSpecs?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}", transactionalItemProcessStep);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return false;

            }

        }
        public async Task<bool> SaveImage(string transactionalItemId, TransactItemImage transactItemImage)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var resul = await _httpClient.PostAsJsonAsync<TransactItemImage>($"api/TransactionalItems/SaveImage?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}", transactItemImage);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return false;

            }

        }
        public async Task<bool> SaveQualityParameters(string? transactionalItemId, TransactionalItemQualityPair transactionalItemQualityPair)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var resul = await _httpClient.PostAsJsonAsync<TransactionalItemQualityPair>($"api/TransactionalItems/SaveQualityParameters?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}", transactionalItemQualityPair);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }

        }

        //---
        public async Task<bool> SaveCharacteristics(string? transactionalItemId, TransactionalItemCharacteristicPair transactionalItemCharacteristicPair)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var resul = await _httpClient.PostAsJsonAsync<TransactionalItemCharacteristicPair>($"api/TransactionalItems/SaveCharacteristics?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}", transactionalItemCharacteristicPair);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }

        }
        public async Task<bool> SaveTransactionalItem(TransactionalItem transactionalItem)
        {

            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var resul = await _httpClient.PostAsJsonAsync<TransactionalItem>($"api/TransactionalItems/SaveTransactionalItem?userId={userId}&ipAddress={ipAddress}", transactionalItem);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }

        }
        public async Task<bool> SaveSeason(SeasonBusiness seasonBusiness)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var resul = await _httpClient.PostAsJsonAsync<SeasonBusiness>($"api/TransactionalItemsRelatedConcepts/SaveSeason?userId={userId}&ipAddress={ipAddress}", seasonBusiness);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }

        }
        public async Task<bool> SaveBox( Box box)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;            
            try
            {
                var resul = await _httpClient.PostAsJsonAsync<Box>($"api/TransactionalItemsRelatedConcepts/SaveBox?userId={userId}&ipAddress={ipAddress}", box);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }

        }
        public async Task<bool> SaveTransactionalItemType( TransactionalItemType transactionalItemType)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            
            try
            {
                var resul = await _httpClient.PostAsJsonAsync<TransactionalItemType>($"api/TransactionalItemsRelatedConcepts/SaveTransactionalItemType?userId={userId}&ipAddress={ipAddress}", transactionalItemType);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }

        }
        public async Task<bool> SaveTransactionalItemGroup( ConceptGroup conceptGroup)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;

            try
            {
                var resul = await _httpClient.PostAsJsonAsync<ConceptGroup>($"api/TransactionalItemsRelatedConcepts/SaveTransactionalItemGroup?userId={userId}&ipAddress={ipAddress}", conceptGroup);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }

        }
        public async Task<bool> SaveStatus(TransactionalItemStatus transactionalItemStatus)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;

            try
            {
                var resul = await _httpClient.PostAsJsonAsync<TransactionalItemStatus>($"api/TransactionalItemsRelatedConcepts/SaveStatus?userId={userId}&ipAddress={ipAddress}", transactionalItemStatus);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }

        }

        public async Task<string> UploadFiles(MultipartFormDataContent multipartFormDataContent)
        {

            try
            {

                var resul = await _httpClient.PostAsync($"api/UploadFiles", multipartFormDataContent);
                FilePath filePath = await resul.Content.ReadFromJsonAsync<FilePath>();
                return filePath.filePath;


            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return "";

            }

        }

        public Task<List<Concept>> GetSelectorListEntityActors(string? nameLike = null)
        {
            throw new NotImplementedException();
        }       

       


    }

    public class FilePath
    {
        public string filePath { get; set; }

    }

}
