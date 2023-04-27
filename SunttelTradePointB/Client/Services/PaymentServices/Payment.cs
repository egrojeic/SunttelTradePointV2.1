using SunttelTradePointB.Client.Interfaces.InventoryInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Sales;
using System.Net.Http.Json;
using SunttelTradePointB.Shared.Accounting;
using Syncfusion.Blazor.PivotView;
using SunttelTradePointB.Client.Interfaces.IPaymentInterfaces;

namespace SunttelTradePointB.Client.Services.PaymentServices
{
    public class PaymentServices  : IPayment
    {
        private readonly HttpClient _httpClient;
        private string pathApi = "/api/Payment/*Name?userId=*Id&ipAddress=*Ip";
        public PaymentServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Payment> SavePaymentItem(Payment payment)
        {
            try
            {
                string path = $"{pathApi}";
                payment.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<Payment>($"{path}", payment);
                return await responseMessage.Content.ReadFromJsonAsync<Payment>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<PaymentType> SavePaymentType(PaymentType paymentType)
        {
            try
            {
                string path = $"/api/Inventory/GetInventory?&";
                paymentType.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<PaymentType>($"{path}", paymentType);
                return await responseMessage.Content.ReadFromJsonAsync<PaymentType>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<PaymentStatus> SavePaymentStatus(PaymentStatus paymentStatus)
        {
            try
            {
                string path = $"/api/Inventory/GetInventory?";
                paymentStatus.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<PaymentStatus>($"{path}", paymentStatus);
                return await responseMessage.Content.ReadFromJsonAsync<PaymentStatus>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<PaymentMode> SavePaymentMode(PaymentMode paymentMode)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "SaveDocPaymentMode");
                path = GetGlobalVariables(path);               
                paymentMode.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<PaymentMode>($"{path}", paymentMode);
                return await responseMessage.Content.ReadFromJsonAsync<PaymentMode>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<AppliedPayment> SaveAppliedPayment(AppliedPayment appliedPayment)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "pendiente");
                path = GetGlobalVariables(path);
                appliedPayment.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<AppliedPayment>($"{path}", appliedPayment);
                return await responseMessage.Content.ReadFromJsonAsync<AppliedPayment>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<PaymentWithOverpayment> SavepaymentWithOverpayment(PaymentWithOverpayment paymentWithOverpayment)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "pendiente");
                path = GetGlobalVariables(path);
                paymentWithOverpayment.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<PaymentWithOverpayment>($"{path}", paymentWithOverpayment);
                return await responseMessage.Content.ReadFromJsonAsync<PaymentWithOverpayment>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<PaymentWithOverplus> SavePaymentWithOverplus(PaymentWithOverplus paymentWithOverplus)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "pendiente");
                path = GetGlobalVariables(path);
                paymentWithOverplus.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<PaymentWithOverplus>($"{path}", paymentWithOverplus);
                return await responseMessage.Content.ReadFromJsonAsync<PaymentWithOverplus>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<PaymentWithCredit> SavPymentWithCredit(PaymentWithCredit pymentWithCredit)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "pendiente");
                path = GetGlobalVariables(path);
                pymentWithCredit.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<PaymentWithCredit>($"{path}", pymentWithCredit);
                return await responseMessage.Content.ReadFromJsonAsync<PaymentWithCredit>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<PaymentVia> SavePaymentVia(PaymentVia paymentVia)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "SaveDocPaymentVia");
                //paymentVia.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<PaymentVia>($"{path}", paymentVia);
                return await responseMessage.Content.ReadFromJsonAsync<PaymentVia>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<PaymentMode>> GetPaymentViaById(string paymentViaId)
        {
            try
            {             
                string path = $"{pathApi}";
                path = path.Replace("*Name", "GetDocPaymentVias");
                var responseMessage = await Gethttp(path);
                var list = await responseMessage.Content.ReadFromJsonAsync<List<PaymentMode>>();
                return list != null ? list : new List<PaymentMode>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<Payment> GetPaymentById(string paymentId)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "GetPaymentById");
                path = GetGlobalVariables(path);
                var responseMessage = await Gethttp($"{path}&paymentId={paymentId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<Payment>();
                return list != null ? list : new Payment();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<Concept>> GetPayerList(string filter, bool isASale, int? page = 1, int? perPage = 10)
        {
            try
            {

                string path = $"/api/ConceptsSelector/GetBuyers?isASale={isASale}&userId=*Id&ipAddress=*Ip&page={page}&perPage={perPage}&filterString={filter}";
                var responseMessage = await Gethttp($"{path}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Concept>>();             
                return list != null ? list : new List<Concept>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<Concept>> GetReceiverList(string filter, bool isASale, int? page = 1, int? perPage = 10)
        {
            try
            {
                string path = $"/api/ConceptsSelector/GetBuyers?isASale={isASale}&userId=*Id&ipAddress=*Ip&page={page}&perPage={perPage}&filterString={filter}";
                var responseMessage = await Gethttp($"{path}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Concept>>();               
                return list != null ? list : new List<Concept>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }



        public async Task<List<Payment>> GetPaymentList(string filterName, string documentTypeId,string date, int? page = 1, int? perPage = 10)
        {
            try
            {
                string path = $"{pathApi}";
                path = path.Replace("*Name", "GetPaymentsByDateSpan");
                path = GetGlobalVariables(path);
                var responseMessage = await Gethttp($"{path}&PaymentDate={date}&filter={filterName}");
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

        public async Task<List<PaymentVia>> GetPaymentViasList( int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string Url = pathApi.Replace("*Name", "GetDocPaymentVias");
                Url = GetGlobalVariables($"{Url}&filter={filter}");
                var responseMessage = await Gethttp(Url);
                var list = await responseMessage.Content.ReadFromJsonAsync<List<PaymentVia>>();              
                return list != null ? list : new List<PaymentVia>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<PaymentType>> GetPaymentModeById(string paymentModeById)
        {
            try
            {
                string Url = GetGlobalVariables($"");
                var responseMessage = await Gethttp(Url);
                var list = await responseMessage.Content.ReadFromJsonAsync<List<PaymentType>>();              
                return list != null ? list : new List<PaymentType>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<PaymentStatus>> GetPaymentStatusById(string paymentTypeId)
        {
            try
            {
                string Url = GetGlobalVariables($"");
                var responseMessage = await Gethttp(Url);
                var list = await responseMessage.Content.ReadFromJsonAsync<List<PaymentStatus>>();
              
                return list != null ? list : new List<PaymentStatus>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<PaymentType>> GetPaymentTypeList(int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string Url = pathApi.Replace("*Name", "GetDocPaymentVias");
                Url = GetGlobalVariables($"{Url}&filter={filter}");
                var responseMessage = await Gethttp(Url);
                var list = await responseMessage.Content.ReadFromJsonAsync<List<PaymentType>>();
                return list != null ? list : new List<PaymentType>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<PaymentMode>> GetPaymentModesList(int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string Url = pathApi.Replace("*Name", "GetDocPaymentModes");
                Url = GetGlobalVariables($"{Url}&filter={filter}");
                var responseMessage = await Gethttp(Url);
                var list = await responseMessage.Content.ReadFromJsonAsync<List<PaymentMode>>();
                return list != null ? list : new List<PaymentMode>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<PaymentStatus>> GetPaymentStatusList(int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string Url = pathApi.Replace("*Name", "pendientes");
                Url = GetGlobalVariables($"{Url}&filter={filter}");
                var responseMessage = await Gethttp(Url);
                var list = await responseMessage.Content.ReadFromJsonAsync<List<PaymentStatus>>();
                return list != null ? list : new List<PaymentStatus>();
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
