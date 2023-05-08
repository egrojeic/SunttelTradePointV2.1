using SunttelTradePointB.Client.Interfaces.SalesInterfaces;

namespace SunttelTradePointB.Client.Services.SalesServices
{
    public class SalesBIViewerServices : ISalesBIViewer
    {



        private readonly HttpClient _httpClient;
        private string basepath = "/api/Sales/*Name?userId=*Id&ipAddress=*Ip";

        public SalesBIViewerServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
