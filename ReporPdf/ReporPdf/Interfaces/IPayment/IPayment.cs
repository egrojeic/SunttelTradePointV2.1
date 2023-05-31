using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Accounting;

namespace SunttelTPointReporPdf.Interfaces.IPayment
{
    public interface IPayment
    {
        Task<(bool IsSuccess, Payment? transactionalItem, string? ErrorDescription)> GetPayment(string paymentId);
    }
}
