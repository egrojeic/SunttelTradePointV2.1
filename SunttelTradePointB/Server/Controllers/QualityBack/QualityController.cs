using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Controllers.InventoryBack;
using SunttelTradePointB.Server.Interfaces.QualityBkServices;
using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.ImportingData;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Quality;
using Syncfusion.Blazor.Grids;

namespace SunttelTradePointB.Server.Controllers.QualityBack
{
    /// <summary>
    /// Controller for inventory
    /// </summary>
    [Tags("Quality operations EndPoints")]
    [Route("api/[controller]/[action]")]
    public class QualityController : ControllerBase
    {
        private IQuality _quality;
        private readonly ILogger<QualityController> _logger;
        IConfiguration config;

        public QualityController(ILogger<QualityController> logger, IConfiguration _config, IQuality quality)
        {
            _logger = logger;
            config = _config;
            _quality = quality;
        }


        #region Quality Parameters
        /// <summary>
        /// Returns a list of quality parameters with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetQualityParameters")]
        public async Task<IActionResult> GetQualityParameters(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filterName = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityParameters(userId, ipAddress, squadId, page, perPage, filterName);

            if (response.IsSuccess)
            {
                return Ok(response.QualityParametersList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an quality parameters object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetQualityParameteById")]
        public async Task<IActionResult> GetQualityParameteById(string userId, string ipAddress, string qualityId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityParameteById(userId, ipAddress, squadId, qualityId);

            if (response.IsSuccess)
            {
                return Ok(response.QualityParameter);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an quality parameters. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveQualityParameter")]
        public async Task<IActionResult> SaveQualityParameter(string userId, string ipAddress, [FromBody] QualityAssuranceParameter quality)
        {
            // Setear Squad Id
            quality.SquadId = quality.SquadId.ToUpper();
            var squadId = quality.SquadId;

            var response = await _quality.SaveQualityParameter(userId, ipAddress, squadId, quality);

            if (response.IsSuccess)
            {
                return Ok(response.QualityParameter);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

         /// <summary>
        /// Delete a QualityTrafficLight if not associated with a Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="qualityParameterId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteQualityAssuranceParameterById")]
        public async Task<IActionResult> DeleteQualityAssuranceParameterById(string userId, string ipAddress, string qualityParameterId)
        {
            try
            {
                var customHeaderValue = Request.Headers["SquadId"];
                var squadId = customHeaderValue.ToString() ?? "";
                (bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription) response = await _quality.DeleteQualityAssuranceParameterById(userId, ipAddress, squadId, qualityParameterId);
                if (response.IsSuccess)
                {
                    if (response.iCanRemoveIt)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound("in use");
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

        #region Quality Groups
        /// <summary>
        /// Returns a list of quality parameters groups with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetQualityParametersGroups")]
        public async Task<IActionResult> GetQualityParametersGroups(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityParametersGroups(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.QualityGroupsList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an quality parameters groups object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetQualityParameteGroupsById")]
        public async Task<IActionResult> GetQualityParameteGroupsById(string userId, string ipAddress, string qualityId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityParameteGroupsById(userId, ipAddress, squadId, qualityId);

            if (response.IsSuccess)
            {
                return Ok(response.QualityGroup);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an quality parameters groups. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveQualityParameterGroups")]
        public async Task<IActionResult> SaveQualityParameterGroups(string userId, string ipAddress, [FromBody] QualityParameterGroup quality)
        {
            // Setear Squad Id
            quality.SquadId = quality.SquadId.ToUpper();
            var squadId = quality.SquadId;

            var response = await _quality.SaveQualityParameterGroups(userId, ipAddress, squadId, quality);

            if (response.IsSuccess)
            {
                return Ok(response.QualityGroup);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

         /// <summary>
        /// Delete a QualityTrafficLight if not associated with a Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="qualityParameterGroupId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteQualityParameterGroupById")]
        public async Task<IActionResult> DeleteQualityParameterGroupById(string userId, string ipAddress, string qualityParameterGroupId)
        {
            try
            {
                var customHeaderValue = Request.Headers["SquadId"];
                var squadId = customHeaderValue.ToString() ?? "";
                (bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription) response = await _quality.DeleteQualityParameterGroupById(userId, ipAddress, squadId, qualityParameterGroupId);
                if (response.IsSuccess)
                {
                    if (response.iCanRemoveIt)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound("in use");
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

        #region Quality Traffic Light
        /// <summary>
        /// Returns a list of quality traffic lights with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetQualityTrafficLights")]
        public async Task<IActionResult> GetQualityTrafficLights(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityTrafficLights(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.QualityTrafficLightsList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an quality traffic light object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetQualityTrafficLightById")]
        public async Task<IActionResult> GetQualityTrafficLightById(string userId, string ipAddress, string qualityId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityTrafficLightById(userId, ipAddress, squadId, qualityId);

            if (response.IsSuccess)
            {
                return Ok(response.QualityTrafficLight);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an quality traffic light. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveQualityTrafficLight")]
        public async Task<IActionResult> SaveQualityTrafficLight(string userId, string ipAddress, [FromBody] QualityTrafficLight quality)
        {
            // Setear Squad Id
            quality.SquadId = quality.SquadId.ToUpper();
            var squadId = quality.SquadId;

            var response = await _quality.SaveQualityTrafficLight(userId, ipAddress, squadId, quality);

            if (response.IsSuccess)
            {
                return Ok(response.QualityTrafficLight);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        
        /// <summary>
        /// Delete a QualityTrafficLight if not associated with a Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="qualityTrafficLightId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteQualityTrafficLightById")]
        public async Task<IActionResult> DeleteQualityTrafficLightById(string userId, string ipAddress, string qualityTrafficLightId)
        {
            try
            {
                var customHeaderValue = Request.Headers["SquadId"];
                var squadId = customHeaderValue.ToString() ?? "";
                (bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription) response = await _quality.DeleteQualityTrafficLightById(userId, ipAddress, squadId, qualityTrafficLightId);
                if (response.IsSuccess)
                {
                    if (response.iCanRemoveIt)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound("in use");
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

        #region Quality Action
        /// <summary>
        /// Returns a list of quality actions with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetQualityActions")]
        public async Task<IActionResult> GetQualityActions(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityActions(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.QualityActionsList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an quality action object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetQualityActionById")]
        public async Task<IActionResult> GetQualityActionById(string userId, string ipAddress, string qualityId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityActionById(userId, ipAddress, squadId, qualityId);

            if (response.IsSuccess)
            {
                return Ok(response.QualityAction);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an quality action. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveQualityAction")]
        public async Task<IActionResult> SaveQualityAction(string userId, string ipAddress, [FromBody] QualityAction quality)
        {
            // Setear Squad Id
            quality.SquadId = quality.SquadId.ToUpper();
            var squadId = quality.SquadId;

            var response = await _quality.SaveQualityAction(userId, ipAddress, squadId, quality);

            if (response.IsSuccess)
            {
                return Ok(response.QualityAction);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        /// <summary>
        /// Delete a QualityReportType if not associated with a Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="qualityActioId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteQualityActionById")]
        public async Task<IActionResult> DeleteQualityActionById(string userId, string ipAddress, string qualityActioId)
        {
            try
            {
                var customHeaderValue = Request.Headers["SquadId"];
                var squadId = customHeaderValue.ToString() ?? "";
                (bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription) response = await _quality.DeleteQualityActionById(userId, ipAddress, squadId, qualityActioId);
                if (response.IsSuccess)
                {
                    if (response.iCanRemoveIt)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound("in use");
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

        #region Quality Type
        /// <summary>
        /// Returns a list of quality report types with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetQualityReportTypes")]
        public async Task<IActionResult> GetQualityReportTypes(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityReportTypes(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.GetQualityReportTypesList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an quality report type object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetQualityReportTypeById")]
        public async Task<IActionResult> GetQualityReportTypeById(string userId, string ipAddress, string qualityId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityReportTypeById(userId, ipAddress, squadId, qualityId);

            if (response.IsSuccess)
            {
                return Ok(response.QualityReportType);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an quality report type. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveQualityReportType")]
        public async Task<IActionResult> SaveQualityReportType(string userId, string ipAddress, [FromBody] QualityReportType quality)
        {
            // Setear Squad Id
            quality.SquadId = quality.SquadId.ToUpper();
            var squadId = quality.SquadId;

            var response = await _quality.SaveQualityReportType(userId, ipAddress, squadId, quality);

            if (response.IsSuccess)
            {
                return Ok(response.QualityReportType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        /// <summary>
        /// Delete a QualityReportType if not associated with a Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="paymentModeid"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteQualityReportTypeById")]
        public async Task<IActionResult> DeleteQualityReportTypeById(string userId, string ipAddress, string qualityReportTypeId)
        {
            try
            {
                var customHeaderValue = Request.Headers["SquadId"];
                var squadId = customHeaderValue.ToString() ?? "";
                (bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription) response = await _quality.DeleteQualityReportTypeById(userId, ipAddress, squadId, qualityReportTypeId);
                if (response.IsSuccess)
                {
                    if (response.iCanRemoveIt)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound("in use");
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

        #region QCDocuments
        /// <summary>
        /// Returns a list of quality documents with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetQCDocuments")]
        public async Task<IActionResult> GetQCDocuments(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQCDocuments(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.GetQCDocumentsList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an quality report type object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetQCDocumentById")]
        public async Task<IActionResult> GetQCDocumentById(string userId, string ipAddress, string qualityId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQCDocumentById(userId, ipAddress, squadId, qualityId);

            if (response.IsSuccess)
            {
                return Ok(response.QCDocument);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an quality report type. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveQCDocument")]
        public async Task<IActionResult> SaveQCDocument(string userId, string ipAddress, [FromBody] QualityEvaluation quality)
        {
            // Setear Squad Id
            quality.SquadId = quality.SquadId.ToUpper();
            var squadId = quality.SquadId;

            var response = await _quality.SaveQCDocument(userId, ipAddress, squadId, quality);

            if (response.IsSuccess)
            {
                return Ok(response.QCDocument);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }
        #endregion
    }

}
