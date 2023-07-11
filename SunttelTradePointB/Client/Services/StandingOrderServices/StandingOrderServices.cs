using SunttelTradePointB.Client.Interfaces.InventoryInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Sales;
using System.Net.Http.Json;
using SunttelTradePointB.Shared.Accounting;
using Syncfusion.Blazor.PivotView;
using SunttelTradePointB.Client.Interfaces.ICreditInterfaces;
using SunttelTradePointB.Client.Interfaces.StandingOrderDetails;

namespace SunttelTradePointB.Client.Services.StandingOrderServices
{
    public class StandingOrderServices : IStandingOrderDetails
    {
        private readonly HttpClient _httpClient;
        private string pathApi = "/api/StandingOrder/*Name?userId=*Id&ipAddress=*Ip"; 
        public StandingOrderServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<StandingOrder> SaveStandingOrde(StandingOrder standing)
        {
            try
            {
                string path = $"{pathApi}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "SaveStandingOrder");
                standing.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<StandingOrder>($"{path}", standing);
                return await responseMessage.Content.ReadFromJsonAsync<StandingOrder>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<StandingOrder>> GetStandingOrderList(string? filter = null, int? page = 1, int? perPage = 10)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "GetStandingOrders");
                var responseMessage = await Gethttp($"{path}&filter={filter}&page={page}&perPage={perPage}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<StandingOrder>>();
                return list != null ? list : new List<StandingOrder>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }



        public async Task<StandingOrder> GetStandingOrderById(string? standingOrderId)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "GetStandingOrderById");
                var responseMessage = await Gethttp($"{path}&standingOrderId={standingOrderId}");
                var item = await responseMessage.Content.ReadFromJsonAsync<StandingOrder>();
                return item != null ? item : new StandingOrder();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public string GetGlobalVariables(string Url)
        {
            var SquadId = UIClientGlobalVariables.ActiveSquad;
            var ReplaceIdUser = UIClientGlobalVariables.UserId;
            var ReplacePublicIpAddress = UIClientGlobalVariables.PublicIpAddress;

            Url = Url.Replace("*IP", ReplacePublicIpAddress??"000");
            Url = Url.Replace("*Id", ReplaceIdUser ?? "000");

            return Url;
        }






        public async Task<HttpResponseMessage> Gethttp(string Url)
        {
            try
            {

                var SquadId = UIClientGlobalVariables.ActiveSquad;
                var ReplaceIdUser = UIClientGlobalVariables.UserId;
                var ReplacePublicIpAddress = UIClientGlobalVariables.PublicIpAddress;
                if (ReplaceIdUser == "") ReplaceIdUser = "000";
                if (ReplacePublicIpAddress == "") ReplacePublicIpAddress = "000";

                Url = Url.Replace("*Id", ReplaceIdUser ?? "000");
                Url  = Url.Replace("*Ip", ReplacePublicIpAddress ?? "000");

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

    }

}
