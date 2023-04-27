using SunttelTradePointB.Shared.SquadsMgr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.ImportingData
{
    public class StandingOrderImports
    {

        public string SquadId {get; set;}
        public int DocumentNumber { get; set; }
        public string LegacyObjectId {get; set;}
        public string LegacyId {get; set;}
        public string IDOrderTypes {get; set;}
        public string CommercialDocumentType_LegacyId {get; set;}
        public string CommercialDocumentType_Name {get; set;}
        public string CommercialDocumentType_Description {get; set;}
        public string BusinessLineDocId {get; set;}

        public string BusinessLineDoc_Name { get; set; }
        public string Vendor_ObjectId {get; set;}
        public string Vendor_Name {get; set;}
        public string Buyer_ObjectId {get; set;}
        public string Buyer_Name {get; set;}
        public string Buyer_Groups__id {get; set;}
        public string Groups_Name {get; set;}
        public string DeliveryAddress_Address1 {get; set;}
        public string DeliveryAddress_Address2 {get; set;}
        public string DeliveryAddress_Country {get; set;}
        public string DeliveryAddress_State {get; set;}
        public string DeliveryAddress_City {get; set;}
        public string PO {get; set;}
        public DateTime StartingShipDate {get; set;}
        public DateTime FinalShipDate {get; set;}
        public Boolean IsEndUndefined {get; set;}
        public string Season_ObjectId {get; set;}
        public string Season_Name {get; set;}
        public int FrequencyInDays {get; set;}
        public string TransportationMode { get; set; }


    }
}
