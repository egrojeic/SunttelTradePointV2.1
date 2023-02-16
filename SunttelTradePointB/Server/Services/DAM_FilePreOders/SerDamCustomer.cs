using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.DAM_FilePreOders;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.FilePreOders;

namespace SunttelTradePointB.Server.Services.DAM_FilePreOders
{
    /// <summary>
    /// 
    /// </summary>
    public class SerDamCustomer : ISerDamCustomer
    {
        private readonly string SERVER_MONGO;
        private readonly string DATABASE_MONGO;
        private readonly MongoClient clientMongo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public SerDamCustomer(IConfiguration configuration)
        {
            SERVER_MONGO = configuration["SrvMongo"];
            DATABASE_MONGO = configuration["DatabaseMongo"];
            clientMongo = new MongoClient(configuration.GetConnectionString("CadenaMongo"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityActor"></param>
        /// <returns></returns>
        public async Task<Response<string>> SaveCustomerMigration(EntityActor entityActor)
        {
            try
            {
                var clientMongo = new MongoClient(SERVER_MONGO);
                var database = clientMongo.GetDatabase(DATABASE_MONGO);
                var tblPreOrders = database.GetCollection<EntityActor>("EntityActor");
                tblPreOrders.InsertOne(entityActor);
                return new Response<string>(false, "", "Successfully Saved Mongo");
            }
            catch (Exception ex)
            {
                return new Response<string>(true, ex.Message, "");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> SearchActorMongo(int id)
        {
            try
            {
                //var clientMongo = new MongoClient(SERVER_MONGO);
                //var database = clientMongo.GetDatabase(DATABASE_MONGO);
                //var tblActor = database.GetCollection<EntityActor>("EntityActor");

                //var resp = await Task.FromResult(tblActor.Find(x => x.LegacyId == id).ToList());

                //return resp.Count;
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
