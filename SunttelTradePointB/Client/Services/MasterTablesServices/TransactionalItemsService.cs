using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.JSInterop;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Client.Shared.TransactionalItems.TransactionalItemsSubComponents;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.SquadsMgr;
using Syncfusion.PdfExport;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public TransactionalItemProcessStep? ConceptTransactionalItemProcessStep { get; set; }
        public TransactionalItemQualityPair? ConceptTransactionalItemQualityPair { get; set; }
        public TransactionalItemTag? ConceptTransactionalItemTag { get; set; }
        public TransactionalItemCharacteristicPair ConcepttransactionalItemCharacteristicPair { get; set; }
        public AssemblyType ConceptItemAssemblyType { get; set; }
        public ProductModel ConceptProductModel { get; set; }
        public LabelStyle ConceptLabelStyle { get; set; }
        public enum UploadingFileType
        {
            TransactionalItemImage,
            EntityImage
        }
        public string Host { get { return UIClientGlobalVariables.PathEntityImages + "/"; } }
        #endregion Property

        private readonly HttpClient _httpClient;

        #region Mode Edit

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
        List<TransactionalItemCharacteristicPair> transactionalItemCharacteristicPair;
        List<LabelStyle> labelStyles;
        List<ProductModel> productModel;
        #endregion

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
                var responseMessage = await Gethttp($"/api/TransactionalItems/GetTransactionalItems?userId={UIClientGlobalVariables.UserId}&ipAddress={UIClientGlobalVariables.PublicIpAddress}&page={page}&perPage={perPage}&filterName={namteToFind}");
                transactionalItemsList = await responseMessage.Content.ReadFromJsonAsync<List<TransactionalItem>>();

                page = page == 0 ? 1 : page;
                return transactionalItemsList != null ? transactionalItemsList : new List<TransactionalItem>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }

        public async Task<List<Concept>> GetSelectorListPackingMaterials(string? filterString = null)
        {

            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {

                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetSelectorListPackingMaterials?filterString={filterString}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Concept>>();

                return list != null ? list : new List<Concept>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }

        public async Task<List<RecipeModifier>> GetRecipeModifiersByTypeID(string? transactionalItemTypeId = null)
        {

            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {

                var responseMessage = await Gethttp($"/api/TransactionalItems/GetRecipeModifiersByTypeID?userId={userId}&ipAddress={ipAddress}&transactionalItemTypeId={transactionalItemTypeId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<RecipeModifier>>();
                return list != null ? list : new List<RecipeModifier>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }
        
        public async Task<List<Concept>> GetConceptProduct(string? filterString = null)
        {

            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var list = await GetTransactionalItemsList(1, 250, filterString);
                if (list == null) list = new();
                // var list = await _httpClient.GetFromJsonAsync<List<Concept>>($"api/ConceptsSelector/GetSelectorListPackingMaterials?filterString={filterString}");



                return new List<Concept>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }

        public async Task<List<AssemblyType>> GetSelectorListAssemblyTypes(string? filterString = null)
        {

            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetSelectorListAssemblyTypes?filterString={filterString}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<AssemblyType>>();


                return list != null ? list : new List<AssemblyType>();

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
            string ipAddress = UIClientGlobalVariables.PublicIpAddress == "" ? "127.0.0.0" : UIClientGlobalVariables.PublicIpAddress;
            try
            {


                var responseMessage = await Gethttp($"/api/TransactionalItems/GetTransactionalItemById?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}");
                var transactionalItem = await responseMessage.Content.ReadFromJsonAsync<TransactionalItem>();


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

                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetSelectorListEntityActor?filterString={namteToFind}&roleName={roleName}");
                var conceptList = await responseMessage.Content.ReadFromJsonAsync<List<Concept>>();

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
                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetSelectorListTransactionalItemModels?transactionalItemId={transactionalItemId}");
                ProductModelList = await responseMessage.Content.ReadFromJsonAsync<List<ProductModel>>();

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
                var responseMessage = await Gethttp($"​/api​/ConceptsSelector​/GetSelectorListBoxes");
                boxsList = await responseMessage.Content.ReadFromJsonAsync<List<Box>>();

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
                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetSelectorListBoxes");
                boxsList = await responseMessage.Content.ReadFromJsonAsync<List<Box>>();

                return boxsList;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }

        public async Task<List<Box>> GetBoxes()
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"/api/TransactionalItemsRelatedConcepts/GetBoxTable?userId={userId}&ipAddress={ipAddress}");
                boxsList = await responseMessage.Content.ReadFromJsonAsync<List<Box>>();

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
                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetSelectorListSeasonBusiness?filterString={namteToFind}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<SeasonBusiness>>();
                return list;

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }

        public async Task<TransactionalItemTag> GetTagToId(string transactionalItemId, string transactionalItemTagId)
        {

            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"/api/TransactionalItems/GetTransactionalItemDetailsTags?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}");
                var obj  = await responseMessage.Content.ReadFromJsonAsync<TransactionalItemTag>();

                return obj != null ? obj : new TransactionalItemTag();
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
                var responseMessage = await Gethttp($"/api/TransactionalItems/GetTransactionalItemDetailsTags?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}");
                transactionalItemTags = await responseMessage.Content.ReadFromJsonAsync<List<TransactionalItemTag>>();


                return transactionalItemTags != null ? transactionalItemTags : new List<TransactionalItemTag>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }


        }

        public async Task<LabelStyle> GetLabelStyle(string? labelStyleId = null)

        {
            transactionalItemId = transactionalItemId != null ? transactionalItemId : "";
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"/api/TransactionalItemsRelatedConcepts/GetLabelStyle?userId={userId}&ipAddress={ipAddress}&labelStyleId={labelStyleId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<LabelStyle>();

                return list != null ? list : new LabelStyle();
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
                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetSelectorListTransactionalItemGroups?filterCondition={namteToFind}");
                conceptGroupsList = await responseMessage.Content.ReadFromJsonAsync<List<ConceptGroup>>();

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
          
            try
            {
                var responseMessage = await Gethttp($"/api/TransactionalItemsRelatedConcepts/GetTransactionalItemGroups?userId={UIClientGlobalVariables.UserId}&ipAddress={UIClientGlobalVariables.PublicIpAddress}&filterCondition={namteToFind}");
                conceptGroupsList = await responseMessage.Content.ReadFromJsonAsync<List<ConceptGroup>>();

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
                var responseMessage = await Gethttp($"/api/TransactionalItems/GetTransactionalItemDetailsPathImages?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}");
                transactItemImagesList = await responseMessage.Content.ReadFromJsonAsync<List<TransactItemImage>>();


                return transactItemImagesList != null ? transactItemImagesList : new List<TransactItemImage>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return new List<TransactItemImage>();

            }


        }

        public async Task<List<PackingSpecs>> GetTransactionalItemDetailsPackingRecipe(string? transactionalItemId = null)

        {
            transactionalItemId = transactionalItemId != null ? transactionalItemId : "";
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"/api/TransactionalItems/GetTransactionalItemDetailsPackingRecipe?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}");
                packingSpecs = await responseMessage.Content.ReadFromJsonAsync<List<PackingSpecs>>();


                return packingSpecs != null ? packingSpecs : new List<PackingSpecs>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }


        }
        public async Task<PackingSpecs> GetPackingRecipeToId(string? transactionalItemId, string packingSpecsId)

        {
            transactionalItemId = transactionalItemId != null ? transactionalItemId : "";

            try
            {
                var responseMessage = await Gethttp($"/api/TransactionalItems/GetTransactionalItemDetailsPackingRecipe?userId={UIClientGlobalVariables.UserId}&ipAddress={UIClientGlobalVariables.PublicIpAddress}&transactionalItemId={transactionalItemId}");
                var _packingSpecs = await responseMessage.Content.ReadFromJsonAsync<PackingSpecs>();

                return _packingSpecs != null ? _packingSpecs : new PackingSpecs();
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
                var responseMessage = await Gethttp($"/api/TransactionalItems/GetTransactionalItemDetailsProductionSpecs?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<TransactionalItemProcessStep>>();


                return list != null ? list : new List<TransactionalItemProcessStep>();
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
                var responseMessage = await Gethttp($"/api/TransactionalItems/GetTransactionalItemDetailsQualityParameters?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}");
                transactionalItemQualityPairsList = await responseMessage.Content.ReadFromJsonAsync<List<TransactionalItemQualityPair>>();


                return transactionalItemQualityPairsList != null ? transactionalItemQualityPairsList : new List<TransactionalItemQualityPair>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }

        }

        public async Task<List<Box>> GetBoxTable(string? nameLike = null)
        {
            transactionalItemId = transactionalItemId != null ? transactionalItemId : "";
            nameLike = nameLike != null ? nameLike : "";
            try
            {
                var responseMessage = await Gethttp($"/api/TransactionalItemsRelatedConcepts/GetBoxTable");
                boxes = await responseMessage.Content.ReadFromJsonAsync<List<Box>>();


                return boxes != null ? boxes.Where(s => s.Name != null && s.Name.ToLower().Contains(nameLike.ToLower())).ToList() : new List<Box>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }

        public async Task<List<Box>> GetSelectorListBoxes(string? nameLike = null)
        {
            transactionalItemId = transactionalItemId != null ? transactionalItemId : "";
            nameLike = nameLike != null ? nameLike : "";
            try
            {
                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetSelectorListBoxes");
                boxes = await responseMessage.Content.ReadFromJsonAsync<List<Box>>();


                return boxes != null ? boxes.Where(s => s.Name != null && s.Name.ToLower().Contains(nameLike.ToLower())).ToList() : new List<Box>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }

        public async Task<List<LabelStyle>> GetLabelStyles(string? filterString)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;

            try
            {
                var responseMessage = await Gethttp($"/api/TransactionalItemsRelatedConcepts/GetLabelStyles?userId={userId}&ipAddress={ipAddress}&filterString={filterString}");
                labelStyles = await responseMessage.Content.ReadFromJsonAsync<List<LabelStyle>>();


                return labelStyles != null ? labelStyles : new();
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
                var responseMessage = await Gethttp($"/api/TransactionalItemsRelatedConcepts/GetBox?userId={userId}&ipAddress={ipAddress}&boxID={boxID}");
                var Box = await responseMessage.Content.ReadFromJsonAsync<Box>();


                return Box != null ? Box : new Box();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }

        public async Task<List<ProductModel>> GetSelectorListTransactionalItemModels(string? transactionalItemId)
        {

            try
            {
                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetSelectorListTransactionalItemModels?transactionalItemId={transactionalItemId}");
                var productModel = await responseMessage.Content.ReadFromJsonAsync<List<ProductModel>>();


                return productModel != null ? productModel : new();
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
                var responseMessage = await Gethttp($"api/TransactionalItemsRelatedConcepts/GetSeasonsTable");
                var seasonBusiness = await responseMessage.Content.ReadFromJsonAsync<List<SeasonBusiness>>();


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

        public async Task<List<SeasonBusiness>> GetSeasons(string? filterCondition = null)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"/api/TransactionalItemsRelatedConcepts/GetSeasons?userId={userId}&ipAddress={ipAddress}&filterCondition={filterCondition}");
                var seasonBusiness = await responseMessage.Content.ReadFromJsonAsync<List<SeasonBusiness>>();


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
                var responseMessage = await Gethttp($"/api/TransactionalItemsRelatedConcepts/GetSeason?userId={userId}&ipAddress={ipAddress}&seasonId={seasonId}");
                var seasonBusiness = await responseMessage.Content.ReadFromJsonAsync<SeasonBusiness>();

                return seasonBusiness != null ? seasonBusiness : new SeasonBusiness();
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
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetSelectorListTransactionalItemTypes?userId={userId}&ipAddress={ipAddress}&filterCondition={nameLike}");
                transactionalItemTypes = await responseMessage.Content.ReadFromJsonAsync<List<TransactionalItemType>>();

                return transactionalItemTypes;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;

            }

        }

        public async Task<List<TransactionalItemType>> GetTransactionalItemTypes(string? nameLike = null)
        {
            nameLike = nameLike != null ? nameLike : "";
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"api/TransactionalItems/GetTransactionalItemTypes?userId={userId}&ipAddress={ipAddress}&filterCondition={nameLike}");
                transactionalItemTypes = await responseMessage.Content.ReadFromJsonAsync<List<TransactionalItemType>>();


                return transactionalItemTypes;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;

            }

        }

        public async Task<List<TransactionalItemType>> GetProductRecipeQualityModifiersByModifierId(string? modifierId = null)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetProductRecipeQualityModifiersByModifierId?userId={userId}&ipAddress={ipAddress}&modifierId={modifierId}");
                transactionalItemTypes = await responseMessage.Content.ReadFromJsonAsync<List<TransactionalItemType>>();


                return transactionalItemTypes;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;

            }

        }

        public async Task<TransactionalItemProcessStep> GetTransactionalItemDetailsProductionSpecsToId(string transactionalItemId, string productionSpecsToId)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetProductRecipeQualityModifiersByModifierId?userId={userId}&ipAddress={ipAddress}&modifierId={productionSpecsToId}");
                var obj = await responseMessage.Content.ReadFromJsonAsync<TransactionalItemProcessStep>();

                return obj;
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
                var responseMessage = await Gethttp($"/api/TransactionalItems/GetTransactionalItemType?userId={userId}&ipAddress={ipAddress}&transactionalItemTypeId={transactionalItemTypeId}");
                var transactionalItemType = await responseMessage.Content.ReadFromJsonAsync<List<TransactionalItemType>>();


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
                var responseMessage = await Gethttp($"/api/TransactionalItemsRelatedConcepts/GetTransactionalStatusesTable");
                transactionalItemStatuss = await responseMessage.Content.ReadFromJsonAsync<List<TransactionalItemStatus>>();


                if (!nameLike.ToLower().Contains("all") && !nameLike.ToLower().Contains("todo")) return transactionalItemStatuss != null ? transactionalItemStatuss.Where(s => s.Name.ToLower().Contains(nameLike.ToLower())).ToList() : new List<TransactionalItemStatus>();
                return transactionalItemStatuss;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }

        public async Task<List<TransactionalItemCharacteristicPair>> GetCharacteristic(string? labelPaperId = null)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"/api/TransactionalItems/GetTransactionalItemDetailsItemCharacteristics?userId={userId}&ipAddress={ipAddress}&transactionalItemId={labelPaperId}");
                transactionalItemCharacteristicPair = await responseMessage.Content.ReadFromJsonAsync<List<TransactionalItemCharacteristicPair>>();


                return transactionalItemCharacteristicPair != null ? transactionalItemCharacteristicPair : new List<TransactionalItemCharacteristicPair>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }

        public async Task<List<LabelPaper>> GetSelectorListLabelPaper(string? nameLike = null)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetSelectorListLabelPaper");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<LabelPaper>>();


                if (nameLike.ToLower().Contains("all") || nameLike.ToLower().Contains("todo"))
                {
                    list = list.Where(s => s.Name.ToLower().Contains(nameLike)).ToList();
                }
                return list != null ? list : new List<LabelPaper>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }

        }

        public async Task<LabelPaper> GetConceptPaperId(string? labelPaperId = null)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"/api/TransactionalItemsRelatedConcepts/GetLabelPaperById?userId={userId}&ipAddress={ipAddress}&labelPaperId={labelPaperId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<LabelPaper>();

                return list != null ? list : new LabelPaper();
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

            packingSpecs.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

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

            transactionalItemTag.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

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

            transactionalItemProcessStep.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

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

            transactItemImage.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

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

            transactionalItemQualityPair.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

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

        public async Task<bool> SaveTransactionalItemModels(string? transactionalItemId, ProductModel productModel)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;

            productModel.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

            try
            {
                var resul = await _httpClient.PostAsJsonAsync<ProductModel>($"api/TransactionalItems/SaveTransactionalItemModels?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}", productModel);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }

        }

        public async Task<bool> SaveCharacteristics(string? transactionalItemId, TransactionalItemCharacteristicPair transactionalItemCharacteristicPair)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;

            transactionalItemCharacteristicPair.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

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

        public async Task<TransactionalItem> SaveTransactionalItem(TransactionalItem transactionalItem)
        {

            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            transactionalItem.SquadId = UIClientGlobalVariables.ActiveSquad!=null ?  UIClientGlobalVariables.ActiveSquad?.ID.ToString():"";


            try
            {             
               
                var resul = await _httpClient.PostAsJsonAsync<TransactionalItem>($"api/TransactionalItems/SaveTransactionalItem?userId={userId}&ipAddress={ipAddress}", transactionalItem);
                TransactionalItem item = await resul.Content.ReadFromJsonAsync<TransactionalItem>();
                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }

        }

        public async Task<bool> SaveSeason(SeasonBusiness seasonBusiness)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;

            seasonBusiness.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();


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

        public async Task<bool> SaveLabelStyle(LabelStyle labelStyle)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;

            labelStyle.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

            try
            {
                var resul = await _httpClient.PostAsJsonAsync<LabelStyle>($"api/TransactionalItemsRelatedConcepts/SaveLabelStyle?userId={userId}&ipAddress={ipAddress}", labelStyle);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> SaveBox(Box box)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;

            box.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

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

        public async Task<bool> SaveTransactionalItemType(TransactionalItemType transactionalItemType)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;

            transactionalItemType.SquadId = UIClientGlobalVariables.ActiveSquad!.ID.ToString();

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

        public async Task<bool> SaveTransactionalItemGroup(ConceptGroup conceptGroup)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;

            conceptGroup.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

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

            transactionalItemStatus.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();


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

        public async Task<bool> SaveConceptPaper(LabelPaper labelPaper)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;

            labelPaper.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

            try
            {
                var resul = await _httpClient.PostAsJsonAsync<LabelPaper>($"api/TransactionalItemsRelatedConcepts/SaveLabelPaper?userId={userId}&ipAddress={ipAddress}", labelPaper);
                return resul.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                Console.WriteLine("error : " + ex.Message);
                return false;
            }

        }

        public async Task<bool> SaveAssemblyType(AssemblyType assemblyType)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;

            assemblyType.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

            try
            {
                var resul = await _httpClient.PostAsJsonAsync<AssemblyType>($"api/TransactionalItemsRelatedConcepts/SaveAssemblyType?userId={userId}&ipAddress={ipAddress}", assemblyType);
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

        Task<TransactionalItemStatus> ITransactionalItems.GetSelectorListPackingMaterials(string? filterString)
        {
            throw new NotImplementedException();
        }


        public async Task<HttpResponseMessage> Gethttp(string Url)
        {

            try
            {


                var request = new HttpRequestMessage(HttpMethod.Get, Url);
                //request.Headers.Add("SquadId", UIClientGlobalVariables.ActiveSquad.ID.ToString());


                var response = await _httpClient.SendAsync(request);
                var content  = await response.Content.ReadFromJsonAsync<List<TransactionalItem>>();
                System.Diagnostics.Debug.WriteLine(response.IsSuccessStatusCode);
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else { return null; }

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                System.Diagnostics.Debug.WriteLine(errMessage);

            }
            return null;

        }

    }

    public class FilePath
    {
        public string filePath { get; set; }

    }

}
