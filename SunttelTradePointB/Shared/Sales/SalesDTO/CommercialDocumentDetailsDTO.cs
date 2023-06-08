using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SunttelTradePointB.Shared.Common;
using System.ComponentModel;

namespace SunttelTradePointB.Shared.Sales.SalesDTO
{
    public class CommercialDocumentDetailsDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull]
        [DisplayName("Document")]
        public string IdCommercialDocument { get; set; }

        [BsonIgnoreIfNull]
        [DisplayName("Purchases Specs")]
        public PurchaseItemDetails PurchaseSpecs { get; set; }

        [DisplayName("Item")]
        public Concept TransactionalItem { get; set; }

        public Header Header { get; set; }
    }

    public class Header
    {
        public Concept Buyer { get; set; }
        public string PO { get; set; }
    }
}
