using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SunttelTradePointB.Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Shared.Sales
{
    public class StandingOrder: RecordItem
    {
        [DisplayName("Document Type")]
        public CommercialDocumentType DocumentType { get; set; }

        [DisplayName("Order No")]
        public int DocumentNumber { get; set; }

        [DisplayName("Business Line")]
        public BusinessLine BusinessLineDoc { get; set; }
        public string PO { get; set; }

        [DisplayName("Starting Ship Date")]
        public DateTime StartingShipDate { get; set; }

        [DisplayName("Final Date")]
        public DateTime FinalShipDate { get; set; }

        [DisplayName("End not defined")]
        public bool IsEndUndefined { get; set; }

        public SeasonBusiness Season { get; set; }
        public Concept Vendor { get; set; }
        public Concept Buyer { get; set; }

        [DisplayName("Sales Person")]
        public Concept SalesPerson { get; set; }

        [DisplayName("Frequency (Days)")]
        public int FrequencyInDays { get; set; }

        [DisplayName("Delivery Address")]
        public Address DeliveryAddress { get; set; }

        public TransportationMode TransportationMode { get; set; }

    }


    public class StandingOrderDetails: RecordItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [DisplayName("Document")]
        public string StandingOrderId { get; set; }

        [DisplayName("Item")]
        public Concept TransactionalItem { get; set; }

        [DisplayName("Item Specs")]
        public PackingSpecs TransactionalItemSpecs { get; set; }


        [DisplayName("Qty")]
        public double Qty { get; set; }

        [DisplayName("Chargeable Qty")]
        public double ChargeableQty { get; set; }

        [DisplayName("Chargeable Units")]
        public ChargeableUnits ChargeableUnits { get; set; }

        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }

        [DisplayName("Taxable Unit Price")]
        public double TaxableUnitPrice { get; set; }

        [DisplayName("Expected Cost")]
        public double ExpectedCost { get; set; }

        [BsonIgnoreIfNull]
        [DisplayName("Provider")]
        public BasicConcept Provider { get; set; }

        [DisplayName("Provider Confirmed Qty")]
        public int ProviderConfirmedQty { get; set; }

        [DisplayName("Starting Provider Ship Date")]
        public DateTime StartingProviderShipDate { get; set; }

        [DisplayName("Confirmed Cost")]
        public int ConfirmedCost { get; set; }

        public CancelationTrack CancelationInfo { get; set; }


    }
}
