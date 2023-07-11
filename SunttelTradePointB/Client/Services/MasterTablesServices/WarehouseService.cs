using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Communications;
using SunttelTradePointB.Shared.Common;
using System.Net;
using System.Net.Http.Json;
using System.Net.Http;

namespace SunttelTradePointB.Client.Services.MasterTablesServices
{
    public class WarehouseService: IWarehouse
    {
        private readonly HttpClient _httpClient;

        public WarehouseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// Gets
        public async Task<Warehouse> GetWarehouseById(string warehouseId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                var warehouse = await _httpClient.GetFromJsonAsync<Warehouse>($"/api/GeographicPlaces/GetWarehouse?entityId={userId}&ipAdress={ipAddress}&warehouseId={warehouseId}");
                return warehouse;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<Warehouse>> GetWarehouseList(string filterWarehouse)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                HttpResponseMessage? listWarehouse = await Gethttp($"/api/GeographicPlaces/GetWarehouses?entityId={userId}&ipAdress={ipAddress}&nameLike={filterWarehouse}");
                return await listWarehouse.Content.ReadFromJsonAsync<List<Warehouse>>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }




        ///Post
        public async Task<bool> SaveWarehuse(Warehouse warehouse)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                var result = await _httpClient.PostAsJsonAsync($"/api/GeographicPlaces/SaveWarehouse?entityId={userId}&ipAdress={ipAddress}", warehouse);
                return result.IsSuccessStatusCode;
                    
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }


          public async Task<HttpResponseMessage> Gethttp(string Url)
        {
            try
            {

                var SquadId = UIClientGlobalVariables.ActiveSquad;
                var ReplaceIdUser = UIClientGlobalVariables.UserId;
                var ReplacePublicIpAddress = UIClientGlobalVariables.PublicIpAddress;
                if (ReplacePublicIpAddress == "") ReplacePublicIpAddress = "000";
                if (ReplaceIdUser == "") ReplaceIdUser = "000";

                Url = Url.Replace("*Id", ReplaceIdUser ?? "000");
                Url = Url.Replace("*Ip", ReplacePublicIpAddress ?? "000");

                var request = new HttpRequestMessage(HttpMethod.Get, Url);

                if (SquadId != null) request.Headers.Add("SquadId", SquadId.ID.ToString().ToUpper());
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
