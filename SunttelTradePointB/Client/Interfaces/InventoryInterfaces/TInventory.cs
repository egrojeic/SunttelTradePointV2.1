using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.InvetoryModels;

namespace SunttelTradePointB.Client.Interfaces.InventoryInterfaces
{
    public interface TInventory
    {
        /// <summary>
        /// Save a inventoryDetail item 
        /// </summary>
        /// <param name="inventoryDetail"></param>
        /// <returns></returns>   
        Task<InventoryDetail> SaveInventoryItem(InventoryDetail inventoryDetail);



        /// <summary>
        /// Retrives a list with Warehouse items meeting search criteria
        /// </summary>
        /// <param name="nameLike"></param>       
        /// <returns></returns>
        Task<List<Warehouse>> GetWarehouseList(string? nameLike = "all");

        /// <summary>
        /// Retrives a list with InventoryDetail items meeting search criteria
        /// </summary>
        /// <param name="filterName"></param>       
        /// <param name="documentTypeId"></param>       
        /// <param name="statrDate"></param>       
        /// <param name="page"></param>       
        /// <param name="perPage"></param>       
        /// <returns></returns>
        Task<List<InventoryDetail>> GetInventoryList(string filterName, string documentTypeId, DateTime statrDate, int? page = 1, int? perPage = 10);



        /// <summary>
        /// Retrives a item with inventoryDetail meeting search criteria
        /// </summary>
        /// <param name="inventoryId"></param>           
        /// <returns></returns>
        Task<InventoryDetail> GetInventoryById(string inventoryId);



        /// <summary>
        /// Retrives a item with TransactionalItem meeting search criteria
        /// </summary>
        /// <param name="transactionalItemId"></param>           
        /// <returns></returns>
        Task<TransactionalItem> GetTransactionalItemById(string? transactionalItemId = null);


        /// <summary>
        /// Retrives a item with EntityActor meeting search criteria
        /// </summary>
        /// <param name="nameLike"></param>           
        /// <returns></returns>
        Task<List<EntityActor>> GetEntityActorFaceList(string? nameLike = null);


        /// <summary>
        /// Retrives a list with Box items meeting search criteria
        /// </summary>
        /// <param name="filter"></param>           
        /// <returns></returns>
        Task<List<Box>> GetBoxes(string filter);

      



    }


}
