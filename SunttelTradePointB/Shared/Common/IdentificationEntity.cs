using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.Common
{
    public class IdentificationEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Code { get; set; }
        public IdentificationType EntityTypeId { get; set; }
    }

    public class IdentificationType: AtomConcept
    {
      
        public AtomConcept EntityIssuer { get; set; }
    }

}
