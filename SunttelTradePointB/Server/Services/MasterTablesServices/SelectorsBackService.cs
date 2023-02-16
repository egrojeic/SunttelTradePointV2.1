using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;
using Syncfusion.PdfExport;

namespace SunttelTradePointB.Server.Services.MasterTablesServices
{

    /// <summary>
    /// Service to provide Data to fill selectors of concepts for controls in the app
    /// </summary>
    public class SelectorsBackService : ISelectorDataSource
    {

        IMongoCollection<AtomConcept> _entities;
        IMongoCollection<AtomConcept> _addresses;
        IMongoCollection<AtomConcept> _entityComercialGroups;
        IMongoCollection<AtomConcept> _entityRoles;
        IMongoCollection<AtomConcept> _seasons;
        IMongoCollection<AtomConcept> _transactionalItemsGroups;
        IMongoCollection<AtomConcept> _transactionalItemsTypes;
        IMongoCollection<Box> _boxes;
        IMongoCollection<AtomConcept> _transactionalItemsModels;
        IMongoCollection<IdentificationType> _identificationTypes;

        /// <summary>
        /// Service class initialize
        /// </summary>
        /// <param name="config"></param>
        public SelectorsBackService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _entities = mongoDatabase.GetCollection<AtomConcept>("EntityNodes");
            _addresses = mongoDatabase.GetCollection<AtomConcept>("EntityNodes");
            _entityComercialGroups = mongoDatabase.GetCollection<AtomConcept>("EntityGroups");
            _entityRoles = mongoDatabase.GetCollection<AtomConcept>("Roles");
            _seasons = mongoDatabase.GetCollection<AtomConcept>("BusinessSeasons");
            _transactionalItemsGroups = mongoDatabase.GetCollection<AtomConcept>("TransactionalItemsGroups");
            _transactionalItemsTypes = mongoDatabase.GetCollection<AtomConcept>("TransactionalItemTypes");
            _boxes = mongoDatabase.GetCollection<Box>("Box");
            _transactionalItemsModels = mongoDatabase.GetCollection<AtomConcept>("TransactionalItems");
            _identificationTypes = mongoDatabase.GetCollection<IdentificationType>("IdentificationTypes");

        }

        /// <summary>
        /// Retrieves the list of Entity/Nodes/Actors filtered by the optional parameter
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<AtomConcept>? EntityActorList, string? ErrorDescription)> GetSelectorListEntityActor(string? filterString)
        {
            try
            {
                string strNameFiler = filterString == null ? "" : filterString;

                var pipeline = new[] {

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
                    },
                    new BsonDocument(
                        "$match",
                        new BsonDocument("Status.IsEnabledForTransactions",true)
                    ),
                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "Code", 1 },
                                { "Name", 1 }
                            }
                        }
                    },
                };

                List<AtomConcept> results = await _entities.Aggregate<AtomConcept>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrives the different address of an Entity
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? EntityActorAddressList, string? ErrorDescription)> GetSelectorListEntityAddress(string? entityId)
        {
            try
            {
                string strNameFiler = entityId == null ? "" : entityId;

                var pipeline = new[] {


                    new BsonDocument(
                        "$match",
                        new BsonDocument("_id",new ObjectId(strNameFiler))
                    ),
                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "AddressList", 1 }
                            }
                        }
                    },
                    new BsonDocument(
                        "$unwind", "$AddressList"
                    ),
                    new BsonDocument {
                        { "$replaceRoot",
                            new BsonDocument {
                                { "newRoot", "$AddressList" }
                            }
                        }
                    },
                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "Code", 1 },
                                { "Name", 1 }
                            }
                        }
                    },
                };

                List<AtomConcept> results = await _entities.Aggregate<AtomConcept>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves Entity Groups matching a filter pattern
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? EntityGroupsList, string? ErrorDescription)> GetSelectorListEntityGroups(string? filterString)
        {
            try
            {
                string strNameFiler = filterString == null ? "" : filterString;

                var pipeline = new[] {


                    new BsonDocument(
                        "$match",
                          new BsonDocument(
                                 "Name",
                                    new BsonDocument (
                                        "$regex", new BsonRegularExpression($"/{strNameFiler}/i"))
                            )
                    ),

                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "Code", 1 },
                                { "Name", 1 }
                            }
                        }
                    },
                };

                List<AtomConcept> results = await _entityComercialGroups.Aggregate<AtomConcept>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrives the list of possible Entity Roles
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? EntityRolesList, string? ErrorDescription)> GetSelectorListEntityRoles(string? filterString)
        {
            try
            {
                string strNameFiler = filterString == null ? "" : filterString;

                var pipeline = new[] {


                    new BsonDocument(
                        "$match",
                          new BsonDocument(
                                 "Name",
                                    new BsonDocument (
                                        "$regex", new BsonRegularExpression($"/{strNameFiler}/i"))
                            )
                    ),

                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "Code", 1 },
                                { "Name", 1 }
                            }
                        }
                    },
                };

                List<AtomConcept> results = await _entityRoles.Aggregate<AtomConcept>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrives the list of seasosns
        /// </summary>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? SeasonsList, string? ErrorDescription)> GetSelectorListSeasonBusiness()
        {
            try
            {

                var pipeline = new[] {

                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "Code", 1 },
                                { "Name", 1 }
                            }
                        }
                    },
                };

                List<AtomConcept> results = await _seasons.Aggregate<AtomConcept>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrives Transactional Items groups
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<ConceptGroup>? TransactionalItemGroupsList, string? ErrorDescription)> GetSelectorListTransactionalItemGroups(string? filterString)
        {
            try
            {
                string strNameFiler = filterString == null ? "" : filterString;

                var pipeline = new[] {


                    new BsonDocument(
                        "$match",
                          new BsonDocument(
                                 "Name",
                                    new BsonDocument (
                                        "$regex", new BsonRegularExpression($"/{strNameFiler}/i"))
                            )
                    ),

                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "Code", 1 },
                                { "Name", 1 },
                                { "GroupClassificationCriteria", 1 },
                            }
                        }
                    },
                };

                List<ConceptGroup> results = await _transactionalItemsGroups.Aggregate<ConceptGroup>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }

        /// <summary>
        /// Retrieves a list of Transactional Item Types
        /// </summary>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? TransactionalItemTypesList, string? ErrorDescription)> GetSelectorListTransactionalItemTypes()
        {
            try
            {

                var pipeline = new[] {

                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "Code", 1 },
                                { "Name", 1 }
                            }
                        }
                    },
                };

                List<AtomConcept> results = await _transactionalItemsTypes.Aggregate<AtomConcept>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves a list of Boxes
        /// </summary>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<Box>? boxes, string? ErrorDescription)> GetSelectorListBoxes()
        {
            try
            {

                List<Box> results = await _boxes.Find<Box>(new BsonDocument()).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a the list of the different models are already registered for a transactional item
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? transactionalItemModels, string? ErrorDescription)> GetSelectorListTransactionalItemModels(string transactionalItemId)
        {
            try
            {

                var pipeline = new[] {
                    new BsonDocument{
                        {"$match", new BsonDocument( "_id", new ObjectId(transactionalItemId))
                        }
                    },
                    new BsonDocument(
                        "$unwind",  "$ProductPackingSpecs"
                    ),
                    new BsonDocument(
                        "$project",  new BsonDocument("ProductPackingSpecs.ModelRecipe", 1)
                    ),
                    new BsonDocument(
                        "$group",  new BsonDocument("_id", "$ProductPackingSpecs.ModelRecipe")
                    ),
                    new BsonDocument(
                        "$replaceRoot",  new BsonDocument("newRoot", "$_id")
                    )
                };

                List<AtomConcept> results = await _transactionalItemsTypes.Aggregate<AtomConcept>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Returns a list of the different Identification Types in the system
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<IdentificationType>? identificationTypes, string? ErrorDescription)> GetSelectorListIdentificationTypes()
        {
            try
            {
                var identificationTypes = await _identificationTypes.Find<IdentificationType>(new BsonDocument()).ToListAsync();

                if (identificationTypes == null || identificationTypes.Count == 0)
                {
                    return (false, null, "Unpopulated Identification Types");
                }
                else
                {
                    return (true, identificationTypes, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrives Id, and name of electronic Address which group is Network Platform
        /// db.EntityNodes.find({"Groups.Name": {"$in": ["Network Platforms"]}}, {"Name": 1})
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<AtomConcept>? ElectronicAddressEntities, string? ErrorDescription)> GetSelectorElectronicAddressEntities()
        {
            try
            {
                FindOptions<AtomConcept> findOptions= new FindOptions<AtomConcept> { 
                    Projection= Builders<AtomConcept>.Projection.Include(cx=>cx.Name)
                };
                List<AtomConcept> results = await _addresses.FindAsync(Builders<AtomConcept>.Filter.In("Groups.Name", new[] { "Network Platforms" }), findOptions).Result.ToListAsync<AtomConcept>();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return(false, null, e.Message);
            }
        }
    }
}
