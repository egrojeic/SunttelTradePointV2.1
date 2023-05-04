using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Client.Pages.SalesPages;
using SunttelTradePointB.Server.Interfaces.SalesBkServices;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.DataViews.BI;
using SunttelTradePointB.Shared.DataViews.Profiles;
using SunttelTradePointB.Shared.ImportingData;
using SunttelTradePointB.Shared.Sales;

namespace SunttelTradePointB.Server.Services.SalesBkServices
{
    /// <summary>
    /// Commercial documents service
    /// </summary>
    public class CommercialDocumentsService : ICommercialDocument
    {
        private readonly IMapper _mapper;
        IMongoCollection<SalesDocuments> _SalesDocumentsCollection;
        IMongoCollection<BusinessLine> _BusinessLineCollection;
        IMongoCollection<ShippingStatus> _ShippingStatusCollection;
        IMongoCollection<CommercialDocument> _CommercialDocumentCollection;
        IMongoCollection<CommercialDocumentType> _CommercialDocumentType;
        IMongoCollection<SalesDocumentItemsDetails> _CommercialDocumentDetailImports;
        IMongoCollection<FinanceStatus> _FinanceStatusCollection;


        /// <summary>
        /// Constructor
        /// </summary>
        public CommercialDocumentsService(IConfiguration config, IMapper mapper)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _mapper = mapper;
            _SalesDocumentsCollection = mongoDatabase.GetCollection<SalesDocuments>("CommercialDocuments");
            _BusinessLineCollection = mongoDatabase.GetCollection<BusinessLine>("BusinessLineDocuments");
            _ShippingStatusCollection = mongoDatabase.GetCollection<ShippingStatus>("ShippingStatusDocuments");
            _CommercialDocumentCollection = mongoDatabase.GetCollection<CommercialDocument>("CommercialDocuments");
            _CommercialDocumentType = mongoDatabase.GetCollection<CommercialDocumentType>("CommercialDocumentTypes");
            _FinanceStatusCollection = mongoDatabase.GetCollection<FinanceStatus>("FinanceStatus");
            _CommercialDocumentDetailImports = mongoDatabase.GetCollection<SalesDocumentItemsDetails>("CommercialDocumentsDetails");
        }


        /// <summary>
        /// Retrieves a document having the specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="commercialDocumentId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, CommercialDocument? CommercialDocument, string? ErrorDescription)> GetCommercialDocumentById(string userId, string ipAdress, string squadId, string commercialDocumentId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(commercialDocumentId)))
                );

                var resultPrev = await _CommercialDocumentCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                CommercialDocument result = resultPrev.Select(d => BsonSerializer.Deserialize<CommercialDocument>(d)).ToList()[0];
               
                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }


        /// <summary>
        /// Inserts / Updates an Transactional Item Type object
        /// </summary>
        /// <param name="commercialDocument"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, CommercialDocument? commercialDocument, string? ErrorDescription)> SaveCommercialDocument(string userId, string ipAddress, CommercialDocument commercialDocument)
        {
            try
            {
                if (commercialDocument.Id == null)
                {
                    commercialDocument.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterCommercialDocument = Builders<CommercialDocument>.Filter.Eq("_id", new ObjectId(commercialDocument.Id));
                var updateCommercialDocument = new ReplaceOptions { IsUpsert = true };
                var resultCommercialDocument = await _CommercialDocumentCollection.ReplaceOneAsync(filterCommercialDocument, commercialDocument, updateCommercialDocument);

                return (true, commercialDocument, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a list of Commercial documents of the Squad for the date span and 
        /// Document type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="documentTypeId"></param>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<CommercialDocument>? CommercialDocuments, string? ErrorDescription)> GetCommercialDocumentsByDateSpan(string userId, string ipAddress, string squadId, DateTime startDate, DateTime endDate, string documentTypeId, string? filter = null, int? page = 1, int? perPage = 10)
        {
            try
            {
                string strNameFiler = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;

                var pipeline = new List<BsonDocument>();

                if (!(strNameFiler.ToLower() == "all" || strNameFiler.ToLower() == "todos" || strNameFiler.ToLower() == ""))
                {
                    pipeline.Add(
                    new BsonDocument{
                        { "$match",  new BsonDocument {
                            { "$text",
                                new BsonDocument {
                                    { "$search",strNameFiler },
                                    { "$language","english" },
                                    { "$caseSensitive",false },
                                    { "$diacriticSensitive",false }
                                }
                            }
                        }}
                    }
                );
                }
                
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

                List<CommercialDocument> results = await _SalesDocumentsCollection.Aggregate<CommercialDocument>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        #region Commercial Document Type
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
                var pipeline = new List<BsonDocument>();

                if (filter.ToLower() != "all")
                {
                    pipeline.Add(
                    new BsonDocument(
                        "$match",
                        new BsonDocument(
                                 "Name",
                                    new BsonDocument(
                                        "$regex", new BsonRegularExpression($"/{filter}/i"))
                            )
                    )
                );
                }

                List<CommercialDocumentType> results = await _CommercialDocumentType.Aggregate<CommercialDocumentType>(pipeline).ToListAsync();
                
                return (true, results, null);
                
                
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
        /// <param name="commercialDocumentTypeId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, CommercialDocumentType? commercialDocumentType, string? ErrorDescription)> GetCommercialDocumentTypeById(string userId, string ipAddress, string squadId, string commercialDocumentTypeId)
        {
            try
            {
                var filterCommercialDocumentType = Builders<CommercialDocumentType>.Filter.Eq(x => x.Id, commercialDocumentTypeId);
                var resultCommercialDocumentType = await _CommercialDocumentType.Find(filterCommercialDocumentType).FirstOrDefaultAsync();

                if (resultCommercialDocumentType == null)
                {
                    return (false, null, "There is no type of commercial document with that id");
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
        #endregion

        #region Finance Status
        /// <summary>
        /// Retrieves a list of Transactional Item Types with the posibility to receive an optional paremeter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<FinanceStatus>? financeStatuses, string? ErrorDescription)> GetFinanceStatuses(string userId, string ipAddress, string? filterCondition = null)
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


                    List<FinanceStatus> results = await _FinanceStatusCollection.Aggregate<FinanceStatus>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var financeStatuses = await _FinanceStatusCollection.Find<FinanceStatus>(new BsonDocument()).ToListAsync();

                    if (financeStatuses == null || financeStatuses.Count == 0)
                    {
                        return (false, null, "Unpopulated Finance Statuses");
                    }
                    else
                    {
                        return (true, financeStatuses, null);
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
        /// <param name="financeStatusId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, FinanceStatus? financeStatus, string? ErrorDescription)> GetFiananceStatusById(string userId, string ipAddress, string financeStatusId)
        {
            try
            {
                var filterFinanceStatus = Builders<FinanceStatus>.Filter.Eq(x => x.Id, financeStatusId);
                var resultFinanceStatus = await _FinanceStatusCollection.Find(filterFinanceStatus).FirstOrDefaultAsync();

                if (resultFinanceStatus == null)
                {
                    return (false, null, "Unpopulated Finance Status");
                }
                else
                {
                    return (true, resultFinanceStatus, null);
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
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="financeStatus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, FinanceStatus? financeStatus, string? ErrorDescription)> SaveFinanceStatus(string userId, string ipAddress, FinanceStatus financeStatus)
        {
            try
            {
                if (financeStatus.Id == null)
                {
                    financeStatus.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterFinanceStatus = Builders<FinanceStatus>.Filter.Eq("_id", new ObjectId(financeStatus.Id));
                var updateFinanceStatus = new ReplaceOptions { IsUpsert = true };
                var resultFinanceStatus = await _FinanceStatusCollection.ReplaceOneAsync(filterFinanceStatus, financeStatus, updateFinanceStatus);

                return (true, financeStatus, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion

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
                                { "Name", 1 },
                                { "AffectInventory", 1},
                                { "EditingAllowed", 1 }
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

        /// <summary>
        /// Retrieves a list of Commercial documents of the Squad for the date span and 
        /// Document type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="shippingDate"></param>
        /// <param name="warehouseId"></param>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<CommercialDocument>? CommercialDocuments, string? ErrorDescription)> GetShippingInvoices(string userId, string ipAddress, string squadId, DateTime shippingDate, string warehouseId, string? filter = null, int? page = 1, int? perPage = 10)
        {
            try
            {
                string strNameFiler = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;

                var pipeline = new List<BsonDocument>();

                if (!(strNameFiler.ToLower() == "all" || strNameFiler.ToLower() == "todos" || strNameFiler.ToLower() == ""))
                {
                    pipeline.Add(
                    new BsonDocument{
                        { "$match",  new BsonDocument {
                            { "$text",
                                new BsonDocument {
                                    { "$search",strNameFiler },
                                    { "$language","english" },
                                    { "$caseSensitive",false },
                                    { "$diacriticSensitive",false }
                                }
                            }
                        }}
                    }
                    );
                }


                pipeline.Add(
                    new BsonDocument{
                        { "$match",
                            new BsonDocument{
                                { "VendorWarehouse._id", new ObjectId(warehouseId)},
                                { "ShipDate", new BsonDocument{
                                    { "$gte", shippingDate }
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

                List<CommercialDocument> results = await _SalesDocumentsCollection.Aggregate<CommercialDocument>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        #endregion

        #region Commercial Document Detail
        /// <summary>
        /// Saves an Commercial Document Detail. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squadId"></param>
        /// <param name="salesDocumentItemsDetails"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, SalesDocumentItemsDetails? salesDocumentItemsDetailsResponse, string? ErrorDescription)> SaveCommercialDocumentDetail(string userId, string ipAdress, string squadId, SalesDocumentItemsDetails salesDocumentItemsDetails)
        {
            
            try
            {
                if (salesDocumentItemsDetails.SquadId == null)
                {
                    salesDocumentItemsDetails.SquadId = ObjectId.GenerateNewId().ToString();
                }

                var filter = Builders<SalesDocumentItemsDetails>.Filter.Eq("_id", new ObjectId(salesDocumentItemsDetails.Id));

                var updateOptions = new ReplaceOptions { IsUpsert = true };

                var result = await _CommercialDocumentDetailImports.ReplaceOneAsync(filter, salesDocumentItemsDetails, updateOptions);

                return (true, salesDocumentItemsDetails, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
            
        }

        /// <summary>
        ///  Retrives a list of PRoducts, services, and all Transactional Items based on a search criteria
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="nameLike"></param>
        /// <param name="groupName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<SalesDocumentItemsDetails>? GetCommercialDocumentDetails, string? ErrorDescription)> GetCommercialDocumentDetails(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? nameLike = null, string? groupName = null, string? code = null)
        {
            try
            {
                string strNameFiler = nameLike == null ? "" : nameLike;
                var skip = (page - 1) * perPage;

                var pipeline = new List<BsonDocument>();

                if (!(strNameFiler.ToLower() == "all" || strNameFiler.ToLower() == "todos"))
                {
                    pipeline.Add(
                    new BsonDocument{
                        { "$match",  new BsonDocument {
                            { "$text",
                                new BsonDocument {
                                    { "$search",strNameFiler },
                                    { "$language","english" },
                                    { "$caseSensitive",false },
                                    { "$diacriticSensitive",false }
                                }
                            }
                        }}
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

                List<SalesDocumentItemsDetails> results = await _CommercialDocumentDetailImports.Aggregate<SalesDocumentItemsDetails>(pipeline).ToListAsync();

                //var results = await _entityActorsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves an object of a transactional Item by id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="commercialDocumentDetailId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, SalesDocumentItemsDetails? GetCommercialDocumentDetailsById, string? ErrorDescription)> GetCommercialDocumentDetailById(string userId, string ipAddress, string commercialDocumentDetailId)
        {

            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(commercialDocumentDetailId)))
                );

                var resultPrev = await _CommercialDocumentDetailImports.Aggregate<BsonDocument>(pipeline).ToListAsync();


                SalesDocumentItemsDetails result = resultPrev.Select(d => BsonSerializer.Deserialize<SalesDocumentItemsDetails>(d)).ToList()[0];


                return (true, result, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }


        #endregion

        #region Account Receivable
        /// <summary>
        /// Retrieves a list of Commercial documents of the Squad for the date span and 
        /// Document type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="shippingDate"></param>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<CommercialDocument>? CommercialDocuments, string? ErrorDescription)> GetAccountReceivable(string userId, string ipAddress, string squadId, DateTime shippingDate, string? filter = null, int? page = 1, int? perPage = 10)
        {
            try
            {
                string strNameFiler = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;

                var pipeline = new List<BsonDocument>();

                if (!(strNameFiler.ToLower() == "all" || strNameFiler.ToLower() == "todos" || strNameFiler.ToLower() == ""))
                {
                    pipeline.Add(
                    new BsonDocument{
                        { "$match",  new BsonDocument {
                            { "$text",
                                new BsonDocument {
                                    { "$search",strNameFiler },
                                    { "$language","english" },
                                    { "$caseSensitive",false },
                                    { "$diacriticSensitive",false }
                                }
                            }
                        }}
                    }
                    );
                }


                pipeline.Add(
                    new BsonDocument{
                        { "$match",
                            new BsonDocument{
                                { "ShipDate", new BsonDocument{
                                    { "$gte", shippingDate }
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

                List<CommercialDocument> results = await _SalesDocumentsCollection.Aggregate<CommercialDocument>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion

        #region Sales BI
        /// <summary>
        /// Retrieves a list of Commercial documents of the Squad for the date span and 
        /// Document type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="documentTypeId"></param>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<BISalesConsolidated>? CommercialDocuments, string? ErrorDescription)> GetSalesBI(string userId, string ipAddress, string squadId, DateTime startDate, DateTime endDate, string documentTypeId, string? filter = null, int? page = 1, int? perPage = 10)
        {
            try
            {
                string strNameFiler = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;

                var pipeline = new List<BsonDocument>();

                if (!(strNameFiler.ToLower() == "all" || strNameFiler.ToLower() == "todos" || strNameFiler.ToLower() == ""))
                {
                    pipeline.Add(
                    new BsonDocument{
                        { "$match",  new BsonDocument {
                            { "$text",
                                new BsonDocument {
                                    { "$search",strNameFiler },
                                    { "$language","english" },
                                    { "$caseSensitive",false },
                                    { "$diacriticSensitive",false }
                                }
                            }
                        }}
                    }
                );
                }

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

                List<CommercialDocument> resultsCommercialDocuments = await _SalesDocumentsCollection.Aggregate<CommercialDocument>(pipeline).ToListAsync();

                //automapper
                List<BISalesConsolidated> results = _mapper.Map<List<BISalesConsolidated>>(resultsCommercialDocuments);


                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion


    }
}
