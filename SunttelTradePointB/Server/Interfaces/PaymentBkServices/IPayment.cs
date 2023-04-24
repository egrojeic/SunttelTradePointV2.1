using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.InvetoryModels;

namespace SunttelTradePointB.Server.Interfaces.PaymentBkServices
{
    /// <summary>
    /// Interface of service to manage payment
    /// </summary>
    public interface IPayment
    {
        #region Payment
        /// <summary>
        /// Retrieves a document having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="PaymentDate"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<Payment>? PaymentList, string? ErrorDescription)> GetPaymentsByDateSpan(string userId, string ipAddress, string squadId, string PaymentDate, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves a inventory having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, Payment? Payment, string? ErrorDescription)> GetPaymentById(string userId, string ipAddress, string squadId, string paymentId);

        /// <summary>
        /// Insert/ Updates a Inventory Type of the corresponding id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, Payment? Payment, string? ErrorDescription)> SavePayment(string userId, string ipAddress, string squadId, Payment payment);
        #endregion

        #region PaymentModes
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
        Task<(bool IsSuccess, List<PaymentMode>? PaymentModesList, string? ErrorDescription)> GetDocPaymentModes(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves a payment mode having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="paymentModeId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, PaymentMode? PaymentMode, string? ErrorDescription)> GetDocPaymentModeById(string userId, string ipAddress, string squadId, string paymentModeId);

        /// <summary>
        /// Insert/ Updates a Inventory Type of the corresponding id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="paymentMode"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, PaymentMode? PaymentMode, string? ErrorDescription)> SaveDocPaymentMode(string userId, string ipAddress, string squadId, PaymentMode paymentMode);




        #endregion

        #region PaymentVia
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
        Task<(bool IsSuccess, List<PaymentVia>? PaymentViaList, string? ErrorDescription)> GetDocPaymentVias(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrieves a payment mode having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="paymentViaId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, PaymentVia? PaymentVia, string? ErrorDescription)> GetDocPaymentViaById(string userId, string ipAddress, string squadId, string paymentViaId);

        /// <summary>
        /// Insert/ Updates a Inventory Type of the corresponding id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="paymentVia"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, PaymentVia? PaymentVia, string? ErrorDescription)> SaveDocPaymentVia(string userId, string ipAddress, string squadId, PaymentVia paymentVia);


        #endregion
    }
}
