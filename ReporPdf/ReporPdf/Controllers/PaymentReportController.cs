using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReporPdf.Controllers;
using Rotativa.AspNetCore;
using SunttelTradePointB.Shared.Sales;
using SunttelTradePointB.Shared.Common;
using SunttelTPointReporPdf.Interfaces.IPayment;
using SunttelTPointReporPdf.Interfaces.IPayment;
using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Client;

namespace SunttelTPointReporPdf.Controllers
{
    public class PaymentReportController : Controller
    {
        public IPayment _Payment;
        private readonly ILogger<Payment> _logger;
        public PaymentReportController(ILogger<Payment> logger, IConfiguration _config, IPayment payment)
        {
            _logger = logger;
            _Payment = payment;
        }

        public ActionResult Payment(string paymentId, string skinImage)
        {
            Task<(bool IsSuccess, Payment? Payment, string? ErrorDescription)>? reult = _Payment.GetPayment(paymentId);
            Payment? model = reult.Result.Payment;

              if(skinImage!=null && skinImage.Trim()!="")  ViewBag.SquadsImages = $"{UIClientGlobalVariables.pathSquadsImages}/{skinImage}";
            else ViewBag.SquadsImages = "/ActorIco.png";

            return new ViewAsPdf("Payment", model)
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
