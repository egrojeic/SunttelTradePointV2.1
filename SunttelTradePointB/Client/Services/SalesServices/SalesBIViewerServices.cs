using SunttelTradePointB.Client.Interfaces.SalesInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.DataViews.BI;
using SunttelTradePointB.Shared.Sales;
using System.Globalization;
using System.Net.Http.Json;

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


        public async Task<List<BISalesConsolidated>> GetSalesBIList(DateTime startDate, DateTime endDate, string documentTypeId, string? filter = null, int? page = 1, int? perPage = 10)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("*Name", "GetSalesBI");
                var responseMessage = await Gethttp($"{path}&startDate={startDate.ToString("yyyy-MM-dd")}&endDate={endDate.ToString("yyyy-MM-dd")}&documentTypeId={documentTypeId}&filter={filter}&page={page}&perPage={perPage}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<BISalesConsolidated>>();
                return list != null ? list : new List<BISalesConsolidated>();
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
    }
}
