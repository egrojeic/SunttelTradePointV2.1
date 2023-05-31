using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using SunttelTradePointB.Shared.Common;
using SunttelTPointReporPdf.Interfaces.IPayment;
using SunttelTradePointB.Shared.Accounting;

namespace SunttelTPointReporPdf.Services.PaymentServices

{
    public class PaymentServices : IPayment
    {

        IMongoCollection<Payment> _CommercialDocument;

        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentServices(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];
            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _CommercialDocument = mongoDatabase.GetCollection<Payment>("Payments");
        }

        public async Task<(bool IsSuccess, Payment? transactionalItem, string? ErrorDescription)> GetPayment(string paymentId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(paymentId)))
                );

                var result = await _CommercialDocument.Aggregate<Payment>(pipeline).FirstOrDefaultAsync();
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
