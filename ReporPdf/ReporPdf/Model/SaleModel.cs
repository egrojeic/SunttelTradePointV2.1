using SunttelTradePointB.Shared.Sales;

namespace SunttelTPointReporPdf.Model
{

    public class SaleModel
    {
        public string Invoice { get; set; } = "...";
        public string Date { get; set; } = "...";
        public string SquadName { get; set; } = "...";
        public string SquadId { get; set; } = "...";
        public string SquadTollFree { get; set; } = "...";
        public string SquadPhone { get; set; } = "...";
        public string SquadAddress { get; set; } = "...";
        public string BillName { get; set; } = "...";
        public string BillAddressAndPhone { get; set; } = "...";
        public string BillCity { get; set; } = "...";
        public string ShipName { get; set; } = "...";
        public string ShipAddressAndPhone { get; set; } = "...";
        public string ShipCity { get; set; } = "...";
        public string CustomerNumber { get; set; } = "...";
        public string CustomerPO { get; set; } = "...";
        public string SalesMan { get; set; } = "...";
        public double SubTotal { get; set; } = 0;
        public double SaleTax { get; set; } = 0;
        public double TotalInvoice { get; set; } = 0;
        public List<SalesDocumentItemsDetails> SalesItems { get; set; }
        public static SaleModel ToSaleModel(ref SaleModel saleModel, CommercialDocument commercialDocument)
        {
            if (commercialDocument != null)
            {
                saleModel.Invoice = commercialDocument.DocumentNumber.ToString();
                saleModel.SquadId = commercialDocument.SquadId.ToString();  
                saleModel.Date = commercialDocument.ShipDate.ToString("MM/dd/yyyy");
                saleModel.BillName = commercialDocument.Buyer != null ? commercialDocument.Buyer.Name : "";
                saleModel.SalesItems = commercialDocument.Items != null ? commercialDocument.Items : new List<SalesDocumentItemsDetails>();
                if (commercialDocument.Items != null) saleModel.SubTotal = commercialDocument.Items.Sum(s => s.Total);
                saleModel.SalesItems = commercialDocument.Items != null ? commercialDocument.Items : new List<SalesDocumentItemsDetails>();
                saleModel.SaleTax = commercialDocument.FinanceSummary.TotalTaxes;
                saleModel.TotalInvoice = (saleModel.SaleTax + saleModel.SubTotal);
                saleModel.SalesMan = commercialDocument.SalesPerson != null && commercialDocument.SalesPerson.Name != null ? commercialDocument.SalesPerson.Name : "";

            }
            return saleModel;
        }
    }
}

