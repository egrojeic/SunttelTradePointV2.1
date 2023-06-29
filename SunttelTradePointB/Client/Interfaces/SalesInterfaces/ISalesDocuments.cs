using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;
using SunttelTradePointB.Shared.Sales.CommercialDocumentDTO;
using SunttelTradePointB.Shared.Sales.SalesDTO;

namespace SunttelTradePointB.Client.Interfaces.SalesInterfaces
{
    public interface ISalesDocuments
    {

        /// <summary>
        /// Save a commercialDocument item
        /// </summary>
        /// <param name="commercialDocument"></param>
        /// <returns></returns>
        Task<CommercialDocumentDTO> SaveCommercialDocument(CommercialDocumentDTO commercialDocument);

        /// <summary>
        /// Save a commercialDocumentType item
        /// </summary>
        /// <param name="commercialDocumentType"></param>
        /// <returns></returns>
        Task<CommercialDocumentType> SaveCommercialDocumentType(CommercialDocumentType commercialDocumentType);

        /// <summary>
        /// Save a shippingStatus item
        /// </summary>
        /// <param name="shippingStatus"></param>
        /// <returns></returns>
        Task<ShippingStatus> SaveShippingStatus(ShippingStatus shippingStatus);


        /// <summary>
        /// Save a SaveCommercialBusinessLine item
        /// </summary>
        /// <param name="businessLine"></param>
        /// <returns></returns>
        Task<BusinessLine> SaveCommercialBusinessLine(BusinessLine businessLine);

        /// <summary>
        /// Save a SaveCommercialBusinessLine item
        /// </summary>
        /// <param name="salesDocumentItemsDetails"></param>
        /// <returns></returns>
        Task<SalesDocumentItemsDetails> SaveCommercialDocumentDetail(SalesDocumentItemsDetails salesDocumentItemsDetails);

        /// <summary>
        /// Retrives a item with commercialDocument items meeting search criteria
        /// </summary>
        /// <param name="commercialDocumentId"></param>      
        /// <returns></returns>
        Task<CommercialDocumentDTO> GetItemCommercialDocumentById(string commercialDocumentId);

        /// <summary>
        /// Retrives a item with CommercialDocumentType items
        /// </summary>
        /// <param">No</param>      
        /// <returns></returns>
        Task<List<CommercialDocumentType>> GetCommercialDocumentTypes(bool isSale);


        /// <summary>
        /// Retrives a item with ShippingStatus items meeting search criteria
        /// </summary>
        /// <param name="filter"></param>      
        /// <returns></returns>
        Task<List<ShippingStatus>> GetShippingStatuses(string filter);

        /// <summary>
        /// Retrives a item with ShippingStatus items meeting search criteria
        /// </summary>
        /// <param name="ShippingStatusId"></param>      
        /// <returns></returns>
        Task<ShippingStatus> GetShippingStatusById(string ShippingStatusId);



        /// <summary>
        /// Retrives a item with Concept vendor items meeting search criteria
        /// </summary>
        /// <param name="filter"></param>      
        /// <param name="documentType"></param>     
        /// <param name="page"></param>     
        /// <param name="perPage"></param>     
        /// <returns></returns>
        Task<List<Concept>> GetCommercialVendorList(string filter, bool IsASale, int? page = 1, int? perPage = 10);


        /// <summary>
        /// Delete a ShippingStatus
        /// </summary>  
        /// <param name="shippingStatusId"></param>     
        /// <returns></returns>
        Task<bool> DeleteShippingStatusById(string shippingStatusId);

        /// <summary>
        /// Delete a BusinessLine
        /// </summary>  
        /// <param name="businessLineId"></param>     
        /// <returns></returns>
        Task<bool> DeleteBusinessLineById(string businessLineId);


        /// <summary>
        /// Delete a BusinessLine
        /// </summary>  
        /// <param name="commercialDocumentTypeId"></param>     
        /// <returns></returns>
        Task<bool> DeleteCommercialDocumentTypeById(string commercialDocumentTypeId);

        /// <summary>
        /// Delete a SaleDetail
        /// </summary>  
        /// <param name="saleId"></param>     
        /// <param name="detailId"></param>     
        /// <returns></returns>
        Task<bool> DeleteSaleDetailById(string saleId, string detailId);

        /// <summary>
        /// Update the PurchaseItemDetails into a SalesDocumentItemsDetails
        /// </summary>
        /// <param name="salesDocumentItemsDetails"></param>
        /// <returns></returns>
        Task<SalesDocumentItemsDetails> UpdateSalesDocumentItemsDetails(CommercialDocumentDetailsDTO salesDocumentItemsDetails);

        /// <summary>
        /// Retrives a list of CommercialDocumentDetailsDTO
        /// </summary>
        /// <param name="EntityId"></param>
        /// <returns></returns>
        Task<List<CommercialDocumentDetailsDTO>> GetSalesOrders(string EntityId);

        /// <summary>
        /// Retrives a list of Buyers
        /// </summary>
        /// <param name="IsASale"></param>
        /// <returns></returns>
        Task<List<Concept>> GetCommercialBuyerList(string filter, bool IsASale, int? page = 1, int? perPage = 10, bool paginate = true);

        /// <summary>
        ///  Retrives a list of Commercial Business Lines
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<BusinessLine>> GetCommercialBusinessLines(string filter);

        /// <summary>
        /// Update Docuemnt Type of a Commercial Document
        /// </summary>
        /// <param name="commercialDocument"></param>
        /// <returns></returns>
        public Task<bool> UpdateDocumentType(CommercialDocumentDTO commercialDocument);

        /// <summary>
        /// Retrives a Commercial Document Detail by Commercial Document Id
        /// </summary>
        /// <param name="commercialDocumentId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public Task<List<SalesDocumentItemsDetails>> GetCommercialDocumentDetails(string commercialDocumentId, int? page = 1, int? perPage = 30);

        /// <summary>
        /// GetCommercialDocumentList
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="documentTypeId"></param>
        /// <param name="filter"></param>
        /// <param name="vendor"></param>
        /// <param name="isSales"></param>
        /// <returns></returns>
        public Task<List<CommercialDocumentDTO>> GetCommercialDocumentList(DateTime startDate, DateTime endDate, string documentTypeId, string filter, Concept vendor, bool isSales);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<Concept>> GetCarrierList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<List<SeasonBusiness>> GetCommercialSeasonList(string filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="isASale"></param>
        /// <returns></returns>
        public Task<List<CommercialDocumentType>> GetCommercialDocumentsTypes(string filter, bool isASale);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<List<Concept>> GetCommercialSalesPersonList(string filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="nameLike"></param>
        /// <returns></returns>
        public Task<List<BasicConcept>> GetCommercialBuyerWarehouseList(string entityId, string? nameLike = "all");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public Task<List<Address>> GetEntityDetailsAddressList(string entityActorId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="nameLike"></param>
        /// <returns></returns>
        public Task<List<BasicConcept>> GetCommercialVendorWarehouseList(string entityId, string? nameLike = "all");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commercialDocumentId"></param>
        /// <param name="customerId"></param>
        /// <param name="filter"></param>
        /// <param name="paginate"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public Task<List<SalesDocumentItemsDetails>> GetCommercialProductList(string commercialDocumentId, string customerId, string filter, bool paginate = true, int? page = 1, int? perPage = 10);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterString"></param>
        /// <param name="IsASale"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public Task<List<AtomConcept>> GetProviderList(string filterString, bool IsASale, int? page = 1, int? perPage = 10);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<CommercialDocument>> GetProcurementList();
    }
}
