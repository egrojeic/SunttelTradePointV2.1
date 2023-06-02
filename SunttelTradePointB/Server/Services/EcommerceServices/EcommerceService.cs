using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.EcommerceInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.ecommerce;
using SunttelTradePointB.Shared.InvetoryModels;

namespace SunttelTradePointB.Server.Services.EcommerceServices
{
    /// <summary>
    /// Service of Ecommerce
    /// </summary>
    public class EcommerceService : IEcommerce
    {
        /// <summary>
        /// Inventory service
        /// </summary>
        IMongoCollection<Product> _EcommerceCollection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        public EcommerceService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _EcommerceCollection = mongoDatabase.GetCollection<Product>("EcommerceProducts");
        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<Product>> AddProduct(Product product)
        {
            ServiceResponse<Product> resp = new ServiceResponse<Product>();
            try
            {
                await _EcommerceCollection.InsertOneAsync(product);
            }
            catch (Exception ex)
            {
                resp.IsSuccess = false;
                resp.ErrorDescription = ex.Message;
            }
            return resp;
        }
    }
}
