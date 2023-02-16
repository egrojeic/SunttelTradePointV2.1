using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.DAM_FilePreOders;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.FilePreOders;

namespace SunttelTradePointB.Server.Services.DAM_FilePreOders
{
    /// <summary>
    /// 
    /// </summary>
    public class SerDamFileEDI : ISerDamFileEDI
    {
        private readonly string SERVER_MONGO;
        private readonly string DATABASE_MONGO;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public SerDamFileEDI(IConfiguration configuration)
        {
            SERVER_MONGO = configuration["SrvMongo"];
            DATABASE_MONGO = configuration["DatabaseMongo"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Response<List<PreOrders>>> getPreOrders()
        {
            List<PreOrders> lsPreOrders = await Task.FromResult(new List<PreOrders>());
            try
            {
                var clientMongo = new MongoClient(SERVER_MONGO);
                var database = clientMongo.GetDatabase(DATABASE_MONGO);
                var tblPreOrders = database.GetCollection<PreOrders>("CargaPreOrders");

                lsPreOrders = tblPreOrders.Find<PreOrders>(d => true).ToList();

                return new Response<List<PreOrders>>(false, "", lsPreOrders); ;
            }
            catch (Exception ex)
            {
                return new Response<List<PreOrders>>(true, ex.Message, lsPreOrders);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myPreOrders"></param>
        /// <returns></returns>
        public async Task<Response<string>> upLoadPreOrders(List<PreOrders> myPreOrders)
        {
            List<PreOrders> lsPreOrders = await Task.FromResult(new List<PreOrders>());
            try
            {
                var clientMongo = new MongoClient(SERVER_MONGO);
                var database = clientMongo.GetDatabase(DATABASE_MONGO);
                var tblPreOrders = database.GetCollection<PreOrders>("CargaPreOrders");
                tblPreOrders.InsertMany(myPreOrders);
                return new Response<string>(false, "", "Successfully Saved Mongo");
            }
            catch (Exception ex)
            {
                return new Response<string>(true, ex.Message, "");
            }
        }



    }
}
