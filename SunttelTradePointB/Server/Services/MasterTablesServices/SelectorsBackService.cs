using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Client.Shared.EntityShareComponents.RelatedConcepts;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;
using Syncfusion.PdfExport;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.IO.Pipelines;

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
        IMongoCollection<EntitiyRelationshipType> _entityRelationshipTypes;
        IMongoCollection<PalletType> _palletTypes;
        IMongoCollection<Shared.Common.EntityType> _entityTypes;
        IMongoCollection<AssemblyType> _assemblyTypes;

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
            _entityRelationshipTypes = mongoDatabase.GetCollection<EntitiyRelationshipType>("EntitiyRelationshipTypes");
            _palletTypes = mongoDatabase.GetCollection<PalletType>("PalletTypes");
            _entityTypes = mongoDatabase.GetCollection<Shared.Common.EntityType>("EntityTypes");
            _assemblyTypes = mongoDatabase.GetCollection<AssemblyType>("AssemblyTypes");
        }

        /// <summary>
        /// Retrieves the list of Entity/Nodes/Actors filtered by the optional parameter
        /// </summary>
        /// <param name="filterString"></param>
        /// <param name="roleIndex"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? EntityActorList, string? ErrorDescription)> GetSelectorListEntityActor(string? filterString, BasicRolesFilter? roleIndex)
        {
            try
            {
                Dictionary<BasicRolesFilter, string> roleDict = new Dictionary<BasicRolesFilter, string>
                {
                    { 0, "" },
                    { BasicRolesFilter.Provider, "Provider" },
                    { BasicRolesFilter.Customer, "Customer" },
                    { BasicRolesFilter.Carrier, "Carrier" },
                    { BasicRolesFilter.Company, "Company" },
                    { BasicRolesFilter.User, "User" },
                    { BasicRolesFilter.Employee, "Employee" }
                };

                string strNameFilter = filterString == null ? "" : filterString;
                string strRoleName = roleIndex == null ? "" : roleDict[roleIndex??0];

                //if(strRoleName.Length > 0)
                //{
                //    strNameFilter = "ALL";
                //}

                var pipe = new List<BsonDocument>();
                
                if (strNameFilter.ToUpper() != "ALL" && strNameFilter.ToUpper() != "TODOS")
                {
                   
                    pipe.Add(
                        new BsonDocument
                        {
                            { "$match", 
                                new BsonDocument{
                                    { "$text", new BsonDocument {
                                            { "$search", strNameFilter },
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

                if(strRoleName != "")
                {
                    pipe.Add(
                        new BsonDocument(
                        "$match",
                          new BsonDocument(
                                 "DefaultEntityRole.Name",
                                    new BsonDocument(
                                        "$regex", new BsonRegularExpression($"/{strRoleName}/i"))
                                )
                        )
                    );
                }

                pipe.Add(
                    new BsonDocument(
                        "$match", new BsonDocument("Status.IsEnabledForTransactions", true)
                    )
                );

                pipe.Add(
                    new BsonDocument {
                        { "$project",
                            new BsonDocument{
                                { "Code", 1 },
                                { "Name", 1 }
                            }
                        }
                    }
                );

                List<AtomConcept> results = await _entities.Aggregate<AtomConcept>(pipe).ToListAsync();

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

                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(strNameFiler)))
                );

                pipeline.Add(
                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "AddressList", 1 }
                            }
                        }
                    }
                );

                pipeline.Add(
                    new BsonDocument("$unwind", "$AddressList")
                );

                pipeline.Add(
                    new BsonDocument {
                        { "$replaceRoot",
                            new BsonDocument {
                                { "newRoot", "$AddressList" }
                            }
                        }
                    }
                );

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

                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument(
                        "$match",
                          new BsonDocument(
                                 "Name",
                                    new BsonDocument(
                                        "$regex", new BsonRegularExpression($"/{strNameFiler}/i"))
                            )
                    )
                );

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

                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument(
                        "$match",
                          new BsonDocument(
                                 "Name",
                                    new BsonDocument (
                                        "$regex", new BsonRegularExpression($"/{strNameFiler}/i"))
                            )
                    )
                );

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
                var pipeline = new List<BsonDocument>();

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

                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument(
                        "$match",
                          new BsonDocument(
                                 "Name",
                                    new BsonDocument (
                                        "$regex", new BsonRegularExpression($"/{strNameFiler}/i"))
                            )
                    )
                );

                pipeline.Add(
                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "Code", 1 },
                                { "Name", 1 },
                                { "GroupClassificationCriteria", 1 },
                            }
                        }
                    }
                );

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
                var pipeline = new List<BsonDocument>();

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
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument{
                        {"$match", new BsonDocument( "_id", new ObjectId(transactionalItemId))
                        }
                    }
                );

                pipeline.Add(
                    new BsonDocument(
                        "$unwind",  "$ProductPackingSpecs"
                    )
                );

                pipeline.Add(
                    new BsonDocument(
                        "$project",  new BsonDocument("ProductPackingSpecs.ModelRecipe", 1)
                    )
                );

                pipeline.Add(
                    new BsonDocument(
                        "$group",  new BsonDocument("_id", "$ProductPackingSpecs.ModelRecipe")
                    )
                );

                pipeline.Add(
                    new BsonDocument(
                        "$replaceRoot",  new BsonDocument("newRoot", "$_id")
                    )
                );

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


        /// <summary>
        /// Retrieves a list of EntitiyRelationship Types
        /// </summary>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<EntitiyRelationshipType>? entitiyRelationshipTypes, string? ErrorDescription)> GetSelectorEntitiyRelationshipTypes()
        {
            try
            {
                var entityRelationshipTypes = await _entityRelationshipTypes.Find<EntitiyRelationshipType>(new BsonDocument()).ToListAsync();

                if (entityRelationshipTypes == null || entityRelationshipTypes.Count == 0)
                {
                    return (false, null, "Unpopulated EntitiyRelationship Types");
                }
                else
                {
                    return (true, entityRelationshipTypes, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }


        /// <summary>
        /// Retrieves a list of Pallet Types
        /// </summary>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<PalletType>? palletTypes, string? ErrorDescription)> GetSelectorListPalletTypes()
        {
            try
            {
                var palletTypes = await _palletTypes.Find<PalletType>(new BsonDocument()).ToListAsync();

                if (palletTypes == null || palletTypes.Count == 0)
                {
                    return (false, null, "Unpopulated Pallet Types");
                }
                else
                {
                    return (true, palletTypes, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a list of Entity Types
        /// </summary>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<Shared.Common.EntityType>? entityTypes, string? ErrorDescription)> GetSelectorListEntityTypes()
        {
            try
            {
                var entityTypes = await _entityTypes.Find<Shared.Common.EntityType>(new BsonDocument()).ToListAsync();

                if (entityTypes == null || entityTypes.Count == 0)
                {
                    return (false, null, "Unpopulated Entity Types");
                }
                else
                {
                    return (true, entityTypes, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves the posible values for an specific recipe modifier
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="modifierId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<(bool IsSuccess, List<ProductRecipeQualityModifier>? productRecipeQualityModifiers, string? ErrorDescription)> GetProductRecipeQualityModifiersByModifierId(string userId, string ipAddress, string modifierId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Retrieves thelist of products used as packing material
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<(bool IsSuccess, List<AtomConcept>? materialsList, string? ErrorDescription)> GetSelectorListPackingMaterials(string filterString)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Retrieves a list of assembly types
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<AtomConcept>? assemblyTypes, string? ErrorDescription)> GetSelectorListAssemblyTypes(string filterString)
        {
            try
            {
                string strNameFiler = filterString == null ? "" : filterString;

                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument(
                        "$match",
                          new BsonDocument(
                                 "Name",
                                    new BsonDocument(
                                        "$regex", new BsonRegularExpression($"/{strNameFiler}/i"))
                            )
                    )
                );

                List<AtomConcept> results = await _assemblyTypes.Aggregate<AtomConcept>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
    }
}
