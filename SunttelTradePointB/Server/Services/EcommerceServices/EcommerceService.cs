using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.EcommerceInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.ecommerce;
using MongoDB.Bson;
using System.Runtime.Serialization;

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
                foreach (var image in product.PathImages)
                {
                    image.Id = ObjectId.GenerateNewId().ToString();
                }

                await _EcommerceCollection.InsertOneAsync(product);
            }
            catch (Exception ex)
            {
                resp.IsSuccess = false;
                resp.ErrorDescription = ex.Message;
            }
            return resp;
        }


        /// <summary>
        /// Get All Products
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<Product>>> GetProducts()
        {
            ServiceResponse<List<Product>> resp = new ServiceResponse<List<Product>>();
            try
            {
                var filter = Builders<Product>.Filter.Empty; // Empty filter to retrieve all documents
                var products = await _EcommerceCollection.FindAsync(filter);

                resp.Entity = await products.ToListAsync();
                resp.IsSuccess = true;
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
