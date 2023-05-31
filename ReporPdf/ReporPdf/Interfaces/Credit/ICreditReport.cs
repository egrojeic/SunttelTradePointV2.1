using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTPointReporPdf.Interfaces.CreditReport
{
    public interface ICreditDocument
    {
        Task<(bool IsSuccess, CreditDocument? transactionalItem, string? ErrorDescription)> GetCreditDocument(string creditId);

        Task<(bool IsSuccess, List<SalesDocumentItemsDetails>? GetCommercialDocumentDetails, string? ErrorDescription)> GetCommercialDocumentDetails(string commercialDocumentId);
    }
}
