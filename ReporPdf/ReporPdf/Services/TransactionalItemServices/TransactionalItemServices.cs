using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using SunttelTradePointB.Shared.Common;
using SunttelTPointReporPdf.Interfaces.TransactionalReport;

namespace SunttelTPointReporPdf.Services.ReportServices
{
    public class TransactionalItemServices : ITransactionalItem
    {

        IMongoCollection<TransactionalItem> _CommercialDocument;

        /// <summary>
        /// Constructor
        /// </summary>
        public TransactionalItemServices(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];
            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _CommercialDocument = mongoDatabase.GetCollection<TransactionalItem>("TransactionalItems");


        }


        public async Task<(bool IsSuccess, TransactionalItem? transactionalItem, string? ErrorDescription)> GetTransactionalItem(string commercialDocumentId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(commercialDocumentId)))
                );

                var result = await _CommercialDocument.Aggregate<TransactionalItem>(pipeline).FirstOrDefaultAsync();
               // var  result = resultPrev.Select(d => BsonSerializer.Deserialize<TransactionalItem>(d)).ToList()[0];

                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }
    }
}
