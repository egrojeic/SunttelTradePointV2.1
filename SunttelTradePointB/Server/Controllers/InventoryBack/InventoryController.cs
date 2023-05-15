using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Controllers.SalesBack;
using SunttelTradePointB.Server.Interfaces.InventoryBkServices;
using SunttelTradePointB.Server.Interfaces.SalesBkServices;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Server.Controllers.InventoryBack
{

    /// <summary>
    /// Controller for inventory
    /// </summary>
    [Tags("Inventory operations EndPoints")]
    [Route("api/[controller]/[action]")]
    public class InventoryController : ControllerBase
    {
        private IInventory _inventory;
        private readonly ILogger<InventoryController> _logger;
        IConfiguration config;

        public InventoryController(ILogger<InventoryController> logger, IConfiguration _config, IInventory inventory)
        {
            _logger = logger;
            config = _config;
            _inventory = inventory;
        }

        /// <summary>
        /// Returns a list of inventories with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="warehouseId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterName"></param>
        /// <param name="customerName"></param>
        /// <param name="itemName"></param>
        /// <param name="boxCode"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetInventory")]
        public async Task<IActionResult> GetInventory(string userId, string ipAddress, string warehouseId, DateTime startDate, DateTime endDate, int? page = 1, int? perPage = 10, string? filterName = null, string? customerName = null, string? itemName = null, string? boxCode = null )
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _inventory.GetInventory(userId, ipAddress, squadId, warehouseId, startDate, endDate, page, perPage, filterName, customerName, itemName, boxCode);

            if (response.IsSuccess)
            {
                return Ok(response.InventoryList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an inventory object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetInventoryById")]
        public async Task<IActionResult> GetInventoryById(string userId, string ipAddress, string inventoryId)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _inventory.GetInventoryById(userId, ipAddress, squadId, inventoryId);

            if (response.IsSuccess)
            {
                return Ok(response.Inventory);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an inventory. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="inventory"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveInventory")]
        public async Task<IActionResult> SaveInventory(string userId, string ipAddress, [FromBody] InventoryDetail inventory)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _inventory.SaveInventory(userId, ipAddress, squadId, inventory);

            if (response.IsSuccess)
            {
                return Ok(response.Inventory);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


    }
}
