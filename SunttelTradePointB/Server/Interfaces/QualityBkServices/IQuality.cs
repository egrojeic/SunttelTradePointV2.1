using SunttelTradePointB.Shared.ImportingData;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Quality;

namespace SunttelTradePointB.Server.Interfaces.QualityBkServices
{
    /// <summary>
    /// Interface of service to manage quality
    /// </summary>
    public interface IQuality
    {
        #region Quality Parameters
        /// <summary>
        /// Retrieves a document having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<QualityAssuranceParameter>? QualityParametersList, string? ErrorDescription)> GetQualityParameters(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filterName = null);

        /// <summary>
        /// Retrieves a inventory having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, QualityAssuranceParameter? QualityParameter, string? ErrorDescription)> GetQualityParameteById(string userId, string ipAddress, string squadId, string qualityId);

        /// <summary>
        /// Insert/ Updates a Inventory Type of the corresponding id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, QualityAssuranceParameter? QualityParameter, string? ErrorDescription)> SaveQualityParameter(string userId, string ipAddress, string squadId, QualityAssuranceParameter quality);


        /// <summary>
        /// Delete an QualityAssuranceParameter not associated with QualityAssurance
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityParameterGroupId"></param>       
        /// <returns></returns>
        Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteQualityAssuranceParameterById(string userId, string ipAddress, string squadId, string? qualityParameterGroupId);



        #endregion


        #region Quality Groups

        /// <summary>
        /// Retrieves a document having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<QualityParameterGroup>? QualityGroupsList, string? ErrorDescription)> GetQualityParametersGroups(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves a inventory having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, QualityParameterGroup? QualityGroup, string? ErrorDescription)> GetQualityParameteGroupsById(string userId, string ipAddress, string squadId, string qualityId);

        /// <summary>
        /// Insert/ Updates a Inventory Type of the corresponding id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, QualityParameterGroup? QualityGroup, string? ErrorDescription)> SaveQualityParameterGroups(string userId, string ipAddress, string squadId, QualityParameterGroup quality);
     
        
        /// <summary>
        /// Delete an QualityParameterGroup not associated with Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityParameterGroupId"></param>       
        /// <returns></returns>
        Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteQualityParameterGroupById(string userId, string ipAddress, string squadId, string? qualityParameterGroupId);
        
        #endregion

        #region Quality Traffic Light
        /// <summary>
        /// Retrieves a document having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<QualityTrafficLight>? QualityTrafficLightsList, string? ErrorDescription)> GetQualityTrafficLights(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves a inventory having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, QualityTrafficLight? QualityTrafficLight, string? ErrorDescription)> GetQualityTrafficLightById(string userId, string ipAddress, string squadId, string qualityId);

        /// <summary>
        /// Insert/ Updates a Inventory Type of the corresponding id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, QualityTrafficLight? QualityTrafficLight, string? ErrorDescription)> SaveQualityTrafficLight(string userId, string ipAddress, string squadId, QualityTrafficLight quality);

        
        /// <summary>
        /// Delete an QualityTrafficLight not associated with Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityTrafficLightId"></param>       
        /// <returns></returns>
        Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteQualityTrafficLightById(string userId, string ipAddress, string squadId, string? qualityTrafficLightId);

        #endregion

        #region Quality Action
        /// <summary>
        /// Retrieves a document having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<QualityAction>? QualityActionsList, string? ErrorDescription)> GetQualityActions(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves a inventory having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, QualityAction? QualityAction, string? ErrorDescription)> GetQualityActionById(string userId, string ipAddress, string squadId, string qualityId);

        /// <summary>
        /// Insert/ Updates a Inventory Type of the corresponding id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, QualityAction? QualityAction, string? ErrorDescription)> SaveQualityAction(string userId, string ipAddress, string squadId, QualityAction quality);
      
        
        /// <summary>
        /// Delete an QualityAction not associated with Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityActioId"></param>       
        /// <returns></returns>
        Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteQualityActionById(string userId, string ipAddress, string squadId, string? qualityActioId);
        
        #endregion

        #region Quality Type
        /// <summary>
        /// Returns a list of quality report types with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<QualityReportType>? GetQualityReportTypesList, string? ErrorDescription)> GetQualityReportTypes(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves an quality report type object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, QualityReportType? QualityReportType, string? ErrorDescription)> GetQualityReportTypeById(string userId, string ipAddress, string squadId, string qualityId);

        /// <summary>
        /// Saves an quality report type. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, QualityReportType? QualityReportType, string? ErrorDescription)> SaveQualityReportType(string userId, string ipAddress, string squadId, QualityReportType quality);

        /// <summary>
        /// Delete an QualityReportType not associated with Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityReportTypeId"></param>       
        /// <returns></returns>
        Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteQualityReportTypeById(string userId, string ipAddress, string squadId, string? qualityReportTypeId);



        #endregion

        #region QCDocument
        /// <summary>
        /// Returns a list of quality documents with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="qualityReportTypeName"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, List<QualityEvaluation>? GetQCDocumentsList, string? ErrorDescription)> GetQCDocuments(string userId, string ipAddress, string squadId, DateTime startDate, DateTime endDate, string? qualityReportTypeName, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves an quality report type object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, QualityEvaluation? QCDocument, string? ErrorDescription)> GetQCDocumentById(string userId, string ipAddress, string squadId, string qualityId);

        /// <summary>
        /// Saves an quality report type. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, QualityEvaluation? QCDocument, string? ErrorDescription)> SaveQCDocument(string userId, string ipAddress, string squadId, QualityEvaluation quality);
        #endregion
    }
}
