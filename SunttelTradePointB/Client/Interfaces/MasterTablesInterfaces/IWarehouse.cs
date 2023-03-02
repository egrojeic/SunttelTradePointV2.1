using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces
{
    public interface IWarehouse
    {
        Task<Warehouse> GetWarehouseById(string warehouseId);

        Task<List<Warehouse>> GetWarehouseList(string filterWarehouse);

        Task<bool> SaveWarehuse(Warehouse warehouse);
    }
}
