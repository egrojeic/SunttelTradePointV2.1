using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Shared.ecommerce
{
    internal class TemporalSale : RecordItem
    {
        public EntityActor Vendor { get; set; }
        public EntityActor Buyer { get; set; }

        public List<Product> Product { get; set; }

        public double TotalPrice { get; set; }

        public double TotalTaxes { get; set; }

        public double TotalQuantity { get; set; }

    }
}
