using CsvHelper.Configuration;
using CsvHelper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.CreditBkServices;
using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.Common;
using Syncfusion.Blazor.Grids;
using System.Globalization;
using SunttelTradePointB.Shared.SquadsMgr;

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
        IMongoCollection<CreditType> _CreditTypeCollection;
        IMongoCollection<CreditStatus> _CreditStatusCollection;
        IMongoCollection<CreditReason> _CreditReasonCollection;

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
            _CreditTypeCollection = mongoDatabase.GetCollection<CreditType>("CreditTypes");
            _CreditStatusCollection = mongoDatabase.GetCollection<CreditStatus>("CreditStatuses");
            _CreditReasonCollection = mongoDatabase.GetCollection<CreditReason>("CreditReasons");

        }

        #region Credit Documents
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
        /// <param name="isAsale"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<CreditDocument>? CreditDocumentsList, string? ErrorDescription)> GetCreditDocuments(string userId, string ipAddress, string squadId, DateTime startDate, DateTime endDate, bool isAsale, int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string filterString = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;
                var pipeline = new List<BsonDocument>();
                if (filterString!="all")
                {


                    //if (filterString.ToLower() != "all")
                    //{
                    //    pipeline.Add(
                    //        new BsonDocument {
                    //            { "$match",
                    //                new BsonDocument{
                    //                    { "SquadId", squadId },
                    //                    { "CreditDate", new BsonDocument{
                    //                        { "$gte", startDate },
                    //                        { "$lte", endDate }
                    //                    }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    );
                    //}

                    pipeline.Add(
                  new BsonDocument{
                        {"$match",  new BsonDocument{ {  "CreditDocumentType.IsASale",isAsale} } }
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

                    List<CreditDocument> results = await _CreditDocumentCollection.Aggregate<CreditDocument>(pipeline).ToListAsync();

                    return (true, results, null);
                }
                else
                {

                    pipeline.Add(
                new BsonDocument{
                        {"$match",  new BsonDocument{ {  "CreditDocumentType.IsASale",isAsale} } }
                      }
                     );

                    List<CreditDocument> creditDocuments = await _CreditDocumentCollection.Aggregate<CreditDocument>(pipeline).ToListAsync();

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
        #endregion

        #region Credit Types
        /// <summary>
        /// Returns a list of credit type with a filter like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<CreditType>? CreditTypesList, string? ErrorDescription)> GetCreditTypes(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string filterString = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;

                var pipeline = new List<BsonDocument>();

                // Filtro general
                if (filterString.ToLower() != "all")
                {
                    pipeline.Add(
                    new BsonDocument(
                        "$match",
                        new BsonDocument(
                                 "Name",
                                    new BsonDocument(
                                        "$regex", new BsonRegularExpression($"/{filterString}/i"))
                            )
                    )
                    );
                }
                // Filtro por SquadId
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

                List<CreditType> results = await _CreditTypeCollection.Aggregate<CreditType>(pipeline).ToListAsync();
                return (true, results, null);

            }

            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }



        /// <summary>
        /// Delete an CreditType not associated with CreditDocument
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditTypeId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteCreditTypeById(string userId, string ipAddress, string squadId, string? creditTypeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("CreditDocumentType._id", new ObjectId(creditTypeId))
                  )
               );

                pipeline.Add(
                    new BsonDocument("$group", new BsonDocument(
                        "_id", new BsonDocument(
                           "count", new BsonDocument(
                               "$sum", 1
                               )
                            )))
                    );


                var resultCount = _CreditDocumentCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _CreditTypeCollection.DeleteOne(s => s.Id == creditTypeId);
                    return (true, result.IsAcknowledged, null);
                }
                else
                {
                    return (true, false, ($"Count  {count}"));
                }
            }
            catch (Exception e)
            {
                return (false, false, e.Message);
            }
        }


        /// <summary>
        /// Saves an Credit document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditTypeId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, CreditType? CreditType, string? ErrorDescription)> GetCreditTypeById(string userId, string ipAddress, string squadId, string creditTypeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();
                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(creditTypeId)))
                );
                // Filtro por SquadId
                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("SquadId", squadId))
                );

                var resultPrev = await _CreditTypeCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                CreditType result = resultPrev.Select(d => BsonSerializer.Deserialize<CreditType>(d)).ToList()[0];

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
        /// <param name="creditType"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, CreditType? CreditType, string? ErrorDescription)> SaveCreditType(string userId, string ipAddress, string squadId, CreditType creditType)
        {
            try
            {
                if (creditType.Id == null || creditType.Id == "")
                {
                    creditType.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterCreditType = Builders<CreditType>.Filter.Eq("_id", new ObjectId(creditType.Id));
                var updateCreditType = new ReplaceOptions { IsUpsert = true };
                var resultCreditType = await _CreditTypeCollection.ReplaceOneAsync(filterCreditType, creditType, updateCreditType);

                return (true, creditType, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }
        #endregion

        #region Credit Status
        /// <summary>
        /// Saves an Credit document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<CreditStatus>? CreditStatusesList, string? ErrorDescription)> GetCreditStatuses(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string filterString = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;

                var pipeline = new List<BsonDocument>();

                // Filtro general
                if (filterString.ToLower() != "all")
                {
                    pipeline.Add(
                    new BsonDocument(
                        "$match",
                        new BsonDocument(
                                 "Name",
                                    new BsonDocument(
                                        "$regex", new BsonRegularExpression($"/{filterString}/i"))
                            )
                    )
                    );
                }
                // Filtro por SquadId
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

                List<CreditStatus> results = await _CreditStatusCollection.Aggregate<CreditStatus>(pipeline).ToListAsync();
                return (true, results, null);

            }

            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }





        /// <summary>
        /// Delete an CreditStatus not associated with CreditDocument
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditStatusid"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteCreditStatusById(string userId, string ipAddress, string squadId, string? creditStatusid)
        {
            try
            {
                var pipeline = new List<BsonDocument>();
                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("CreditDocumentStatus._id", new ObjectId(creditStatusid))
                  )
               );

                pipeline.Add(
                    new BsonDocument("$group", new BsonDocument(
                        "_id", new BsonDocument(
                           "count", new BsonDocument(
                               "$sum", 1
                               )
                            )))
                    );


                var resultCount = _CreditDocumentCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;

                if (count <= 0)
                {
                    var result = _CreditStatusCollection.DeleteOne(s => s.Id == creditStatusid);
                    return (true, result.IsAcknowledged, null);
                }
                else
                {
                    return (true, false, ($"Count creditDocuments {count}"));
                }
                return new();
            }
            catch (Exception e)
            {
                return (false, false, e.Message);
            }
        }




        /// <summary>
        /// Saves an Credit document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditStatusId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, CreditStatus? CreditStatus, string? ErrorDescription)> CreditStatusById(string userId, string ipAddress, string squadId, string creditStatusId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument {
                        { "$match",
                            new BsonDocument{
                                { "SquadId", squadId },
                                { "_id", new ObjectId(creditStatusId) }
                            }
                        }
                    }
                );

                var resultPrev = await _CreditStatusCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                CreditStatus result = resultPrev.Select(d => BsonSerializer.Deserialize<CreditStatus>(d)).ToList()[0];

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
        /// <param name="creditStatus"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, CreditStatus? CreditStatus, string? ErrorDescription)> SaveCreditStatus(string userId, string ipAddress, string squadId, CreditStatus creditStatus)
        {
            try
            {
                if (creditStatus.Id == null)
                {
                    creditStatus.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterCreditStatus = Builders<CreditStatus>.Filter.Eq("_id", new ObjectId(creditStatus.Id));
                var updateCreditStatus = new ReplaceOptions { IsUpsert = true };
                var resultCreditStatus = await _CreditStatusCollection.ReplaceOneAsync(filterCreditStatus, creditStatus, updateCreditStatus);

                return (true, creditStatus, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion

        #region Credit Reason
        /// <summary>
        /// Saves an Credit document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<CreditReason>? CreditReasonsList, string? ErrorDescription)> GetCreditReasons(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filter = null)
        {
            try
            {
                string filterString = filter == null ? "" : filter;
                var skip = (page - 1) * perPage;

                var pipeline = new List<BsonDocument>();

                // Filtro general
                if (filterString.ToLower() != "all")
                {
                    pipeline.Add(
                    new BsonDocument(
                        "$match",
                        new BsonDocument(
                                 "Name",
                                    new BsonDocument(
                                        "$regex", new BsonRegularExpression($"/{filterString}/i"))
                            )
                    )
                    );
                }
                // Filtro por SquadId
                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("SquadId", squadId))
                );

                // Filtro por SquadId
                //pipeline.Add(
                //    new BsonDocument(
                //        "$match",
                //        new BsonDocument(
                //                 "Id",
                //                    new BsonDocument(
                //                        "$regex", new BsonRegularExpression($"/{squadId}/i"))
                //            )

                //    )
                //);


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

                List<CreditReason> results = await _CreditReasonCollection.Aggregate<CreditReason>(pipeline).ToListAsync();
                return (true, results, null);

            }

            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }




        /// <summary>
        /// Delete an CreditReasons not associated with CreditDocument
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditReasonId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteCreditReasonsById(string userId, string ipAddress, string squadId, string? creditReasonId)
        {
            try
            {

                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("CreditDocumentReason._id", new ObjectId(creditReasonId))
                  )
               );

                pipeline.Add(
                    new BsonDocument("$group", new BsonDocument(
                        "_id", new BsonDocument(
                           "count", new BsonDocument(
                               "$sum", 1
                               )
                            )))
                    );


                var resultCount = _CreditDocumentCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;

                if (count <= 0)
                {
                    var result = _CreditReasonCollection.DeleteOne(s => s.Id == creditReasonId);
                    return (true, result.IsAcknowledged, null);
                }
                else
                {
                    return (true, false, ($"Count creditDocuments {count}"));
                }

            }
            catch (Exception e)
            {
                return (false, false, e.Message);
            }
        }



        /// <summary>
        /// Saves an Credit document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditTypeId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, CreditReason? CreditReason, string? ErrorDescription)> CreditReasonById(string userId, string ipAddress, string squadId, string creditReasonId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument {
                        { "$match",
                            new BsonDocument{
                                { "SquadId", squadId },
                                { "_id", new ObjectId(creditReasonId) }
                            }
                        }
                    }
                );

                var resultPrev = await _CreditReasonCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
                CreditReason result = resultPrev.Select(d => BsonSerializer.Deserialize<CreditReason>(d)).ToList()[0];

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
        /// <param name="creditReason"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, CreditReason? CreditReason, string? ErrorDescription)> SaveCreditReason(string userId, string ipAddress, string squadId, CreditReason creditReason)
        {
            try
            {
                if (creditReason.Id == null)
                {
                    creditReason.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterCreditReason = Builders<CreditReason>.Filter.Eq("_id", new ObjectId(creditReason.Id));
                var updateCreditReason = new ReplaceOptions { IsUpsert = true };
                var resultCreditReason = await _CreditReasonCollection.ReplaceOneAsync(filterCreditReason, creditReason, updateCreditReason);

                return (true, creditReason, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Upload file csv a products
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, string? ActorsNodesList, string? ErrorDescription)> SaveProductsCSV(string userId, string ipAddress, string squadId, IFormFile file)
        {
            try
            {
                List<CreditType>? lista = new List<CreditType>();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    // Configuración de CsvHelper
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ",",
                        HasHeaderRecord = true
                    };
                    var csv = new CsvReader(reader, config);

                    // Lectura de los registros del archivo CSV
                    var records = csv.GetRecords<CreditType>().ToList();
                    foreach (var record in records)
                    {
                        var creditType = await SaveCreditType(userId, ipAddress, squadId, record);
                    }
                    return (true, null, "Services created successfully");

                }

            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


    }
}
