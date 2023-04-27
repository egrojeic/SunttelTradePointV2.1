using SunttelTradePointB.Shared.Quality;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Server.Interfaces.StandingOrderBkServices
{
    /// <summary>
    /// Interface of service to manage standing 
    /// </summary>
    public interface IStandingOrder
    {
        #region Standing Order
        /// <summary>
        /// Retrieves a stading order having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<StandingOrder>? StandingOrdersList, string? ErrorDescription)> GetStandingOrders(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves a stading order having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="standingOrderId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, StandingOrder? StandingOrder, string? ErrorDescription)> GetStandingOrderById(string userId, string ipAddress, string squadId, string standingOrderId);

        /// <summary>
        /// Insert/ Updates a stading order Type of the corresponding id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="standing"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, StandingOrder? StandingOrder, string? ErrorDescription)> SaveStandingOrder(string userId, string ipAddress, string squadId, StandingOrder standing);

        #endregion

        #region Standing Order Detail
        /// <summary>
        /// Retrieves a stading order having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<StandingOrderDetails>? StandingOrdersDetailsList, string? ErrorDescription)> GetStandingOrdersDetails(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves a stading order having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="standingOrderDetailId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, StandingOrderDetails? StandingOrderDetail, string? ErrorDescription)> GetStandingOrderDetailById(string userId, string ipAddress, string squadId, string standingOrderDetailId);

        /// <summary>
        /// Insert/ Updates a stading order Type of the corresponding id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="standing"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, StandingOrderDetails? StandingOrderDetail, string? ErrorDescription)> SaveStandingOrderDetail(string userId, string ipAddress, string squadId, StandingOrderDetails standing);

        #endregion
    }
}
