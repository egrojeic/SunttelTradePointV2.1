using SunttelTradePointB.Shared.Sales;
using SunttelTradePointB.Shared.Accounting;

namespace SunttelTPointReporPdf.Model
{
    public class ModelCreditSale
    {
        public List<SalesDocumentItemsDetails> SaleDetail { get; set; }
        public CreditDocument CreditDocument { get; set; }
    }
}
