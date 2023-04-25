using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.InvetoryModels;

namespace SunttelTradePointB.Server.Interfaces.CreditBkServices
{
    /// <summary>
    /// Interface of service to manage credit
    /// </summary>
    public interface ICredit
    {
        #region Credit Documents
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
        #endregion

        #region Credit Types
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
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<CreditType>? CreditTypesList, string? ErrorDescription)> GetCreditTypes(string userId, string ipAddress, string squadId, DateTime startDate, DateTime endDate, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves a credit comercial having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditTypeId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, CreditType? CreditType, string? ErrorDescription)> GetCreditTypeById(string userId, string ipAddress, string squadId, string creditTypeId);

        /// <summary>
        /// Insert/ Updates a Credit Document 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditType"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, CreditType? CreditType, string? ErrorDescription)> SaveCreditType(string userId, string ipAddress, string squadId, CreditType creditType);
        #endregion

        #region Credit Statuses
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
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<CreditStatus>? CreditStatusesList, string? ErrorDescription)> GetCreditStatuses(string userId, string ipAddress, string squadId, DateTime startDate, DateTime endDate, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves a credit comercial having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditStatusId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, CreditStatus? CreditStatus, string? ErrorDescription)> CreditStatusById(string userId, string ipAddress, string squadId, string creditStatusId);

        /// <summary>
        /// Insert/ Updates a Credit Document 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditDocument"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, CreditStatus? CreditStatus, string? ErrorDescription)> SaveCreditStatus(string userId, string ipAddress, string squadId, CreditStatus creditStatus);
        #endregion

        #region Credit Reasons
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
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<CreditReason>? CreditReasonsList, string? ErrorDescription)> GetCreditReasons(string userId, string ipAddress, string squadId, DateTime startDate, DateTime endDate, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves a credit comercial having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditDocumentId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, CreditReason? CreditReason, string? ErrorDescription)> CreditReasonById(string userId, string ipAddress, string squadId, string creditDocumentId);

        /// <summary>
        /// Insert/ Updates a Credit Document 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditReason"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, CreditReason? CreditReason, string? ErrorDescription)> SaveCreditReason(string userId, string ipAddress, string squadId, CreditReason creditReason);
        #endregion

    }
}
