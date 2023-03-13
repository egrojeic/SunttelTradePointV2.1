using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunttelTradePointB.Shared.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SunttelTradePointB.Shared.Communications
{

    /// <summary>
    /// Message types
    /// </summary>
    public enum CommunicationsMessageType {
        
        SystemMessage = 0,
        ChatMessage = 1

    }


    public enum ConceptTypes
    {
        Entity = 0,
        TransactionalItems = 1
    }

    /// <summary>
    /// Represents the message to be spread
    /// </summary>
    public class CommunicationsMessage
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ParentMessageId { get; set; }

        public EntityNodeCommunications SenderEntity { get; set; }
        public DateTime SendDateTime { get; set; }
        public string Message { get; set; }

        [BsonIgnoreIfNull]
        public List<ReceiverMessage> Receivers { get; set; }
        public CommunicationsMessageType MessageTypeId { get; set; }

        [BsonIgnoreIfNull]
        public List<RelatedConcept>? RelatedLinks { get; set; }

        

        [BsonIgnoreIfNull]
        public string MessageIconName { get; set; }


        public EntityNodeCommunications DestinyGroupChannel { get; set; }

        public string GetOriginName()
        {
            return SenderEntity.Name;
        }

        public string GetOriginImage()
        {
            return "AppImages/" + SenderEntity.SkinImageName;
        }

    }

    public class RelatedConcept {

        [BsonRepresentation(BsonType.ObjectId)]
        public string ConceptId { get; set; }
        public string Notes { get; set; }

        public ConceptTypes ConceptTypeId { get; set; }

    }

    /// <summary>
    /// Describes the Reception event
    /// </summary>
    public class ReceiverMessage {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public EntityNodeCommunications ReceiverEntity { get; set; }
        public DateTime ReceiveDateTime { get; set; }
        public DateTime ReadDateTime { get; set; }
        public bool ReceptionConfirmed { get; set; } = false;
        public bool ReadConfirmed { get; set; } = false;

    }

    /// <summary>
    /// Represent a node sender or receiver
    /// </summary>
    public class EntityNodeCommunications {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [MinLength(3)]
        [BsonIgnoreIfNull]
        public string Name { get; set; }

        [DisplayName("Skin Image")]
        [BsonIgnoreIfNull]
        public string SkinImageName { get; set; }
    }

    /// <summary>
    /// Describes a Channel where participants messages will be spread
    /// </summary>
    public class ChannelCommunicationsGroup: EntityNodeCommunications
    {
       
        [BsonIgnoreIfNull]
        public List<CommunicationsGroupParticipant> Participants { get; set; }

        [BsonIgnoreIfNull]
        public EntityNodeCommunications Owner { get; set; }

        [BsonIgnore]
        public virtual int WeightByUser { get; set; }
    }

    /// <summary>
    /// Describes the relation of anode with the channel group
    /// </summary>
    public class CommunicationsGroupParticipant {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public EntityNodeCommunications Participant { get; set; }
        public bool InvitationRequestSent { get; set; } = false;
        public bool InvitationRequestAnswered { get; set; } = false;
        public bool InvitationRequestApprovedByOwner { get; set; } = false;
        public bool InvitationRequestApprovedByParticipant { get; set; } = false;
        public bool IsActive { get; set; }
    }



}
