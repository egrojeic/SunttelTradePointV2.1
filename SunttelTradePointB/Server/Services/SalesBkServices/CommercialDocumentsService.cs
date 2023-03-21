using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SunttelTradePointB.Client.Pages.SalesPages;
using SunttelTradePointB.Server.Interfaces.SalesBkServices;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Server.Services.SalesBkServices
{
    /// <summary>
    /// Commercial documents service
    /// </summary>
    public class CommercialDocumentsService : ICommercialDocument
    {
        IMongoCollection<SalesDocuments> _SalesDocumentsCollection;


        /// <summary>
        /// Constructor
        /// </summary>
        public CommercialDocumentsService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _SalesDocumentsCollection = mongoDatabase.GetCollection<SalesDocuments>("CommercialDocuments");
        }


        /// <summary>
        /// Retrieves a document having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, CommercialDocument? CommercialDocument, string? ErrorDescription)> GetCommercialDocumentById(string userId, string ipAdress, string squadId, string documentId)
        {
            var pipeline = new List<BsonDocument>();
            try
            {
                pipeline.Add(
                  new BsonDocument("$match", new BsonDocument("_id", new ObjectId(documentId)))
                );

                var resultPrev = await _SalesDocumentsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();

                CommercialDocument result = resultPrev.Select(d => BsonSerializer.Deserialize<CommercialDocument>(d)).ToList()[0];



                return (true, result, null);

            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }



        }


        /// <summary>
        /// Retrieves a list of Commercial documents of the Squad for the date span and 
        /// Document type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="documentTypeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<CommercialDocument>? CommercialDocuments, string? ErrorDescription)> GetCommercialDocumentsByDateSpan(string userId, string ipAdress, string squadId, DateTime startDate, DateTime endDate, string documentTypeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument{
                        { "$match",
                            new BsonDocument{
                                { "DocumentType._id", new ObjectId(documentTypeId)},
                                { "ShipDate", new BsonDocument{
                                    { "$gte", startDate },
                                    { "$lte", endDate }
                                } }
                            }
                        }
                    }
                );

               
                List<CommercialDocument> results = await _SalesDocumentsCollection.Aggregate<CommercialDocument>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
    }
}
