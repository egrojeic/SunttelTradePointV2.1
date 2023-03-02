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
                var warehouse = await _httpClient.GetFromJsonAsync<Warehouse>($"/api/GeographicPlaces/GetWarehouse?warehouseId={warehouseId}");
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
                var listWarehouse = await _httpClient.GetFromJsonAsync<List<Warehouse>>($"/api/GeographicPlaces/GetWarehouses?nameLike={filterWarehouse}");
                return listWarehouse;
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
                var result = await _httpClient.PostAsJsonAsync($"/api/GeographicPlaces/SaveWarehouse", warehouse);
                return result.IsSuccessStatusCode;
                    
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

    }
}
