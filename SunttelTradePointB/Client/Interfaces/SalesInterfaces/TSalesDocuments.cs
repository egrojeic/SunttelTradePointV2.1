using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Client.Interfaces.SalesInterfaces
{
    public interface TSalesDocuments
    {

        /// <summary>
        /// Retrives a list with commercialDocument items meeting search criteria
        /// </summary>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        Task<List<CommercialDocument>> GetCommercialDocumentList(DateTime startDate, DateTime endDate, string documentTypeId);

        /// <summary>
        /// Retrives a item with commercialDocument items meeting search criteria
        /// </summary>
        /// <param name="commercialDocumentId"></param>      
        /// <returns></returns>
        Task<CommercialDocument> GetItemCommercialDocumentById(string commercialDocumentId);


    }
}
