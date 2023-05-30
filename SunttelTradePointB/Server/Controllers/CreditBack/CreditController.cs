using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces.CreditBkServices;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.InvetoryModels;
using System.Net;

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
        /// <param name="isAsale"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCreditDocuments")]
        public async Task<IActionResult> GetCreditDocuments(string userId, string ipAddress, DateTime startDate, DateTime endDate,bool isAsale, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _credit.GetCreditDocuments(userId, ipAddress, squadId, startDate, endDate,isAsale, page, perPage, filter);

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

            (bool IsSuccess, CreditDocument? CreditDocument, string? ErrorDescription) response = await _credit.SaveCreditDocument(userId, ipAddress, squadId, creditDocument);

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
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _credit.GetCreditTypes(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.CreditTypesList);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Delete a CreditType if not associated with a CreditDocument
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="creditTypeId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteCreditTypeById")]
        public async Task<IActionResult> DeleteCreditTypeById(string userId, string ipAddress, string creditTypeId)
        {
            try
            {
                var customHeaderValue = Request.Headers["SquadId"];
                var squadId = customHeaderValue.ToString() ?? "";
                (bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription) response = await _credit.DeleteCreditTypeById(userId, ipAddress, squadId, creditTypeId);
                if (response.IsSuccess)
                {
                    if (response.iCanRemoveIt)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound("Type in use");
                    }
                }
                else
                {
                    return NotFound(response.ErrorDescription);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
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
            // Setear Squad Id
            creditType.SquadId = creditType.SquadId.ToUpper();
            var squadId = creditType.SquadId;

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


        /// <summary>
        /// Delete a CreditStatus if not associated with a CreditDocument
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="creditStatusId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteCreditStatusById")]
        public async Task<IActionResult> DeleteCreditStatusById(string userId, string ipAddress, string creditStatusId)
        {
            try
            {
                var customHeaderValue = Request.Headers["SquadId"];
                var squadId = customHeaderValue.ToString() ?? "";
                (bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription) response = await _credit.DeleteCreditStatusById(userId, ipAddress, squadId, creditStatusId);
                if (response.IsSuccess)
                {
                    if (response.iCanRemoveIt)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound("Status in use");
                    }
                }
                else
                {
                    return NotFound(response.ErrorDescription);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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


        /// <summary>
        /// Delete a CreditReason if not associated with a CreditDocument
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="reasonId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteCreditReasonById")]
        public async Task<IActionResult> DeleteCreditReasonById(string userId, string ipAddress, string reasonId)
        {
            try
            {
                var customHeaderValue = Request.Headers["SquadId"];
                var squadId = customHeaderValue.ToString() ?? "";
                (bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription) response = await _credit.DeleteCreditReasonsById(userId, ipAddress, squadId, reasonId);
                if (response.IsSuccess)
                {
                    if (response.iCanRemoveIt)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound("Reason in use");
                    }
                }
                else
                {
                    return NotFound(response.ErrorDescription);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        #endregion

        /// <summary>
        /// Upload file csv a products
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveProductsCSV")]
        public async Task<IActionResult> SaveProductsCSV(string userId, string ipAddress, IFormFile file)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _credit.SaveProductsCSV(userId, ipAddress, squadId, file);

            if (response.IsSuccess)
            {
                return Ok(response.ActorsNodesList);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

    }
}
