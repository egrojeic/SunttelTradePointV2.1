using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.InvetoryModels;

namespace SunttelTradePointB.Client.Interfaces.InventoryInterfaces
{
    public interface TInventory
    {

        /// <summary>
        /// Retrives a list with commercialDocument items meeting search criteria
        /// </summary>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        // Task<List<CommercialDocument>> GetCommercialDocumentList(DateTime startDate, DateTime endDate, string documentTypeId);


        /// <summary>
        /// Save a list with InventoryDetail items 
        /// </summary>
        /// <param name="inventoryDetail"></param>
        /// <returns></returns>
        Task<InventoryDetail> SaveInventoryItem(InventoryDetail inventoryDetail);

        /// <summary>
        /// Save a list with Warehouse items meeting search criteria
        /// </summary>
        /// <param name="nameLike"></param>
        /// <returns></returns>
        Task<List<Warehouse>> GetWarehouseList(string? nameLike = "all");


        /// <summary>
        /// Save a list with InventoryDetail items meeting search criteria
        /// </summary>
        /// <param name="filterName"></para
        /// <param name="documentTypeId"></para>
        /// <param name="documentTypeId"></para>
        Task<List<InventoryDetail>> GetInventoryList(string filterName, string documentTypeId, string DocumentDate, int? page = 1, int? perPage = 10);

        /// <summary>
        /// Save a list with InventoryDetail items meeting search criteria
        /// </summary>
        /// <param name="inventoryId"></para>      
        Task<InventoryDetail> GetInventoryById(string inventoryId);

        /// <summary>
        /// Save a list with TransactionalItem items meeting search criteria
        /// </summary>
        /// <param name="nameLike"></para>      
        /// <param name="page"></para>     
        /// <param name = "perPage" ></ para >
        Task< List<TransactionalItem>> GetTransactionalItemsList(int? page = 1, int? perPage = 10, string? nameLike = null);

        /// <summary>
        /// Save a list with TransactionalItem items meeting search criteria
        /// </summary>
        /// <param name="transactionalItemId"></para>      
        Task<TransactionalItem> GetTransactionalItemById(string? transactionalItemId = null);

        /// <summary>
        /// Save a list with EntityActor items meeting search criteria
        /// </summary>
        /// <param name="nameLike"></para>      
        Task<List<EntityActor>> GetEntityActorFaceList(string? nameLike = null);


        /// <summary>
        /// Save a list with Box items meeting search criteria
        /// </summary>
        /// <param name="filter"></para>      
        Task<List<Box>> GetBoxes(string filter);


        /// <summary>
        /// Save a list with Box items meeting search criteria
        /// </summary>
        /// <param name="filter"></para>      
        Task<List<Box>> GetWarehouses(string filter);

    }

   
}
