using SunttelTradePointB.Client.Interfaces.InventoryInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Sales;
using System.Net.Http.Json;
using SunttelTradePointB.Shared.Accounting;

namespace SunttelTradePointB.Client.Services.PaymentServices
{
    public class PaymentServices
    {
        private readonly HttpClient _httpClient;
        private string Configpath = "userId=*Id&ipAddress=*Ip";
        public PaymentServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<Payment> SavePaymentItem(Payment payment)
        {
            try
            {
                string path = $"/api/Payment/pendiente?&{Configpath}";
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
                string path = $"/api/Inventory/GetInventory?&{Configpath}";
               // paymentType.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<PaymentType>($"{path}", paymentType);
                return await responseMessage.Content.ReadFromJsonAsync<PaymentType>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<PaymentStatus> SavePaymentType(PaymentStatus paymentType)
        {
            try
            {
                string path = $"/api/Inventory/GetInventory?&{Configpath}";
                //paymentType.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<PaymentStatus>($"{path}", paymentType);
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
                string path = $"/api/Inventory/GetInventory?&{Configpath}";
                //paymentType.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<PaymentMode>($"{path}", paymentMode);
                return await responseMessage.Content.ReadFromJsonAsync<PaymentMode>();

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
                string path = $"/api/Inventory/GetInventory?&{Configpath}";
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
                string Url = GetGlobalVariables($"");
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

        public async Task<List<PaymentMode>> GetPaymentById(string paymentTypeId)
        {
            try
            {
                string Url = GetGlobalVariables($"");
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



        public async Task<List<Payment>> GetPaymentList(string filterName, string documentTypeId,string date, int? page = 1, int? perPage = 10)
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
