using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using SunttelTPointReporPdf.Interfaces.IActor;
using SunttelTPointReporPdf.Interfaces.Sale;
using SunttelTPointReporPdf.Model;
using SunttelTradePointB.Client;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;
using SunttelTradePointB.Shared.Sales.CommercialDocument;

namespace ReporPdf.Controllers
{
    public class SaleReportController : Controller
    {
        public ISale _CommercialDocument;
        public IActor _Actor;
        private readonly ILogger<SaleReportController> _logger;
        public SaleReportController(ILogger<SaleReportController> logger, IConfiguration _config, ISale commercialDocument, IActor actor)
        {
            _logger = logger;
            _CommercialDocument = commercialDocument;
            _Actor = actor;
        }

        public ActionResult Sale(string commercialDocumentId, string skinImage)
        {
            SaleModel model = new SaleModel();
            EntityActor Buyer = new EntityActor();
            Task<(bool IsSuccess, CommercialDocument? CommercialDocument, string? ErrorDescription)>? reult = _CommercialDocument.GetCommercialDocument(commercialDocumentId);
            CommercialDocument? Sale = reult.Result.CommercialDocument;
            if (Sale != null)
            {
                var detail = _CommercialDocument.GetCommercialDocumentDetails(Sale.Id);
                Sale.Items = detail.Result.GetCommercialDocumentDetails;

                if (Sale.Buyer != null)
                {
                    var actor = _Actor.GetEntityActorById(Sale.Buyer.Id);
                    Buyer = actor.Result.entityActorResponse;
                }
            }


            SaleModel.ToSaleModel(ref model, Sale);
            SaleModel.ToSaleModel(ref model, Buyer);

            if (skinImage != null && skinImage.Trim() != "") model.BuyerSkinImage = $"{UIClientGlobalVariables.PathEntityImages}/{skinImage}";
            else model.BuyerSkinImage = "/ActorIco.png";
           // return View(model);
            return new ViewAsPdf("Sale", model)
            {
                //PageSize = Rotativa.AspNetCore.Options.Size.,
                // FileName = $"{model.Buyer.Name}.pdf"
            };
        }

        public ActionResult Purchases(string commercialDocumentId, string skinImage)
        {
            Task<(bool IsSuccess, CommercialDocument? CommercialDocument, string? ErrorDescription)>? reult = _CommercialDocument.GetCommercialDocument(commercialDocumentId);
            CommercialDocument? Sale = reult.Result.CommercialDocument;

            SaleModel model = new SaleModel();
            SaleModel.ToSaleModel(ref model, Sale);

            if (skinImage != null && skinImage.Trim() != "") model.BuyerSkinImage = $"{UIClientGlobalVariables.PathEntityImages}/{skinImage}";
            else model.BuyerSkinImage = "/ActorIco.png";

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
