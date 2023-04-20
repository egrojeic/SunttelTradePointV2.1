using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.ImportingData
{
    public class SalesPaymentsImports
    {
        
        public string SquadId {get;set;}
        public string LegacyObjectId {get;set;}
        public string DocumentType_ID {get;set;}
        public string DocumentType_Name {get;set;}
        public string LegacyId {get;set;}
        public string Codigo {get;set;}
        public string PaymentRef {get;set;}
        public DateTime PaymentDate {get;set;}
        public string Payer_ObjectId {get;set;}
        public string Payer_Name {get;set;}
        public string Receiver_ObjectId {get;set;}
        public string Receiver_Name {get;set;}
        public string Status_Id {get;set;}
        public string Status_Name {get;set;}
        public string DocPaymentMode_Id {get;set;}
        public string DocPaymentMode_Name {get;set;}
        public string DocPaymentVia_Id {get;set;}
        public string DocPaymentVia_Name {get;set;}
        public double Surplus {get;set;}
        public double PaymentValue {get;set;}
        public string ReturnNotes {get;set;}
        public int FlagPaymentRetrurned { get; set; }


    }
}
