using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.CreditBkServices;
using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.InvetoryModels;

namespace SunttelTradePointB.Server.Services.CreditBkServices
{
    /// <summary>
    /// Credit Documents service
    /// </summary>
    public class CreditService : ICredit
    {
        /// <summary>
        /// Credit Documents service
        /// </summary>
        IMongoCollection<CreditDocument> _CreditDocumentCollection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// 
        public CreditService(IConfiguration config) 
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _CreditDocumentCollection = mongoDatabase.GetCollection<CreditDocument>("Credits");

        }

        /// <summary>
        /// Retrives the list of an Credit documents
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<CreditDocument>? CreditDocumentsList, string? ErrorDescription)> GetCreditDocuments(string userId, string ipAddress, string squadId, DateTime startDate, DateTime endDate, int? page = 1, int? perPage = 10, string? filter = null)
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
                                        { "SquadId", squadId },
                                        { "CreditDate", new BsonDocument{
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

                    List<CreditDocument> results = await _CreditDocumentCollection.Aggregate<CreditDocument>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var creditDocuments = await _CreditDocumentCollection.Find<CreditDocument>(new BsonDocument()).ToListAsync();

                    if (creditDocuments == null || creditDocuments.Count == 0)
                    {
                        return (false, null, "Unpopulated Credit Documents");
                    }
                    else
                    {
                        return (true, creditDocuments, null);
                    }
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Saves an Entity/Actor document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditDocumentId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, CreditDocument? CreditDocument, string? ErrorDescription)> GetCreditDocumentById(string userId, string ipAddress, string squadId, string creditDocumentId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(creditDocumentId)))
                );

                var resultPrev = await _CreditDocumentCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                CreditDocument result = resultPrev.Select(d => BsonSerializer.Deserialize<CreditDocument>(d)).ToList()[0];

                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        /// <summary>
        /// Saves an Credit document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditDocument"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, CreditDocument? CreditDocument, string? ErrorDescription)> SaveCreditDocument(string userId, string ipAddress, string squadId, CreditDocument creditDocument)
        {
            try
            {
                if (creditDocument.Id == null)
                {
                    creditDocument.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterCreditDocument = Builders<CreditDocument>.Filter.Eq("_id", new ObjectId(creditDocument.Id));
                var updateCreditDocument = new ReplaceOptions { IsUpsert = true };
                var resultCreditDocument = await _CreditDocumentCollection.ReplaceOneAsync(filterCreditDocument, creditDocument, updateCreditDocument);

                return (true, creditDocument, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
    }
}
