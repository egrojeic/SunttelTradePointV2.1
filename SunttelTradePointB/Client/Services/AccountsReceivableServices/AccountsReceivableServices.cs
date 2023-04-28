using SunttelTradePointB.Client.Interfaces.AccountsReceivableInterfaces;
using SunttelTradePointB.Client.Interfaces.SalesInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;

namespace SunttelTradePointB.Client.Services.AccountsReceivableServices
{
    public class AccountsReceivableServices : QualityEvaluation
    {
        private readonly HttpClient _httpClient;
        private string basepath = "/api/AccountsReceivableServices/Name?userId=*Id&ipAddress=*Ip";
      
        public AccountsReceivableServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        ///Gets
        public async Task<List<CommercialDocument>> GetAccountsReceivableServicesList(DateTime startDate, DateTime endDate, string warehouseId, string filter,int page,int perPage)
        {           
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                //
                string path = basepath.Replace("Name", "Pendiente");
                var responseMessage = await Gethttp($"{path}&shippingDate={startDate.ToString("yyyy-MM-dd", culture)}&warehouseId={warehouseId}&filter={filter}&page={page}&perPage={perPage}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<CommercialDocument>>();
                return list != null ? list : new List<CommercialDocument>();
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
