using SunttelTradePointB.Client.Interfaces.InventoryInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Sales;
using System.Net.Http.Json;

namespace SunttelTradePointB.Client.Services.PaymentServices
{
    public class Payment
    {
        private readonly HttpClient _httpClient;
        private string Configpath = "userId=*Id&ipAddress=*Ip";
        public Payment(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<Payment> SavePaymentItem(InventoryDetail commercialDocument)
        {
            try
            {
                string path = $"/api/Inventory/GetInventory?&{Configpath}";
                commercialDocument.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<InventoryDetail>($"{path}", commercialDocument);
                return await responseMessage.Content.ReadFromJsonAsync<Payment>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }





        public async Task<List<Payment>> GetPaymentList(string filterName, string documentTypeId, int? page = 1, int? perPage = 10)
        {
            try
            {
                string Url = GetGlobalVariables($"");
                var responseMessage = await Gethttp(Url);
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Payment>>();
                List<Payment> conceptLis = new();
                return conceptLis != null ? conceptLis : new List<Payment>();
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
