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
using SunttelTradePointB.Client;
using Microsoft.AspNet.Identity;

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

        public ActionResult Credit(string creditId, string skinImage)
        {
            var Model = new ModelCreditSale();
            Task<(bool IsSuccess, CreditDocument? creditDocument, string? ErrorDescription)>? reult = _CreditDocument.GetCreditDocument(creditId);
            Model.CreditDocument = reult.Result.creditDocument;

            if (Model.CreditDocument != null && Model.CreditDocument.CommercialDocumentRef != null)
            {
                Task<(bool IsSuccess, List<SalesDocumentItemsDetails>? detail, string? ErrorDescription)>? Sale = _CreditDocument.GetCommercialDocumentDetails(Model.CreditDocument.CommercialDocumentRef.DocumentId);
                Model.SaleDetail = Sale.Result.detail;

            }
            Model.skinImage = $"{UIClientGlobalVariables.PathEntityImages}/{skinImage}";
         
           
            return new ViewAsPdf("Credit", Model)
            {
                //PageSize = Rotativa.AspNetCore.Options.Size.,
                // FileName = $"{model.Buyer.Name}.pdf"
            };
        }

        public ActionResult PurchasesCredit(string creditId, string skinImage)
        {
            var Model = new ModelCreditSale();
            Task<(bool IsSuccess, CreditDocument? creditDocument, string? ErrorDescription)>? reult = _CreditDocument.GetCreditDocument(creditId);
            Model.CreditDocument = reult.Result.creditDocument;

            if (Model.CreditDocument != null && Model.CreditDocument.CommercialDocumentRef != null)
            {
                Task<(bool IsSuccess, List<SalesDocumentItemsDetails>? detail, string? ErrorDescription)>? Sale = _CreditDocument.GetCommercialDocumentDetails(Model.CreditDocument.CommercialDocumentRef.DocumentId);
                Model.SaleDetail = Sale.Result.detail;

            }

            if(skinImage!=null && skinImage.Trim()!="")  Model.skinImage = $"{UIClientGlobalVariables.PathEntityImages}/{skinImage}";
            else Model.skinImage = "/ActorIco.png";

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
