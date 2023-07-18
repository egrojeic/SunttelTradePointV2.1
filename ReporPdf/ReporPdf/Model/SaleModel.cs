using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;
using SunttelTradePointB.Shared.Sales.CommercialDocument;

namespace SunttelTPointReporPdf.Model
{

    public class SaleModel
    {

        public string BuyerSkinImage { get; set; } = "...";
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
        public static SaleModel ToSaleModel(ref SaleModel saleModel, EntityActor Buyer)
        {
            if (Buyer != null)
            {
                saleModel.BillName = Buyer.Name != null ? Buyer.Name : "";
                saleModel.BillAddressAndPhone = Buyer.InvoicingAddress != null && Buyer.InvoicingAddress.Name != null ? Buyer.InvoicingAddress.Name : "";
                saleModel.BillAddressAndPhone = saleModel.BillAddressAndPhone + " " + Buyer.PhoneNumbers.FirstOrDefault().Number != null ? Buyer.PhoneNumbers.FirstOrDefault().Number.ToString() : "";
                saleModel.BillCity = Buyer.InvoicingAddress != null && Buyer.InvoicingAddress.Name != null ? Buyer.InvoicingAddress.Name : "";
                saleModel.BillCity = saleModel.BillCity + " " + Buyer.InvoicingAddress != null && Buyer.InvoicingAddress.ZipCode != null ? Buyer.InvoicingAddress.ZipCode : "";
            }
            return saleModel;
        }

        public static SaleModel ToSaleModel(ref SaleModel saleModel, CommercialDocument commercialDocument)
        {
            if (commercialDocument != null)
            {
                saleModel.Invoice = commercialDocument.DocumentNumber.ToString();
                saleModel.SquadId = commercialDocument.SquadId.ToString();
                saleModel.SquadName = commercialDocument.Vendor != null && commercialDocument.Vendor.Name != null ? commercialDocument.Vendor.Name : "";
                saleModel.SquadAddress = commercialDocument.Vendor != null && commercialDocument.Vendor.Name != null ? commercialDocument.Vendor.Name : "";
                saleModel.Date = commercialDocument.ShipDate.ToString("MM/dd/yyyy");
                //saleModel.ShipName = commercialDocument.DeliveryAddress!=null && commercialDocument.DeliveryAddress.Name!=null ?  commercialDocument.DeliveryAddress.Name:"";
                //saleModel.ShipAddressAndPhone =  commercialDocument.DeliveryAddress!=null && commercialDocument.DeliveryAddress.CityAddress!=null &&  commercialDocument.DeliveryAddress.CityAddress.Name!=null? commercialDocument.DeliveryAddress.CityAddress.Name:"";
                //saleModel.ShipCity =  commercialDocument.DeliveryAddress!=null && commercialDocument.DeliveryAddress.CityAddress!=null? commercialDocument.DeliveryAddress.CityAddress.Name:"";
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

