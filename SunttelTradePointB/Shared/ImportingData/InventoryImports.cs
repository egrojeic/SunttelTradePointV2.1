using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SunttelTradePointB.Shared.ImportingData
{
    public class InventoryImports
    {
        public string InventoryObjectId { get;set;}
        public string Warehouse_Id {get;set;}
        public string Warehouse_Name {get;set;}
        public string Item_Id {get;set;}
        public string Item_LegacyId {get;set;}
        public string Item_Name {get;set;}
        public string Item_Class {get;set;}
        public double Qty {get;set;}
        public double QtyAvailable {get;set;}
        public double EntryScannedQty {get;set;}
        public DateTime EntryDate {get;set;}
        public string Box_Id {get;set;}
        public string Bx_Name {get;set;}
        public int FlagWet {get;set;}
        public double Largo {get;set;}
        public double Ancho {get;set;}
        public double Alto {get;set;}
        public double PalletEquivalent {get;set;}
        public double Pack {get;set;}
        public string BoxCode {get;set;}
        public double UnitCost {get;set;}
        public double BasicUnitsQty {get;set;}
        public string CustomerReservedFor_Id {get;set;}
        public string CustomerReservedFor_Name {get;set;}
        public string BusinessLine_Id {get;set;}
        public string BusinessLine_Name {get;set;}
        public string OriginDesc_Id {get;set;}
        public string OriginDesc_Name {get;set;}
        public string OriginDesc_TarnsportationDocumentId {get;set;}
        public string OriginDesc_TarnsportationDocumentDetailId {get;set;}
        public string OriginDesc_TarnsportationDocumentCode { get; set; }

        public string LegacyId { get; set; }

    }
}
