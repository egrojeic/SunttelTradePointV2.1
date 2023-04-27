using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Controllers.InventoryBack;
using SunttelTradePointB.Server.Interfaces.QualityBkServices;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Quality;

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
        /// Retrieves a Quality Parameters matching with its Id
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
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityParameters(userId, ipAddress, squadId, page, perPage, filterName);

            if (response.IsSuccess)
            {
                return Ok(response.QualityParametersList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves a Quality Paramete matching with its Id
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
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityParameteById(userId, ipAddress, squadId, qualityId);

            if (response.IsSuccess)
            {
                return Ok(response.QualityParameter);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Insert / Update a Quality Parameters
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveQualityParameter")]
        public async Task<IActionResult> SaveQualityParameter(string userId, string ipAddress, [FromBody] QualityAssuranceParameter quality)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

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
        #endregion

        #region Quality Groups
        /// <summary>
        /// Retrieves a Quality Groups matching with its Id
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
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityParametersGroups(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.QualityGroupsList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves a Quality Groups matching with its Id
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
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityParameteGroupsById(userId, ipAddress, squadId, qualityId);

            if (response.IsSuccess)
            {
                return Ok(response.QualityGroup);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Insert / Update a Quality Parameters
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveQualityParameterGroups")]
        public async Task<IActionResult> SaveQualityParameterGroups(string userId, string ipAddress, [FromBody] QualityParameterGroup quality)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

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

        #endregion

        #region Quality Traffic Light
        /// <summary>
        /// Retrieves a Quality Action matching with its Id
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
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityTrafficLights(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.QualityTrafficLightsList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves a Quality Paramete matching with its Id
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
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityTrafficLightById(userId, ipAddress, squadId, qualityId);

            if (response.IsSuccess)
            {
                return Ok(response.QualityTrafficLight);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Insert / Update a Quality Parameters
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveQualityTrafficLight")]
        public async Task<IActionResult> SaveQualityTrafficLight(string userId, string ipAddress, [FromBody] QualityTrafficLight quality)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

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
        #endregion

        #region Quality Action
        /// <summary>
        /// Retrieves a Quality Action matching with its Id
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
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityActions(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.QualityActionsList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves a Quality Paramete matching with its Id
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
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _quality.GetQualityActionById(userId, ipAddress, squadId, qualityId);

            if (response.IsSuccess)
            {
                return Ok(response.QualityAction);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Insert / Update a Quality Parameters
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveQualityAction")]
        public async Task<IActionResult> SaveQualityAction(string userId, string ipAddress, [FromBody] QualityAction quality)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

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
        #endregion


    }

}
