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
        /// Retrieves an Credit object by Id
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
        /// Insert / Update a Credit Document
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

    }
}
