using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.QualityBkServices;
using SunttelTradePointB.Server.Interfaces.StandingOrderBkServices;
using SunttelTradePointB.Shared.Quality;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Server.Services.StandingOrderBkServices
{
    /// <summary>
    /// Quality service
    /// </summary>
    /// 
    public class StandingOrderService : IStandingOrder
    {
        /// <summary>
        /// Stading Order service
        /// </summary>
        /// 
        IMongoCollection<StandingOrder> _StandingOrderCollection;
        IMongoCollection<StandingOrderDetails> _StandingOrderDetailsCollection;

        /// <summary>
        /// Constructor
        /// </summary>
        public StandingOrderService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _StandingOrderCollection = mongoDatabase.GetCollection<StandingOrder>("StandingOrders");
            _StandingOrderDetailsCollection = mongoDatabase.GetCollection<StandingOrderDetails>("StandingOrderDetails");

        }

        #region Stading Order
        /// <summary>
        /// Saves an Entity/Actor document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<StandingOrder>? StandingOrdersList, string? ErrorDescription)> GetStandingOrders(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string filterString = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;

                if (filterString.Length > 0)
                {
                    var pipeline = new List<BsonDocument>();

                    if (filterString.ToLower() != "all")
                    {
                        pipeline.Add(
                            new BsonDocument {
                                { "$match",
                                    new BsonDocument{
                                        { "Vendor.Name", new BsonDocument("$regex", new BsonRegularExpression($"/{filter}/i")) }
                                    }
                                }
                            }
                        );
                    }

                    // Filtro por SquadId
                    pipeline.Add(
                        new BsonDocument("$match", new BsonDocument("SquadId", squadId))
                    );

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

                    List<StandingOrder> results = await _StandingOrderCollection.Aggregate<StandingOrder>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var standingOrders = await _StandingOrderCollection.Find<StandingOrder>(new BsonDocument()).ToListAsync();

                    if (standingOrders == null || standingOrders.Count == 0)
                    {
                        return (false, null, "Unpopulated Inventory");
                    }
                    else
                    {
                        return (true, standingOrders, null);
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
        /// <param name="standingOrderId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, StandingOrder? StandingOrder, string? ErrorDescription)> GetStandingOrderById(string userId, string ipAddress, string squadId, string standingOrderId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(standingOrderId)))
                );
                // Filtro por SquadId
                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("SquadId", squadId))
                );

                var resultPrev = await _StandingOrderCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                StandingOrder result = resultPrev.Select(d => BsonSerializer.Deserialize<StandingOrder>(d)).ToList()[0];

                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        /// <summary>
        /// Inserts / Updates an Quanlity Item Type object
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="standing"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, StandingOrder? StandingOrder, string? ErrorDescription)> SaveStandingOrder(string userId, string ipAddress, string squadId, StandingOrder standing)
        {
            try
            {
                if (standing.Id == null)
                {
                    standing.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterStading = Builders<StandingOrder>.Filter.Eq("_id", new ObjectId(standing.Id));
                var updateStading = new ReplaceOptions { IsUpsert = true };
                var resultStading = await _StandingOrderCollection.ReplaceOneAsync(filterStading, standing, updateStading);

                return (true, standing, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion

        #region Stading Order Detail
        /// <summary>
        /// Saves an Entity/Actor document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, List<StandingOrderDetails>? StandingOrdersDetailsList, string? ErrorDescription)> GetStandingOrdersDetails(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a particular Transactional Item Type by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, StandingOrderDetails? StandingOrderDetail, string? ErrorDescription)> GetStandingOrderDetailById(string userId, string ipAddress, string squadId, string standingOrderDetailId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserts / Updates an Quanlity Item Type object
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, StandingOrderDetails? StandingOrderDetail, string? ErrorDescription)> SaveStandingOrderDetail(string userId, string ipAddress, string squadId, StandingOrderDetails standing)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
