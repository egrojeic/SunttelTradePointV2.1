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
        private string basepath = "/api/Quality/*Name?userId=*Id&ipAddress=*Ip";

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
                path = path.Replace("*Name", "Pendiente");
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
        public async Task<QualityTrafficLight> SaveQualityTrafficLight(QualityTrafficLight quality)
        {
            try
            {
                string path = $"{basepath}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "SaveQualityTrafficLight");
                quality.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<QualityTrafficLight>($"{path}", quality);
                return await responseMessage.Content.ReadFromJsonAsync<QualityTrafficLight>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<QualityAssuranceParameter> SaveQualityAssuranceParameter(QualityAssuranceParameter quality)
        {
            try
            {
                string path = $"{basepath}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "SaveQualityParameter");
                quality.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<QualityAssuranceParameter>($"{path}", quality);
                return await responseMessage.Content.ReadFromJsonAsync<QualityAssuranceParameter>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<QualityParameterGroup> SaveQualityParameterGroup(QualityParameterGroup quality)
        {
            try
            {
                string path = $"{basepath}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "SaveQualityParameterGroups");
                quality.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<QualityParameterGroup>($"{path}", quality);
                return await responseMessage.Content.ReadFromJsonAsync<QualityParameterGroup>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<QualityAssuranceParameter> SaveQualityParameterGroup(QualityAssuranceParameter quality)
        {
            try
            {
                string path = $"{basepath}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "SaveQualityParameter");
                quality.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<QualityAssuranceParameter>($"{path}", quality);
                return await responseMessage.Content.ReadFromJsonAsync<QualityAssuranceParameter>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<QualityAction> SaveQualityPaction(QualityAction quality)
        {
            try
            {
                string path = $"{basepath}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "SaveQualityAction");
                quality.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<QualityAction>($"{path}", quality);
                return await responseMessage.Content.ReadFromJsonAsync<QualityAction>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }




        ///Gets
        public async Task<List<QualityEvaluation>> GetQualityEvaluationServicesList(string filter, int? page = 1, int? perPage = 10)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                //
                string path = basepath.Replace("*Name", "GetQualityParametersGroups");
                var responseMessage = await Gethttp($"{path}&filter={filter}&page={page}&perPage={perPage}");
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
                string path = basepath.Replace("*Name", "Pendiente");
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

            try
            {
                string path = basepath.Replace("*Name", "GetQualityActions");
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


        public async Task<QualityParameterGroup> GetQualityParameterGroupById(string qualityId)
        {
            try
            {
                string path = basepath.Replace("*Name", "GetQualityParameteGroupsById");
                var responseMessage = await Gethttp($"{path}&qualityId={qualityId}");
                var item = await responseMessage.Content.ReadFromJsonAsync<QualityParameterGroup>();
                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }
        public async Task<QualityAction> GetQualityActionById(string qualityId)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("*Name", "GetQualityActionById");
                var responseMessage = await Gethttp($"{path}&qualityParameterGroupId={qualityId}");
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
                string path = basepath.Replace("*Name", "Pendiente");
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
                string path = basepath.Replace("*Name", "Pendiente");
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

        public async Task<QualityTrafficLight> GetQualityTrafficLightById(string qualityId)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("*Name", "GetQualityTrafficLightById");
                var responseMessage = await Gethttp($"{path}&qualityId={qualityId}");
                var item = await responseMessage.Content.ReadFromJsonAsync<QualityTrafficLight>();
                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<QualityAssuranceParameter> GetQualityAssuranceParameterById(string qualityId)
        {

            try
            {
                string path = basepath.Replace("*Name", "GetQualityTrafficLightById");
                var responseMessage = await Gethttp($"{path}&qualityId={qualityId}");
                var item = await responseMessage.Content.ReadFromJsonAsync<QualityAssuranceParameter>();
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
                string path = basepath.Replace("*Name", "Pendiente");
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

            try
            {
                string path = basepath.Replace("*Name", "GetQualityTrafficLights");
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
                string path = basepath.Replace("*Name", "GetQualityParametersGroups");
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


        public async Task<List<QualityAssuranceParameter>> GetQualityAssuranceParameterlList(string filter, int page, int perPage)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string path = basepath.Replace("*Name", "GetQualityParameters");
                var responseMessage = await Gethttp($"{path}&filter={filter}&page={page}&perPage={perPage}");
                var item = await responseMessage.Content.ReadFromJsonAsync<List<QualityAssuranceParameter>>();
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
