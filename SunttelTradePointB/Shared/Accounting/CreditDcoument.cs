﻿using SunttelTradePointB.Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.Accounting
{
    public class CreditDocument : RecordItem
    {
        [DisplayName("Credit Type")]
        public CreditType CreditDocumentType { get; set; }

        [DisplayName("Code")]
        public string Code { get; set; }


        [DisplayName("Date")]
        public DateTime CreditDate { get; set; }

        [DisplayName("Vendor")]
        public Concept Vendor { get; set; }

        [DisplayName("Buyer")]
        public Concept Buyer { get; set; }

        [DisplayName("Status")]
        public CreditStatus CreditDocumentStatus { get; set; }

        [DisplayName("Reason")]
        public CreditReason CreditDocumentReason { get; set; }

        [DisplayName("Value")]
        public double CreditValue { get; set; }

        public bool IsDirect { get; set; }

        public CommercialDocumentSummaryInfo CommercialDocumentRef { get; set; }

    }


    public class CreditType : BasicConcept
    {
        public bool IsASale { get; set; }
    }

    public class CreditStatus : BasicConcept
    {

    }

    public class CreditReason : BasicConcept
    {

    }

    public class CommercialDocumentSummaryInfo
    {
        public string DocumentId { get; set; }

        [DisplayName("Invoice")]
        public string DocumentCode { get; set; }

        [DisplayName("Invoice Date")]
        public DateTime DocumentDate { get; set; }
    }

}
