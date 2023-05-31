using SunttelTradePointB.Shared.Common;

namespace SunttelTPointReporPdf.Interfaces.TransactionalReport
{
    public interface ITransactionalItem
    {
        Task<(bool IsSuccess, TransactionalItem? transactionalItem, string? ErrorDescription)> GetTransactionalItem(string commercialDocumentId);
    }
}
