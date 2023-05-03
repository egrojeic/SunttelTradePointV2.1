using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.DataViews.BI
{
    public class BISalesConsolidated
    {
        public string Id { get; set; }
        [DisplayName("Document Type")]
        public string DocumentTypeName { get; set; }
        [DisplayName("Order No")]
        public int DocumentNumber { get; set; }

        [DisplayName("Business Line")]
        public string BusinessLineDocName { get; set; }
        public string PO { get; set; }
        public DateTime ShipDate { get; set; }
        [DisplayName("Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        [DisplayName("Arrival Date")]
        public DateTime ArrivalDate { get; set; }

        [DisplayName("Vendor")]
        public string VendorName { get; set; }

        [DisplayName("Buyer")]
        public string BuyerName { get; set; }

        [DisplayName("Buyer Commercial Group")]
        public string BuyerCommercialGroup { get; set; }

        [DisplayName("Sales")]
        public string SalesPersonName { get; set; }
        
        [DisplayName("Shipper")]
        public string CarrierName { get; set; }

        [DisplayName("Country")]
        public string DeliveryAddressCountry { get; set; }

        [DisplayName("State")]
        public string DeliveryAddressState { get; set; }

        [DisplayName("City")]
        public string DeliveryAddressCity { get; set; }

        [DisplayName("Finance Status")]
        public bool FinanceStatusDocumentAffectFinance { get; set; }
        public string SeasonName { get; set; }

    
        [DisplayName("Total Amount")]
        public double FinanceSummaryTotalAmount { get; set; }

        [DisplayName("Total Costs")]
        public double FinanceSummaryTotalCosts { get; set; }

        [DisplayName("Total Taxes")]
        public double FinanceSummaryTotalTaxes { get; set; }

        [DisplayName("Total Discounts")]
        public double FinanceSummaryTotalDiscounts { get; set; }

        [DisplayName("Total Paid")]
        public double FinanceSummaryTotalPaid { get; set; }

        public string StandingOrderId { get; set; }

    }
}
