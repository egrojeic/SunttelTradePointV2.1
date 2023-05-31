using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SunttelTPointReporPdf.Interfaces.IActor;
using SunttelTradePointB.Shared.Common;

namespace SunttelTPointReporPdf.Services.Actor
{
    public class EntityNodesServicescs : IActor
    {
        IMongoCollection<EntityActor> _entityActorsCollection;
        public EntityNodesServicescs(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);

            _entityActorsCollection = mongoDatabase.GetCollection<EntityActor>("EntityNodes");

        }

        /// <summary>
        /// Retrieves the whole object of an Entity/Actor
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, EntityActor entityActorResponse, string? ErrorDescription)> GetEntityActorById(string entityActorId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(entityActorId)))
                );

                pipeline.Add(
                    new BsonDocument
                    {
                        {
                            "$lookup",
                            new BsonDocument
                            {
                                { "from", "GeographicCities" },
                                { "let", new BsonDocument { { "cityId", "$InvoicingAddress.CityAddressRef" } } },
                                { "pipeline", new BsonArray
                                {
                                    new BsonDocument("$match", new BsonDocument("$expr",
                                        new BsonDocument("$cond", new BsonArray
                                        {
                                            new BsonDocument("$eq", new BsonArray { "$$cityId", BsonNull.Value }),
                                            new BsonDocument("$eq", new BsonArray { "$_id", BsonNull.Value }),
                                            new BsonDocument("$eq", new BsonArray { "$_id", "$$cityId" })
                                        })
                                    ))
                                }},
                                { "as", "InvoicingAddress.CityAddress" }
                            }
                        }
                    }
                );

                pipeline.Add(
                    new BsonDocument("$unwind", new BsonDocument
                    {
                        { "path", "$InvoicingAddress.CityAddress" },
                        { "preserveNullAndEmptyArrays", true },
                    })
                );
                //var resultPrev = await _entityActorsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();

                var resultPrev = await _entityActorsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();


                EntityActor result = resultPrev.Select(d => BsonSerializer.Deserialize<EntityActor>(d)).ToList()[0];



                return (true, result, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

    }
}
