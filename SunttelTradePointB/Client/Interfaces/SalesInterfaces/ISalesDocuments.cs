using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Client.Interfaces.SalesInterfaces
{
    public interface ISalesDocuments
    {

        /// <summary>
        /// Save a commercialDocument item
        /// </summary>
        /// <param name="commercialDocument"></param>
        /// <returns></returns>
        Task<CommercialDocument> SaveCommercialDocument(CommercialDocument commercialDocument);

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
        /// Retrives a list with commercialDocument items meeting search criteria
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="documentTypeId"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<CommercialDocument>> GetCommercialDocumentList(DateTime startDate, DateTime endDate, string documentTypeId, string filter);

        /// <summary>
        /// Retrives a item with commercialDocument items meeting search criteria
        /// </summary>
        /// <param name="commercialDocumentId"></param>      
        /// <returns></returns>
        Task<CommercialDocument> GetItemCommercialDocumentById(string commercialDocumentId);

        /// <summary>
        /// Retrives a item with CommercialDocumentType items
        /// </summary>
        /// <param">No</param>      
        /// <returns></returns>
        Task<List<CommercialDocumentType>> GetCommercialDocumentTypes();


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
        Task<List<Concept>> GetCommercialVendorList(string filter, CommercialDocumentType documentType, int? page = 1, int? perPage = 10);
    }
}
