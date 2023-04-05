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
        public async Task<List<CommercialDocument>> GetCommercialDocumentList(string filterCriteria)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentTypes");
                var responseMessage = await Gethttp($"{path}&filterCondition={filterCriteria}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<CommercialDocument>>();
                return list != null ? list : new List<CommercialDocument>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<CommercialDocumentType>> GetCommercialDocumentTypes()
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentTypes");
                var responseMessage = await Gethttp($"{path}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<CommercialDocumentType>>();
                return list != null ? list : new List<CommercialDocumentType>();
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
                string path = basepath.Replace("Name", "GetShippingStatusDocs");
                var responseMessage = await Gethttp($"{path}&filter={filter}");
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
                string path = basepath.Replace("Name", "GetShippingStatusDocById");
                var responseMessage = await Gethttp($"{path}&ShippingStatusId={ShippingStatusId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<ShippingStatus>();
                return list != null ? list : new ShippingStatus();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
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

        public async Task<List<Concept>> GetCommercialVendorList(string filter)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentsTypes");
                var responseMessage = await Gethttp($"{path}?filterCondition={filter}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Concept>>();
                return list != null ? list : new List<Concept>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BasicConcept>> GetCommercialVendorWarehouseList()
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentsTypes");
                var responseMessage = await Gethttp($"{path}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<BasicConcept>>();
                return list != null ? list : new List<BasicConcept>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }  
        
        
        public async Task<List<BasicConcept>> GetCommercialBuyerWarehouseList()
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentsTypes");
                var responseMessage = await Gethttp($"{path}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<BasicConcept>>();
                return list != null ? list : new List<BasicConcept>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<Concept>> GetCommercialSalesPersonList(string filter)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentsTypes");
                var responseMessage = await Gethttp($"{path}?filterCondition={filter}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Concept>>();
                return list != null ? list : new List<Concept>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


      



        public async Task<List<SeasonBusiness>> GetCommercialSeasonList(string filter)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentsTypes");              
                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetSelectorListSeasonBusiness?filterString={filter}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<SeasonBusiness>>();
               
                return list != null ? list : new List<SeasonBusiness>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<Concept>> GetCommercialBuyerList(string filter)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentsTypes");
                var responseMessage = await Gethttp($"{path}?filterCondition={filter}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Concept>>();
                return list != null ? list : new List<Concept>();
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
     

        //Buisiness Lines
        public async Task<List<BusinessLine>> GetCommercialBusinessLines(string filter)
        {
            try
            {
                string path = basepath.Replace("Name", "GetBusinessLinesDocs");
                var responseMessage = await Gethttp($"{path}&filter={filter}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<BusinessLine>>();
                return list != null ? list : new List<BusinessLine>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BusinessLine>> GetCommercialBusinessLines()
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialBusinessLines");
                var responseMessage = await Gethttp($"{path}");
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
        public async Task<BusinessLine?> GetCommercialBusinessLineById(string businessLineDocId)
        {
            try
            {
                string path = basepath.Replace("Name", "GetBusinessLineDocById");
                var responseMessage = await Gethttp($"{path}&businessLineDocId={businessLineDocId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<BusinessLine>();
                return list != null ? list : new BusinessLine();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
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

        public async Task<CommercialDocument> GetDeliveryAddress(string IdEntity)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentById");
                var responseMessage = await Gethttp($"{path}&documentId={IdEntity}");
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
                string path = basepath.Replace("Name", "SaveCommercialDocumentType");
                var responseMessage = await _httpClient.PostAsJsonAsync<CommercialDocument>($"{path}", commercialDocument);
                return await responseMessage.Content.ReadFromJsonAsync<CommercialDocument>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<ShippingStatus> SaveShippingStatus(ShippingStatus shippingStatus)
        {
            try
            {           
                string path = basepath.Replace("Name", "SaveShippingStatus");
                var responseMessage = await _httpClient.PostAsJsonAsync<ShippingStatus>($"{path}", shippingStatus);
                return await responseMessage.Content.ReadFromJsonAsync<ShippingStatus>();
               
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<CommercialDocumentType> SaveCommercialDocumentType(CommercialDocumentType commercialDocumentType)
        {
            try
            {               
                string path = basepath.Replace("Name", "SaveCommercialDocumentType");
                var responseMessage = await _httpClient.PostAsJsonAsync<CommercialDocumentType>($"{path}", commercialDocumentType);
                return await responseMessage.Content.ReadFromJsonAsync<CommercialDocumentType>();
               
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }
        public async Task<BusinessLine> SaveCommercialBusinessLine(BusinessLine businessLine)
        {
            try
            {
                //businessLine.SquadId = UIClientGlobalVariables.ActiveSquad.IDAppUserOwner;
                string path = basepath.Replace("Name", "SaveBusinessLineDoc");
                var responseMessage = await _httpClient.PostAsJsonAsync<BusinessLine>($"{path}", businessLine);
                return await responseMessage.Content.ReadFromJsonAsync<BusinessLine>();
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

 

}
