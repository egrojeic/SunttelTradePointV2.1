using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces.StandingOrderBkServices;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Server.Controllers.StandingOrderBack
{
    /// <summary>
    /// Controller for inventory
    /// </summary>
    [Tags("Standing Order operations EndPoints")]
    [Route("api/[controller]/[action]")]
    public class StandingOrderController : ControllerBase
    {
        private IStandingOrder _standingOrder;
        private readonly ILogger<StandingOrderController> _logger;
        IConfiguration config;

        public StandingOrderController(ILogger<StandingOrderController> logger, IConfiguration _config, IStandingOrder standingOrder)
        {
            _logger = logger;
            config = _config;
            _standingOrder = standingOrder;
        }

        #region StandingOrder
        /// <summary>
        /// Returns a list of standing orders with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetStandingOrders")]
        public async Task<IActionResult> GetStandingOrders(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _standingOrder.GetStandingOrders(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.StandingOrdersList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an standing orders object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="standingOrderId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetStandingOrderById")]
        public async Task<IActionResult> GetStandingOrderById(string userId, string ipAddress, string standingOrderId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _standingOrder.GetStandingOrderById(userId, ipAddress, squadId, standingOrderId);

            if (response.IsSuccess)
            {
                return Ok(response.StandingOrder);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an standing orders. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="standing"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveStandingOrder")]
        public async Task<IActionResult> SaveStandingOrder(string userId, string ipAddress, [FromBody] StandingOrder standing)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _standingOrder.SaveStandingOrder(userId, ipAddress, squadId, standing);

            if (response.IsSuccess)
            {
                return Ok(response.StandingOrder);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }
        #endregion

        #region Standing Order Detail
        /// <summary>
        /// Returns a list of standing orders details with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetStandingOrdersDetails")]
        public async Task<IActionResult> GetStandingOrdersDetails(string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filter = null)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _standingOrder.GetStandingOrdersDetails(userId, ipAddress, squadId, page, perPage, filter);

            if (response.IsSuccess)
            {
                return Ok(response.StandingOrdersDetailsList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an standing orders details object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="standingOrderDetailId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetStandingOrderDetailById")]
        public async Task<IActionResult> GetStandingOrderDetailById(string userId, string ipAddress, string standingOrderDetailId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _standingOrder.GetStandingOrderDetailById(userId, ipAddress, squadId, standingOrderDetailId);

            if (response.IsSuccess)
            {
                return Ok(response.StandingOrderDetail);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an standing orders details. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="standing"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveStandingOrderDetail")]
        public async Task<IActionResult> SaveStandingOrderDetail(string userId, string ipAddress, [FromBody] StandingOrderDetails standing)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _standingOrder.SaveStandingOrderDetail(userId, ipAddress, squadId, standing);

            if (response.IsSuccess)
            {
                return Ok(response.StandingOrderDetail);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }
        #endregion

    }
}
