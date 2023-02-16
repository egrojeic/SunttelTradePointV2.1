using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.Common
{
    public class BusinessWeek
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int WeekNumber { get; set; }
        public int YearOfWeek { get; set; }
        [BsonIgnoreIfNull]
        public DateTime? StartDate { get; set; }

        [BsonIgnoreIfNull]
        public DateTime? EndDate { get; set; }

    }

    public class SeasonBusiness: AtomConcept
    {

        public bool FlagSundayShipping { get; set; }
        [BsonIgnoreIfNull]
        public DateTime? StartDate { get; set; }

        [BsonIgnoreIfNull]
        public DateTime? EndDate { get; set; }

    }
}
