using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Server.Interfaces.DAM_FilePreOders
{
    public interface ISerDamCustomer
    {
        Task<Response<string>> SaveCustomerMigration(EntityActor entityActor);
        Task<int> SearchActorMongo(int id);
    }
}
