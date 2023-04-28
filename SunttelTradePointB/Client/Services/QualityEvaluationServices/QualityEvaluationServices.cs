using SunttelTradePointB.Shared.Quality;
using SunttelTradePointB.Client.Interfaces.QualityEvaluationInterfaces;

using SunttelTradePointB.Shared.Common;

using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Client.Services.QualityEvaluationServices
{
    public class QualityEvaluationServices : IQualityEvaluation
    {
        private readonly HttpClient _httpClient;
        private string basepath = "/api/QualityEvaluation/Name?userId=*Id&ipAddress=*Ip";

        public QualityEvaluationServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<QualityEvaluation> SaveQualityEvaluation(QualityEvaluation qualityEvaluation)
        {
            try
            {
                string path = $"{basepath}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "SaveStandingOrder");
                qualityEvaluation.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<QualityEvaluation>($"{path}", qualityEvaluation);
                return await responseMessage.Content.ReadFromJsonAsync<QualityEvaluation>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<QualityReportType> SaveQualityReportType(QualityReportType qualityReportType)
        {
            try
            {
                string path = $"{basepath}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "pendiente");
                qualityReportType.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<QualityReportType>($"{path}", qualityReportType);
                return await responseMessage.Content.ReadFromJsonAsync<QualityReportType>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }
           public async Task<QualityTrafficLight> SaveQualityTrafficLight(QualityTrafficLight qualityTrafficLight)
        {
            try
            {
                string path = $"{basepath}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "pendiente");
                qualityTrafficLight.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<QualityTrafficLight>($"{path}", qualityTrafficLight);
                return await responseMessage.Content.ReadFromJsonAsync<QualityTrafficLight>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<QualityParameterGroup> SaveQualityParameterGroup(QualityParameterGroup qualityParameterGroup)
        {
            try
            {
                string path = $"{basepath}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "pendiente");
                qualityParameterGroup.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<QualityParameterGroup>($"{path}", qualityParameterGroup);
                return await responseMessage.Content.ReadFromJsonAsync<QualityParameterGroup>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<QualityAction> SaveQualityPaction(QualityAction qualityAction)
        {
            try
            {
                string path = $"{basepath}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "pendiente");
                qualityAction.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<QualityAction>($"{path}", qualityAction);
                return await responseMessage.Content.ReadFromJsonAsync<QualityAction>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }




        ///Gets
        public async Task<List<QualityEvaluation>> GetQualityEvaluationServicesList(DateTime startDate, DateTime endDate, string qualityReportTypeId, string filter, int page, int perPage)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                //
                string path = basepath.Replace("Name", "Pendiente");
                var responseMessage = await Gethttp($"{path}&shippingDate={startDate.ToString("yyyy-MM-dd", culture)}&warehouseId={qualityReportTypeId}&filter={filter}&page={page}&perPage={perPage}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<QualityEvaluation>>();
                return list != null ? list : new List<QualityEvaluation>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<QualityReportType>> GetQualityReportTypeList(string filter, int page, int perPage)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("Name", "Pendiente");
                var responseMessage = await Gethttp($"{path}&filter={filter}&page={page}&perPage={perPage}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<QualityReportType>>();
                return list != null ? list : new List<QualityReportType>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }




        public async Task<List<QualityAction>> GetQualityActionList(string filter, int page, int perPage)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("Name", "Pendiente");
                var responseMessage = await Gethttp($"{path}&filter={filter}&page={page}&perPage={perPage}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<QualityAction>>();
                return list != null ? list : new List<QualityAction>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }


        }




        public async Task<QualityReportType> GetQualityReportType(string qualityReportTypeId)
        {
            
            try
            {
                string path = basepath.Replace("Name", "Pendiente");
                var responseMessage = await Gethttp($"{path}&qualityReportTypeId={qualityReportTypeId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<QualityReportType>();
                return list;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<QualityParameterGroup> GetQualityParameterGroupById(string qualityParameterGroupId)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("Name", "Pendiente");
                var responseMessage = await Gethttp($"{path}&qualityParameterGroupId={qualityParameterGroupId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<QualityParameterGroup>();
                return list;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }
        public async Task<QualityAction> GetQualityActionById(string qualityActionId)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("Name", "Pendiente");
                var responseMessage = await Gethttp($"{path}&qualityParameterGroupId={qualityActionId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<QualityAction>();
                return list;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }




        public async Task<QualityEvaluation> GetItemQualityEvaluationById(string qualityReportTypeId)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("Name", "Pendiente");
                var responseMessage = await Gethttp($"{path}&qualityReportTypeId={qualityReportTypeId}");
                var item = await responseMessage.Content.ReadFromJsonAsync<QualityEvaluation>();
                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }



        public async Task<CommercialDocument> GetItemSalesDocumentItemsDetailsById(string salesDocumentItemsDetailsId)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("Name", "Pendiente");
                var responseMessage = await Gethttp($"{path}&salesDocumentItemsDetailsId={salesDocumentItemsDetailsId}");
                var item = await responseMessage.Content.ReadFromJsonAsync<CommercialDocument>();
                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

          public async Task<QualityTrafficLight> GetQualityTrafficLightById(string qualityTrafficLightId)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("Name", "Pendiente");
                var responseMessage = await Gethttp($"{path}&qualityTrafficLightId={qualityTrafficLightId}");
                var item = await responseMessage.Content.ReadFromJsonAsync<QualityTrafficLight>();
                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<CommercialDocument>> GetItemSalesDocumentItemsDetailList(string filter, int page, int perPage)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("Name", "Pendiente");
                var responseMessage = await Gethttp($"{path}&filter={filter}&page={page}&perPage={perPage}");
                var item = await responseMessage.Content.ReadFromJsonAsync<List<CommercialDocument>>();
                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<QualityTrafficLight>> GetQualityTrafficLightList(string filter, int page, int perPage)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("Name", "Pendiente");
                var responseMessage = await Gethttp($"{path}&filter={filter}&page={page}&perPage={perPage}");
                var item = await responseMessage.Content.ReadFromJsonAsync<List<QualityTrafficLight>>();
                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<QualityParameterGroup>> GetQualityParameterGrouplList(string filter, int page, int perPage)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("Name", "Pendiente");
                var responseMessage = await Gethttp($"{path}&filter={filter}&page={page}&perPage={perPage}");
                var item = await responseMessage.Content.ReadFromJsonAsync<List<QualityParameterGroup>>();
                return item;
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

            Url = Url.Replace("*IP", ReplacePublicIpAddress ?? "000");
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
