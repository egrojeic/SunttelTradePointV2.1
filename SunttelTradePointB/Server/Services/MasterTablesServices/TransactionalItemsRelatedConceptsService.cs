using MongoDB.Bson;
using MongoDB.Driver;
using SixLabors.ImageSharp.ColorSpaces;
using SunttelTradePointB.Client.Shared.ConceptSelectors;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;
using System.IO.Pipelines;
using System.Net.WebSockets;

namespace SunttelTradePointB.Server.Services.MasterTablesServices
{
    /// <summary>
    /// Service inteded to provide CRUD functions to deal with Related Concepts to Transactional Items
    /// </summary>
    public class TransactionalItemsRelatedConceptsService : ITransactionalItemsRelatedConceptsBKService
    {
        //1. Llamar el manejador de la colección
        IMongoCollection<TransactionalItem> _TransactionalItemsCollection;
        IMongoCollection<Box> _boxes;
        IMongoCollection<Box> _boxesCollection;
        IMongoCollection<SeasonBusiness> _seasonBusiness;
        IMongoCollection<SeasonBusiness> _seasonBusinessCollection;
        IMongoCollection<TransactionalItemType> _transactionalItemType;
        IMongoCollection<TransactionalItemStatus> _transactionalItemStatus;
        IMongoCollection<ConceptGroup> _transactionalItemGroup;
        IMongoCollection<ConceptGroup> _transactionalItemGroups;
        IMongoCollection<AssemblyType> _assemblyType;
        IMongoCollection<LabelStyle> _labelStyle;
        IMongoCollection<LabelPaper> _LabelPaper;

        /// <summary>
        /// Constructor of the service which rretrieves the connection string
        /// </summary>
        /// <param name="config"></param>
        public TransactionalItemsRelatedConceptsService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
             _TransactionalItemsCollection = mongoDatabase.GetCollection<TransactionalItem>("TransactionalItems");
            _boxes = mongoDatabase.GetCollection<Box>("Box");
            _boxesCollection = mongoDatabase.GetCollection<Box>("Box");
            _seasonBusiness = mongoDatabase.GetCollection<SeasonBusiness>("BusinessSeasons");
            _seasonBusinessCollection = mongoDatabase.GetCollection<SeasonBusiness>("BusinessSeasons");
            _transactionalItemType = mongoDatabase.GetCollection<TransactionalItemType>("TransactionalItemTypes");
            _transactionalItemStatus = mongoDatabase.GetCollection<TransactionalItemStatus>("TransactionalItemStatuses");
            _transactionalItemGroup = mongoDatabase.GetCollection<ConceptGroup>("TransactionalItemsGroups");
            _transactionalItemGroups = mongoDatabase.GetCollection<ConceptGroup>("TransactionalItemsGroups");
            _assemblyType = mongoDatabase.GetCollection<AssemblyType>("AssemblyTypes");
            _labelStyle = mongoDatabase.GetCollection<LabelStyle>("LabelStyles");
            _LabelPaper = mongoDatabase.GetCollection<LabelPaper>("LabelPaperTypes");
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
        /// Retrives a Transactional Item Status by Id
        /// </summary>
        /// <param name="statusId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, TransactionalItemStatus? transactionalItemStatuses, string? ErrorDescription)> GetTransactionalStatusById(string userId, string ipAddress, string statusId)
        {
            try
            {
                var filterPrev = Builders<TransactionalItemStatus>.Filter.Eq(x => x.Id, statusId);
                var transactionalItemStatus = await _transactionalItemStatus.Find<TransactionalItemStatus>(filterPrev).FirstOrDefaultAsync();


                if (transactionalItemStatus == null)
                {
                    return (false, null, "Transactional Item Status not found");
                }
                else
                {
                    return (true, transactionalItemStatus, null);
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


                /*
                if (strFilterCondition.ToUpper() != "ALL" && strFilterCondition.ToUpper() != "TODOS")
                {

                    pipeline.Add(
                        new BsonDocument
                        {
                            { "$match",
                                new BsonDocument{
                                    { "$text", new BsonDocument {
                                            { "$search", strFilterCondition },
                                            { "$language", "english" },
                                            { "$caseSensitive", false },
                                            { "$diacriticSensitive", false}
                                        }
                                    }
                                }
                            }
                        }
                    );
                }
                */

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
        /// Retrieves a particular assembly type by its ID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="assemblyTypeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, AssemblyType? assemblyType, string? ErrorDescription)> GetAssemblyTypeByID(string userId, string ipAddress, string assemblyTypeId)
        {
            try
            {
                var filterAssemblyType = Builders<AssemblyType>.Filter.Eq(x => x.Id, assemblyTypeId);
                var resultAssemblyType = await _assemblyType.Find(filterAssemblyType).FirstOrDefaultAsync();

                if (resultAssemblyType == null)
                {
                    return (false, null, "Unpopulated Assembly Types");
                }
                else
                {
                    return (true, resultAssemblyType, null);
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
                if (box.Id == null)
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
                if (transactionalItemType.Id == null)
                {
                    transactionalItemType.Id = ObjectId.GenerateNewId().ToString();
                }

                if (transactionalItemType.QualityParameters != null)
                {
                    transactionalItemType.QualityParameters.ForEach(e =>
                    {
                        if (e.Id == null)
                            e.Id = ObjectId.GenerateNewId().ToString();
                    });

                }

                if (transactionalItemType.Groups != null)
                {
                    transactionalItemType.Groups.ForEach(e =>
                    {
                        if (e.Id == null)
                            e.Id = ObjectId.GenerateNewId().ToString();
                    });
                }



                if (transactionalItemType.TransactionalItemProcesses != null)
                {
                    transactionalItemType.TransactionalItemProcesses.ForEach(e =>
                    {
                        if (e.Id == null)
                            e.Id = ObjectId.GenerateNewId().ToString();
                    });
                }


                if (transactionalItemType.TransactionalItemTypeCharacteristics != null)
                {
                    transactionalItemType.TransactionalItemTypeCharacteristics.ForEach(e =>
                    {
                        if (e.Id == null)
                            e.Id = ObjectId.GenerateNewId().ToString();
                    });
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
                if (transactionalItemGroup.Id == null)
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


        /// <summary>
        /// Saves (INSERT/UPDATE) a Transactional Item Process Step
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="transactionalItemProcessStep"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, TransactionalItemProcessStep? transactionalItemProcessStep, string? ErrorDescription)> SaveTransactionalItemProcessStep(string userId, string ipAddress, string transactionalItemTypeId, TransactionalItemProcessStep transactionalItemProcessStep)
        {
            try
            {
                if (transactionalItemProcessStep.Id == null)
                {
                    transactionalItemProcessStep.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPrev = Builders<TransactionalItemType>.Filter.Eq(x => x.Id, transactionalItemTypeId);
                var resultPrev = await _transactionalItemType.Find(filterPrev).FirstOrDefaultAsync();

                if (transactionalItemTypeId.Length > 0 && resultPrev != null && resultPrev.TransactionalItemProcesses.Any(x => x.Id == transactionalItemProcessStep.Id))
                {
                    //Update element
                    var filter = Builders<TransactionalItemType>.Filter.And(
                        Builders<TransactionalItemType>.Filter.Eq(x => x.Id, transactionalItemTypeId),
                        Builders<TransactionalItemType>.Filter.ElemMatch(x => x.TransactionalItemProcesses, y => y.Id == transactionalItemProcessStep.Id)
                    );
                    var update = Builders<TransactionalItemType>.Update.Set(x => x.TransactionalItemProcesses[-1], transactionalItemProcessStep);
                    await _transactionalItemType.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add element
                    var filter = Builders<TransactionalItemType>.Filter.Eq(x => x.Id, transactionalItemTypeId);
                    var update = Builders<TransactionalItemType>.Update.AddToSet(x => x.TransactionalItemProcesses, transactionalItemProcessStep);
                    await _transactionalItemType.UpdateOneAsync(filter, update);
                }

                return (true, transactionalItemProcessStep, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Saves (INSERT/UPDATE) a Transactional Item Type Characteristic
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="transactionalItemTypeCharacteristic"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, TransactionalItemTypeCharacteristic? transactionalItemTypeCharacteristic, string? ErrorDescription)> SaveTransactionalItemTypeCharacteristic(string userId, string ipAddress, string transactionalItemTypeId, TransactionalItemTypeCharacteristic transactionalItemTypeCharacteristic)
        {
            try
            {
                if (transactionalItemTypeCharacteristic.Id == null)
                {
                    transactionalItemTypeCharacteristic.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPrev = Builders<TransactionalItemType>.Filter.Eq(x => x.Id, transactionalItemTypeId);
                var resultPrev = await _transactionalItemType.Find(filterPrev).FirstOrDefaultAsync();

                if (transactionalItemTypeId.Length > 0 && resultPrev != null && resultPrev.TransactionalItemTypeCharacteristics.Any(x => x.Id == transactionalItemTypeCharacteristic.Id))
                {
                    //Update element
                    var filter = Builders<TransactionalItemType>.Filter.And(
                        Builders<TransactionalItemType>.Filter.Eq(x => x.Id, transactionalItemTypeId),
                        Builders<TransactionalItemType>.Filter.ElemMatch(x => x.TransactionalItemTypeCharacteristics, y => y.Id == transactionalItemTypeCharacteristic.Id)
                    );
                    var update = Builders<TransactionalItemType>.Update.Set(x => x.TransactionalItemTypeCharacteristics[-1], transactionalItemTypeCharacteristic);
                    await _transactionalItemType.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add element
                    var filter = Builders<TransactionalItemType>.Filter.Eq(x => x.Id, transactionalItemTypeId);
                    var update = Builders<TransactionalItemType>.Update.AddToSet(x => x.TransactionalItemTypeCharacteristics, transactionalItemTypeCharacteristic);
                    await _transactionalItemType.UpdateOneAsync(filter, update);
                }

                return (true, transactionalItemTypeCharacteristic, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Saves (INSERT/UPDATE) a Transactional Item Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="transactionalItemQuality"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, TransactionalItemQuality? transactionalItemQuality, string? ErrorDescription)> SaveTransactionalItemQuality(string userId, string ipAddress, string transactionalItemTypeId, TransactionalItemQuality transactionalItemQuality)
        {
            try
            {
                if (transactionalItemQuality.Id == null)
                {
                    transactionalItemQuality.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPrev = Builders<TransactionalItemType>.Filter.Eq(x => x.Id, transactionalItemTypeId);
                var resultPrev = await _transactionalItemType.Find(filterPrev).FirstOrDefaultAsync();

                if (transactionalItemTypeId.Length > 0 && resultPrev != null && resultPrev.QualityParameters.Any(x => x.Id == transactionalItemQuality.Id))
                {
                    //Update element
                    var filter = Builders<TransactionalItemType>.Filter.And(
                        Builders<TransactionalItemType>.Filter.Eq(x => x.Id, transactionalItemTypeId),
                        Builders<TransactionalItemType>.Filter.ElemMatch(x => x.QualityParameters, y => y.Id == transactionalItemQuality.Id)
                    );
                    var update = Builders<TransactionalItemType>.Update.Set(x => x.QualityParameters[-1], transactionalItemQuality);
                    await _transactionalItemType.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add element
                    var filter = Builders<TransactionalItemType>.Filter.Eq(x => x.Id, transactionalItemTypeId);
                    var update = Builders<TransactionalItemType>.Update.AddToSet(x => x.QualityParameters, transactionalItemQuality);
                    await _transactionalItemType.UpdateOneAsync(filter, update);
                }

                return (true, transactionalItemQuality, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Saves (INSERT/UPDATE) a Recipe Modifier
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="recipeModifier"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, RecipeModifier? recipeModifier, string? ErrorDescription)> SaveRecipeModifier(string userId, string ipAddress, string transactionalItemTypeId, RecipeModifier recipeModifier)
        {
            try
            {
                if (recipeModifier.Id == null)
                {
                    recipeModifier.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPrev = Builders<TransactionalItemType>.Filter.Eq(x => x.Id, transactionalItemTypeId);
                var resultPrev = await _transactionalItemType.Find(filterPrev).FirstOrDefaultAsync();

                if (transactionalItemTypeId.Length > 0 && resultPrev != null && resultPrev.InRecipeModifiers.Any(x => x.Id == recipeModifier.Id))
                {
                    //Update element
                    var filter = Builders<TransactionalItemType>.Filter.And(
                        Builders<TransactionalItemType>.Filter.Eq(x => x.Id, transactionalItemTypeId),
                        Builders<TransactionalItemType>.Filter.ElemMatch(x => x.InRecipeModifiers, y => y.Id == recipeModifier.Id)
                    );
                    var update = Builders<TransactionalItemType>.Update.Set(x => x.InRecipeModifiers[-1], recipeModifier);
                    await _transactionalItemType.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add element
                    var filter = Builders<TransactionalItemType>.Filter.Eq(x => x.Id, transactionalItemTypeId);
                    var update = Builders<TransactionalItemType>.Update.AddToSet(x => x.InRecipeModifiers, recipeModifier);
                    await _transactionalItemType.UpdateOneAsync(filter, update);
                }

                return (true, recipeModifier, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Saves an assembly type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="assemblyType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, AssemblyType? assemblyType, string? ErrorDescription)> SaveAssemblyType(string userId, string ipAddress, AssemblyType assemblyType)
        {
            try
            {
                if (assemblyType.Id == null)
                {
                    assemblyType.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterAssemblyType = Builders<AssemblyType>.Filter.Eq("_id", new ObjectId(assemblyType.Id));
                var updateAssemblyType = new ReplaceOptions { IsUpsert = true };
                var resultAssemblyType = await _assemblyType.ReplaceOneAsync(filterAssemblyType, assemblyType, updateAssemblyType);
                return (true, assemblyType, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves information of a particular Label Style by its id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="labelStyleId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, LabelStyle? labelStyle, string? ErrorDescription)> GetLabelStyle(string userId, string ipAddress, string labelStyleId)
        {
            try
            {
                var filterLabelStyle = Builders<LabelStyle>.Filter.Eq(x => x.Id, labelStyleId);
                var resultLabelStyle = await _labelStyle.Find(filterLabelStyle).FirstOrDefaultAsync();

                if (resultLabelStyle == null)
                {
                    return (false, null, "Unpopulated Label Style");
                }
                else
                {
                    return (true, resultLabelStyle, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Save label style
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="labelStyle"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, LabelStyle? labelStyle, string? ErrorDescription)> SaveLabelStyle(string userId, string ipAddress, LabelStyle labelStyle)
        {
            try
            {
                if (labelStyle.Id == null)
                {
                    labelStyle.Id = ObjectId.GenerateNewId().ToString();
                }

                if (labelStyle.DataMaxLabelSettings != null)
                {
                    if (labelStyle.DataMaxLabelSettings.DataMaxFieldsSpecs != null)
                    {
                        labelStyle.DataMaxLabelSettings.DataMaxFieldsSpecs.ForEach(e =>
                        {
                            if (e.Id == null)
                            {
                                e.Id = ObjectId.GenerateNewId().ToString();
                            }
                        });
                    }
                }

                if (labelStyle.ZebraLabelSettings != null)
                {
                    if (labelStyle.ZebraLabelSettings.ZebraFieldsSpecs != null)
                    {
                        labelStyle.ZebraLabelSettings.ZebraFieldsSpecs.ForEach(e =>
                        {
                            if (e.Id == null)
                            {
                                e.Id = ObjectId.GenerateNewId().ToString();
                            }
                        });
                    }
                }

                var filterlabelStyle = Builders<LabelStyle>.Filter.Eq("_id", new ObjectId(labelStyle.Id));
                var updatelabelStyle = new ReplaceOptions { IsUpsert = true };
                var resultlabelStyle = await _labelStyle.ReplaceOneAsync(filterlabelStyle, labelStyle, updatelabelStyle);
                return (true, labelStyle, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves the list of  Label Styles
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="filterString"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<LabelStyle>? labelStyles, string? ErrorDescription)> GetLabelStyles(string userId, string ipAddress, string? filterString)
        {
            try
            {
                string filter = filterString == null ? "" : filterString;

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

                    List<LabelStyle> results = await _labelStyle.Aggregate<LabelStyle>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var labelStyles = await _labelStyle.Find<LabelStyle>(new BsonDocument()).ToListAsync();

                    if (labelStyles == null || labelStyles.Count == 0)
                    {
                        return (false, null, "Unpopulated Label Styles");
                    }
                    else
                    {
                        return (true, labelStyles, null);
                    }
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a list of label papers
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<LabelPaper>? labelPapers, string? ErrorDescription)> GetLabelPapers(string userId, string ipAddress, string? filterString)
        {
            try
            {
                var labelPapers = await _LabelPaper.Find<LabelPaper>(new BsonDocument()).ToListAsync();

                if (labelPapers == null || labelPapers.Count == 0)
                {
                    return (false, null, "Unpopulated Label Papers");
                }
                else
                {
                    return (true, labelPapers, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves a label paper by id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="labelPaperId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, LabelPaper? labelPaper, string? ErrorDescription)> GetLabelPaper(string userId, string ipAddress, string? labelPaperId)
        {
            try
            {
                var filterLabelPaper = Builders<LabelPaper>.Filter.Eq(x => x.Id, labelPaperId);
                var resultLabelStyle = await _LabelPaper.Find(filterLabelPaper).FirstOrDefaultAsync();

                if (resultLabelStyle == null)
                {
                    return (false, null, "Label Paper NOT Found");
                }
                else
                {
                    return (true, resultLabelStyle, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Save Laabel Paper
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="labelPaper"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, LabelPaper? labelPaper, string? ErrorDescription)> SaveLabelPaper(string userId, string ipAddress, LabelPaper labelPaper)
        {
            try
            {
                if (labelPaper.Id == null)
                {
                    labelPaper.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterBox = Builders<LabelPaper>.Filter.Eq("_id", new ObjectId(labelPaper.Id));

                var updateBoxOptions = new ReplaceOptions { IsUpsert = true };

                var resultBox = await _LabelPaper.ReplaceOneAsync(filterBox, labelPaper, updateBoxOptions);

                return (true, labelPaper, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }



        /// <summary>
        /// Delete an ConceptGroup not associated with TransactionalItems
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="conceptGroupId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteConceptGroupById(string userId, string ipAddress, string squadId, string? conceptGroupId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("Groups._id", new ObjectId(conceptGroupId))
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


                var resultCount = _TransactionalItemsCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _transactionalItemGroups.DeleteOne(s => s.Id == conceptGroupId);
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
        /// Delete an Box not associated with TransactionalItems
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="boxId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteBoxById(string userId, string ipAddress, string squadId, string? boxId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("PackingBoxToSale._id", new ObjectId(boxId))
                  )
               );

                pipeline.Add(
                new BsonDocument("$match",
                new BsonDocument("PackingBoxToBuy._id", new ObjectId(boxId))
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


                var resultCount = _TransactionalItemsCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _boxes.DeleteOne(s => s.Id == boxId);
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
        /// Delete an LabelStyle not associated with TransactionalItems
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="labelStyleId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteLabelStyleById(string userId, string ipAddress, string squadId, string? labelStyleId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("PalletLabelStyle._id", new ObjectId(labelStyleId))
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


                var resultCount = _TransactionalItemsCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _labelStyle.DeleteOne(s => s.Id == labelStyleId);
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
        /// Delete an LabelPaper not associated with TransactionalItems
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="labelPaperId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteLabelPaperById(string userId, string ipAddress, string squadId, string? labelPaperId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("Paper._id", new ObjectId(labelPaperId))
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


                var resultCount = _TransactionalItemsCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _LabelPaper.DeleteOne(s => s.Id == labelPaperId);
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
        /// Delete an SeasonBusiness not associated with TransactionalItems
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="seasonBusinessId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteSeasonBusinessById(string userId, string ipAddress, string squadId, string? seasonBusinessId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("Season._id", new ObjectId(seasonBusinessId))
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


                var resultCount = _TransactionalItemsCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _seasonBusiness.DeleteOne(s => s.Id == seasonBusinessId);
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
        /// Delete an TransactionalItemStatus not associated with TransactionalItems
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="transactionalItemStatusId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteTransactionalItemStatusById(string userId, string ipAddress, string squadId, string? transactionalItemStatusId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("Status._id", new ObjectId(transactionalItemStatusId))
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


                var resultCount = _TransactionalItemsCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _transactionalItemStatus.DeleteOne(s => s.Id == transactionalItemStatusId);
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
        /// Delete an TransactionalItemType not associated with TransactionalItems
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="transactionalItemTypeId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteTransactionalItemTypeById(string userId, string ipAddress, string squadId, string? transactionalItemTypeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("TotalPriceItemTypeBasedOn._id", new ObjectId(transactionalItemTypeId))
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


                var resultCount = _TransactionalItemsCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _transactionalItemType.DeleteOne(s => s.Id == transactionalItemTypeId);
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
        /// Delete an assemblyType not associated with TransactionalItems
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="assemblyTypeId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteAssemblyTypeById(string userId, string ipAddress, string squadId, string? assemblyTypeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("AssemblyRecipeItems._id", new ObjectId(assemblyTypeId))
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


                var resultCount = _TransactionalItemsCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _assemblyType.DeleteOne(s => s.Id == assemblyTypeId);
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

    }
}
