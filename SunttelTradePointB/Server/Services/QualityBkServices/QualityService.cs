using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.QualityBkServices;
using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.ImportingData;
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
        IMongoCollection<QualityTrafficLight> _QualityTrafficLightCollection;
        IMongoCollection<QualityAction> _QualityActionCollection;
        IMongoCollection<QualityReportType> _QualityReportTypeCollection;
        IMongoCollection<QualityEvaluation> _QCDocumentCollection;

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
            _QualityTrafficLightCollection = mongoDatabase.GetCollection<QualityTrafficLight>("QualityTrafficLightStatus");
            _QualityActionCollection = mongoDatabase.GetCollection<QualityAction>("QualityActionToTake");
            _QualityReportTypeCollection = mongoDatabase.GetCollection<QualityReportType>("QualityReportTypes");
            _QCDocumentCollection = mongoDatabase.GetCollection<QualityEvaluation>("QCEvaluations");

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

                    //  Filtro por SquadId
                    //pipeline.Add(
                    //    new BsonDocument("$match", new BsonDocument("SquadId"))
                    //);

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
                        return (true, qualityParameters.Where(s => s.SquadId.ToLower() == squadId.ToLower()).ToList(), null);
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


        /// <summary>
        /// Delete an QualityAssuranceParameter not associated with QualityAssurance
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityParameterId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteQualityAssuranceParameterById(string userId, string ipAddress, string squadId, string? qualityParameterId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();
                pipeline.Add(
                new BsonDocument("$match",
                  new BsonDocument("EvaluationParameters._id", new ObjectId(qualityParameterId))
                  )
               );

                pipeline.Add(
                    new BsonDocument("$group", new BsonDocument(
                        "_id", new BsonDocument(
                           "count", new BsonDocument(
                               "$sum", 1
                               )
                            )))
                    );



                var resultCount = _QCDocumentCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _QualityParameterCollection.DeleteOne(s => s.Id == qualityParameterId);
                    return (true, result.IsAcknowledged, null);
                }
                else
                {
                    return (true, false, ($"Count {count}"));
                }
            }
            catch (Exception e)
            {
                return (false, false, e.Message);
            }


        }


        #endregion

        #region Quality Parameters Groups
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


        /// <summary>
        /// Delete an QualityParameterGroup not associated with Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityParameterGroupId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteQualityParameterGroupById(string userId, string ipAddress, string squadId, string? qualityParameterGroupId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();
                pipeline.Add(
                new BsonDocument("$match",
                  new BsonDocument("ParameterGroup._id", new ObjectId(qualityParameterGroupId))
                  )
               );

                pipeline.Add(
                    new BsonDocument("$group", new BsonDocument(
                        "_id", new BsonDocument(
                           "count", new BsonDocument(
                               "$sum", 1
                               )
                            )))
                    );



                var resultCount = _QCDocumentCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _QualityGroupCollection.DeleteOne(s => s.Id == qualityParameterGroupId);
                    return (true, result.IsAcknowledged, null);
                }
                else
                {
                    return (true, false, ($"Count item {count}"));
                }
            }
            catch (Exception e)
            {
                return (false, false, e.Message);
            }


        }



        #endregion

        #region Quality Traffic Lights
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
        public async Task<(bool IsSuccess, List<QualityTrafficLight>? QualityTrafficLightsList, string? ErrorDescription)> GetQualityTrafficLights(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null)
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

                    List<QualityTrafficLight> results = await _QualityTrafficLightCollection.Aggregate<QualityTrafficLight>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var quality = await _QualityTrafficLightCollection.Find<QualityTrafficLight>(new BsonDocument()).ToListAsync();

                    if (quality == null || quality.Count == 0)
                    {
                        return (false, null, "Unpopulated Quality Traffic Light");
                    }
                    else
                    {
                        return (true, quality, null);
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
        public async Task<(bool IsSuccess, QualityTrafficLight? QualityTrafficLight, string? ErrorDescription)> GetQualityTrafficLightById(string userId, string ipAddress, string squadId, string qualityId)
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

                var resultPrev = await _QualityTrafficLightCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                QualityTrafficLight result = resultPrev.Select(d => BsonSerializer.Deserialize<QualityTrafficLight>(d)).ToList()[0];

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
        public async Task<(bool IsSuccess, QualityTrafficLight? QualityTrafficLight, string? ErrorDescription)> SaveQualityTrafficLight(string userId, string ipAddress, string squadId, QualityTrafficLight quality)
        {
            try
            {
                if (quality.Id == null)
                {
                    quality.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterQuantity = Builders<QualityTrafficLight>.Filter.Eq("_id", new ObjectId(quality.Id));
                var updateQuantity = new ReplaceOptions { IsUpsert = true };
                var resultQuantity = await _QualityTrafficLightCollection.ReplaceOneAsync(filterQuantity, quality, updateQuantity);

                return (true, quality, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Delete an QualityTrafficLight not associated with Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityTrafficLightId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteQualityTrafficLightById(string userId, string ipAddress, string squadId, string? qualityTrafficLightId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();
                pipeline.Add(
                new BsonDocument("$match",
                  new BsonDocument("TrafficLightStatus._id", new ObjectId(qualityTrafficLightId))
                  )
               );

                pipeline.Add(
                    new BsonDocument("$group", new BsonDocument(
                        "_id", new BsonDocument(
                           "count", new BsonDocument(
                               "$sum", 1
                               )
                            )))
                    );



                var resultCount = _QCDocumentCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;



                if (count <= 0)
                {
                    var result = _QualityTrafficLightCollection.DeleteOne(s => s.Id == qualityTrafficLightId);
                    return (true, result.IsAcknowledged, null);
                }
                else
                {
                    return (true, false, ($"Count item {count}"));
                }
            }
            catch (Exception e)
            {
                return (false, false, e.Message);
            }


        }



        #endregion

        #region Quality Actions
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
        public async Task<(bool IsSuccess, List<QualityAction>? QualityActionsList, string? ErrorDescription)> GetQualityActions(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null)
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

                    List<QualityAction> results = await _QualityActionCollection.Aggregate<QualityAction>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var quality = await _QualityActionCollection.Find<QualityAction>(new BsonDocument()).ToListAsync();

                    if (quality == null || quality.Count == 0)
                    {
                        return (false, null, "Unpopulated Quality Action");
                    }
                    else
                    {
                        return (true, quality, null);
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
        public async Task<(bool IsSuccess, QualityAction? QualityAction, string? ErrorDescription)> GetQualityActionById(string userId, string ipAddress, string squadId, string qualityId)
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

                var resultPrev = await _QualityActionCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                QualityAction result = resultPrev.Select(d => BsonSerializer.Deserialize<QualityAction>(d)).ToList()[0];

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
        public async Task<(bool IsSuccess, QualityAction? QualityAction, string? ErrorDescription)> SaveQualityAction(string userId, string ipAddress, string squadId, QualityAction quality)
        {
            try
            {
                if (quality.Id == null)
                {
                    quality.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterQuantity = Builders<QualityAction>.Filter.Eq("_id", new ObjectId(quality.Id));
                var updateQuantity = new ReplaceOptions { IsUpsert = true };
                var resultQuantity = await _QualityActionCollection.ReplaceOneAsync(filterQuantity, quality, updateQuantity);

                return (true, quality, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }



        /// <summary>
        /// Delete an QualityAction not associated with Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityActioId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteQualityActionById(string userId, string ipAddress, string squadId, string? qualityActioId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();
                pipeline.Add(
                new BsonDocument("$match",
                  new BsonDocument("ActionToTake._id", new ObjectId(qualityActioId))
                  )
               );


                pipeline.Add(
                    new BsonDocument("$group", new BsonDocument(
                        "_id", new BsonDocument(
                           "count", new BsonDocument(
                               "$sum", 1
                               )
                            )))
                    );


                var resultCount = _QCDocumentCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _QualityActionCollection.DeleteOne(s => s.Id == qualityActioId);
                    return (true, result.IsAcknowledged, null);
                }
                else
                {
                    return (true, false, ($"Count item {count}"));
                }
            }
            catch (Exception e)
            {
                return (false, false, e.Message);
            }


        }


        #endregion

        #region Quality Type
        /// <summary>
        /// Returns a list of quality report types with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<QualityReportType>? GetQualityReportTypesList, string? ErrorDescription)> GetQualityReportTypes(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null)
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

                    List<QualityReportType> results = await _QualityReportTypeCollection.Aggregate<QualityReportType>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var quality = await _QualityReportTypeCollection.Find<QualityReportType>(new BsonDocument()).ToListAsync();

                    if (quality == null || quality.Count == 0)
                    {
                        return (false, null, "Unpopulated Quality report type");
                    }
                    else
                    {
                        return (true, quality, null);
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
        public async Task<(bool IsSuccess, QualityReportType? QualityReportType, string? ErrorDescription)> GetQualityReportTypeById(string userId, string ipAddress, string squadId, string qualityId)
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

                var resultPrev = await _QualityReportTypeCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                QualityReportType result = resultPrev.Select(d => BsonSerializer.Deserialize<QualityReportType>(d)).ToList()[0];

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
        public async Task<(bool IsSuccess, QualityReportType? QualityReportType, string? ErrorDescription)> SaveQualityReportType(string userId, string ipAddress, string squadId, QualityReportType quality)
        {
            try
            {
                if (quality.Id == null)
                {
                    quality.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterQuantity = Builders<QualityReportType>.Filter.Eq("_id", new ObjectId(quality.Id));
                var updateQuantity = new ReplaceOptions { IsUpsert = true };
                var resultQuantity = await _QualityReportTypeCollection.ReplaceOneAsync(filterQuantity, quality, updateQuantity);

                return (true, quality, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Delete an QualityReportType not associated with Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityReportTypeId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteQualityReportTypeById(string userId, string ipAddress, string squadId, string? qualityReportTypeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();
                pipeline.Add(
                new BsonDocument("$match",
                  new BsonDocument("QualityReportType._id", new ObjectId(qualityReportTypeId))
                  )
               );


                pipeline.Add(
                    new BsonDocument("$group", new BsonDocument(
                        "_id", new BsonDocument(
                           "count", new BsonDocument(
                               "$sum", 1
                               )
                            )))
                    );



                var resultCount = _QCDocumentCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;



                if (count <= 0)
                {
                    var result = _QualityReportTypeCollection.DeleteOne(s => s.Id == qualityReportTypeId);
                    return (true, result.IsAcknowledged, null);
                }
                else
                {
                    return (true, false, ($"Count item {count}"));
                }
            }
            catch (Exception e)
            {
                return (false, false, e.Message);
            }


        }

        #endregion

        #region QC Document
        /// <summary>
        /// Returns a list of quality documents with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<QualityEvaluation>? GetQCDocumentsList, string? ErrorDescription)> GetQCDocuments(string userId, string ipAddress, string squadId, DateTime startDate, DateTime endDate, int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string startDateIso = startDate.ToString("O");
                string endDateIso = endDate.ToString("O");


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

                    // TODO: falta agregar el filtro de fecha
                    //pipeline.Add(
                    //    new BsonDocument
                    //    {
                    //        {
                    //            "$match",
                    //            new BsonDocument
                    //            {
                    //                 { "InspectionDate", new BsonDocument{
                    //                { "$gte", startDateIso },
                    //                { "$lte", endDateIso }
                    //            } }
                    //          }
                    //        }
                    //    }
                    //);

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

                    List<QualityEvaluation> results = await _QCDocumentCollection.Aggregate<QualityEvaluation>(pipeline).ToListAsync();
                    return (true, results, null);

                }
                else
                {
                    var quality = await _QCDocumentCollection.Find<QualityEvaluation>(new BsonDocument()).ToListAsync();

                    if (quality == null || quality.Count == 0)
                    {
                        return (false, null, "Unpopulated Quality Documents");
                    }
                    else
                    {
                        return (true, quality, null);
                    }
                }

            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves an quality document type object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="qualityId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, QualityEvaluation? QCDocument, string? ErrorDescription)> GetQCDocumentById(string userId, string ipAddress, string squadId, string qualityId)
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

                var resultPrev = await _QCDocumentCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                QualityEvaluation result = resultPrev.Select(d => BsonSerializer.Deserialize<QualityEvaluation>(d)).ToList()[0];

                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        /// <summary>
        /// Saves an quality document type. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, QualityEvaluation? QCDocument, string? ErrorDescription)> SaveQCDocument(string userId, string ipAddress, string squadId, QualityEvaluation quality)
        {
            try
            {
                if (quality.Id == null)
                {
                    quality.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterQuantity = Builders<QualityEvaluation>.Filter.Eq("_id", new ObjectId(quality.Id));

                var updateQuantity = new ReplaceOptions { IsUpsert = true };
                var resultQuantity = await _QCDocumentCollection.ReplaceOneAsync(filterQuantity, quality, updateQuantity);

                return (true, quality, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion
    }
}
