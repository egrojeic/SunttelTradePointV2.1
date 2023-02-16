namespace MigrationProcess.Server.ServiceSQL
{
    public interface ISerCustomer
    {
        Task<string> Migrate();
    }
}
