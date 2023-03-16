using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunttelTradePointB.Shared.Common;
using System.ComponentModel;
using Newtonsoft.Json;

namespace SunttelTradePointB.Shared.Sales
{

    public class CommercialDocumentType : BasicConcept
    {

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DisplayName("Affect Inventory")]
        public bool AffectInventory { get; set; }

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DisplayName("Affect Finance")]
        public bool AffectFinance { get; set; }

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DisplayName("Check Prices Needed")]
        public bool NeedsCheckPrices { get; set; }

        [BsonIgnoreIfNull]
        public List<EntityType> BuyerTypes { get; set; }

        [BsonIgnoreIfNull]
        public List<EntityType> VendorTypes { get; set; }
    }

    public class ShippingStatus : BasicConcept
    {
        [DisplayName("Affect Inventory")]
        public bool AffectInventory { get; set; }

        [DisplayName("Editing Allowed")]
        public bool EditingAllowed { get; set; }
    }

    public class BusinessLine : BasicConcept
    {
        
    }

    public class ShippingType : BasicConcept
    {

    }

    public class FinanceStatus : BasicConcept
    {

        [DisplayName("Affect Finance")]
        public bool AffectFinance { get; set; }
        [DisplayName("Editing Allowed")]
        public bool EditingAllowed { get; set; }
    }

    public class CommercialDocument: RecordItem
    {

        public CommercialDocumentType DocumentType { get; set; }
        
        [DisplayName("Business Line")]
        public BusinessLine BusinessLineDoc { get; set; }
        public string PO { get; set; }
        [DisplayName("Ship Date")]
        public DateTime ShipDate { get; set; }
        [DisplayName("Delivery Date")]
        public DateTime DeliveryDate { get; set; }
       
        [DisplayName("Provider Arrival Date")]
        public DateTime ArrivalDate { get; set; }
        public Concept Vendor { get; set; }
        public Concept Buyer { get; set; }
        [DisplayName("Sales Person")]
        public Concept SalesPerson { get; set; }
        public Concept Carrier { get; set; }

        [DisplayName("Delivery Address")]
        public Address DeliveryAddress { get; set; }

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


        [DisplayName("To Invoice")]
        public bool ToInvoice { get; set; }

    }

    public class SalesDocumentPrintingSatus {

        [DisplayName("Printed")]
        public bool DocumentPrinted { get; set; }

        [DisplayName("BOL Printed")]
        public bool BillOfLandingPrinted { get; set; }

        [DisplayName("PT Printed")]
        public bool PickingTicketPrinted { get; set; }

        [DisplayName("Labels Printed")]
        public bool LabelsPrinted { get; set; }

        [DisplayName("Selected to Print")]
        public bool SelectedToPrint { get; set; }

    }

    public class FinanceSalesDocumentSummary {

        [DisplayName("Total Amount")]
        public double TotalAmount { get; set; }

        [DisplayName("Total Costs")]
        public double TotalCosts { get; set; }
        
        [DisplayName("Total Taxes")]
        public double TotalTaxes { get; set; }

        [DisplayName("Total Discounts")]
        public double TotalDiscounts { get; set; }


    }

    public enum ProductSources { 
        Inventory = 0,
        Production = 2,
        PurchaseOrder = 3
    }
    public class SalesDocumentItemsDetails: RecordItem
    {
        [DisplayName("Item")]
        public Concept TransactionalItem { get; set; }

        [DisplayName("Item Specs")]
        public PackingSpecs TransactionalItemSpecs { get; set; }

        [DisplayName("Vendor Box")]
        public Box VendorBox { get; set; }
        
        [DisplayName("Vendor Pack")]
        public double VendorPack { get; set; }

        [DisplayName("Boxes Qty")]
        public double BoxesQty { get; set; }

        [DisplayName("Chargeable Qty")]
        public double ChargeableQty { get; set; }

        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }

        [DisplayName("Taxable Unit Price")]
        public double TaxableUnitPrice { get; set; }

        [DisplayName("Inventory Reference")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InventoryItemId { get; set; }

        [DisplayName("Unit Cost")]
        public double UnitCost { get; set; }

        [DisplayName("Sale Tax Rate")]
        public double TaxRate { get; set; }

        [DisplayName("Tax")]
        public virtual double TaxValue => ChargeableQty * TaxRate * TaxableUnitPrice;

        public virtual double Total => ChargeableQty * UnitPrice;

        public ProductSources ProductSource { get; set; }

        [BsonIgnoreIfNull]
        [DisplayName("Provider")]
        public BasicConcept Provider { get; set; }

        [DisplayName("Provider Box")]
        public Box ProviderBox { get; set; }

        [DisplayName("Provider Box Layers")]
        public double ProviderBoxLayers { get; set; }

        [DisplayName("Provider Pack")]
        public double ProviderPack { get; set; }

        [DisplayName("Provider Boxes")]
        public int ProviderBoxes { get; set; }

        [BsonIgnoreIfNull]
        [DisplayName("Purchases Specs")]
        public List<PurchaseItemDetails> PurchaseSpecs { get; set; }


        [DisplayName("Cancel Info")]
        public CancelationTrack  CancelationInfo { get; set; }


        [DisplayName("Original Price")]
        public double OriginalPrice { get; set; }

        [DisplayName("Boxes Scanned")]
        public virtual int ScannedBoxes { get; set; }

        [DisplayName("Procurement %")]
        public virtual bool ProcurementPtje { get; set; }


        [BsonIgnoreIfNull]
        [DisplayName("Pull Date")]
        public DateTime PullDate { get; set; }

        [BsonIgnoreIfNull]
        [DisplayName("Pull Date Formated")]
        public string FormatedPullDate { get; set; }
    }

    public class CancelationTrack
    {

        [DisplayName("Cancel Request")]
        public bool CancelationRequest { get; set; } = false;

        [DisplayName("Cancel Request Answered")]
        public bool CancelRequestAnswered { get; set; } = false;

        [DisplayName("Cancel Aproved")]
        public bool CancelAproved { get; set; } = false;

    }


    public class PurchaseItemDetails: RecordItem
    {

    }
}
