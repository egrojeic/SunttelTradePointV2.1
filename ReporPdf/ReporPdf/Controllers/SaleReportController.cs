using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using SunttelTPointReporPdf.Interfaces.Sale;
using SunttelTPointReporPdf.Model;
using SunttelTradePointB.Shared.Sales;

namespace ReporPdf.Controllers
{
    public class SaleReportController : Controller
    {
        public ISale _CommercialDocument;
        private readonly ILogger<SaleReportController> _logger;
        public SaleReportController(ILogger<SaleReportController> logger, IConfiguration _config, ISale commercialDocument)
        {
            _logger = logger;
            _CommercialDocument = commercialDocument;
        }

        public ActionResult Sale(string commercialDocumentId)
        {
            Task<(bool IsSuccess, CommercialDocument? CommercialDocument, string? ErrorDescription)>? reult = _CommercialDocument.GetCommercialDocument(commercialDocumentId);
            CommercialDocument? Sale = reult.Result.CommercialDocument;
            if (Sale != null)
            {
                var detail = _CommercialDocument.GetCommercialDocumentDetails(Sale.Id);
                Sale.Items = detail.Result.GetCommercialDocumentDetails;
            }

            SaleModel model = new SaleModel();
            SaleModel.ToSaleModel(ref model, Sale);

            //return View(model);
            return new ViewAsPdf("Sale", model)
            {
                //PageSize = Rotativa.AspNetCore.Options.Size.,
                // FileName = $"{model.Buyer.Name}.pdf"
            };
        }

        public ActionResult Purchases(string commercialDocumentId)
        {
            Task<(bool IsSuccess, CommercialDocument? CommercialDocument, string? ErrorDescription)>? reult = _CommercialDocument.GetCommercialDocument(commercialDocumentId);
            CommercialDocument? Sale = reult.Result.CommercialDocument;

            SaleModel model = new SaleModel();
            SaleModel.ToSaleModel(ref model, Sale);

            // return View(model);
            return new ViewAsPdf("Purchases", model)
            {
                //PageSize = Rotativa.AspNetCore.Options.Size.,
                // FileName = $"{model.Buyer.Name}.pdf"
            };
        }




        public ActionResult Home()
        {
            return View();
        }
    }
}
