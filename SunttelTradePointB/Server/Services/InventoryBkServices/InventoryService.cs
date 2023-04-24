using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using SunttelTradePointB.Server.Interfaces.InventoryBkServices;
using SunttelTradePointB.Shared.InvetoryModels;
using MongoDB.Driver;
using SunttelTradePointB.Client.Pages.DirectoryInventory;
using SunttelTradePointB.Server.Controllers.InventoryBack;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Server.Services.InventoryBkServices
{
    /// <summary>
    /// Inventory service
    /// </summary>
    public class InventoryService : IInventory
    {
        /// <summary>
        /// Inventory service
        /// </summary>
        IMongoCollection<InventoryDetail> _InventoryCollection;

        /// <summary>
        /// Constructor
        /// </summary>
        public InventoryService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _InventoryCollection = mongoDatabase.GetCollection<InventoryDetail>("Inventory");

        }

        /// <summary>
        /// Saves an Entity/Actor document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="BuyerId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<InventoryDetail>? InventoryList, string? ErrorDescription)> GetInventory(string userId, string ipAddress, string squadId, string warehouseId, DateTime startDate, DateTime endDate, string BuyerId, int? page = 1, int? perPage = 10, string? filterName = null)
        {
            try
            {
                string filter = filterName == null ? "" : filterName;
                var skip = (page - 1) * perPage;

                if (filter.Length > 0)
                {
                    var pipeline = new List<BsonDocument>();

                    if (filter.ToLower() != "all")
                    {
                        /*
                        if(warehouseId != null)
                        {
                            pipeline.Add(
                                new BsonDocument("$match", new BsonDocument("CurrentWarehouse._id", new ObjectId(warehouseId)))
                            );
                        }
                        */
                        pipeline.Add(
                            new BsonDocument {
                                { "$match",
                                    new BsonDocument{
                                        { "CurrentWarehouse.Name", new BsonDocument("$regex", new BsonRegularExpression($"/{filter}/i")) },
                                        { "EntryDate", new BsonDocument{
                                            { "$gte", startDate },
                                            { "$lte", endDate }
                                        }
                                        }
                                    }
                                }
                            }
                        );
                    }

                    pipeline.Add(
                    new BsonDocument{
                        {"$skip",  skip}
                    }
                    );

                    pipeline.Add(
                        new BsonDocument{
                        {"$limit",  perPage}
                        }
                    );

                    List<InventoryDetail> results = await _InventoryCollection.Aggregate<InventoryDetail>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var inventories = await _InventoryCollection.Find<InventoryDetail>(new BsonDocument()).ToListAsync();

                    if (inventories == null || inventories.Count == 0)
                    {
                        return (false, null, "Unpopulated Inventory");
                    }
                    else
                    {
                        return (true, inventories, null);
                    }
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves a particular Transactional Item Type by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, InventoryDetail? Inventory, string? ErrorDescription)> GetInventoryById(string userId, string ipAddress, string squadId, string inventoryId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(inventoryId)))
                );

                var resultPrev = await _InventoryCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                InventoryDetail result = resultPrev.Select(d => BsonSerializer.Deserialize<InventoryDetail>(d)).ToList()[0];

                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }

        }

        /// <summary>
        /// Inserts / Updates an Inventory Item Type object
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, InventoryDetail? Inventory, string? ErrorDescription)> SaveInventory(string userId, string ipAddress, string squadId, InventoryDetail inventory)
        {
            try
            {
                if (inventory.Id == null)
                {
                    inventory.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterInventory = Builders<InventoryDetail>.Filter.Eq("_id", new ObjectId(inventory.Id));
                var updateInventory = new ReplaceOptions { IsUpsert = true };
                var resultInventory = await _InventoryCollection.ReplaceOneAsync(filterInventory, inventory, updateInventory);

                return (true, inventory, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
    }
}
