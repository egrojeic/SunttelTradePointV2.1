using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.ImportingData;
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
        Task<(bool IsSuccess, CommercialDocument? CommercialDocument, string? ErrorDescription)> GetCommercialDocumentById(string userId, string ipAdress, string squadId, string documentId);

        /// <summary>
        /// Insert/ Updates a Transactional Type of the corresponding id
        /// </summary>
        /// <param name="commercialDocument"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, CommercialDocument? commercialDocument, string? ErrorDescription)> SaveCommercialDocument(string userId, string ipAddress, CommercialDocument commercialDocument);

        /// <summary>
        /// Retrieves a list of the documents having the specified type and date span
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="documentTypeId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<CommercialDocument>? CommercialDocuments, string? ErrorDescription)> GetCommercialDocumentsByDateSpan(string userId, string ipAdress, string squadId, DateTime startDate, DateTime endDate, string documentTypeId);

        /// <summary>
        /// Saves an  Business line document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, BusinessLine? entity, string? ErrorDescription)> SaveBusinessLineDoc(string userId, string ipAdress, string squadId, BusinessLine entity);

        /// <summary>
        /// Retrieves the list of Transactional Types that match with the filter condition in the parameter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<CommercialDocumentType>? commercialDocumentTypes, string? ErrorDescription)> GetCommercialDocumentTypes(string userId, string ipAddress, string filterCondition);

        /// <summary>
        /// Retrieves a Transactional Type of the corresponding id
        /// </summary>
        /// <param name="commercialDocumentTypeId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, CommercialDocumentType? commercialDocumentType, string? ErrorDescription)> GetCommercialDocumentTypeById(string userId, string ipAddress, string squadId, string commercialDocumentTypeId);

        /// <summary>
        /// Insert/ Updates a Transactional Type of the corresponding id
        /// </summary>
        /// <param name="commercialDocumentType"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, CommercialDocumentType? commercialDocumentType, string? ErrorDescription)> SaveCommercialDocumentType(string userId, string ipAddress, CommercialDocumentType commercialDocumentType);

        /// <summary>
        /// Retrieves the list of Transactional Types that match with the filter condition in the parameter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<FinanceStatus>? financeStatuses, string? ErrorDescription)> GetFinanceStatuses(string userId, string ipAddress, string filterCondition);

        /// <summary>
        /// Retrieves a Transactional Type of the corresponding id
        /// </summary>
        /// <param name="financeStatusId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, FinanceStatus? financeStatus, string? ErrorDescription)> GetFiananceStatusById(string userId, string ipAddress, string financeStatusId);

        /// <summary>
        /// Insert/ Updates a Transactional Type of the corresponding id
        /// </summary>
        /// <param name="financeStatus"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, FinanceStatus? financeStatus, string? ErrorDescription)> SaveFinanceStatus(string userId, string ipAddress, FinanceStatus financeStatus);

        /// <summary>
        /// Retrieves Entity Groups matching a filter pattern
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="filterString"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<BusinessLine>? businessesLinesDocs, string? ErrorDescription)> GetBusinessLinesDocs(string userId, string ipAdress, string squadId, string? filterString);

        /// <summary>
        /// Retrieves a Business Line Doc by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="businessLineDocId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, BusinessLine? businessLineDoc, string? ErrorDescription)> GetBusinessLineDocById(string userId, string ipAdress, string squadId, string businessLineDocId);

        /// <summary>
        /// Saves an  Shipping Status document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, ShippingStatus? entity, string? ErrorDescription)> SaveShippinStatus(string userId, string ipAdress, string squadId, ShippingStatus entity);

        /// <summary>
        /// Retrieves Shipping statuses matching a filter pattern
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="filterString"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<ShippingStatus>? shippingStatuses, string? ErrorDescription)> GetShippingStatusDocs(string userId, string ipAdress, string squadId, string? filterString);

        /// <summary>
        /// Retrieves a Shipping Status Doc by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="businessLineDocId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, ShippingStatus? shippingStatus, string? ErrorDescription)> GetShippingStatusDocById(string userId, string ipAdress, string squadId, string businessLineDocId);

        /// <summary>
        /// Saves an  Shipping Status document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="salesDocumentItemsDetails"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, SalesDocumentItemsDetails? salesDocumentItemsDetailsResponse, string? ErrorDescription)> SaveCommercialDocumentDetail(string userId, string ipAdress, string squadId, SalesDocumentItemsDetails salesDocumentItemsDetails);

        /// <summary>
        /// Retrievs a list of transactional Items filtered by the parameters name, group, and code
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="nameLike"></param>
        /// <param name="groupName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<SalesDocumentItemsDetails>? GetCommercialDocumentDetails, string? ErrorDescription)> GetCommercialDocumentDetails(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? nameLike = null, string? groupName = null, string? code = null);

        /// <summary>
        /// Retrieves an object of a transactional Item by id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="commercialDocumentDetailId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, SalesDocumentItemsDetails? GetCommercialDocumentDetailsById, string? ErrorDescription)> GetCommercialDocumentDetailById(string userId, string ipAddress, string commercialDocumentDetailId);






    }
}
