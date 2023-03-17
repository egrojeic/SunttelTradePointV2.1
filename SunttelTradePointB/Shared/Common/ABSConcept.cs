﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SunttelTradePointB.Shared.Common
{

    [BsonIgnoreExtraElements]
    public class RecordItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LegacyId { get; set; }


        [BsonIgnore]
        public bool? IsSelected { get; set; }

        [BsonIgnore]
        [DisplayName("Has Changed")]
        public bool? IsLoadingData { get; set; }

        [BsonIgnore]
        public bool? HasChanged { get; set; }

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SquadId { get; set; }


    }

    [BsonIgnoreExtraElements]
    public class AtomConcept: RecordItem
    {
      
        
        [BsonIgnoreIfNull]
        public string Code { get; set; }


        [MinLength(3)]
        [BsonIgnoreIfNull]
        public string Name { get; set; }


        [ObjectPropertyAttributes(ObjectPropertyAttributes.EditModes.LogAuditory)]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public InfoAuditRecord AuditRecord { get; set; }
     

        public AtomConcept()
        {
       
            this.IsSelected = false;
            this.IsLoadingData = false;
            this.HasChanged = false;
        }


        public override string ToString()
        {
            return Name;
        }

    }


    public class BasicConcept
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class Concept: AtomConcept
    {
        [DisplayName("Skin Image")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SkinImageName { get; set; }

        [ObjectPropertyAttributes(ObjectPropertyAttributes.EditModes.Groups)]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ConceptGroup> Groups{ get; set; }

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ConceptStatus Status { get; set; }

        [DisplayName("Type of Concept")]
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ConceptType? TypeOfConcept { get; set; }

        [BsonRepresentation(BsonType.String)]
        public virtual string FullClassName
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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ConceptGroup> ChildrenGroups { get; set; }
        public string GroupClassificationCriteria { get; set; }

    }

#nullable enable
  
    public class ConceptType: AtomConcept
    {

        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ConceptGroup>? Groups { get; set; }
    }
#nullable disable

    public class ConceptStatus : AtomConcept
    {
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsEnabledForTransactions { get; set; }
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsEditable { get; set; }
        [BsonIgnoreIfNull]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
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
