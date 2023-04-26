using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.QualityBkServices;
using SunttelTradePointB.Shared.InvetoryModels;
using SunttelTradePointB.Shared.Quality;

namespace SunttelTradePointB.Server.Services.QualityBkServices
{
    /// <summary>
    /// Quality service
    /// </summary>
    /// 
    public class QualityService : IQuality
    {
        /// <summary>
        /// Quality service
        /// </summary>
        /// 
        IMongoCollection<QualityAssuranceParameter> _QualityParameterCollection;
        IMongoCollection<QualityParameterGroup> _QualityGroupCollection;

        /// <summary>
        /// Constructor
        /// </summary>
        public QualityService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _QualityParameterCollection = mongoDatabase.GetCollection<QualityAssuranceParameter>("QualityParameters");
            _QualityGroupCollection = mongoDatabase.GetCollection<QualityParameterGroup>("QualityParameterGroups");

        }

        #region Quality Parameters
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
        public async Task<(bool IsSuccess, List<QualityAssuranceParameter>? QualityParametersList, string? ErrorDescription)> GetQualityParameters(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filterName = null)
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
                        pipeline.Add(
                            new BsonDocument {
                                { "$match",
                                    new BsonDocument{
                                        { "Name", new BsonDocument("$regex", new BsonRegularExpression($"/{filter}/i")) }
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

                    List<QualityAssuranceParameter> results = await _QualityParameterCollection.Aggregate<QualityAssuranceParameter>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var qualityParameters = await _QualityParameterCollection.Find<QualityAssuranceParameter>(new BsonDocument()).ToListAsync();

                    if (qualityParameters == null || qualityParameters.Count == 0)
                    {
                        return (false, null, "Unpopulated Inventory");
                    }
                    else
                    {
                        return (true, qualityParameters, null);
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
        /// <param name="qualityId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, QualityAssuranceParameter? QualityParameter, string? ErrorDescription)> GetQualityParameteById(string userId, string ipAddress, string squadId, string qualityId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(qualityId)))
                );
                // Filtro por SquadId
                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("SquadId", squadId))
                );

                var resultPrev = await _QualityParameterCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                QualityAssuranceParameter result = resultPrev.Select(d => BsonSerializer.Deserialize<QualityAssuranceParameter>(d)).ToList()[0];

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
        /// <param name="quality"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, QualityAssuranceParameter? QualityParameter, string? ErrorDescription)> SaveQualityParameter(string userId, string ipAddress, string squadId, QualityAssuranceParameter quality)
        {
            try
            {
                if (quality.Id == null)
                {
                    quality.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterQuantity = Builders<QualityAssuranceParameter>.Filter.Eq("_id", new ObjectId(quality.Id));
                var updateQuantity = new ReplaceOptions { IsUpsert = true };
                var resultQuantity = await _QualityParameterCollection.ReplaceOneAsync(filterQuantity, quality, updateQuantity);

                return (true, quality, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion

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
        public async Task<(bool IsSuccess, List<QualityParameterGroup>? QualityGroupsList, string? ErrorDescription)> GetQualityParametersGroups(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null)
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
                                        { "Name", new BsonDocument("$regex", new BsonRegularExpression($"/{filterString}/i")) }
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

                    List<QualityParameterGroup> results = await _QualityGroupCollection.Aggregate<QualityParameterGroup>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var qualityGroups = await _QualityGroupCollection.Find<QualityParameterGroup>(new BsonDocument()).ToListAsync();

                    if (qualityGroups == null || qualityGroups.Count == 0)
                    {
                        return (false, null, "Unpopulated Inventory");
                    }
                    else
                    {
                        return (true, qualityGroups, null);
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
        /// <param name="qualityId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, QualityParameterGroup? QualityGroup, string? ErrorDescription)> GetQualityParameteGroupsById(string userId, string ipAddress, string squadId, string qualityId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(qualityId)))
                );
                // Filtro por SquadId
                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("SquadId", squadId))
                );

                var resultPrev = await _QualityGroupCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                QualityParameterGroup result = resultPrev.Select(d => BsonSerializer.Deserialize<QualityParameterGroup>(d)).ToList()[0];

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
        /// <param name="quality"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, QualityParameterGroup? QualityGroup, string? ErrorDescription)> SaveQualityParameterGroups(string userId, string ipAddress, string squadId, QualityParameterGroup quality)
        {
            try
            {
                if (quality.Id == null)
                {
                    quality.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterQuantity = Builders<QualityParameterGroup>.Filter.Eq("_id", new ObjectId(quality.Id));
                var updateQuantity = new ReplaceOptions { IsUpsert = true };
                var resultQuantity = await _QualityGroupCollection.ReplaceOneAsync(filterQuantity, quality, updateQuantity);

                return (true, quality, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
    }
}
