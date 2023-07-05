using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel;

namespace SunttelTradePointB.Shared.Common
{

    [BsonIgnoreExtraElements]
    public class TransactionalItemDTO : Concept
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

}
