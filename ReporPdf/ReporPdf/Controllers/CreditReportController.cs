using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReporPdf.Controllers;

using Rotativa.AspNetCore;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Accounting;
using SunttelTPointReporPdf.Interfaces.CreditReport;
using SunttelTradePointB.Shared.Sales;
using System.Net.Http.Json;
using SunttelTPointReporPdf.Interfaces.Sale;
using SunttelTPointReporPdf.Model;

namespace SunttelTPointReporPdf.Controllers
{
    public class CreditReportController : Controller
    {
        public ICreditDocument _CreditDocument;
        public ISale _CommercialDocument;

        private readonly ILogger<CreditDocument> _logger;
        public CreditReportController(ILogger<CreditDocument> logger, IConfiguration _config, ICreditDocument creditDocumentId, ISale commercialDocument)
        {
            _logger = logger;

            _CreditDocument = creditDocumentId;
            _CommercialDocument = commercialDocument;
        }

        public ActionResult Credit(string creditId)
        {
            var Model = new ModelCreditSale();
            Task<(bool IsSuccess, CreditDocument? creditDocument, string? ErrorDescription)>? reult = _CreditDocument.GetCreditDocument(creditId);
            Model.CreditDocument = reult.Result.creditDocument;

            if (Model.CreditDocument != null && Model.CreditDocument.CommercialDocumentRef != null)
            {
                Task<(bool IsSuccess, List<SalesDocumentItemsDetails>? detail, string? ErrorDescription)>? Sale = _CreditDocument.GetCommercialDocumentDetails(Model.CreditDocument.CommercialDocumentRef.DocumentId);
                Model.SaleDetail = Sale.Result.detail;

            }

            // return View(Model);
            return new ViewAsPdf("Credit", Model)
            {
                //PageSize = Rotativa.AspNetCore.Options.Size.,
                // FileName = $"{model.Buyer.Name}.pdf"
            };
        }

        public ActionResult PurchasesCredit(string creditId)
        {
            var Model = new ModelCreditSale();
            Task<(bool IsSuccess, CreditDocument? creditDocument, string? ErrorDescription)>? reult = _CreditDocument.GetCreditDocument(creditId);
            Model.CreditDocument = reult.Result.creditDocument;

            if (Model.CreditDocument != null && Model.CreditDocument.CommercialDocumentRef != null)
            {
                Task<(bool IsSuccess, List<SalesDocumentItemsDetails>? detail, string? ErrorDescription)>? Sale = _CreditDocument.GetCommercialDocumentDetails(Model.CreditDocument.CommercialDocumentRef.DocumentId);
                Model.SaleDetail = Sale.Result.detail;

            }

            // return View(Model);
            return new ViewAsPdf("PurchasesCredit", Model)
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
