using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Client.Interfaces.SalesInterfaces
{
    public interface TSalesDocuments
    {

        /// <summary>
        /// Retrives a list with commercialDocument items meeting search criteria
        /// </summary>
        /// <param name="filterCriteria"></param>
        /// <param name="startingDate"></param>
        /// <param name="endDate"></param>       
        /// <returns></returns>
        Task<List<CommercialDocument>> GetCommercialDocumentList(string filterCriteria, DateTime? startingDate = null, DateTime? endDate = null);

        /// <summary>
        /// Retrives a item with commercialDocument items meeting search criteria
        /// </summary>
        /// <param name="commercialDocumentId"></param>      
        /// <returns></returns>
        Task<CommercialDocument> GetItemCommercialDocumentById(string commercialDocumentId);


    }
}
