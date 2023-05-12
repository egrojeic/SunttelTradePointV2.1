using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Client.Interfaces.IPaymentInterfaces
{
    public interface IPayment
    {

        /// <summary>
        /// Save a Payment item 
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>    
        Task<Payment> SavePaymentItem(Payment payment);


        /// <summary>
        /// Save a paymentType item 
        /// </summary>
        /// <param name="paymentType"></param>
        /// <returns></returns>    
        Task<PaymentType> SavePaymentType(PaymentType paymentType);

        /// <summary>
        /// Save a PaymentStatus item
        /// </summary>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>    
        Task<PaymentStatus> SavePaymentStatus(PaymentStatus paymentStatus);

        /// <summary>
        /// Save a paymentMode item 
        /// </summary>
        /// <param name="paymentMode"></param>
        /// <returns></returns>    
        Task<PaymentMode> SavePaymentMode(PaymentMode paymentMode);


        /// <summary>
        /// Save a appliedPayment item 
        /// </summary>
        /// <param name="appliedPayment"></param>
        /// <returns></returns>    
        Task<AppliedPayment> SaveAppliedPayment(AppliedPayment appliedPayment);

        /// <summary>
        /// Save a paymentWithOverpayment item 
        /// </summary>
        /// <param name="paymentWithOverpayment"></param>
        /// <returns></returns>    
        Task<PaymentWithOverpayment> SavepaymentWithOverpayment(PaymentWithOverpayment paymentWithOverpayment);

        /// <summary>
        /// Save a paymentWithOverplus item 
        /// </summary>
        /// <param name="paymentWithOverplus"></param>
        /// <returns></returns>    
        Task<PaymentWithOverplus> SavePaymentWithOverplus(PaymentWithOverplus paymentWithOverplus);


        /// <summary>
        /// Save a pymentWithCredit item 
        /// </summary>
        /// <param name="pymentWithCredit"></param>
        /// <returns></returns>   
        Task<PaymentWithCredit> SavPymentWithCredit(PaymentWithCredit pymentWithCredit);


        /// <summary>
        /// Save a paymentVia item 
        /// </summary>
        /// <param name="paymentVia"></param>
        /// <returns></returns>   
        Task<PaymentVia> SavePaymentVia(PaymentVia paymentVia);

        /// <summary>
        /// Retrives a with paymentMode item meeting search criteria
        /// </summary>
        /// <param name="paymentViaId"></param>
        /// <returns></returns>   
        Task<List<PaymentMode>> GetPaymentViaById(string paymentViaId);

        /// <summary>
        /// Retrives a with Payment item meeting search criteria
        /// </summary>
        /// <param name="paymentTypeId"></param>
        /// <returns></returns>   
        Task<Payment> GetPaymentById(string paymentId);


        /// <summary>
        /// Retrives a list with concept payer items meeting search criteria
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="isASale"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        Task<List<Concept>> GetPayerList(string filter, bool isASale, int? page = 1, int? perPage = 10);



        /// <summary>
        /// Retrives a list with concept receiver items meeting search criteria
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="isASale"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        Task<List<Concept>> GetReceiverList(string filter, bool isASale, int? page = 1, int? perPage = 10);

        /// <summary>
        /// Retrives a list with Payment items meeting search criteria
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="documentTypeId"></param>
        /// <param name="date"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        Task<List<Payment>> GetPaymentList(string filterName, string documentTypeId, DateTime startDate,DateTime endDate, int? page = 1, int? perPage = 10);


        /// <summary>
        /// Retrives a list with paymentVia items meeting search criteria
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="documentTypeId"></param>
        /// <param name="date"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        Task<List<PaymentVia>> GetPaymentViasList(int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrives a item with PaymentType  meeting search criteria
        /// </summary>
        /// <param name="paymentModeById"></param>       
        /// <returns></returns>
        Task<List<PaymentType>> GetPaymentModeById(string paymentModeById);



        /// <summary>
        /// Retrives a item with paymentStatus  meeting search criteria
        /// </summary>
        /// <param name="paymentTypeId"></param>       
        /// <returns></returns>
        Task<PaymentStatus> GetPaymentStatusById(string paymentTypeId);



        /// <summary>
        /// Retrives a item with paymentType meeting search criteria
        /// </summary>
        /// <param name="page"></param>       
        /// <param name="perPage"></param>       
        /// <param name="filter"></param>       
        /// <returns></returns>
        Task<List<PaymentType>> GetPaymentTypeList(int? page = 1, int? perPage = 10, string? filter = null);


        /// <summary>
        /// Retrives a item with paymentMode meeting search criteria
        /// </summary>
        /// <param name="page"></param>       
        /// <param name="perPage"></param>       
        /// <param name="filter"></param>       
        /// <returns></returns>
        Task<List<PaymentMode>> GetPaymentModesList(int? page = 1, int? perPage = 10, string? filter = null);

        /// <summary>
        /// Retrives a item with paymentStatus meeting search criteria
        /// </summary>
        /// <param name="page"></param>       
        /// <param name="perPage"></param>       
        /// <param name="filter"></param>       
        /// <returns></returns>
        Task<List<PaymentStatus>> GetPaymentStatusList(int? page = 1, int? perPage = 10, string? filter = null);

    }
}
