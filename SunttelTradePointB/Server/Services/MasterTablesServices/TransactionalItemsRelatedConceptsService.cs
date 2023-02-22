using MongoDB.Bson;
using MongoDB.Driver;
using SunttelTradePointB.Client.Shared.ConceptSelectors;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Server.Services.MasterTablesServices
{
    /// <summary>
    /// Service inteded to provide CRUD functions to deal with Related Concepts to Transactional Items
    /// </summary>
    public class TransactionalItemsRelatedConceptsService : ITransactionalItemsRelatedConceptsBKService
    {
        //1. Llamar el manejador de la colección
        IMongoCollection<Box> _boxes;
        IMongoCollection<Box> _boxesCollection;
        IMongoCollection<SeasonBusiness> _seasonBusiness;
        IMongoCollection<SeasonBusiness> _seasonBusinessCollection;
        IMongoCollection<TransactionalItemType> _transactionalItemType;
        IMongoCollection<TransactionalItemStatus> _transactionalItemStatus;
        IMongoCollection<ConceptGroup> _transactionalItemGroup;
        IMongoCollection<ConceptGroup> _transactionalItemGroups;

        /// <summary>
        /// Constructor of the service which rretrieves the connection string
        /// </summary>
        /// <param name="config"></param>
        public TransactionalItemsRelatedConceptsService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _boxes = mongoDatabase.GetCollection<Box>("Box");
            _boxesCollection = mongoDatabase.GetCollection<Box>("Box");
            _seasonBusiness = mongoDatabase.GetCollection<SeasonBusiness>("BusinessSeasons");
            _seasonBusinessCollection = mongoDatabase.GetCollection<SeasonBusiness>("BusinessSeasons");
            _transactionalItemType = mongoDatabase.GetCollection<TransactionalItemType>("TransactionalItemTypes");
            _transactionalItemStatus = mongoDatabase.GetCollection<TransactionalItemStatus>("TransactionalItemStatuses");
            _transactionalItemGroup = mongoDatabase.GetCollection<ConceptGroup>("TransactionalItemsGroups");
            _transactionalItemGroups = mongoDatabase.GetCollection<ConceptGroup>("TransactionalItemsGroups");
        }

        /// <summary>
        /// Retrieves info of a particular Box
        /// </summary>
        /// <param name="boxID"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, Box? box, string? ErrorDescription)> GetBox(string userId, string ipAddress, string boxID)
        {
            try
            {
                var filterBox = Builders<Box>.Filter.Eq(x => x.Id, boxID);
                var resultBoxes = await _boxes.Find(filterBox).FirstOrDefaultAsync();

                if (resultBoxes == null)
                {

                    return (false, null, "Unpopulated Boxes");

                }
                else
                {
                    return (true, resultBoxes, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves the list of boxes
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<Box>? boxes, string? ErrorDescription)> GetBoxTable()
        {
            try
            {
                var boxes = await _boxesCollection.Find<Box>(new BsonDocument()).ToListAsync();

                if (boxes == null || boxes.Count == 0)
                {
                    return (false, null, "Unpopulated Boxes");
                }
                else
                {
                    return (true, boxes, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves the information of a particular Season
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, SeasonBusiness? season, string? ErrorDescription)> GetSeason(string userId, string ipAddress, string seasonId)
        {
            try
            {
                var filterSeasons = Builders<SeasonBusiness>.Filter.Eq(x => x.Id, seasonId);
                var resultSeasons = await _seasonBusiness.Find(filterSeasons).FirstOrDefaultAsync();

                if (resultSeasons == null)
                {
                    return (false, null, "Unpopulated Season Business");
                }
                else
                {
                    return (true, resultSeasons, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves the list of all seasons in the system
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<SeasonBusiness>? seasons, string? ErrorDescription)> GetSeasonsTable()
        {
            try
            {
                var seasons = await _seasonBusinessCollection.Find<SeasonBusiness>(new BsonDocument()).ToListAsync();

                if (seasons == null || seasons.Count == 0)
                {
                    return (false, null, "Unpopulated Season Business");
                }
                else
                {
                    return (true, seasons, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retives the information of a particular Transactional Item Type
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, TransactionalItemType? transactionalItemType, string? ErrorDescription)> GetTransactionalItemType(string userId, string ipAddress, string transactionalItemId)
        {
            try
            {
                var filterTransacionalItemTypes = Builders<TransactionalItemType>.Filter.Eq(x => x.Id, transactionalItemId);
                var resultTransactionalItemTypes = await _transactionalItemType.Find(filterTransacionalItemTypes).FirstOrDefaultAsync();

                if (resultTransactionalItemTypes == null)
                {
                    return (false, null, "Unpopulated Transactional Item Types");
                }
                else
                {
                    return (true, resultTransactionalItemTypes, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrives a table with the different Transactional Item Status
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<TransactionalItemStatus>? transactionalItemStatuses, string? ErrorDescription)> GetTransactionalStatusesTable()
        {
            try
            {
                var transactionalItemStatuses = await _transactionalItemStatus.Find<TransactionalItemStatus>(new BsonDocument()).ToListAsync();

                if (transactionalItemStatuses == null || transactionalItemStatuses.Count == 0)
                {
                    return (false, null, "Unpopulated Transactional Item Statuses");
                }
                else
                {
                    return (true, transactionalItemStatuses, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a list of Transactional Item Groups with a specified condition
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<ConceptGroup>? transactionalItemGroups, string? ErrorDescription)> GetTransactionalItemGroups(string userId, string ipAddress, string filterCondition)
        {
            try
            {
                string strFilterCondition = filterCondition == null ? "" : filterCondition;

                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument(
                        "$match",
                            new BsonDocument(
                                "Name",
                                new BsonDocument(
                                    "$regex", new BsonRegularExpression($"/{filterCondition}/i")
                            )
                        )
                    )
                );

                List<ConceptGroup> results = await _transactionalItemGroups.Aggregate<ConceptGroup>(pipeline).ToListAsync();
                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a particular TransactionalItemGroup
        /// </summary>
        /// <param name="transactionalItemGroupId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, ConceptGroup? transactionalItemGroup, string? ErrorDescription)> GetTransactionalItemGroup(string userId, string ipAddress, string transactionalItemGroupId)
        {
            try
            {
                var filterTransacionalItemGroup = Builders<ConceptGroup>.Filter.Eq(x => x.Id, transactionalItemGroupId);
                var resultTransactionalItemGroup = await _transactionalItemGroup.Find(filterTransacionalItemGroup).FirstOrDefaultAsync();

                if (resultTransactionalItemGroup == null)
                {
                    return (false, null, "Unpopulated Transactional Item Groups");
                }
                else
                {
                    return (true, resultTransactionalItemGroup, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Insert / Updates box information
        /// </summary>
        /// <param name="box"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, Box? box, string? ErrorDescription)> SaveBox(string userId, string ipAddress, Box box)
        {
            try
            {
                if(box.Id == null)
                {
                    box.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterBox = Builders<Box>.Filter.Eq("_id", new ObjectId(box.Id));

                var updateBoxOptions = new ReplaceOptions { IsUpsert = true };

                var resultBox = await _boxesCollection.ReplaceOneAsync(filterBox, box, updateBoxOptions);

                return (true, box, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Insert / Updates Season info
        /// </summary>
        /// <param name="season"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, SeasonBusiness? season, string? ErrorDescription)> SaveSeason(string userId, string ipAddress, SeasonBusiness season)
        {
            try
            {
                if (season.Id == null)
                {
                    season.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterSeason = Builders<SeasonBusiness>.Filter.Eq("_id", new ObjectId(season.Id));
                var updateSeasonOptions = new ReplaceOptions { IsUpsert = true };
                var resultSeason = await _seasonBusinessCollection.ReplaceOneAsync(filterSeason, season, updateSeasonOptions);
                return (true, season, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
                throw;
            }
        }

        /// <summary>
        ///  Insert / Updates Transactional Item Status 
        /// </summary>
        /// <param name="transactionalItemStatus"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, TransactionalItemStatus? transactionalItemStatus, string? ErrorDescription)> SaveStatus(string userId, string ipAddress, TransactionalItemStatus transactionalItemStatus)
        {
            try
            {
                if (transactionalItemStatus.Id == null)
                {
                    transactionalItemStatus.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterStatus = Builders<TransactionalItemStatus>.Filter.Eq("_id", new ObjectId(transactionalItemStatus.Id));
                var updateStatusOptions = new ReplaceOptions { IsUpsert = true };
                var resultStatus = await _transactionalItemStatus.ReplaceOneAsync(filterStatus, transactionalItemStatus, updateStatusOptions);
                return (true, transactionalItemStatus, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Insert / Uodates Transactional Item Types
        /// </summary>
        /// <param name="transactionalItemType"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, TransactionalItemType? transactionalItemType, string? ErrorDescription)> SaveTransactionalItemType(string userId, string ipAddress, TransactionalItemType transactionalItemType)
        {
            try
            {
                if(transactionalItemType.Id == null)
                {
                    transactionalItemType.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterTransactionalItemType = Builders<TransactionalItemType>.Filter.Eq("_id", new ObjectId(transactionalItemType.Id));
                var updateTransactionalItemTypeOptions = new ReplaceOptions { IsUpsert = true };
                var resultTransactionalItemType = await _transactionalItemType.ReplaceOneAsync(filterTransactionalItemType, transactionalItemType, updateTransactionalItemTypeOptions);
                return (true, transactionalItemType, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Insert / Updates Transactional Item Group
        /// </summary>
        /// <param name="transactionalItemGroup"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, ConceptGroup? transactionalItemGroup, string? ErrorDescription)> SaveTransactionalItemGroup(string userId, string ipAddress, ConceptGroup transactionalItemGroup)
        {
            try
            {
                if(transactionalItemGroup.Id == null)
                {
                    transactionalItemGroup.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterTransactionalItemGroup = Builders<ConceptGroup>.Filter.Eq("_id", new ObjectId(transactionalItemGroup.Id));
                var updateTransactionalItemGroupOptions = new ReplaceOptions { IsUpsert = true };
                var resultTransactionalItemGroup = await _transactionalItemGroup.ReplaceOneAsync(filterTransactionalItemGroup, transactionalItemGroup, updateTransactionalItemGroupOptions);
                return (true, transactionalItemGroup, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
    }
}
