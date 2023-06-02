using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Shared.ecommerce
{
    public class EcommerceOrder : RecordItem
    {
        public EntityActor Carrier { get; set; }
        public EntityActor Buyer { get; set; }
        public Address InvoicingAddress { get; set; }
        public Address ShippingAddress { get; set; }

        public DateTime? InvoicingDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string CarrierStatus { get; set; }
        public string VendorStatus { get; set; }
        public string CommercialDocumentId { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double Taxes { get; set; }
        public string TrackingNumber { get; set; }

    }
}
