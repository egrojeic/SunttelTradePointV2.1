using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.PaymentBkServices;
using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.InvetoryModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SunttelTradePointB.Server.Services.PaymentBkServices
{
    /// <summary>
    /// Payment service
    /// </summary>
    public class PaymentService : IPayment
    {
        /// <summary>
        /// Payment service
        /// </summary>
        IMongoCollection<Payment> _PaymentCollection;
        IMongoCollection<PaymentMode> _PaymentModeCollection;
        IMongoCollection<PaymentVia> _PaymentViaCollection;
        IMongoCollection<PaymentType> _PaymentTypeCollection;
        IMongoCollection<PaymentStatus> _PaymentStatusCollection;

        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _PaymentCollection = mongoDatabase.GetCollection<Payment>("Payments");
            _PaymentModeCollection = mongoDatabase.GetCollection<PaymentMode>("PaymentModes");
            _PaymentViaCollection = mongoDatabase.GetCollection<PaymentVia>("PaymentVias");
            _PaymentTypeCollection = mongoDatabase.GetCollection<PaymentType>("PaymentTypes");
            _PaymentStatusCollection = mongoDatabase.GetCollection<PaymentStatus>("PaymentStatuses");

        }

        #region Payment
        /// <summary>
        /// Saves an Payment document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="PaymentDate"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<Payment>? PaymentList, string? ErrorDescription)> GetPaymentsByDateSpan(string userId, string ipAddress, string squadId, DateTime PaymentDate, int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                var pipeline = new List<BsonDocument>();
                string strNameFilter = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;

                pipeline.Add(
                    new BsonDocument
                    {
                        { "$match",
                            new BsonDocument{
                                { "SquadId", squadId},
                                { "PaymentDate", new BsonDocument{
                                    { "$gte", PaymentDate }
                                } }
                            }
                        }
                    }
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

                List<Payment> results = await _PaymentCollection.Aggregate<Payment>(pipeline).ToListAsync();

                return (true, results, null);

            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }

        }

        /// <summary>
        /// Retrieves a particular Transactional Item Type by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, Payment? Payment, string? ErrorDescription)> GetPaymentById(string userId, string ipAddress, string squadId, string paymentId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument{
                        { "$match",
                            new BsonDocument{
                                { "_id", new ObjectId(paymentId) },
                                { "SquadId", squadId }
                            }
                        }
                    }
                );

                var resultPrev = await _PaymentCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                Payment result = resultPrev.Select(d => BsonSerializer.Deserialize<Payment>(d)).ToList()[0];

                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }

        }


        /// <summary>
        /// Inserts / Updates an Payment Item Type object
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, Payment? Payment, string? ErrorDescription)> SavePayment(string userId, string ipAddress, string squadId, Payment payment)
        {
            try
            {
                if (payment.Id == null)
                {
                    payment.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPayment = Builders<Payment>.Filter.Eq("_id", new ObjectId(payment.Id));
                var updatePayment = new ReplaceOptions { IsUpsert = true };
                var resultPayment = await _PaymentCollection.ReplaceOneAsync(filterPayment, payment, updatePayment);

                return (true, payment, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion

        #region PaymentMode
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
        public async Task<(bool IsSuccess, List<PaymentMode>? PaymentModesList, string? ErrorDescription)> GetDocPaymentModes(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string strFilter = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;

                if (strFilter.Length > 0)
                {
                    var pipeline = new List<BsonDocument>();

                    if (strFilter.ToLower() != "all")
                    {
                        pipeline.Add(
                       new BsonDocument(
                           "$match", new BsonDocument(
                               "Name", new BsonDocument("$regex", new BsonRegularExpression($"/{strFilter}/i"))
                           )
                       )
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

                    List<PaymentMode> results = await _PaymentModeCollection.Aggregate<PaymentMode>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var paymentModes = await _PaymentModeCollection.Find<PaymentMode>(new BsonDocument()).ToListAsync();

                    if (paymentModes == null || paymentModes.Count == 0)
                    {
                        return (false, null, "Unpopulated PaymentModes");
                    }
                    else
                    {
                        return (true, paymentModes, null);
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
        /// <param name="paymentModeId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, PaymentMode? PaymentMode, string? ErrorDescription)> GetDocPaymentModeById(string userId, string ipAddress, string squadId, string paymentModeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(paymentModeId)))
                );

                var resultPrev = await _PaymentModeCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                PaymentMode result = resultPrev.Select(d => BsonSerializer.Deserialize<PaymentMode>(d)).ToList()[0];

                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }

        }

        /// <summary>
        /// Inserts / Updates an PaymentMode Item Type object
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="paymentMode"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, PaymentMode? PaymentMode, string? ErrorDescription)> SaveDocPaymentMode(string userId, string ipAddress, string squadId, PaymentMode paymentMode)
        {
            try
            {
                if (paymentMode.Id == null)
                {
                    paymentMode.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPaymentMode = Builders<PaymentMode>.Filter.Eq("_id", new ObjectId(paymentMode.Id));
                var updatePaymentMode = new ReplaceOptions { IsUpsert = true };
                var resultPaymentMode = await _PaymentModeCollection.ReplaceOneAsync(filterPaymentMode, paymentMode, updatePaymentMode);

                return (true, paymentMode, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion

        #region PaymentVia
        /// <summary>
        /// Saves an Payment Via document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<PaymentVia>? PaymentViaList, string? ErrorDescription)> GetDocPaymentVias(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string strFilter = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;

                if (strFilter.Length > 0)
                {
                    var pipeline = new List<BsonDocument>();

                    if (strFilter.ToLower() != "all")
                    {
                        pipeline.Add(
                       new BsonDocument(
                           "$match", new BsonDocument(
                               "Name", new BsonDocument("$regex", new BsonRegularExpression($"/{strFilter}/i"))
                           )
                       )
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

                    List<PaymentVia> results = await _PaymentViaCollection.Aggregate<PaymentVia>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var paymentVias = await _PaymentViaCollection.Find<PaymentVia>(new BsonDocument()).ToListAsync();

                    if (paymentVias == null || paymentVias.Count == 0)
                    {
                        return (false, null, "Unpopulated PaymentVias");
                    }
                    else
                    {
                        return (true, paymentVias, null);
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
        /// <returns></returns>
        public async Task<(bool IsSuccess, PaymentVia? PaymentVia, string? ErrorDescription)> GetDocPaymentViaById(string userId, string ipAddress, string squadId, string paymentViaId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(paymentViaId)))
                );

                var resultPrev = await _PaymentViaCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                PaymentVia result = resultPrev.Select(d => BsonSerializer.Deserialize<PaymentVia>(d)).ToList()[0];

                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }

        }

        /// <summary>
        /// Inserts / Updates an PaymentVia Item Type object
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="paymentVia"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, PaymentVia? PaymentVia, string? ErrorDescription)> SaveDocPaymentVia(string userId, string ipAddress, string squadId, PaymentVia paymentVia)
        {
            try
            {
                if (paymentVia.Id == null)
                {
                    paymentVia.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPaymentVia = Builders<PaymentVia>.Filter.Eq("_id", new ObjectId(paymentVia.Id));
                var updatePaymentVia = new ReplaceOptions { IsUpsert = true };
                var resultPaymentVia = await _PaymentViaCollection.ReplaceOneAsync(filterPaymentVia, paymentVia, updatePaymentVia);

                return (true, paymentVia, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion

        #region Payment Type
        /// <summary>
        /// Returns a list of payment type with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<PaymentType>? PaymentTypesList, string? ErrorDescription)> GetPaymentDocumentTypes(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string strFilter = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;

                if (strFilter.Length > 0)
                {
                    var pipeline = new List<BsonDocument>();

                    if (strFilter.ToLower() != "all")
                    {
                        pipeline.Add(
                       new BsonDocument(
                           "$match", new BsonDocument(
                               "Name", new BsonDocument("$regex", new BsonRegularExpression($"/{strFilter}/i"))
                           )
                       )
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

                    List<PaymentType> results = await _PaymentTypeCollection.Aggregate<PaymentType>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var paymentType = await _PaymentTypeCollection.Find<PaymentType>(new BsonDocument()).ToListAsync();

                    if (paymentType == null || paymentType.Count == 0)
                    {
                        return (false, null, "Unpopulated Payment type");
                    }
                    else
                    {
                        return (true, paymentType, null);
                    }
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves a particular payment type by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, PaymentType? PaymentType, string? ErrorDescription)> GetPaymentTypeById(string userId, string ipAddress, string squadId, string paymentId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(paymentId)))
                );

                var resultPrev = await _PaymentTypeCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                PaymentType result = resultPrev.Select(d => BsonSerializer.Deserialize<PaymentType>(d)).ToList()[0];

                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        /// <summary>
        /// Inserts / Updates an Payment type Item Type object
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="paymentType"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, PaymentType? PaymentType, string? ErrorDescription)> SavePaymentType(string userId, string ipAddress, string squadId, PaymentType paymentType)
        {
            try
            {
                if (paymentType.Id == null)
                {
                    paymentType.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPaymentType = Builders<PaymentType>.Filter.Eq("_id", new ObjectId(paymentType.Id));
                var updatePaymentType = new ReplaceOptions { IsUpsert = true };
                var resultPaymentType = await _PaymentTypeCollection.ReplaceOneAsync(filterPaymentType, paymentType, updatePaymentType);

                return (true, paymentType, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion

        #region Payment Status
        /// <summary>
        /// Returns a list of payment status with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<PaymentStatus>? PaymentStatusesList, string? ErrorDescription)> GetPaymentStatuses(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string strFilter = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;

                if (strFilter.Length > 0)
                {
                    var pipeline = new List<BsonDocument>();

                    if (strFilter.ToLower() != "all")
                    {
                        pipeline.Add(
                       new BsonDocument(
                           "$match", new BsonDocument(
                               "Name", new BsonDocument("$regex", new BsonRegularExpression($"/{strFilter}/i"))
                           )
                       )
                    );
                    }

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

                    List<PaymentStatus> results = await _PaymentStatusCollection.Aggregate<PaymentStatus>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var paymentStatus = await _PaymentStatusCollection.Find<PaymentStatus>(new BsonDocument()).ToListAsync();

                    if (paymentStatus == null || paymentStatus.Count == 0)
                    {
                        return (false, null, "Unpopulated PaymentVias");
                    }
                    else
                    {
                        return (true, paymentStatus, null);
                    }
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves a particular payment status by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, PaymentStatus? PaymentStatus, string? ErrorDescription)> GetPaymentStatusById(string userId, string ipAddress, string squadId, string paymentId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(paymentId)))
                );

                var resultPrev = await _PaymentStatusCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                PaymentStatus result = resultPrev.Select(d => BsonSerializer.Deserialize<PaymentStatus>(d)).ToList()[0];

                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }
        /// <summary>
        /// Inserts / Updates an Payment status Item Type object
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, PaymentStatus? PaymentStatus, string? ErrorDescription)> SavePaymentStatus(string userId, string ipAddress, string squadId, PaymentStatus paymentStatus)
        {
            try
            {
                if (paymentStatus.Id == null)
                {
                    paymentStatus.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPaymentStatus = Builders<PaymentStatus>.Filter.Eq("_id", new ObjectId(paymentStatus.Id));
                var updatePaymentStatus = new ReplaceOptions { IsUpsert = true };
                var resultPaymentStatus = await _PaymentStatusCollection.ReplaceOneAsync(filterPaymentStatus, paymentStatus, updatePaymentStatus);

                return (true, paymentStatus, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion

    }
}
