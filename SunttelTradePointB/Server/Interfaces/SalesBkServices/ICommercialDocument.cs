using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Server.Interfaces.SalesBkServices
{

    /// <summary>
    /// Interface of service to manage commercial documents
    /// </summary>
    public interface ICommercialDocument
    {
        
        /// <summary>
        /// Retrieves a document having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        Task<CommercialDocument> GetCommercialDocumentById(string userId, string ipAdress, string squadId, string documentId);


        Task<List<CommercialDocument>> GetCommercialDocumentsByDateSpan(string userId, string ipAdress, string squadId, DateTime startDate, DateTime endDate);

    }
}
