using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Quality;

namespace SunttelTradePointB.Server.Interfaces.QualityBkServices
{
    /// <summary>
    /// Interface of service to manage quality
    /// </summary>
    public interface IQuality
    {
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

    }
}
