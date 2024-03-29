﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel;

namespace SunttelTradePointB.Shared.Common
{

    /// <summary>
    /// Different sections to describe a Transactional Item
    /// </summary>
    public enum TransactionalItemDetailsSection
    {
        /// <summary>
        /// List of Shipping to address
        /// </summary>
        PackingRecipe = 0,
        ProductionSpecs = 1,
        QualityParameters = 2,
        PathImages = 3,
        Tags = 4,
        Characteristics = 5
    }

    [BsonIgnoreExtraElements]
    public class TransactionalItem : Concept
    {
        [DisplayName("Catalog")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CatalogItem ItemCatalog { get; set; }

        [DisplayName("Characteristics")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TransactionalItemCharacteristicPair> ItemCharacteristics { get; set; }

        [DisplayName("Packing Specs")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<PackingSpecs> ProductPackingSpecs { get; set; }

        [DisplayName("Production Specs")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TransactionalItemProcessStep> ProductionSpecs { get; set; }

        [DisplayName("Is Generic")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsGeneric { get; set; }

        [DisplayName("Images")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TransactItemImage> PathImages { get; set; }

        [DisplayName("Price Lists")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PriceListTransactionalItems PriceOverridenByPriceList { get; set; }

        [DisplayName("Quality Parameters")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TransactionalItemQualityPair> QualityParameters { get; set; }

        [DisplayName("Tags")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TransactionalItemTag> TransactionalItemTags { get; set; }

        [DisplayName("Reference Cost")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double ReferenceCost { get; set; }

        [DisplayName("Models")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ProductModel> TransactionalItemModels { get; set; }


    }



    public class TransactItemImage : RecordItem
    {

        [BsonIgnoreIfNull]
        public string Name { get; set; }

        [DisplayName("Image Name")]
        public string PathImage { get; set; }

        [DisplayName("Is main Image")]
        public bool IsMainImage { get; set; }
        public override string ToString()
        {
            return Name;
        }

    }
    public class TransactionalItemStatus : ConceptStatus
    {


        [DisplayName("Is Available to Sale")]
        public bool IsAvailableForSale { get; set; }

        [DisplayName("Is Available to Produce")]
        public bool IsAvailableToProduce { get; set; }

        [DisplayName("Is Available to Buy")]
        public bool IsAvailableToBuy { get; set; }
    }



    public class TransactionalItemProcessStep : AtomConcept
    {

        [DisplayName("General Instructions")]
        public string GeneralInstructions { get; set; }
        public double Cost { get; set; }

        public double HolidayDayCost { get; set; }

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DisplayName("Process Tags")]
        public List<TransactionalItemTag> TransactionalItemProcessTags { get; set; }
        public int Order { get; set; }

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DisplayName("Costs exceptions by Quantity")]
        public List<CostExceptionByQuantity> CostExceptionsByQuantity { get; set; }

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ChargeableUnitsType TypeOfComponentsToCharge { get; set; }

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BasicConcept ProcessType { get; set; }

    }


    public class ChargeableUnitsType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }



    }

    public class CostExceptionByQuantity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DisplayName("Minimum Quantity")]
        public double MinimumQty { get; set; }

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        [DisplayName("Maximum Quantity")]
        public double MaximumQty { get; set; }



    }


    public class TransactionalItemTag : RecordItem
    {

        public string Key { get; set; }
        public string Value { get; set; }



    }

    public class CatalogItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayName("Parent Catalog Item")]
        public CatalogItem ParentCatalogItem { get; set; }



    }



    public class TransactionalItemType : ConceptType
    {
        [DisplayName("Has Packing Recipe")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasPackingRecipe { get; set; } = null;

        [DisplayName("Has Production Specs")]

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasProductionSpecs { get; set; } = null;

        [DisplayName("Processes")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TransactionalItemProcessStep> TransactionalItemProcesses { get; set; }

        [DisplayName("Characteristics")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TransactionalItemTypeCharacteristic> TransactionalItemTypeCharacteristics { get; set; }

        [DisplayName("Quality Parameters")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TransactionalItemQuality> QualityParameters { get; set; }


        [DisplayName("Recipe Quality Modifiers")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<RecipeModifier> InRecipeModifiers { get; set; }



    }

    public class TransactionalItemTypeCharacteristic
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }

        [DisplayName("Possible Values")]
        public List<TransactionalItemCharacteristic> PossibleValues { get; set; }
    }

    public class TransactionalItemCharacteristic
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Is Enabled")]
        public bool IsEnabled { get; set; }

        [DisplayName("Additional Description")]
        public string AdditionalDescription { get; set; }

    }

    public class TransactionalItemInRecipeModifiers
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Possible Values")]
        public List<string> PossibleValues { get; set; }
    }


    public class TransactionalItemCharacteristicPair : RecordItem
    {

        [DisplayName("Characteristic")]
        public TransactionalItemTypeCharacteristic TransactionalItemCharacteristic { get; set; }
        public string Value { get; set; }


    }


    public class TransactionalItemQuality : AtomConcept
    {
        [DisplayName("Default Value")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultValue { get; set; }
        public string AdditionalDescription { get; set; }
    }

    public class TransactionalItemQualityPair : RecordItem
    {

        [DisplayName("Item Quality")]
        public TransactionalItemQuality TransactionalItemQuality { get; set; }
        public string Value { get; set; }


    }



    public class ProductModel : AtomConcept
    {

        public string Description { get; set; }
    }

    public class MaterialRecipeItem
    {
        public Concept Material { get; set; }
        public double Quantity { get; set; }

    }


    public class PackingSpecs : RecordItem
    {


        public Concept Customer { get; set; }
        public SeasonBusiness Season { get; set; }

        [DisplayName("Recipe Model")]
        public ProductModel ModelRecipe { get; set; }

        [DisplayName("Pack per layer")]
        public double PackPerBoxLayer { get; set; }

        [DisplayName("Number of Box to sale Layers")]
        public int PackLayersToSale { get; set; } = 1;

        [DisplayName("Number of Box to buy Layers")]
        public int PackLayersToBuy { get; set; } = 1;

        [DisplayName("Sale Box Pack")]
        public Box PackingBoxToSale { get; set; }

        [DisplayName("Buy Box Pack")]
        public Box PackingBoxToBuy { get; set; }

        [DisplayName("Assembly Recipe")]
        public List<PackRecipeItem> AssemblyRecipeItems { get; set; }

        [DisplayName("Customer Label Instructions")]
        public LabelInstruction CustomerLabelInstructions { get; set; }
        public PriceInfo Price { get; set; }

        [DisplayName("Materials List")]
        public List<MaterialRecipeItem> ListOfMaterials { get; set; }

        [DisplayName("Assembly Type")]
        public AssemblyType ItemAssemblyType { get; set; }


    }

    public class AssemblyType : AtomConcept
    {
    }


    public class PackRecipeItem : RecordItem
    {

        [DisplayName("Product")]
        public Concept ItemComponent { get; set; }

        [DisplayName("Product Recipe")]
        [BsonIgnoreIfNull]
        public List<PackRecipeItem> ComponentItemRecipe { get; set; }

        [DisplayName("Product Packing Materials Recipe")]

        [BsonIgnoreIfNull]
        public List<MaterialRecipeItem> ListOfMaterials { get; set; }

        [DisplayName("Quality")]

        [BsonIgnoreIfNull]
        public ProductRecipeQualityModifier RecipeQualityModifier { get; set; }
        public double Quantity { get; set; }
    }


    public class RecipeModifier
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string LegacyId { get; set; }
        public string Name { get; set; }

        [DisplayName("Possible Values of Quality Modifier")]
        public List<ProductRecipeQualityModifier> RecipePossibleModifierValues { get; set; }

    }


    public class ProductRecipeQualityModifier
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string LegacyId { get; set; }
        public string Name { get; set; }


    }


    public class PriceInfo
    {

        [DisplayName("Retail Price")]
        public double RetailPrice { get; set; }

        [DisplayName("Retail Bulk Number of Items")]
        public int RetailBulkNumberOfItems { get; set; }

        [DisplayName("Bulk Reatil Price")]
        public double RetailBulkPrice { get; set; }

        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }

        [DisplayName("Price Based On")]
        public TransactionalItemType TotalPriceItemTypeBasedOn { get; set; }

        [DisplayName("Components Total Price")]
        public double TotalPrice { get; set; }
    }


    public class Box : AtomConcept
    {

        [DisplayName("Inventory Product")]
        [BsonIgnoreIfNull]
        public AtomConcept LinkedTransactionalItem { get; set; }
        [BsonIgnoreIfNull]
        public double Length { get; set; }
        [BsonIgnoreIfNull]
        public double Width { get; set; }
        [BsonIgnoreIfNull]
        public double Height { get; set; }

        [DisplayName("Weight Volume Rate")]
        [BsonIgnoreIfNull]
        public double WeightVolumeRate { get; set; }
        public double Cost { get; set; }

        [DisplayName("Container portion")]
        public double ContainersNumber { get; set; }

        [DisplayName("Pallets Equivalent")]
        public double PalletsEquivalent { get; set; }

        [DisplayName("Is Wet")]
        public bool FlagWet { get; set; }
        public bool Enable { get; set; }


        [DisplayName("Pallets Equivalent")]
        public double FullBoxEquivalent { get; set; }


    }


    public class LabelInstruction
    {

        [DisplayName("Pallet Label Style")]
        public LabelStyle PalletLabelStyle { get; set; }

        [DisplayName("Box Label Style")]
        public LabelStyle BoxLabelStyle { get; set; }

        [DisplayName("Override Name in UPC")]
        public string UPCName { get; set; }

        [DisplayName("Inner Product Label Style By Deafult")]
        public LabelStyle InnerProductLabelStyleByDeafult { get; set; }

        [DisplayName("UPC Comments")]
        public string UPCComments { get; set; }

        [DisplayName("Box UPC Code")]
        public string BoxUPCCode { get; set; }

        [DisplayName("Inner Product UPC Code")]
        public string InnerProductUPCCode { get; set; }

        [DisplayName("UPC Date Code")]
        public int UPCDateCodeFormat { get; set; }

        [DisplayName("UPC Date Code Based On")]
        public DatesReferenceType UPCDateCodeBasedOn { get; set; }

        [DisplayName("UPC Date Code after (days)")]
        public int UPCDateCodeDaysAfterReferenceDate { get; set; }

        [DisplayName("UPC Country Leyend")]
        public string OriginCountryLegend { get; set; }
        public string SKU { get; set; }

        [DisplayName("Show Retail Price in UPC")]
        public Boolean ShowRetailPriceUPCInfo { get; set; }

    }

    public enum DatesReferenceType
    {
        NotDefined = 0,
        BuyDate = 1,
        ShipDate = 2,
        DeliveryDate = 3,
        ProductionDate = 4
    }

    public enum LabelPurpose
    {
        Inventory = 1,
        Customer = 2,
        Provider = 3,
        UPC = 4
    }
    public class LabelStyle : AtomConcept
    {

        [DisplayName("Purpose")]
        public LabelPurpose Purpose { get; set; }

        [DisplayName("PDF Report")]
        [BsonIgnoreIfNull]
        public string PDFReportName { get; set; }
        public LabelPaper Paper { get; set; }

        [DisplayName("DataMax Label Settings")]
        [BsonIgnoreIfNull]
        public DataMaxLabelSpecs DataMaxLabelSettings { get; set; }

        [DisplayName("Zebra Label Settings")]
        [BsonIgnoreIfNull]
        public ZebraLabelSpecs ZebraLabelSettings { get; set; }
    }

    public class LabelPaper : AtomConcept
    {

        public double Width { get; set; }
        public double Height { get; set; }

    }

    public class DataMaxLabelSpecs
    {
        [DisplayName("Column Separator Space")]
        public int ColumnSeparatorSpace { get; set; }

        [DisplayName("DataMax Fields")]
        public List<DataMaxLabelLineSpec> DataMaxFieldsSpecs { get; set; }

    }

    public class DataMaxLabelLineSpec
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public LabelField FieldInLabel { get; set; }

        public string Rotation { get; set; }
        public string Font { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string FontSize { get; set; }
        public string Row { get; set; }
        public string GGColumn { get; set; }
        public string IIIColumn { get; set; }
        public string JColumn { get; set; }

    }


    public class ZebraLabelSpecs
    {

        [DisplayName("Column Separator Space")]
        public int ColumnSeparatorSpace { get; set; }

        [DisplayName("Zebra Fields")]
        public List<ZebraLabelLineSpec> ZebraFieldsSpecs { get; set; }

    }

    public class ZebraLabelLineSpec
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public LabelField FieldInLabel { get; set; }
        public string CampoA { get; set; }
        public string CampoB { get; set; }
        public string CampoC { get; set; }
        public string CampoD { get; set; }
        public string CampoEEE { get; set; }
        public string CampoFFFF { get; set; }
        public string CampoGGGG { get; set; }
        public string CampoHHHH { get; set; }
        public string CampoIIII { get; set; }
        public string CampoJ { get; set; }
        public string CampoK { get; set; }
        public string Orden { get; set; }

    }


    public class LabelField
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Used for")]
        public List<LabelPurpose> UsedFor { get; set; }
    }


    public class PriceListTransactionalItems
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Item Price")]
        public PriceInfo ItemPrice { get; set; }

        [DisplayName("Customers in List")]
        public List<Concept> CustomersInList { get; set; }

        [DisplayName("Ecommerce")]
        public bool IsECommerce { get; set; }
    }



}
