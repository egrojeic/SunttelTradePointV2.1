using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.Accounting
{

    public class Payment : RecordItem
    {
        [DisplayName("Payment Type")]
        public PaymentType DocumentType { get; set; }

        [DisplayName("Code")]
        public string Code { get; set; }

        [DisplayName("Payment Ref")]
        public string PaymentRef { get; set; }

        [DisplayName("Date")]
        public DateTime ShipDate { get; set; }

        [DisplayName("Payer")]
        public Concept Payer { get; set; }

        [DisplayName("Receiver")]
        public Concept Receiver { get; set; }

        [DisplayName("Status")]
        public PaymentStatus Status { get; set; }

        public PaymentMode DocPaymentMode { get; set; }

        public PaymentVia DocPaymentVia { get; set; }

        public double Surplus { get; set; }

        [DisplayName("Value")]
        public double PaymentValue { get; set; }

        [DisplayName("Imported File")]
        public List<PaymentImportFileRecord> ImportFileRecords { get; set; }


        [DisplayName("Applied Payments")]
        public List<AppliedPayment> AppliedPayments { get; set; }

        [DisplayName("Payment with overpayments")]
        public List<PaymentWithOverpayment> PaymentWithOverpayments { get; set; }

        [DisplayName("Payment with overplus")]
        public List<PaymentWithOverplus> PaymentWithOverplus { get; set; }
        
        [DisplayName("Payment with Credits")]
        public List<PaymentWithCredit> PaymentWithCredits { get; set; }

        public bool ReturnedPayment { get; set; } = false;

        [DisplayName("Return Description")]
        public ReturnedPaymentInfo ReturnInfo { get; set; }

    }

    public class PaymentStatus : BasicConcept
    {

        [DisplayName("Editing Allowed")]
        public bool EditingAllowed { get; set; }

        [DisplayName("Enabled")]
        public bool Enabled { get; set; }

    }

    public class PaymentType : BasicConcept
    {
       
    }

    public class PaymentMode : BasicConcept
    {

    }

    public class PaymentVia : BasicConcept
    {

    }

    public class ReturnedPaymentInfo: RecordItem
    {
        public string ReturnDescription { get; set; }
    }

    public class PaymentImportFileRecord: RecordItem
    {

        public string PaymentId { get; set; }
        public string PayerCode { get; set; }

        [DisplayName("Payment Reference")]
        public string PaymentDocRef { get; set; }
        public DateTime PaymentDocDate { get; set; }

        [DisplayName("Invoice to Pay")]
        public string InvoiceToPay { get; set; }

        [DisplayName("PO Number")]
        public string PONumberToPay { get; set; }

        [DisplayName("Invoice Total Amount")]
        public double InvoiceTotalAmount { get; set; }

        [DisplayName("Discount")]
        public double Discount { get; set; }

        [DisplayName("Credits")]
        public double Credits { get; set; }

        [DisplayName("Discount Reason")]
        public string DiscountReason { get; set; }

        [DisplayName("Invoice Error")]
        public bool HasInvoiceError { get; set; }

        [DisplayName("Customer Error")]
        public bool HasCustomerError { get; set; }

    }

    public class AppliedPayment : RecordItem
    {

        public string PaymentId { get; set; }

        [DisplayName("Invoice to Apply")]
        public string InvoiceToApplyRef { get; set; }

        [DisplayName("PO Number")]
        public string PONumberToPay { get; set; }

        [DisplayName("Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        [DisplayName("BuyerRef")]
        public string BuyerRef { get; set; }

        [DisplayName("Buyer Name")]
        public string BuyerName { get; set; }

        [DisplayName("Invoice Total Amount")]
        public double InvoiceTotalAmount { get; set; }

        [DisplayName("Discount")]
        public double Discount { get; set; }

        [DisplayName("Credits")]
        public double Credits { get; set; }

        [DisplayName("Discount Reason")]
        public string DiscountReason { get; set; }


    }

    public class PaymentWithOverpayment: RecordItem
    {
        public string PaymentId { get; set; }
        public string OriginalPaymentId { get; set; }

        [DisplayName("Original Payment Ref")]
        public string OriginalCode { get; set; }

        [DisplayName("Original overpayment")]
        public double OverpaymentValue { get; set; }

        [DisplayName("Value Applied")]
        public double OverpaymentApplied { get; set; }
    }


    public class PaymentWithCredit: RecordItem
    {
        public string PaymentId { get; set; }
        public string CreditDocumentId { get; set; }
        
        [DisplayName("Credit Doc Ref")]
        public string CreditDocumentCode { get; set; }

        [DisplayName("Credit amount")]
        public double CreditValue { get; set; }

        [DisplayName("Value Applied")]
        public double CreditApplied { get; set; }

    }

    public class PaymentWithOverplus: RecordItem
    {
        public string PaymentId { get; set; }
        public string OriginalPaymentId { get; set; }

        [DisplayName("Original Payment Ref")]
        public string OriginalCode { get; set; }

        [DisplayName("Overplus amount")]
        public double OverplusValue { get; set; }
        [DisplayName("Value Applied")]
        public double OverplusApplied { get; set; }
    }
}
