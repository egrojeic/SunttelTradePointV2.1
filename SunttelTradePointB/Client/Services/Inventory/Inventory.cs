using SunttelTradePointB.Client.Interfaces.InventoryInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Sales;
using System.Net.Http.Json;

namespace SunttelTradePointB.Client.Services.InventoryServices
{
    public class Inventory : TInventory
    {
        private readonly HttpClient _httpClient;
        private string Configpath = "userId=*Id&ipAddress=*Ip";
        public Inventory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<InventoryDetail> SaveInventoryItem(InventoryDetail commercialDocument)
        {
            try
            {
                string path = $"/api/Inventory/GetInventory?&{Configpath}";
                commercialDocument.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<InventoryDetail>($"{path}", commercialDocument);
                return await responseMessage.Content.ReadFromJsonAsync<InventoryDetail>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }



        public async Task<List<BasicConcept>> GetWarehouseList(string entityId, string? nameLike = "all")
        {
            try
            {
                var responseMessage = await Gethttp($"/api/GeographicPlaces/GetWarehouses?&{Configpath}&entityId={entityId}&nameLike={nameLike}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Warehouse>>();
                List<BasicConcept> conceptLis = new();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        conceptLis.Add(new BasicConcept
                        {
                            Id = item.Id,
                            Name = item.Name
                        });
                    }
                }
                return conceptLis != null ? conceptLis : new List<BasicConcept>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<InventoryDetail>> GetInventoryList(string documentTypeId, string BuyerId, string DocumentDate, int? page = 1, int? perPage = 10, string? filterName = null)
        {
            try
            {
                var responseMessage = await Gethttp($"/api/Inventory/GetInventory?&{Configpath}&documentTypeId={documentTypeId}&BuyerId={BuyerId}&DocumentDate={DocumentDate}&page={page}&perPage={perPage}&filterName={filterName}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<InventoryDetail>>();
                List<InventoryDetail> conceptLis = new();
               
                return conceptLis != null ? conceptLis : new List<InventoryDetail>();
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

                if (SquadId != null) request.Headers.Add("SquadId", SquadId.IDSquads.ToString());
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



    }
     
 

}
