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
using SunttelTradePointB.Client;
using SunttelTPointReporPdf.Model;

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

        public ActionResult Item(string transactionalItemId, string skinImage)
        {
            TransactionalItemModel item = new TransactionalItemModel();

            Task<(bool IsSuccess, TransactionalItem? transactionalItem, string? ErrorDescription)>? reult = _TransactionalItem.GetTransactionalItem(transactionalItemId);
            item.item = reult.Result.transactionalItem;

            #region Qr         
            string qrString = $"https://localhost:7186/TransactionalItemCard/all/{transactionalItemId}";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrString, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);          

            ImageConverter converter = new ImageConverter();
            byte[] QRbyte = (byte[])converter.ConvertTo(qrCodeImage, typeof(byte[]));
            var img = $"data:img/png;base64,{Convert.ToBase64String(QRbyte)}";

            item.qr = img;            
            if (skinImage != null && skinImage.Trim() != "")item.skinImage = $"{UIClientGlobalVariables.pathSquadsImages}/{skinImage}";
            else item.skinImage = "/ActorIco.png";

            #endregion Qr

           // return View(item);
            //   if (model==null) {return NotFound("Does not exist"); }
            return new ViewAsPdf("Item", item)
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
