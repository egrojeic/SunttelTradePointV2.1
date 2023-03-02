using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Reflection.Emit;
using MongoDB.Bson;

namespace SunttelTradePointB.Shared.ImportingData
{
    public class ProductsImport
    {

        public string LegacyId { get; }
        public string LegacyIdObjectID { get; }
        public string Code { get; }
        public string Name { get; }
        public string ClassOfTransactionalItem_Name { get; }
        public int ClassOfTransactionalItem_LegacyId { get; }
        
        public string ClassTransactionalItemType { get; }
        public string Status { get; }
        public string ReferenceCost { get; set; }
        public string ClassOfTransactionalItem_bsonObjectId { get; set; }
        public string ClassTransactionalItemType_bsonObjectId { get; set; }

        public string GroupClassificationCriteria { get; set; }

    }

    public class TransactionalItemProductionSpecsImport
    {

        public string ProductLegacyId { get; set; }
        public string ProcesoProduccion_bsonObjectId { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }

    }

    public class ProductPackingSpecsImports
    {
        public string CustomerSpecsLegacyId { get; set; }
        public string LegacyIdProduct { get; set; }
        public string LegacyIdCliente { get; set; }
        public string LegacyIDModel { get; set; }
        public string LegacySeasonId { get; set; }
        public string LegacyIDModelObjectId { get; set; }
        public string ModelName { get; set; }
        public string SeasonName { get; set; }
        public double PackPerLayer { get; set; }
        public int BoxPackLayersToSale { get; set; }
        public int BoxPackLayersToBuy { get; set; }
        public string BoxLegacyId { get; set; }
        public string BoxLegacyIdObjectId { get; set; }
        public string Box_Code { get; set; }
        public string Box_Name { get; set; }
        public double Box_Length { get; set; }
        public double Box_Width { get; set; }
        public double Box_Height { get; set; }
        public double UnitPrice { get; set; }
        public string CustomerLabelInstructions_PalletLabelStyle { get; set; }
        public string CustomerLabelInstructions_BoxLabelStyle { get; set; }
        public string CustomerLabelInstructions_UPCName { get; set; }
        public string CustomerLabelInstructions_InnerProductLabelStyleByDeafult { get; set; }
        public string CustomerLabelInstructions_UPCComments { get; set; }
        public string CustomerLabelInstructions_UPCCode { get; set; }
        public string CustomerLabelInstructions_UPCDateCodeFormat { get; set; }
        public string CustomerLabelInstructions_UPCDateCodeBasedOn { get; set; }
        public string CustomerLabelInstructions_UPCDateCodeDaysAfterReferenceDate { get; set; }
        public string CustomerLabelInstructions_OriginCountryLegend { get; set; }
        public string CustomerLabelInstructions_SKU { get; set; }
        public string ProductPackingSpecs_Price_RetailPrice { get; set; }
        public string ProductPackingSpecs_Price_BulkRetailPrice { get; set; }
        public string ProductPackingSpecs_Price_BulkRetailQty { get; set; }

    }

    public class ProductPackingSpecsRecipeImports
    {

        public string RecipeType { get; set; }
        public string LegacyIdProduct { get; set; }
        public string CustomerSpecsLegacyId { get; set; }
        public string LegacyIdCliente { get; set; }
        public string LegacyIDModel { get; set; }
        public string ComponentProductLegacyId { get; set; }
        public string ComponentProductLegacyIdObjectId { get; set; }
        public string ComponentProductName { get; set; }
        public string ComponentProductCode { get; set; }
        public string ClassOfTransactionalItem_Name { get; set; }
        public string ClassTransactionalItemType { get; set; }
        public double Cantidad { get; set; }
        public string ReferenceCost { get; set; }

        public string ClassOfTransactionalItem_LegacyId { get; set; }
        public string ClassOfTransactionalItem_bsonObjectId { get; set; }
        public string ClassTransactionalItemType_bsonObjectId { get; set; }
        public int RecipeQualityModifierLegacyId { get; set; }
        public string RecipeQualityModifierName { get; set; }
        public string GroupClassificationCriteria { get; set; }
        public string Color { get; set; }
        public string TecnicaTratamiento { get; set; }
        public string RecipeQualityModifierLegacyObjectId { get; set; }
        

    }

    public class ListOfMaterialsImports
    {
        public string RecipeType { get; set; }
        public string CustomerSpecsLegacyId { get; set; }
        public string LegacyIdProduct { get; set; }
        public string ComponentProductLegacyId { get; set; }
        public string Quantity { get; set; }
        public string ComponentProductName { get; set; }
        public string ComponentProductCode { get; set; }
        public string ClassOfTransactionalItem_Name { get; set; }
        public string ClassTransactionalItemType { get; set; }
        public string ReferenceCost { get; set; }
        public string GroupClassificationCriteria { get; set; }
    }

    public class PathImagesImports
    {
        public string LegacyIdProduct { get; set; }
        public string PathImage { get; set; }
        public string LegacyIdCliente { get; set; }
        public string LegacyIDModel { get; set; }
        public string LegacySeasonId { get; set; }
    }

    public class QualityParametersImports
    {
        public string LegacyIdProduct { get; set; }
        public string TransactionalItemQuality_Name { get; set; }
        public string TransactionalItemQuality_Type { get; set; }
        public string TransactionalItemQuality_DefaultValue { get; set; }
        public string Value { get; set; }
        public string LegacyIdProduct_bsonId { get; set; }

    }



    public class ClassProductsImports {

       public string ClassOfTransactionalItem_LegacyId {get; set;}
       public string ClassOfTransactionalItem_bsonObjectId   {get; set;}
       public string ClassTransactionalItemType{get; set;}
       public string GroupClassificationCriteria { get; set; }

    }


    public class BoxesImports {

           public string SeasonObjectId {get;set;}
           public string ID  {get;set;}
           public string Nombre {get;set;}
           public double Alto    {get;set;}
           public double Ancho {get;set;}
           public double Largo   {get;set;}
           public int VecesPadre {get;set;}
           public string ArmelliniCode   {get;set;}
           public string Codigo {get;set;}
           public decimal PesoVolumen {get;set;}
           public decimal Costo {get;set;}
           public double NumBuckets  {get;set;}
           public int PalletEquivalent {get;set;}
           public int FlagWet {get;set;}
           public string IDBAS_TipoCaja {get;set;}
           public decimal TIE {get;set;}
           public decimal HI {get;set;}
           public int FlagActivo  {get;set;}
           public int FlagFarm{get;set;}
           public int FlagCalifornia { get; set; }
    }

    public class LabelStyleImports
    {
        public string LegacyId { get; set;}
        public string LegacyIdObjectID {get; set;}
        public string Nombre {get; set;}
        public string FlagActivo {get; set;}
        public string PurposeLegacyId {get; set;}
        public string PurposeLegacyIdObjectID {get; set;}
        public string PurposeName {get; set;}
        public string PDFReportName {get; set;}
        public string DataMaxColumnSeparatorSpace {get; set;}
        public string ZebraColumnSeparatorSpace {get; set;}
        public string PaperName {get; set;}
        public string Ancho {get; set;}
        public string Largo { get; set; }
    }

    public class DataMaxFieldsImports {

        public string LegacyIdObjectID { get; set; }

        public int IDEstilosLabels {get; set;}
        public string LabelFieldName {get; set;}
        public string LabelFieldLegacyIdObjectID {get; set;}
        public string Rotation {get; set;}
        public string Font {get; set;}
        public string Width {get; set;}
        public string Height {get; set;}
        public string FontSize {get; set;}
        public string Row {get; set;}
        public string GGColumn {get; set;}
        public string IIIColumn {get; set;}
        public string JColumn { get; set; }


    }

    public class ZebraFieldsImports {

        public string LegacyIdObjectID {get; set;}
        public int IDEstilosLabels {get; set;}
        public string LabelFieldName {get; set;}
        public string LabelFieldLegacyIdObjectID {get; set;}
        public string CampoA {get; set;}
        public string CampoB {get; set;}
        public string CampoC {get; set;}
        public string CampoD {get; set;}
        public string CampoEEE {get; set;}
        public string CampoFFFF {get; set;}
        public string CampoGGGG {get; set;}
        public string CampoHHHH {get; set;}
        public string CampoIIII {get; set;}
        public string CampoJ {get; set;}
        public string CampoK {get; set;}
        public string Orden { get; set; }

    }


    public class LabelsPaperImports
    {
        public string LegacyIdObjectId {get;set;}
        public string LegacyId {get;set;}
        public string Nombe { get; set; }

    }

}
