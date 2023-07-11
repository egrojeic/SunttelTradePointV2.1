using SunttelTradePointB.Client.Interfaces.InventoryInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Sales;
using System.Globalization;
using System.Net.Http.Json;

namespace SunttelTradePointB.Client.Services.InventoryServices
{
    public class Inventory : IInventory
    {
        private readonly HttpClient _httpClient;
        private string Configpath = "userId=*Id&ipAddress=*Ip";
        public Inventory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<InventoryDetail> SaveInventoryItem(InventoryDetail inventoryDetail)
        {
            try
            {
                string path = $"/api/Inventory/SaveInventory?&{Configpath}";
                inventoryDetail.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<InventoryDetail>($"{path}", inventoryDetail);
                return await responseMessage.Content.ReadFromJsonAsync<InventoryDetail>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }



        public async Task<List<Warehouse>> GetWarehouseList( string? nameLike = "all")
        {
            try
            {
                var responseMessage = await Gethttp($"/api/GeographicPlaces/GetWarehouses?entityId={UIClientGlobalVariables.UserId}&ipAdress={UIClientGlobalVariables.PublicIpAddress}&nameLike={nameLike}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Warehouse>>();
             
                return list != null ? list : new List<Warehouse>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<InventoryDetail>> GetInventoryList(string filterName,string documentTypeId,DateTime statrDate,  int? page = 1, int? perPage = 10)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                var responseMessage = await Gethttp($"/api/Inventory/GetInventory?&{Configpath}&documentTypeId={documentTypeId}&DocumentDate={statrDate.ToString("yyyy-MM-dd", culture)}&page={page}&perPage={perPage}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<InventoryDetail>>();
               
               
                return list != null ? list : new List<InventoryDetail>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<InventoryDetail> GetInventoryById(string inventoryId)
        {
            try
            {
                var responseMessage = await Gethttp($"/api/Inventory/GetInventoryById?&{Configpath}&inventoryId={inventoryId}");
                var item = await responseMessage.Content.ReadFromJsonAsync<InventoryDetail>();
           
                return item != null ? item : new InventoryDetail();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }



        public async Task<List<TransactionalItem>> GetTransactionalItemsList(int? page = 1, int? perPage = 10, string? nameLike = null)
        {
            string namteToFind = nameLike != null ? nameLike : "";

            try
            {
                var responseMessage = await Gethttp($"/api/TransactionalItems/GetTransactionalItems?userId={UIClientGlobalVariables.UserId}&ipAddress={UIClientGlobalVariables.PublicIpAddress}&page={page}&perPage={perPage}&filterName={namteToFind}");
               var transactionalItemsList = await responseMessage.Content.ReadFromJsonAsync<List<TransactionalItem>>();

                page = page == 0 ? 1 : page;
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

        public async Task<List<EntityActor>> GetEntityActorFaceList(string? nameLike = null)
        {

            string nameToFind = nameLike != null ? nameLike : "";
         
            try
            {
              

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetEntityNodes?userId={UIClientGlobalVariables.UserId}&ipAdress={UIClientGlobalVariables.PublicIpAddress}&filterName={nameToFind}");
                var entityNodesList = await response.Content.ReadFromJsonAsync<List<EntityActor>>();

                return entityNodesList.Take(20).ToList();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;

            }
        }


        public async Task<List<Box>> GetBoxes(string filter)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var responseMessage = await Gethttp($"/api/TransactionalItemsRelatedConcepts/GetBoxTable?userId={userId}&ipAddress={ipAddress}");
                var boxsList = await responseMessage.Content.ReadFromJsonAsync<List<Box>>();
                boxsList = boxsList.Where(s => s.Name !=null &&  s.Name.ToLower().Contains(filter.ToLower())).Take(20).ToList();
                return boxsList;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }

        public async Task<List<Box>> GetWarehouses(string filter)
        {
            try
            {
                var responseMessage = await Gethttp($"​/api​/GeographicPlaces​/GetWarehouses/entityId={UIClientGlobalVariables.UserId}&ipAdress={UIClientGlobalVariables.PublicIpAddress}&nameLike={filter}");
                var boxsList = await responseMessage.Content.ReadFromJsonAsync<List<Box>>();

                return boxsList != null ? boxsList.Where(s => s.Name.ToLower().Contains(filter.ToLower())).ToList() : new List<Box>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }



        public async Task<HttpResponseMessage> Gethttp(string Url)
        {
            try
            {

                var SquadId = UIClientGlobalVariables.ActiveSquad;
                var ReplaceIdUser = UIClientGlobalVariables.UserId;
                var ReplacePublicIpAddress = UIClientGlobalVariables.PublicIpAddress;

                Url = Url.Replace("*Id", ReplaceIdUser ?? "000").Replace("*Ip", ReplacePublicIpAddress ?? "000");

                var request = new HttpRequestMessage(HttpMethod.Get, Url);

                if (SquadId != null) request.Headers.Add("SquadId", SquadId.ID.ToString());
                if (SquadId == null) request.Headers.Add("SquadId", "0000000000");
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else { return null; }

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;

            }


        }

        public Task<List<InventoryDetail>> GetInventoryList(string filterName, string documentTypeId, DateTime statrDate, DateTime endDate, int? page = 1, int? perPage = 10)
        {
            throw new NotImplementedException();
        }
    }
     
 

}
