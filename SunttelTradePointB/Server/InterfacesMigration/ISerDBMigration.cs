namespace SunttelTradePointB.Server.InterfacesMigration
{
    public interface ISerDBMigration
    {
        Task<string> MigrateCustomer();
    }
}
