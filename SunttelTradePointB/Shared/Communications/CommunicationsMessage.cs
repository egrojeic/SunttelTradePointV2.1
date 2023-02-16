using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Shared.Communications
{
    public class CommunicationsMessage
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Concept SenderEntity { get; set; }
        public DateTime SendDateTime { get; set; }
        public string Message { get; set; }
        public List<ReceiverMessage> Receivers { get; set; }
        public int MessageStausId { get; set; }
        public int MessageTypeId { get; set; }
        public string? RecipientGroup { get; set; }
        public string? TransactionId { get; set; }
        public int AnswerActionId { get; set; }
        public string MessageTypeName { get; set; }
        public string MessageStausName { get; set; }


        public override string ToString()
        {
            return $"M>{Message} | G>{RecipientGroup} | O> {SenderEntity.Id}";
        }

        public string GetOriginName()
        {
            return SenderEntity.Name;
        }

        public string GetOriginImage()
        {
            return "AppImages/" + SenderEntity.SkinImageName;
        }

    }

    public class ReceiverMessage {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Concept ReceiverEntity { get; set; }
        public DateTime ReceiveDateTime { get; set; }
        public DateTime ReadDateTime { get; set; }

    }

}
