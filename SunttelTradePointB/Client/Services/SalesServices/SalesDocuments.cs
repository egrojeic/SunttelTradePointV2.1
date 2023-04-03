using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Communications;
using SunttelTradePointB.Shared.Common;
using System.Net;
using System.Net.Http.Json;
using SunttelTradePointB.Client.Interfaces.SalesInterfaces;
using SunttelTradePointB.Shared.Sales;
using System.Reflection;
using System.Net.Http;

namespace SunttelTradePointB.Client.Services.SalesServices
{
    public class SalesDocuments : TSalesDocuments
    {
        private readonly HttpClient _httpClient;
        private string basepath = "/api/Sales/Name?userId=*Id&ipAdress=*Ip";
        public SalesDocuments(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        ///Gets
        public async Task<List<CommercialDocument>> GetCommercialDocumentList(string filterCriteria, DateTime? startingDate = null, DateTime? endDate = null)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentsByDateSpan");
                var responseMessage = await Gethttp($"{path}&startDate={startingDate}&endDate={endDate}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<CommercialDocument>>();
                return list != null ? list : new List<CommercialDocument>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        //Shipping Statuses
        public async Task<List<ShippingStatus>> GetShippingStatuses(string filter)
        {
            try
            {
                string path = basepath.Replace("Name", "GetShippingStatuses");
                var responseMessage = await Gethttp($"{path}?filterCondition={filter}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<ShippingStatus>>();
                return list != null ? list : new List<ShippingStatus>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

         public async Task<ShippingStatus> GetShippingStatusById(string ShippingStatusId)
        {
            try
            {
                string path = basepath.Replace("Name", "GetShippingStatusById");
                var responseMessage = await Gethttp($"{path}?ShippingStatusId={ShippingStatusId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<ShippingStatus>();
                return list != null ? list : new ShippingStatus();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<bool> SaveShippingStatus(ShippingStatus shippingStatus)
        {
            try
            {
                var clientPost = new AddProperty<ShippingStatus>(_httpClient);
                shippingStatus = (ShippingStatus)clientPost.Property(shippingStatus);
                string path = basepath.Replace("Name", "SaveShippingStatus");
                var responseMessage = await clientPost.Posthttp(path, shippingStatus);
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

        // Documents types
        public async Task<List<CommercialDocumentType>> GetCommercialDocumentsTypes(string filter)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentsTypes");
                var responseMessage = await Gethttp($"{path}?filterCondition={filter}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<CommercialDocumentType>>();
                return list != null ? list : new List<CommercialDocumentType>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<CommercialDocumentType?> GetCommercialDocumentTypeById(string filter)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentTypeById");
                var responseMessage = await Gethttp($"{path}?filterCondition={filter}");
                var list = await responseMessage.Content.ReadFromJsonAsync<CommercialDocumentType>();
                return list != null ? list : new CommercialDocumentType();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<bool> SaveCommercialDocumentType(CommercialDocumentType commercialDocumentType)
        {
            try
            {
                var clientPost = new AddProperty<CommercialDocumentType>(_httpClient);
                commercialDocumentType = (CommercialDocumentType)clientPost.Property(commercialDocumentType);
                string path = basepath.Replace("Name", "SaveCommercialDocumentType");
                var responseMessage = await clientPost.Posthttp(path, commercialDocumentType);
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

        //Buisiness Lines
        public async Task<List<BusinessLine>> GetCommercialBusinessLines(string filter)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialBusinessLines");
                var responseMessage = await Gethttp($"{path}?filterCondition={filter}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<BusinessLine>>();
                return list != null ? list : new List<BusinessLine>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<BusinessLine?> GetCommercialBusinessLineById(string filter)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialBusinessLineById");
                var responseMessage = await Gethttp($"{path}?filterCondition={filter}");
                var list = await responseMessage.Content.ReadFromJsonAsync<BusinessLine>();
                return list != null ? list : new BusinessLine();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<bool> SaveCommercialBusinessLine(BusinessLine businessLine)
        {
            try
            {
                var clientPost = new AddProperty<BusinessLine>(_httpClient);
                businessLine = (BusinessLine)clientPost.Property(businessLine);
                string path = basepath.Replace("Name", "SaveCommercialBusinessLine");
                var responseMessage = await clientPost.Posthttp(path, businessLine);
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

        public async Task<CommercialDocument> GetItemCommercialDocumentById(string commercialDocumentId)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentById");
                var responseMessage = await Gethttp($"{path}&documentId={commercialDocumentId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<CommercialDocument>();
                return list != null ? list : new CommercialDocument();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<CommercialDocument> SaveCommercialDocument(CommercialDocument commercialDocument)
        {
            try
            {
                var clientPost = new AddProperty<CommercialDocument>(_httpClient);
                commercialDocument =   (CommercialDocument)clientPost.Property(commercialDocument);
                string path = basepath.Replace("Name", "GetCommercialDocumentById");               
                var responseMessage = await clientPost.Posthttp(path, commercialDocument);
                return await responseMessage.Content.ReadFromJsonAsync<CommercialDocument>();
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

                Url = Url.Replace("*Id", ReplaceIdUser).Replace("*Ip", ReplacePublicIpAddress);

                var request = new HttpRequestMessage(HttpMethod.Get, Url);

                if (SquadId !=null) request.Headers.Add("SquadId", SquadId.IDSquads.ToString());

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

    class AddProperty<T>
    {
        private HttpClient _httpClient { get; set; }
        public AddProperty(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> Posthttp(string Url, T item)
        {
            try
            {
                var SquadId = UIClientGlobalVariables.ActiveSquad;
                var ReplaceIdUser = UIClientGlobalVariables.UserId;
                var ReplacePublicIpAddress = UIClientGlobalVariables.PublicIpAddress;

                Url = Url.Replace("*Id", ReplaceIdUser).Replace("*Ip", ReplacePublicIpAddress);

                var request = new HttpRequestMessage(HttpMethod.Get, Url);

                if (SquadId != null) request.Headers.Add("SquadId", SquadId.IDSquads.ToString());

                var response = await _httpClient.PostAsJsonAsync<T>(Url, item);
               
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



        public object Property(object Model)
        {
            object obj = null;            
            PropertyInfo[] lst = typeof(T).GetProperties();
            foreach (PropertyInfo oProperty in lst)
            {
                string Name = oProperty.Name;
                string Tipo = oProperty.GetType().ToString();

                string Valor = "";
                if (!Name.Contains("SquadId")) {
                   oProperty.SetValue(Model, UIClientGlobalVariables.ActiveSquad.IDSquads);
                   obj = oProperty;
                }


            }
            return obj;
        }
       
    }

}
