using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Controllers.InventoryBack;
using SunttelTradePointB.Server.Interfaces.QualityBkServices;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Quality;

namespace SunttelTradePointB.Server.Controllers.QualityBack
{
    /// <summary>
    /// Controller for quality
    /// </summary>
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

        /// <summary>
        /// Retrieves a Quality Parameters matching with its Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="warehouseId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
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
        /// <param name="inventoryId"></param>
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







    }

}
