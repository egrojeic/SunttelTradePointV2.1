using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces.CreditBkServices;
using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.InvetoryModels;

namespace SunttelTradePointB.Server.Controllers.CreditBack
{
    /// <summary>
    /// Controller for credit
    /// </summary>
    [Tags("Credit operations EndPoints")]
    [Route("api/[controller]/[action]")]
    public class CreditController : ControllerBase
    {
        private ICredit _credit;
        private readonly ILogger<CreditController> _logger;
        IConfiguration config;
        public CreditController(ILogger<CreditController> logger, IConfiguration _config, ICredit credit)
        {
            _logger = logger;
            config = _config;
            _credit = credit;
        }
        #region Credit Documents
        /// <summary>
        /// Returns a list of credit documents with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCreditDocuments")]
        public async Task<IActionResult> GetCreditDocuments(string userId, string ipAddress, DateTime startDate, DateTime endDate, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _credit.GetCreditDocuments(userId, ipAddress, squadId, startDate, endDate, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.CreditDocumentsList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an credit document object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="creditId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCreditDocumentById")]
        public async Task<IActionResult> GetCreditDocumentById(string userId, string ipAddress, string creditId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _credit.GetCreditDocumentById(userId, ipAddress, squadId, creditId);

            if (response.IsSuccess)
            {
                return Ok(response.CreditDocument);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an credit document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="creditDocument"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveCreditDocument")]
        public async Task<IActionResult> SaveCreditDocument(string userId, string ipAddress, [FromBody] CreditDocument creditDocument)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _credit.SaveCreditDocument(userId, ipAddress, squadId, creditDocument);

            if (response.IsSuccess)
            {
                return Ok(response.CreditDocument);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        #endregion

        #region Credit Types
        /// <summary>
        /// Returns a list of credit types with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCreditTypes")]
        public async Task<IActionResult> GetCreditTypes(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _credit.GetCreditTypes(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.CreditTypesList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an credit type object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="creditTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCreditTypeById")]
        public async Task<IActionResult> GetCreditTypeById(string userId, string ipAddress, string creditTypeId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _credit.GetCreditTypeById(userId, ipAddress, squadId, creditTypeId);

            if (response.IsSuccess)
            {
                return Ok(response.CreditType);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an credit type. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="creditType"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveCreditType")]
        public async Task<IActionResult> SaveCreditType(string userId, string ipAddress, [FromBody] CreditType creditType)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _credit.SaveCreditType(userId, ipAddress, squadId, creditType);

            if (response.IsSuccess)
            {
                return Ok(response.CreditType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        #endregion

        #region Credit Status
        /// <summary>
        /// Returns a list of entities with a name like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCreditStatuses")]
        public async Task<IActionResult> GetCreditStatuses(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _credit.GetCreditStatuses(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.CreditStatusesList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an Credit Type object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="creditStatusById"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("CreditStatusById")]
        public async Task<IActionResult> CreditStatusById(string userId, string ipAddress, string creditStatusById)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _credit.CreditStatusById(userId, ipAddress, squadId, creditStatusById);

            if (response.IsSuccess)
            {
                return Ok(response.CreditStatus);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Insert / Update a Credit Status
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="creditStatus"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveCreditStatus")]
        public async Task<IActionResult> SaveCreditStatus(string userId, string ipAddress, [FromBody] CreditStatus creditStatus)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _credit.SaveCreditStatus(userId, ipAddress, squadId, creditStatus);

            if (response.IsSuccess)
            {
                return Ok(response.CreditStatus);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        #endregion

        #region Credit Reason
        /// <summary>
        /// Returns a list of entities with a name like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCreditReasons")]
        public async Task<IActionResult> GetCreditReasons(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _credit.GetCreditReasons(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.CreditReasonsList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an Credit Type object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="creditReasonById"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("CreditReasonById")]
        public async Task<IActionResult> CreditReasonById(string userId, string ipAddress, string creditReasonById)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _credit.CreditReasonById(userId, ipAddress, squadId, creditReasonById);

            if (response.IsSuccess)
            {
                return Ok(response.CreditReason);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Insert / Update a Credit Reason
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="creditReason"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveCreditReason")]
        public async Task<IActionResult> SaveCreditReason(string userId, string ipAddress, [FromBody] CreditReason creditReason)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _credit.SaveCreditReason(userId, ipAddress, squadId, creditReason);

            if (response.IsSuccess)
            {
                return Ok(response.CreditReason);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        #endregion
    }
}
