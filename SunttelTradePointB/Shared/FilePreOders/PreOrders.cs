using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.FilePreOders
{
    public class PreOrders
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string CustPO { get; set; }
        public int CustomerNumber { get; set; }
        public string MiamiShip { get; set; }
        public string DeliveryDate { get; set; }
        public string ProductCode { get; set; }
        public string Season { get; set; }
        public int QtyBoxes { get; set; }
        public int Pack { get; set; }
        public Double UnitCost { get; set; }
        public string OrderStatus { get; set; }
        public int ShipTo { get; set; }
        public string Source { get; set; }
    }
}
