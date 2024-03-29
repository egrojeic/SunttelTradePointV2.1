﻿using SunttelTradePointB.Shared.InvetoryModels;

namespace SunttelTradePointB.Server.Interfaces.InventoryBkServices
{
    /// <summary>
    /// Interface of service to manage inventory
    /// </summary>
    public interface IInventory
    {
        /// <summary>
        /// Retrieves a document having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<InventoryDetail>? InventoryList, string? ErrorDescription)> GetInventory(string userId, string ipAddress, string squadId, string warehouseId, DateTime startDate, DateTime endDate, int? page = 1, int? perPage = 10, string? filterName = null, string? customerName = null, string? itemName = null, string? boxCode = null);

        /// <summary>
        /// Retrieves a inventory having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, InventoryDetail? Inventory, string? ErrorDescription)> GetInventoryById(string userId, string ipAddress, string squadId, string inventoryId);

        /// <summary>
        /// Insert/ Updates a Inventory Type of the corresponding id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="inventory"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, InventoryDetail? Inventory, string? ErrorDescription)> SaveInventory(string userId, string ipAddress, string squadId, InventoryDetail inventory);

    }

}
