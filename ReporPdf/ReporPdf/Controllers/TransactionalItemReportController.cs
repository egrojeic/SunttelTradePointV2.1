using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReporPdf.Controllers;
using Rotativa.AspNetCore;
using SunttelTradePointB.Shared.Sales;
using SunttelTradePointB.Shared.Common;
using SunttelTPointReporPdf.Interfaces.Sale;
using SunttelTPointReporPdf.Interfaces.TransactionalReport;
using QRCoder;

using System;
using System.Drawing;
using Microsoft.Extensions.Hosting;

namespace SunttelTPointReporPdf.Controllers
{
    public class TransactionalItemReportController : Controller
    {
        public ITransactionalItem _TransactionalItem;
        private readonly ILogger<TransactionalItem> _logger;
        public TransactionalItemReportController(ILogger<TransactionalItem> logger, IConfiguration _config, ITransactionalItem transactionalItem)
        {
            _logger = logger;
            _TransactionalItem = transactionalItem;
        }

        public ActionResult Item(string transactionalItemId)
        {
            Task<(bool IsSuccess, TransactionalItem? transactionalItem, string? ErrorDescription)>? reult = _TransactionalItem.GetTransactionalItem(transactionalItemId);
            TransactionalItem? model = reult.Result.transactionalItem;

            #region Qr         
            string qrString = $"https://localhost:7186/TransactionalItemCard/all/{transactionalItemId}";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrString, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            string rootpath = AppDomain.CurrentDomain.BaseDirectory;

            var strHostFolder = "https://localhost:7166/uploads/icon-192.png";
            //            qrCodeImage.Save();

            ImageConverter converter = new ImageConverter();
            byte[] QRbyte = (byte[])converter.ConvertTo(qrCodeImage, typeof(byte[]));
            var img = $"data:img/png;base64,{Convert.ToBase64String(QRbyte)}";

            ViewBag.Qr = img;
            #endregion Qr

            return View(model);
            //   if (model==null) {return NotFound("Does not exist"); }
            return new ViewAsPdf("Item", model)
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
