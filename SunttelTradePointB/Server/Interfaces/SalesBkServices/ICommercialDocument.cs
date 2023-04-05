using SunttelTradePointB.Shared.Common;
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
        /// <returns></returns>
        Task<(bool IsSuccess, CommercialDocumentType? commercialDocumentType, string? ErrorDescription)> GetCommercialDocumentTypeById(string userId, string ipAddress, string commercialDocumentTypeId);

        /// <summary>
        /// Insert/ Updates a Transactional Type of the corresponding id
        /// </summary>
        /// <param name="commercialDocumentType"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, CommercialDocumentType? commercialDocumentType, string? ErrorDescription)> SaveCommercialDocumentType(string userId, string ipAddress, CommercialDocumentType commercialDocumentType);




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
    }
}
