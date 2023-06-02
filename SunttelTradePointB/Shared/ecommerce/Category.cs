using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Shared.ecommerce
{
    public class Category : CatalogItem
    {
        public List<TransactItemImage> Images { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public List<string> RelatedKeywords { get; set; }
    }
}
