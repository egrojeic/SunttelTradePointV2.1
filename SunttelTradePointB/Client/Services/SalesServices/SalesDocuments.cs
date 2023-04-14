﻿using SunttelTradePointB.Client.Interfaces.SalesInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;
using System.Net.Http.Json;

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



        public async Task<CommercialDocument> SaveCommercialDocument(CommercialDocument commercialDocument)
        {
            try
            {
                string path = basepath.Replace("Name", "SaveCommercialDocumentType");
                commercialDocument.SquadId = UIClientGlobalVariables.ActiveSquad.IDSquads.ToString();
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





        ///Gets
        public async Task<List<CommercialDocument>> GetCommercialDocumentList(DateTime startDate, DateTime endDate, string documentTypeId)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentsByDateSpan");
                var responseMessage = await Gethttp($"{path}&startDate={startDate}&endDate={endDate}&endDate={documentTypeId}");
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

        public async Task<List<Concept>> GetCommercialVendorList(string filter, CommercialDocumentType documentType)
        {
            try
            {
                string path = $"/api/ConceptsSelector/GetVendors?filterString={filter}&DocumentTypeId={documentType.Id}";
                var responseMessage = await Gethttp($"{path}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Concept>>();
                //if(isSales && list !=null) list = list.Where(s=>s.SquadId == UIClientGlobalVariables.ActiveSquad.IDSquads.ToString()).ToList();
                return list != null ? list : new List<Concept>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BasicConcept>> GetCommercialVendorWarehouseList(string entityId, string? nameLike = "all")
        {
            try
            {
                var responseMessage = await Gethttp($"/api/GeographicPlaces/GetWarehouses?entityId={entityId}&nameLike={nameLike}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Warehouse>>();
                List<BasicConcept> conceptLis = new();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        conceptLis.Add(new BasicConcept
                        {
                            Id = item.Id,
                            Name = item.Name
                        });
                    }
                }
                return conceptLis != null ? conceptLis : new List<BasicConcept>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<AddItemCommercialDocument>> GetCommercialDocumentDetails(string filterName = "",int? page=1,int? perPage=30)
        {
            try
            {
                // page, perPage, filterName
                string path = basepath.Replace("Name", "GetCommercialDocumentDetails");
                var responseMessage = await Gethttp($"{path}page=0");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<AddItemCommercialDocument>>();
                return list != null ? list : new List<AddItemCommercialDocument>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

               


        public async Task<List<BasicConcept>> GetCommercialBuyerWarehouseList(string entityId,string? nameLike ="all")
        {
            try
            {                
                var responseMessage = await Gethttp($"/api/GeographicPlaces/GetWarehouses?entityId={entityId}&nameLike={nameLike}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Warehouse>>();
                List<BasicConcept> conceptLis = new();
                if (list!=null) {
                    foreach (var item in list)
                    {
                        conceptLis.Add(new BasicConcept {
                            Id = item.Id,
                            Name = item.Name
                        });
                    }
                }

                return conceptLis != null ? conceptLis : new List<BasicConcept>();
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
               
                var responseMessage = await Gethttp($"/api/ConceptsSelector/GetSalesPersons?filterString={filter}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<AtomConcept>>();
                List<Concept> conceptList = new();
                foreach (var item in list)
                {
                    conceptList.Add(new Concept
                    {
                        Id = item.Id,
                        Name = item.Name
                        
                    });
                }
                return conceptList != null ? conceptList : new List<Concept>();
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

        public async Task<List<Concept>> GetCommercialBuyerList(string filter, CommercialDocumentType documentType)
        {
            try
            {
                 string path = $"/api/ConceptsSelector/GetBuyers?filterString={filter}&DocumentTypeId={documentType.Id}";
                var responseMessage = await Gethttp($"{path}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<Concept>>();
             //   if (!isASale) list = list.Where(s => s.Id == UIClientGlobalVariables.EntityUserId).ToList();
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
                string path = $"/api/ConceptsSelector/GetCarriers?filterString=all";
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
