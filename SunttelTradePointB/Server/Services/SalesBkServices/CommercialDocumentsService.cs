using Microsoft.AspNetCore.Mvc;
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
        IMongoCollection<BusinessLine> _BusinessLineCollection;
        IMongoCollection<ShippingStatus> _ShippingStatusCollection;


        /// <summary>
        /// Constructor
        /// </summary>
        public CommercialDocumentsService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _SalesDocumentsCollection = mongoDatabase.GetCollection<SalesDocuments>("CommercialDocuments");
            _BusinessLineCollection = mongoDatabase.GetCollection<BusinessLine>("BusinessLineDocuments");
            _ShippingStatusCollection = mongoDatabase.GetCollection<ShippingStatus>("ShippingStatusDocuments");

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

        #region BusinessLineDoc
        /// <summary>
        /// Saves an  Business line document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, BusinessLine? entity, string? ErrorDescription)> SaveBusinessLineDoc(string userId, string ipAdress, string squadId, BusinessLine entity)
        {
            try
            {
                if (entity.Id == null)
                {
                    entity.Id = ObjectId.GenerateNewId().ToString();
                }

                var filter = Builders<BusinessLine>.Filter.Eq("_id", new ObjectId(entity.Id));

                var updateOptions = new ReplaceOptions { IsUpsert = true };

                var result = await _BusinessLineCollection.ReplaceOneAsync(filter, entity, updateOptions);

                return (true, entity, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves Entity Groups matching a filter pattern
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<BusinessLine>? businessesLinesDocs, string? ErrorDescription)> GetBusinessLinesDocs(string userId, string ipAdress, string squadId, string? filterString)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                if (!string.IsNullOrEmpty(filterString) && !filterString.ToLower().Equals("all") && !filterString.ToLower().Equals("todos")) // se agregó verificación de si strNameFiler está vacío
                {
                    pipeline.Add(
                        new BsonDocument {
                            { "$match", new BsonDocument {
                               { "Name", new BsonRegularExpression($"/{filterString}/i")
                                    }
                               }
                            }   
                        }
                    );
                }

                pipeline.Add(
                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "Code", 1 },
                                { "Name", 1 }
                            }
                        }
                    }
                );

                List<BusinessLine> results = await _BusinessLineCollection.Aggregate<BusinessLine>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves a Business Line Doc by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="businessLineDocId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, BusinessLine? businessLineDoc, string? ErrorDescription)> GetBusinessLineDocById(string userId, string ipAdress, string squadId, string businessLineDocId)
        {
            try
            {
                var filterbusinessLineDoc = Builders<BusinessLine>.Filter.Eq(x => x.Id, businessLineDocId);
                var businessLineDoc = await _BusinessLineCollection.Find(filterbusinessLineDoc).FirstOrDefaultAsync();
                if (businessLineDoc == null)
                {
                    return (false, null, "Unpopulated Pallet Types");
                }
                else
                {
                    return (true, businessLineDoc, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion

        #region Shippin Status
         /// <summary>
        /// Saves an  Shipping Status document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, ShippingStatus? entity, string? ErrorDescription)> SaveShippinStatus(string userId, string ipAdress, string squadId,[FromBody] ShippingStatus entity)
        {
            try
            {
                if (entity.Id == null)
                {
                    entity.Id = ObjectId.GenerateNewId().ToString();
                }

                var filter = Builders<ShippingStatus>.Filter.Eq("_id", new ObjectId(entity.Id));

                var updateOptions = new ReplaceOptions { IsUpsert = true };

                var result = await _ShippingStatusCollection.ReplaceOneAsync(filter, entity, updateOptions);

                return (true, entity, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves Shipping statuses matching a filter pattern
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<ShippingStatus>? shippingStatuses, string? ErrorDescription)> GetShippingStatusDocs(string userId, string ipAdress, string squadId, string? filterString)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                if (!string.IsNullOrEmpty(filterString) && !filterString.ToLower().Equals("all") && !filterString.ToLower().Equals("todos")) // se agregó verificación de si strNameFiler está vacío
                {
                    pipeline.Add(
                        new BsonDocument {
                            { "$match", new BsonDocument {
                               { "Name", new BsonRegularExpression($"/{filterString}/i")
                                    }
                               }
                            }
                        }
                    );
                }

                pipeline.Add(
                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "Code", 1 },
                                { "Name", 1 }
                            }
                        }
                    }
                );

                List<ShippingStatus> results = await _ShippingStatusCollection.Aggregate<ShippingStatus>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves a Shipping Status Doc by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="businessLineDocId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, ShippingStatus? shippingStatus, string? ErrorDescription)> GetShippingStatusDocById(string userId, string ipAdress, string squadId, string businessLineDocId)
        {
            try
            {
                var filterbusinessLineDoc = Builders<ShippingStatus>.Filter.Eq(x => x.Id, businessLineDocId);
                var businessLineDoc = await _ShippingStatusCollection.Find(filterbusinessLineDoc).FirstOrDefaultAsync();
                if (businessLineDoc == null)
                {
                    return (false, null, "Unpopulated Pallet Types");
                }
                else
                {
                    return (true, businessLineDoc, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion
    }
}
