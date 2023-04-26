using SunttelTradePointB.Client.Interfaces.InventoryInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Sales;
using System.Net.Http.Json;
using SunttelTradePointB.Shared.Accounting;
using Syncfusion.Blazor.PivotView;
using SunttelTradePointB.Client.Interfaces.ICreditInterfaces;

namespace SunttelTradePointB.Client.Services.StandingOrderServices
{
    public class StandingOrderServices 
    {
        private readonly HttpClient _httpClient;
        private string pathApi = "/api/Credit/*Name?userId=*Id&ipAddress=*Ip"; 
        public StandingOrderServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CreditDocument> SaveCreditDocument(CreditDocument creditDocument)
        {
            try
            {
                string path = $"{pathApi}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "SaveCreditDocument");
                creditDocument.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<CreditDocument>($"{path}", creditDocument);
                return await responseMessage.Content.ReadFromJsonAsync<CreditDocument>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


     


    
        public async Task<CreditStatus> CreditStatusById(   string creditStatusById)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "CreditStatusById");
                var responseMessage = await Gethttp($"{path}&creditStatusById={creditStatusById}");
                var list = await responseMessage.Content.ReadFromJsonAsync<CreditStatus>();
                return list != null ? list : new CreditStatus();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

    

        public async Task<List<CreditReason>> GetStandingOrderList(string? filter = null, int? page = 1, int? perPage = 10)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "GetCreditReasons");
                var responseMessage = await Gethttp($"{path}&filter={filter}&page={page}&perPage={perPage}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<CreditReason>>();
                return list != null ? list : new List<CreditReason>();
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
