using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.InvetoryModels;

namespace SunttelTradePointB.Server.Interfaces.CreditBkServices
{
    /// <summary>
    /// Interface of service to manage credit
    /// </summary>
    public interface ICredit
    {
        /// <summary>
        /// Returns a list of faces of credit documents
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<CreditDocument>? CreditDocumentsList, string? ErrorDescription)> GetCreditDocuments(string userId, string ipAddress, string squadId, DateTime startDate, DateTime endDate, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves a credit comercial having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditDocumentId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, CreditDocument? CreditDocument, string? ErrorDescription)> GetCreditDocumentById(string userId, string ipAddress, string squadId, string creditDocumentId);

        /// <summary>
        /// Insert/ Updates a Credit Document 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditDocument"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, CreditDocument? CreditDocument, string? ErrorDescription)> SaveCreditDocument(string userId, string ipAddress, string squadId, CreditDocument creditDocument);

    }
}
