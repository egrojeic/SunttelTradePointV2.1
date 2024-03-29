﻿using MongoDB.Bson.Serialization.IdGenerators;
using SunttelTradePointB.Client.Interfaces.SalesInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;
using SunttelTradePointB.Shared.Sales.CommercialDocument;
using SunttelTradePointB.Shared.Sales.SalesDTO;
using System.Globalization;
using System.Net.Http.Json;

namespace SunttelTradePointB.Client.Services.SalesServices
{
    public class SalesDocuments : ISalesDocuments
    {
        private readonly HttpClient _httpClient;
        private string basepath = "/api/Sales/Name?userId=*Id&ipAddress=*Ip";

        public SalesDocuments(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CommercialDocument> SaveCommercialDocument(CommercialDocument commercialDocument)
        {
            try
            {
                string path = basepath.Replace("Name", "SaveCommercialDocument");
                path = path.Replace("*Id", UIClientGlobalVariables.UserId ?? "00");
                path = path.Replace("*Ip", UIClientGlobalVariables.PublicIpAddress ?? "00");
                commercialDocument.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<CommercialDocument>($"{path}", commercialDocument);
                return await responseMessage.Content.ReadFromJsonAsync<CommercialDocument>();

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<int> UpdateDocumentType(CommercialDocument commercialDocument)
        {
            try
            {
                string path = basepath.Replace("Name", "UpdateDocumentType");
                path = path.Replace("*Id", UIClientGlobalVariables.UserId ?? "00");
                path = path.Replace("*Ip", UIClientGlobalVariables.PublicIpAddress ?? "00");
                commercialDocument.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync($"{path}", commercialDocument);
                var result = await responseMessage.Content.ReadFromJsonAsync<int>();
                return result;

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return 0;
            }
        }

        public async Task<SalesDocumentItemsDetails> UpdateSalesDocumentItemsDetails(CommercialDocumentDetailsDTO salesDocumentItemsDetails)
        {
            try
            {
                string path = basepath.Replace("Name", "UpdateSalesDocumentItemsDetails");
                path = path.Replace("*Id", UIClientGlobalVariables.UserId ?? "00");
                path = path.Replace("*Ip", UIClientGlobalVariables.PublicIpAddress ?? "00");
                var responseMessage = await _httpClient.PostAsJsonAsync<CommercialDocumentDetailsDTO>($"{path}", salesDocumentItemsDetails);
                return await responseMessage.Content.ReadFromJsonAsync<SalesDocumentItemsDetails>();

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
                shippingStatus.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

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
                commercialDocumentType.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();

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

        public async Task<SalesDocumentItemsDetails> SaveCommercialDocumentDetail(SalesDocumentItemsDetails salesDocumentItemsDetails)
        {
            try
            {
                salesDocumentItemsDetails.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<SalesDocumentItemsDetails>($"/api/Sales/SaveCommercialDocumentDetail?userId={UIClientGlobalVariables.UserId}&ipAdress={UIClientGlobalVariables.PublicIpAddress}", salesDocumentItemsDetails);
                return await responseMessage.Content.ReadFromJsonAsync<SalesDocumentItemsDetails>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<SalesDocumentItemsDetails> DeleteCommercialDocumentDetail(SalesDocumentItemsDetails salesDocumentItemsDetails)
        {
            try
            {
                salesDocumentItemsDetails.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();
                var responseMessage = await _httpClient.PostAsJsonAsync<SalesDocumentItemsDetails>($"/api/Sales/DeleteCommercialDocumentDetail?userId={UIClientGlobalVariables.UserId}&ipAdress={UIClientGlobalVariables.PublicIpAddress}", salesDocumentItemsDetails);
                return await responseMessage.Content.ReadFromJsonAsync<SalesDocumentItemsDetails>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<CommercialDocument>> GetCommercialDocumentList(DateTime startDate, DateTime endDate, string documentTypeId, string filter, Concept vendor, bool isSales)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                string nameVendor = vendor != null ? vendor.Name : "";
                string path = basepath.Replace("Name", newValue: "GetCommercialDocumentsByDateSpan");
                var responseMessage = await Gethttp($"{path}&startDate={startDate.ToString("yyyy-MM-dd")}&endDate={endDate.ToString("yyyy-MM-dd")}&documentTypeId={documentTypeId}&filter={filter}&vendorName={nameVendor}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<CommercialDocument>>();
                return list != null ? list : new List<CommercialDocument>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<CommercialDocumentType>> GetCommercialDocumentTypes(bool isASale)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentTypes");
                var responseMessage = await Gethttp($"{path}&isASale={isASale}");
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

                var responseMessage = await Gethttp($"/api/Sales/GetShippingStatusDocById?userId={UIClientGlobalVariables.UserId}&ipAddress={UIClientGlobalVariables.PublicIpAddress}&shippingStatusId={ShippingStatusId}");
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
        public async Task<List<CommercialDocumentType>> GetCommercialDocumentsTypes(string filter, bool isASale)
        {
            try
            {
                string path = basepath.Replace("Name", "GetCommercialDocumentTypes");
                var responseMessage = await Gethttp($"{path}&isASale={isASale}&filterCondition={filter}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<CommercialDocumentType>>();
                return list != null ? list : new List<CommercialDocumentType>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<Concept>> GetCommercialVendorList(string filter, bool IsASale, int? page = 1, int? perPage = 10, bool paginate = true, string? idvendor = null)
        {
            try
            {

                string path = $"/api/ConceptsSelector/GetVendors?isASale={IsASale}&userId=*Id&ipAddress=*Ip&page={page}&perPage={perPage}&filterString={filter}&paginate={paginate}&idvendor={idvendor}";
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


        public async Task<List<AtomConcept>> GetProviderList(string filterString, bool IsASale, int? page = 1, int? perPage = 10)
        {
            try
            {
                var response = await Gethttp($"/api/ConceptsSelector/GetSelectorListEntityActor?filterString={filterString}&roleIndex=1");
                var list = await response.Content.ReadFromJsonAsync<List<AtomConcept>>();
                return list;
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
                var responseMessage = await Gethttp($"/api/GeographicPlaces/GetWarehouses?&entityId={UIClientGlobalVariables.UserId}&ipAdress={UIClientGlobalVariables.PublicIpAddress}&entityId={entityId}&nameLike={nameLike}");
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

        public async Task<List<SalesDocumentItemsDetails>> GetCommercialDocumentDetails(string commercialDocumentId, int? page = 1, int? perPage = 30)
        {
            try
            {

                // page, perPage, filterName
                string path = basepath.Replace("Name", "GetCommercialDocumentDetails");
                var responseMessage = await Gethttp($"{path}&page={page}&perPage={perPage}&commercialDocumentId={commercialDocumentId}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<SalesDocumentItemsDetails>>();
                return list != null ? list : new List<SalesDocumentItemsDetails>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }



        public async Task<List<SalesDocumentItemsDetails>> GetCommercialProductList(string commercialDocumentId, string customerId, string filter, bool paginate = true, int? page = 1, int? perPage = 10)
        {
            try
            {
                string path = $"/api/TransactionalItems/GetProductsByCustomerId?userId=*Id&ipAdress=*Ip&customerId={customerId}&paginate={paginate}&nameLike={filter}&page={page}&perPage={perPage}";
                var responseMessage = await Gethttp(path);
                var list = await responseMessage.Content.ReadFromJsonAsync<List<SalesDocumentItemsDetails>>();
                return list != null ? list : new List<SalesDocumentItemsDetails>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }




        public async Task<List<BasicConcept>> GetCommercialBuyerWarehouseList(string entityId, string? nameLike = "all")
        {
            try
            {
                var responseMessage = await Gethttp($"/api/GeographicPlaces/GetWarehouses?&entityId={entityId}&ipAdress={UIClientGlobalVariables.PublicIpAddress}&entityId={entityId}&nameLike={nameLike}");
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

        public async Task<List<Concept>> GetCommercialBuyerList(string filter, bool IsASale, int? page = 1, int? perPage = 10, bool paginate = true)
        {
            try
            {

                string path = $"/api/ConceptsSelector/GetBuyers?isASale={IsASale}&userId=*Id&ipAddress=*Ip&page={page}&perPage={perPage}&filterString={filter}&paginate={paginate}";
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


        public async Task<List<Concept>> GetCarrierList(string filter)
        {
            try
            {
                string path = $"/api/ConceptsSelector/GetCarriers?filter={filter}";
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

                var responseMessage = await Gethttp($"/api/Sales/GetCommercialDocumentById?userId={UIClientGlobalVariables.UserId}&ipAdress={UIClientGlobalVariables.PublicIpAddress}&documentId={commercialDocumentId}");
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

        public async Task<List<ShippingInfo>> GetEntityShippingSetup(string entityActorId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetShippingSetup?userId={userId}&ipAdress={ipAddress}&entityActorId={entityActorId}");
                var list = await response.Content.ReadFromJsonAsync<List<ShippingInfo>>();
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



        public async Task<List<CommercialDocument>> GetProcurementList()
        {

            try
            {
                string path = basepath.Replace("Name", newValue: "GetProcurementList");
                var responseMessage = await Gethttp($"{path}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<CommercialDocument>>();
                return list;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }




        public async Task<bool> DeleteShippingStatusById(string shippingStatusId)
        {
            try
            {
                string path = basepath.Replace("Name", "DeleteShippingStatusById");
                var responseMessage = await Deletehttp($"{path}&shippingStatusId={shippingStatusId}");
                var result = responseMessage.IsSuccessStatusCode;
                return result;

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> DeleteBusinessLineById(string businessLineId)
        {
            try
            {
                string path = basepath.Replace("Name", "DeleteBusinessLineById");
                var responseMessage = await Deletehttp($"{path}&businessLineId={businessLineId}");
                var result = responseMessage.IsSuccessStatusCode;
                return result;

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> DeleteCommercialDocumentTypeById(string commercialDocumentTypeId)
        {
            try
            {
                string path = basepath.Replace("Name", "DeleteCommercialDocumentTypeById");
                var responseMessage = await Deletehttp($"{path}&commercialDocumentTypeId={commercialDocumentTypeId}");
                var result = responseMessage.IsSuccessStatusCode;
                return result;

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }
        public async Task<List<SalesDocumentItemsDetails>> GetProcurementDetails()
        {
            try
            {
                string path = basepath.Replace("Name", "GetProcurementDetails");
                var responseMessage = await Gethttp($"{path}");
                var result  = await responseMessage.Content.ReadFromJsonAsync<List<SalesDocumentItemsDetails>>();
                return result;

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<CommercialDocumentDetailsResult> GetSalesOrders(string EntityId, int page = 1, int perPage = 10)
        {
            try
            {
                string path = basepath.Replace("Name", "GetSaleOrders");
                var responseMessage = await Gethttp($"{path}&EntityId={EntityId}&page={page}&perPage={perPage}");
                var result = await responseMessage.Content.ReadFromJsonAsync<CommercialDocumentDetailsResult>();
                return result;

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<bool> DeleteSaleDetailById(string saleId, string detailId)
        {
            try
            {
                string path = basepath.Replace("Name", "DeleteSaleDetailById");
                var responseMessage = await Deletehttp($"{path}&saleId={saleId}&detailId={detailId}");
                var result = responseMessage.IsSuccessStatusCode;
                return result;

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }



        public async Task<HttpResponseMessage> Gethttp(string Url)
        {
            try
            {

                var SquadId = UIClientGlobalVariables.ActiveSquad;
                var ReplaceIdUser = UIClientGlobalVariables.UserId;
                var ReplacePublicIpAddress = UIClientGlobalVariables.PublicIpAddress;
                if (ReplacePublicIpAddress == "") ReplacePublicIpAddress = "000";
                if (ReplaceIdUser == "") ReplaceIdUser = "000";

                Url = Url.Replace("*Id", ReplaceIdUser ?? "000");
                Url = Url.Replace("*Ip", ReplacePublicIpAddress ?? "000");

                var request = new HttpRequestMessage(HttpMethod.Get, Url);

                if (SquadId != null) request.Headers.Add("SquadId", SquadId.ID.ToString().ToUpper());
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

        public async Task<HttpResponseMessage> Deletehttp(string Url)
        {
            try
            {

                var SquadId = UIClientGlobalVariables.ActiveSquad;
                var ReplaceIdUser = UIClientGlobalVariables.UserId;
                var ReplacePublicIpAddress = UIClientGlobalVariables.PublicIpAddress;
                if (ReplacePublicIpAddress == "") ReplacePublicIpAddress = "000";
                if (ReplaceIdUser == "") ReplaceIdUser = "000";

                Url = Url.Replace("*Id", ReplaceIdUser ?? "000");
                Url = Url.Replace("*Ip", ReplacePublicIpAddress ?? "000");

                var request = new HttpRequestMessage(HttpMethod.Delete, Url);

                if (SquadId != null) request.Headers.Add("SquadId", SquadId.ID.ToString().ToUpper());
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
