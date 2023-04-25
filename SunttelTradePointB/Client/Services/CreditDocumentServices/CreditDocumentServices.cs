using SunttelTradePointB.Client.Interfaces.InventoryInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Sales;
using System.Net.Http.Json;
using SunttelTradePointB.Shared.Accounting;
using Syncfusion.Blazor.PivotView;
using SunttelTradePointB.Client.Interfaces.ICreditInterfaces;

namespace SunttelTradePointB.Client.Services.CreditDocumentServices
{
    public class CreditDocumentServices : ICredit
    {
        private readonly HttpClient _httpClient;
        private string pathApi = "/api/Credit/*Name?userId=*Id&ipAddress=*Ip";
        public CreditDocumentServices(HttpClient httpClient)
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


        public async Task<CreditType> SaveCreditType(CreditType creditType)
        {
            try
            {
                string path = $"{pathApi}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "SaveCreditType");
                creditType.SquadId = UIClientGlobalVariables.ActiveSquad != null ? UIClientGlobalVariables.ActiveSquad.IDSquads.ToString() : "000";
                var responseMessage = await _httpClient.PostAsJsonAsync<CreditType>($"{path}", creditType);
                return await responseMessage.Content.ReadFromJsonAsync<CreditType>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<CreditStatus> SaveCreditStatus(CreditStatus creditStatus)
        {
            try
            {
                string path = $"{pathApi}";
                // path = GetGlobalVariables(path);
                 path = path.Replace("*Name", "SaveCreditStatus");
                creditStatus.SquadId = UIClientGlobalVariables.ActiveSquad!=null ?  UIClientGlobalVariables.ActiveSquad.IDSquads.ToString():"000";
                var responseMessage = await _httpClient.PostAsJsonAsync<CreditStatus>($"{path}", creditStatus);
                return await responseMessage.Content.ReadFromJsonAsync<CreditStatus>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<CreditReason> SaveCreditReason(CreditReason creditReason)
        {
            try
            {
                string path = $"{pathApi}";
                path = GetGlobalVariables(path);
                path = path.Replace("*Name", "SaveCreditReason");
                creditReason.SquadId = UIClientGlobalVariables.ActiveSquad !=null ? UIClientGlobalVariables.ActiveSquad.IDSquads.ToString():"000";
                var responseMessage = await _httpClient.PostAsJsonAsync<CreditReason>($"{path}", creditReason);
                return await responseMessage.Content.ReadFromJsonAsync<CreditReason>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }



        public async Task<List<CreditDocument>> GetCreditDocumentList( DateTime startDate, DateTime endDate,string? filter = null,int ? page = 1, int? perPage = 10)
        {
            try
            {             
                string path = $"{pathApi}";
                path = path.Replace("*Name", "GetCreditDocuments");
                var responseMessage = await Gethttp($"{path}&startDate={startDate.ToString("yyyy-MM-dd")}&endDate={endDate.ToString("yyyy-MM-dd")}&filter={filter}&page={page}&perPage={perPage}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<CreditDocument>>();
                return list != null ? list : new List<CreditDocument>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<CreditDocument> GetCreditDocumentById( string creditId)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "GetCreditDocumentById");
                var responseMessage = await Gethttp($"{path}&creditId={creditId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<CreditDocument>();
                return list != null ? list : new CreditDocument();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<CreditType>> GetCreditTypes( string? filter = null, int? page = 1, int? perPage = 10)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "GetCreditTypes");
                var responseMessage = await Gethttp($"{path}&filter={filter}&page={page}&perPage={perPage}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<CreditType>>();
                return list != null ? list : new List<CreditType>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<CreditType> GetCreditTypeById(string creditTypeId)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "GetCreditDocumentById");
                var responseMessage = await Gethttp($"{path}&creditId={creditTypeId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<CreditType>();
                return list != null ? list : new CreditType();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<CreditStatus>> GetCreditStatuses(DateTime startDate, DateTime endDate, string? filter = null, int? page = 1, int? perPage = 10)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "GetCreditStatuses");
                var responseMessage = await Gethttp($"{path}&startDate={startDate.ToString("yyyy-MM-dd")}&endDate={endDate.ToString("yyyy-MM-dd")}&filter={filter}&page={page}&perPage={perPage}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<CreditStatus>>();
                return list != null ? list : new List<CreditStatus>();
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

        public async Task<CreditReason> CreditReasonById(string creditReasonById)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "CreditReasonById");
                var responseMessage = await Gethttp($"{path}&creditStatusById={creditReasonById}");
                var list = await responseMessage.Content.ReadFromJsonAsync<CreditReason>();
                return list != null ? list : new CreditReason();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<CreditReason>> GetCreditReasonList(string? filter = null, int? page = 1, int? perPage = 10)
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
