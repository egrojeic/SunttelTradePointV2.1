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
    public class IA : TSalesDocuments
    {
        private readonly HttpClient _httpClient;
        private string basepath = "/api/Sales/Name?userId=*Id&ipAdress=*Ip";
        public IA(HttpClient httpClient)
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
                string path = basepath.Replace("Name", "GetCommercialDocumentTypes");
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

        public async Task<List<Concept>> GetCommercialVendorList(string filter,bool IsSales)
        {
            try
            {
                string path = $"/api/ConceptsSelector/GetSelectorListEntityActor?filterString={filter}&roleIndex=1";
                var responseMessage = await Gethttp($"{path}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Concept>>();
                if(IsSales && list !=null) list = list.Where(s=>s.SquadId == UIClientGlobalVariables.ActiveSquad.IDSquads.ToString()).ToList();
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

        public async Task<List<Concept>> GetCommercialBuyerList(string filter,bool isASale)
        {
            try
            {
                 string path = $"/api/ConceptsSelector/GetSelectorListEntityActor?filter=all&roleIndex=3";
                var responseMessage = await Gethttp($"{path}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Concept>>();
                if (!isASale) list = list.Where(s => s.Id == UIClientGlobalVariables.EntityUserId).ToList();
                return list != null ? list : new List<Concept>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<Concept>> GetCarrierList()
        {
            try
            {
                string path = $"/api/ConceptsSelector/GetSelectorListEntityActor?filter=all&roleIndex=4";
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


        public async Task<CommercialDocumentType?> GetCommercialDocumentTypeById(string commercialDocumentTypeId)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentTypeById");
                var responseMessage = await Gethttp($"{path}&commercialDocumentTypeId={commercialDocumentTypeId}");
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
                string path = basepath.Replace("Name", "GetBusinessLines");
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
                string path = basepath.Replace("Name", "GetCommercialDocumentTypeById");
                var responseMessage = await Gethttp($"{path}&commercialDocumentTypeId={commercialDocumentId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<CommercialDocument>();
                return list != null ? list : new CommercialDocument();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<Address>> GetEntityDetailsAddressList(string entityActorId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetEntityDetailsAddressList?userId={userId}&ipAdress={ipAddress}&EntityId={entityActorId}");
                var list = await response.Content.ReadFromJsonAsync<List<Address>>();
                return list;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<AtomConcept>> GetSelectorListEntityActor(string filterString, BasicRolesFilter? roleIndex)
        {
          
            try
            {
              
                var response = await Gethttp($"/api/ConceptsSelector/GetSelectorListEntityActor?filterString ={filterString}&roleIndex={roleIndex}");
                var list = await response.Content.ReadFromJsonAsync<List<AtomConcept>>();
                return list;
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
