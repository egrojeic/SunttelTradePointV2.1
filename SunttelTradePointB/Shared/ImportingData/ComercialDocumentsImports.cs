using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.ImportingData
{
    public class ComercialDocumentsImports
    {

        public string SquadId {get;set;}
        public string LegacyObjectId {get;set;}
        public string LegacyId {get;set;}
        public string IDOrderTypes {get;set;}
        public string CommercialDocumentType_LegacyId {get;set;}
        public string CommercialDocumentType_LegacyObjectId {get;set;}
        public string CommercialDocumentType_Name {get;set;}
        public string CommercialDocumentType_Description {get;set;}
        public string BusinessLineDocId {get;set;}
        public string BusinessLineDocName {get;set;}
        public string PO {get;set;}
        public DateTime ShipDate {get;set;}
        public DateTime DeliveryDate {get;set;}
        public DateTime ArrivalDate {get;set;}
        public string Vendor_ObjectId {get;set;}
        public string Vendor_Name {get;set;}
        public string Buyer_ObjectId {get;set;}
        public string Buyer_Name {get;set;}
        public string Buyer_Groups__id {get;set;}
        public string Groups_Name {get;set;}
        public string Carrier_ObjectId {get;set;}
        public string Carrier_Name {get;set;}
        public string DeliveryAddress_Address1 {get;set;}
        public string DeliveryAddress_Address2 {get;set;}
        public string DeliveryAddress_Country {get;set;}
        public string DeliveryAddress_State {get;set;}
        public string DeliveryAddress_City {get;set;}
        public string ShippingStatusDocument_Id {get;set;}
        public string ShippingStatusDocument_Name {get;set;}
        public int ShippingStatusDocument_AffectInventory {get;set;}
        public string TrackingNumber {get;set;}
        public string FinanceStatusDocument_Id {get;set;}
        public string FinanceStatusDocument_Name {get;set;}
        public int FinanceStatusDocument_AffectFinance {get;set;}
        public string VendorWarehouse_Id {get;set;}
        public string VendorWarehouse_Name {get;set;}
        public string Season_ObjectId {get;set;}
        public string Season_Name {get;set;}
        public int FlagPrintInovice {get;set;}
        public int FlagPrintBillLading {get;set;}
        public int FlagPrintPickingTicket {get;set;}
        public int FlagPrintLabels {get;set;}
        public int FlagParaImprimir {get;set;}
        public string SpecsNotes {get;set;}
        public double FinanceSummary_TotalAmount {get;set;}
        public double FinanceSummary_TotalCosts {get;set;}
        public double FinanceSummary_TotalTaxes {get;set;}
        public double FinanceSummary_TotalDiscounts {get;set;}
        public int ToInvoice { get; set; }

    }


    public class ComercialDocumentsDetailsImports
    {

        public string SquadId {get;set;}
        public string LegacyObjectId {get;set;}
        public string DocLegacyObjectId { get; set; }
        public string TI_Id {get;set;}
        public string TI_Name {get;set;}
        public string TI_Groups_Id {get;set;}
        public string TI_Groups_Name {get;set;}
        public string TISpecs_SeasonBusiness_Id {get;set;}
        public string TISpecs_SeasonBusiness_Nombre {get;set;}
        public string TISpecs_IDFloresRecetaModelos_Id {get;set;}
        public string TISpecs_IDFloresRecetaModelos_Name {get;set;}
        public string TISpecs_ModelRecipeBuy_Id {get;set;}
        public string TISpecs_ModelRecipeBuy_Name {get;set;}
        public int TISpecs_PackPerBoxLayer {get;set;}
        public int TISpecs_PackLayersToSale {get;set;}
        public int TISpecs_PackLayersToBuy {get;set;}
        public string TISpecs_PackingBoxToSale_Id {get;set;}
        public string TISpecs_PackingBoxToSale_Code {get;set;}
        public string TISpecs_PackingBoxToSale_Name {get;set;}
        public double TISpecs_PackingBoxToSale_Length {get;set;}
        public double TISpecs_PackingBoxToSale_Width {get;set;}
        public double TISpecs_PackingBoxToSale_Height {get;set;}
        public double TISpecs_PackingBoxToSale_WeightVolumeRate {get;set;}
        public double TISpecs_PackingBoxToSale_PalletsEquivalent {get;set;}
        public double TISpecs_PackingBoxToSale_ContainersNumber {get;set;}
        public int TISpecs_PackingBoxToSale_FlagWet {get;set;}
        public double TISpecs_PackingBoxToSale_FullBoxEquivalent {get;set;}
        public double BoxesQty {get;set;}
        public double ChargeableQty {get;set;}
        public string IDChargeableUnitsType {get;set;}
        public string ChargeableUnitsName {get;set;}
        public double UnitPrice {get;set;}
        public double TaxableUnitPrice {get;set;}
        public string InventoryItemId {get;set;}
        public int IDInventarioFinal {get;set;}
        public double UnitCost {get;set;}
        public double TaxRate {get;set;}
        public int ProductSourceId {get;set;}
        public double OriginalPrice {get;set;}
        public DateTime PullDate {get;set;}
        public string PullDateWithFormat { get; set; }


    }

}
