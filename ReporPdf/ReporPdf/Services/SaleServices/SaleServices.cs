using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using SunttelTradePointB.Shared.Sales;
using SunttelTPointReporPdf.Interfaces.Sale;

namespace SunttelTPointReporPdf.Services.SaleServices
{
    public class SaleServices : ISale
    {

        IMongoCollection<CommercialDocument> _CommercialDocument;
        IMongoCollection<SalesDocumentItemsDetails> _CommercialDocumentDetail;

        /// <summary>
        /// Constructor
        /// </summary>
        public SaleServices(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];
            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _CommercialDocument = mongoDatabase.GetCollection<CommercialDocument>("CommercialDocuments");
            _CommercialDocumentDetail = mongoDatabase.GetCollection<SalesDocumentItemsDetails>("CommercialDocumentsDetails");
        }

        public async Task<(bool IsSuccess, CommercialDocument? CommercialDocument, string? ErrorDescription)> GetCommercialDocument(string commercialDocumentId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(commercialDocumentId)))
                );

                var resultPrev = await _CommercialDocument.Aggregate<BsonDocument>(pipeline).ToListAsync();
                CommercialDocument result = resultPrev.Select(d => BsonSerializer.Deserialize<CommercialDocument>(d)).ToList()[0];

                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }


        /// <summary>
        ///  Retrives a list of PRoducts, services, and all Transactional Items based on a search criteria
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="commercialDocumentId"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="nameLike"></param>
        /// <param name="groupName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<SalesDocumentItemsDetails>? GetCommercialDocumentDetails, string? ErrorDescription)> GetCommercialDocumentDetails( string commercialDocumentId)
        {
            try
            {
               
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("IdCommercialDocument", new ObjectId(commercialDocumentId)))
                );

                List<SalesDocumentItemsDetails> results = await _CommercialDocumentDetail.Aggregate<SalesDocumentItemsDetails>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


    }
}
