using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.InvetoryModels
{

    public class InventoryDetail: RecordItem
    {

        [DisplayName("Warehouse")]
        public Warehouse CurrentWarehouse { get; set; }

        [DisplayName("Item")]
        public Concept InventoryItem { get; set; }

        [DisplayName("Qty")]
        public double Qty { get; set; }

        [DisplayName("Available")]
        public double QtyAvailable { get; set; }

        [DisplayName("Reserved")]
        public double QtyReserved { get; set; }

        [DisplayName("On Hand")]
        public double QtyOnHand { get; set; }

        [DisplayName("On Order")]
        public double QtyOnOrder { get; set; }

        [DisplayName("On Purchase Order")]
        public double QtyOnPurchaseOrder { get; set; }

        [DisplayName("On Sales Order")]
        public double QtyOnSalesOrder { get; set; }

        [DisplayName("On Transfer Order")]
        public double QtyOnTransferOrder { get; set; }

        [DisplayName("On Production Order")]
        public double QtyOnProductionOrder { get; set; }

        [DisplayName("Scanned")]
        public double EntryScannedQty { get; set; }

        [DisplayName("Entry Date time")]
        public DateTime EntryDate { get; set; }

        [DisplayName("Box")]
        public Box ItemBox { get; set; }

        [DisplayName("Pack")]
        public double Pack { get; set; }

        [DisplayName("Box Code")]
        public string BoxCode { get; set; }

        [DisplayName("Customer")]
        public BasicConcept CustomerReservedFor { get; set; }

        [DisplayName("Unit Cost")]
        public double UnitCost { get; set; }

        [DisplayName("Basic Units")]
        public double BasicUnitsQty { get; set; }

        public BusinessLine InventoryBusinessLine { get; set; }

        public InventoryOriginDescription OriginDesc{ get; set; }
    }

    public class InventoryOriginDescription
    {
        public string ProviderId { get; set; }
        public string ProviderName { get; set; }

        public string TarnsportationDocumentId { get; set; }
        public string TarnsportationDocumentDetailId { get; set; }
        public string TarnsportationDocumentCode { get; set; }
        
    }
}
