using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using SunttelTradePointB.Shared.Common;
using System.ComponentModel;

namespace SunttelTradePointB.Shared.Sales.CommercialDocument
{
    public class CommercialDocument : RecordItem
    {
        public bool sysLoading { get; set; } =false;
        public bool sysSaving { get; set; } = false;


        [DisplayName("Document Type")]
        public CommercialDocumentType DocumentType { get; set; }

        [DisplayName("Order No")]
        public int DocumentNumber { get; set; }

        [DisplayName("Business Line")]
        public BusinessLine BusinessLineDoc { get; set; }
        public string PO { get; set; }
        [DisplayName("Ship Date")]
        public DateTime ShipDate { get; set; }
        [DisplayName("Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        [DisplayName("Arrival Date")]
        public DateTime ArrivalDate { get; set; }
        public Concept Vendor { get; set; }
        public Concept Buyer { get; set; }
        [DisplayName("Sales Person")]
        public Concept SalesPerson { get; set; }
        public Concept Carrier { get; set; }

        [DisplayName("Shipping Setup")]
        public ShippingInfo ShippingSetup { get; set; }

        [DisplayName("Shipping Status")]
        public ShippingStatus ShippingStatusDocument { get; set; }

        [DisplayName("Tracking Number")]
        public string TrackingNumber { get; set; }

        [DisplayName("Finance Status")]
        public FinanceStatus FinanceStatusDocument { get; set; }

        [DisplayName("Origin Document")]
        public CommercialDocument OriginDocument { get; set; }

        [DisplayName("Vendor Warehouse")]
        public BasicConcept VendorWarehouse { get; set; }

        [DisplayName("Buyer Warehouse")]
        public BasicConcept BuyerWarehouse { get; set; }

        public SeasonBusiness Season { get; set; }

        [DisplayName("Printing Satus")]
        public SalesDocumentPrintingSatus PrintingSatus { get; set; }

        [DisplayName("Specs Notes")]
        public string SpecsNotes { get; set; }

        [DisplayName("Marked for QCInspection")]
        public bool MarkedForQCInspection { get; set; }

        public FinanceSalesDocumentSummary FinanceSummary { get; set; }

        [DisplayName("Items")]
        public List<SalesDocumentItemsDetails> Items { get; set; }

        [DisplayName("Shipping Summary")]
        public ShippingSalesDocumentSummary ShippingSummary { get; set; }

        [DisplayName("To Invoice")]
        public bool ToInvoice { get; set; }

        public string StandingOrderId { get; set; }

        public bool ItemsAdded { get; set; }
        public string? deliveryDateError { get; set; }

        public CommercialDocument()
        {

        }
        public CommercialDocument(Concept vendor) {
            this.Vendor = vendor;
        }
        public void ValidateItems()
        {
            ItemsAdded = Items.Any();
        }
      
    }

    public class CommercialDocumentIdentifier
    {
        public int DocumentNumber { get; set; }
    }

}
