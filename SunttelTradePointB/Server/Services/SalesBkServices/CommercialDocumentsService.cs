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
        IMongoCollection<CommercialDocument> _CommercialDocumentCollection;
        IMongoCollection<CommercialDocumentType> _CommercialDocumentType;


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

        /// <summary>
        /// Retrieves a list of Transactional Item Types with the posibility to receive an optional paremeter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<CommercialDocumentType>? commercialDocumentTypes, string? ErrorDescription)> GetCommercialDocumentTypes(string userId, string ipAddress, string? filterCondition = null)
        {
            try
            {
                string filter = filterCondition == null ? "" : filterCondition;

                if (filter.Length > 0)
                {
                    var pipeline = new List<BsonDocument>();

                    if (filter.ToLower() != "all")
                    {
                        pipeline.Add(
                       new BsonDocument(
                           "$match", new BsonDocument(
                               "Name", new BsonDocument("$regex", new BsonRegularExpression($"/{filter}/i"))
                           )
                       )
                   );
                    }


                    List<CommercialDocumentType> results = await _CommercialDocumentType.Aggregate<CommercialDocumentType>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var commercialDocumentTypes = await _CommercialDocumentType.Find<CommercialDocumentType>(new BsonDocument()).ToListAsync();

                    if (commercialDocumentTypes == null || commercialDocumentTypes.Count == 0)
                    {
                        return (false, null, "Unpopulated Transactional Item Types");
                    }
                    else
                    {
                        return (true, commercialDocumentTypes, null);
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
        /// <param name="commercialDocumentType"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, CommercialDocumentType? commercialDocumentType, string? ErrorDescription)> GetCommercialDocumentTypeById(string userId, string ipAddress, string commercialDocumentTypeId)
        {
            try
            {
                var filterCommercialDocumentType = Builders<CommercialDocumentType>.Filter.Eq(x => x.Id, commercialDocumentTypeId);
                var resultCommercialDocumentType = await _CommercialDocumentType.Find(filterCommercialDocumentType).FirstOrDefaultAsync();

                if (resultCommercialDocumentType == null)
                {
                    return (false, null, "Unpopulated Transactional Item Types");
                }
                else
                {
                    return (true, resultCommercialDocumentType, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Inserts / Updates an Transactional Item Type object
        /// </summary>
        /// <param name="commercialDocumentType"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, CommercialDocumentType? commercialDocumentType, string? ErrorDescription)> SaveCommercialDocumentType(string userId, string ipAddress, CommercialDocumentType commercialDocumentType)
        {
            try
            {
                if (commercialDocumentType.Id == null)
                {
                    commercialDocumentType.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterCommercialDocumentType = Builders<CommercialDocumentType>.Filter.Eq("_id", new ObjectId(commercialDocumentType.Id));
                var updateCommercialDocumentType = new ReplaceOptions { IsUpsert = true };
                var resultCommercialDocumentType = await _CommercialDocumentType.ReplaceOneAsync(filterCommercialDocumentType, commercialDocumentType, updateCommercialDocumentType);

                return (true, commercialDocumentType, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

    }
}
