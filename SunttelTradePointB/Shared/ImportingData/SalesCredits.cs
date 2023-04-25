using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.ImportingData
{
    public class SalesCreditsImports
    {
        public string SquadId {get;set;}
        public string LegacyObjectId {get;set;}
        public string DocumentType_ID {get;set;}
        public string DocumentType_Name {get;set;}
        public string LegacyId {get;set;}

        public string Codigo { get; set; }

        public DateTime CreditDate {get;set;}
        public string Buyer_ObjectId {get;set;}
        public string Buyer_Name {get;set;}
        public string Vendor_ObjectId {get;set;}
        public string Vendor_Name {get;set;}
        public string Status_Id {get;set;}
        public string Status_Name {get;set;}
        public string CreditReason_Id {get;set;}
        public string CreditReason_Name {get;set;}
        public string InvoiceId {get;set;}
        public string InvoiceCode {get;set;}
        public DateTime InvoiceDate {get;set;}
        public string Observaciones { get; set; }

        public double ValorTotal { get; set; }


    }
}
