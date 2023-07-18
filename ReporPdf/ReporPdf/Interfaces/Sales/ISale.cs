using SunttelTradePointB.Shared.Sales;
using SunttelTradePointB.Shared.Sales.CommercialDocument;

namespace SunttelTPointReporPdf.Interfaces.Sale
{
    public interface ISale
    {
        Task<(bool IsSuccess, CommercialDocument? CommercialDocument, string? ErrorDescription)> GetCommercialDocument(string commercialDocumentId);

        Task<(bool IsSuccess, List<SalesDocumentItemsDetails>? GetCommercialDocumentDetails, string? ErrorDescription)> GetCommercialDocumentDetails( string commercialDocumentId);

    }
    
}
