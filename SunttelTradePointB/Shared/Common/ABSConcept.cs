using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SunttelTradePointB.Shared.Common
{

    [BsonIgnoreExtraElements]
    public class AtomConcept {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonIgnoreIfNull]
        public string LegacyId { get; set; }
        
        [BsonIgnoreIfNull]
        public string Code { get; set; }


        [MinLength(3)]
        [BsonIgnoreIfNull]
        public string Name { get; set; }

        [BsonIgnoreIfNull]
        public InfoAuditRecord AuditRecord { get; set; }
     


        [BsonIgnore]
        public bool? IsSelected { get; set; }

        [BsonIgnore]
        public bool? IsLoadingData { get; set; }

        [BsonIgnore]
        public bool? HasChanged { get; set; }

        [BsonIgnoreIfNull]
        public string Notes { get; set; }

        public AtomConcept()
        {
       
            this.IsSelected = false;
            this.IsLoadingData = false;
            this.HasChanged = false;
        }


    }

    [BsonIgnoreExtraElements]
    public class Concept: AtomConcept
    {
        [DisplayName("Skin Image")]
        [BsonIgnoreIfNull]
        public string SkinImageName { get; set; }

        [BsonIgnoreIfNull]
        public List<ConceptGroup> Groups{ get; set; }

        [BsonIgnoreIfNull]
        public ConceptStatus Status { get; set; }

        [DisplayName("Type of Concept")]

        [BsonIgnoreIfNull]
        public ConceptType? TypeOfConcept { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string FullClassName
        {
            get
            {
                if (Groups == null)
                {
                    return "NOT GROUPED";
                }
                else
                {
                    string groupsNames = "";
                    Groups.ForEach(g => groupsNames+= groupsNames == ""? g.Name : " /" + g.Name);
                    return groupsNames;
                }
            }

        }


    }

 

    public class ConceptGroup: AtomConcept
    {

        [BsonIgnoreIfNull]
        public List<ConceptGroup> ChildrenGroups { get; set; }
        public string GroupClassificationCriteria { get; set; }

    }

#nullable enable
  
    public class ConceptType: AtomConcept
    {

        [BsonIgnoreIfNull]
        public List<ConceptGroup>? Groups { get; set; }
    }
#nullable disable

    public class ConceptStatus : AtomConcept
    {

        public bool IsEnabledForTransactions { get; set; }
        public bool IsEditable { get; set; }
        public bool SystemBlocked { get; set; }

    }


    public class InfoAuditRecord {
        [BsonIgnoreIfNull]
        [DisplayName("Creation Time")]
        public DateTime? CreationTime { get; set; }

        [DisplayName("Last Modified Time")]
        [BsonIgnoreIfNull]
        public DateTime? LastModifiedTime { get; set; }
    }

}
