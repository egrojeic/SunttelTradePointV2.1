using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces.PaymentBkServices;
using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.InvetoryModels;

namespace SunttelTradePointB.Server.Controllers.PaymentBack
{
    /// <summary>
    /// Controller for inventory
    /// </summary>
    [Tags("Inventory operations EndPoints")]
    [Route("api/[controller]/[action]")]
    public class PaymentController : ControllerBase
    {
        private IPayment _payment;
        private readonly ILogger<PaymentController> _logger;
        IConfiguration config;

        public PaymentController(ILogger<PaymentController> logger, IConfiguration _config, IPayment payment)
        {
            _logger = logger;
            config = _config;
            _payment = payment;
        }

        #region Payment
        /// <summary>
        /// Returns a list of payments with a filter like the parameter and date
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="PaymentDate"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetPaymentsByDateSpan")]
        public async Task<IActionResult> GetPaymentsByDateSpan(string userId, string ipAddress, DateTime PaymentDate, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _payment.GetPaymentsByDateSpan(userId, ipAddress, squadId, PaymentDate, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.PaymentList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an payment object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetPaymentById")]
        public async Task<IActionResult> GetPaymentById(string userId, string ipAddress, string paymentId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _payment.GetPaymentById(userId, ipAddress, squadId, paymentId);

            if (response.IsSuccess)
            {
                return Ok(response.Payment);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an payment. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SavePayment")]
        public async Task<IActionResult> SavePayment(string userId, string ipAddress, [FromBody] Payment payment)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _payment.SavePayment(userId, ipAddress, squadId, payment);

            if (response.IsSuccess)
            {
                return Ok(response.Payment);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }
        #endregion

        #region PaymentMode
        /// <summary>
        /// Returns a list of doc payment modes with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetDocPaymentModes")]
        public async Task<IActionResult> GetDocPaymentModes(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _payment.GetDocPaymentModes(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.PaymentModesList);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrieves an doc payment mode object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="paymentModeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetDocPaymentModeById")]
        public async Task<IActionResult> GetDocPaymentModeById(string userId, string ipAddress, string paymentModeId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _payment.GetDocPaymentModeById(userId, ipAddress, squadId, paymentModeId);

            if (response.IsSuccess)
            {
                return Ok(response.PaymentMode);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an doc payment mode. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="paymentMode"></param>
        /// <param name="inventory"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveDocPaymentMode")]
        public async Task<IActionResult> SaveDocPaymentMode(string userId, string ipAddress, [FromBody] PaymentMode paymentMode)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _payment.SaveDocPaymentMode(userId, ipAddress, squadId, paymentMode);

            if (response.IsSuccess)
            {
                return Ok(response.PaymentMode);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        #endregion

        #region PaymentVia
        /// <summary>
        /// Returns a list of payment vias with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetDocPaymentVias")]
        public async Task<IActionResult> GetDocPaymentVias(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _payment.GetDocPaymentVias(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.PaymentViaList);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrieves an payment via object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="paymentViaId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetDocPaymentViaById")]
        public async Task<IActionResult> GetDocPaymentViaById(string userId, string ipAddress, string paymentViaId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _payment.GetDocPaymentViaById(userId, ipAddress, squadId, paymentViaId);

            if (response.IsSuccess)
            {
                return Ok(response.PaymentVia);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an payment via. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="paymentVia"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveDocPaymentVia")]
        public async Task<IActionResult> SaveDocPaymentVia(string userId, string ipAddress, [FromBody] PaymentVia paymentVia)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _payment.SaveDocPaymentVia(userId, ipAddress, squadId, paymentVia);

            if (response.IsSuccess)
            {
                return Ok(response.PaymentVia);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }
        #endregion

        #region Payment Type
        /// <summary>
        /// Returns a list of payment type with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetPaymentTypes")]
        public async Task<IActionResult> GetPaymentDocumentTypes(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _payment.GetPaymentDocumentTypes(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.PaymentTypesList);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrieves an payment type object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetPaymentTypeById")]
        public async Task<IActionResult> GetPaymentTypeById(string userId, string ipAddress, string paymentId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _payment.GetPaymentTypeById(userId, ipAddress, squadId, paymentId);

            if (response.IsSuccess)
            {
                return Ok(response.PaymentType);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an payment type. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="paymentType"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SavePaymentType")]
        public async Task<IActionResult> SavePaymentDocumentType(string userId, string ipAddress, [FromBody] PaymentType paymentType)
        {
            var squadId = paymentType.SquadId;

            var response = await _payment.SavePaymentType(userId, ipAddress, squadId, paymentType);

            if (response.IsSuccess)
            {
                return Ok(response.PaymentType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }
        #endregion

        #region Payment Status
        /// <summary>
        /// Returns a list of payment status with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetPaymentStatuses")]
        public async Task<IActionResult> GetPaymentStatuses(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _payment.GetPaymentStatuses(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.PaymentStatusesList);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrieves an payment status object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetPaymentStatusById")]
        public async Task<IActionResult> GetPaymentStatusById(string userId, string ipAddress, string paymentId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _payment.GetPaymentStatusById(userId, ipAddress, squadId, paymentId);

            if (response.IsSuccess)
            {
                return Ok(response.PaymentStatus);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an payment status. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SavePaymentStatus")]
        public async Task<IActionResult> SavePaymentStatus(string userId, string ipAddress, [FromBody] PaymentStatus paymentStatus)
        {
            var squadId = paymentStatus.SquadId;

            var response = await _payment.SavePaymentStatus(userId, ipAddress, squadId, paymentStatus);

            if (response.IsSuccess)
            {
                return Ok(response.PaymentStatus);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }
        #endregion
    }
}
