using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Client.Shared.EntityShareComponents.RelatedConcepts;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.SquadsMgr;
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
        IMongoCollection<EntityActor> _entitiesActor;
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
        IMongoCollection<TransactionalItem> _transactionalItems;

        IMongoCollection<LabelStyle> _labelStyle;
        IMongoCollection<LabelPaper> _LabelPaper;


        /// <summary>
        /// Service class initialize
        /// </summary>
        /// <param name="config"></param>
        public SelectorsBackService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _entitiesActor = mongoDatabase.GetCollection<EntityActor>("EntityNodes");
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
            _transactionalItems = mongoDatabase.GetCollection<TransactionalItem>("TransactionalItems");


            _labelStyle = mongoDatabase.GetCollection<LabelStyle>("LabelStyles");
            _LabelPaper = mongoDatabase.GetCollection<LabelPaper>("LabelPaperTypes");



        }

        /// <summary>
        /// Retrieves the list of Entity/Nodes/Actors filtered by the optional parameter
        /// </summary>
        /// <param name="filterString"></param>
        /// <param name="roleIndex"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? EntityActorList, string? ErrorDescription)> GetSelectorListEntityActor(string? filterString, string squadId, BasicRolesFilter? roleIndex)
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
                    { BasicRolesFilter.Employee, "Employee" },
                    { BasicRolesFilter.SalesPerson, "Salesperson" }
                };

                string strNameFilter = filterString == null ? "" : filterString;

                string strRoleName = roleIndex == null ? "" : roleDict[roleIndex ?? 0];

                var pipe = new List<BsonDocument>();

                if (!strNameFilter.ToLower().Contains("all"))
                {
                    pipe.Add(
                new BsonDocument(
                    "$match",
                      new BsonDocument(
                             "Name",
                                new BsonDocument(
                                    "$regex", new BsonRegularExpression($"/{strNameFilter}/i"))
                        )
                )
                 );
                }



                pipe.Add(
                       new BsonDocument(
                       "$match",
                         new BsonDocument("DefaultEntityRole.Name", strRoleName)
                               )
                   );

                //pipe.Add(
                //    new BsonDocument(
                //        "$match", new BsonDocument("SquadId",new ObjectId())
                //    )
                //);

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

                if (strNameFilter.ToLower() != "all")
                    results = results.Where(n => n.Name.ToLower().Contains(strNameFilter.ToLower())).ToList();
                else
                    results = results.Where(n => n.Id != null).ToList();

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
                //string strNameFiler = filterString == null ? "" : filterString;

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

                //pipeline.Add(
                //    new BsonDocument(
                //        "$match",
                //          new BsonDocument(
                //                 "Name",
                //                    new BsonDocument(
                //                        "$regex", new BsonRegularExpression($"/{strNameFiler}/i"))
                //            )
                //    )
                //);

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
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? EntityRolesList, string? ErrorDescription)> GetSelectorListEntityRoles(string? filterString, int? page = 1, int? perPage = 10)
        {
            try
            {
                var skip = (page - 1) * perPage;


                var pipeline = new List<BsonDocument>();

                if (!string.IsNullOrEmpty(filterString) && filterString.ToLower() !="all" && !filterString.ToLower().Equals("todos")) // se agregó verificación de si strNameFiler está vacío
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

                //pipeline.Add(
                //    new BsonDocument(
                //        "$match",
                //          new BsonDocument(
                //                 "Name",
                //                    new BsonDocument(
                //                        "$regex", new BsonRegularExpression($"/{strNameFiler}/i"))
                //            )
                //    )
                //);

                pipeline.Add(
                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "Code", 1 },
                                { "Name", 1 },
                                { nameof(EntityRole.EntityRoleClassifier), 1 }
                            }
                        }
                    }
                );

                //pipeline.Add(
                //    new BsonDocument{
                //        {"$skip",  skip}
                //        }
                //    );

                //pipeline.Add(
                //    new BsonDocument{
                //        {"$limit",  perPage}
                //    }
                //);

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
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument{
                        { "$project",
                            new BsonDocument{
                                { "Code", 1 },
                                { "Name", 1 }
                            }
                        }
                    }
                );

                List<Box> results = await _boxes.Aggregate<Box>(pipeline).ToListAsync();

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
                      new BsonDocument{
                        {"$project", new BsonDocument( "TransactionalItemModels", 1)
                        }
                      }
                  );

                pipeline.Add(
                     new BsonDocument{
                        {"$unwind", "$TransactionalItemModels"
                        }
                     }
                 );

                pipeline.Add(
                    new BsonDocument{
                        {"$replaceRoot", new BsonDocument( "newRoot", "$TransactionalItemModels")
                        }
                    }
                );

                List<AtomConcept> results = await _transactionalItems.Aggregate<AtomConcept>(pipeline).ToListAsync();

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
        public async Task<(bool IsSuccess, List<IdentificationType>? identificationTypes, string? ErrorDescription)> GetSelectorListIdentificationTypes(string? filterName = null)
        {
            try
            {
                string strNameFiler = filterName == null ? "" : filterName;

                var pipeline = new List<BsonDocument>();

                if (strNameFiler.ToLower() != "all")
                {
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
                }

                var identificationTypes = await _identificationTypes.Aggregate<IdentificationType>(pipeline).ToListAsync();

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
                FindOptions<AtomConcept> findOptions = new FindOptions<AtomConcept>
                {
                    Projection = Builders<AtomConcept>.Projection.Include(cx => cx.Name)
                };
                List<AtomConcept> results = await _addresses.FindAsync(Builders<AtomConcept>.Filter.In("Groups.Name", new[] { "Network Platforms" }), findOptions).Result.ToListAsync<AtomConcept>();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
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
        public async Task<(bool IsSuccess, List<PalletType>? palletTypes, string? ErrorDescription)> GetSelectorListPalletTypes(string? filterName = null)
        {
            try
            {
                string strNameFiler = filterName == null ? "" : filterName;

                var pipeline = new List<BsonDocument>();

                if (strNameFiler.ToLower() != "all")
                {
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
                }

                var palletTypes = await _palletTypes.Aggregate<PalletType>(pipeline).ToListAsync();

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
        public async Task<(bool IsSuccess, List<Shared.Common.EntityType>? entityTypes, string? ErrorDescription)> GetSelectorListEntityTypes(string? filterName = null)
        {
            try
            {
                string strNameFiler = filterName == null ? "" : filterName;

                var pipeline = new List<BsonDocument>();

                if (strNameFiler.ToLower() != "all")
                {
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
                }

                var entityTypes = await _entityTypes.Aggregate<Shared.Common.EntityType>(pipeline).ToListAsync();

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
        public async Task<(bool IsSuccess, List<ProductRecipeQualityModifier>? productRecipeQualityModifiers, string? ErrorDescription)> GetProductRecipeQualityModifiersByModifierId(string userId, string ipAddress, string modifierId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$unwind", "$InRecipeModifiers")
                );

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("InRecipeModifiers._id", new ObjectId(modifierId)))
                );

                pipeline.Add(
                    new BsonDocument("$project", new BsonDocument("InRecipeModifiers", 1))
                );

                pipeline.Add(
                    new BsonDocument("$replaceRoot", new BsonDocument("newRoot", "$InRecipeModifiers"))
                );

                var resultPrev = await _transactionalItemsTypes.Aggregate<BsonDocument>(pipeline).ToListAsync();


                List<ProductRecipeQualityModifier> result = resultPrev.Select(d => BsonSerializer.Deserialize<ProductRecipeQualityModifier>(d)).ToList();


                return (true, result, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves thelist of products used as packing material
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<AtomConcept>? materialsList, string? ErrorDescription)> GetSelectorListPackingMaterials(string filterString)
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
                if (strNameFiler.Length > 0 && strNameFiler.ToLower() != "all")
                {
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
                }
                List<AtomConcept> results = await _assemblyTypes.Aggregate<AtomConcept>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves a selector list for paper label types
        /// </summary>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? labelPapers, string? ErrorDescription)> GetSelectorListLabelPaper()
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument{
                        { "$project",
                            new BsonDocument{
                                { "Name", 1 }
                            }
                        }
                    }
                );

                List<AtomConcept> results = await _LabelPaper.Aggregate<AtomConcept>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves the list of Entity/Vendors filtered by the optional parameter
        /// </summary>
        /// <param name="isASale"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="filterString"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? VendorsList, string? ErrorDescription)> GetVendors(bool isASale, string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filterString = null)
        {
            try
            {
                var pipe = new List<BsonDocument>();

                string strNameFilter = filterString == null ? "" : filterString;
                var skip = (page - 1) * perPage;

                if (strNameFilter.ToUpper() != "ALL" && strNameFilter.ToUpper() != "TODOS" && strNameFilter != "")
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

                if (isASale)
                {
                    //Es venta
                    pipe.Add(
                        new BsonDocument
                        {
                            { "$match",
                                new BsonDocument{
                                    { "EntityActor.DefaultEntityRole.IsCompany", true },
                                    { "SquadId", $"/{squadId}"}
                                }
                            }
                        }
                    );
                }
                else
                {
                    //Es compra
                    pipe.Add(
                        new BsonDocument
                        {
                            { "$match",
                                new BsonDocument{
                                    { "SquadId", $"/{squadId}"}
                                }
                            }
                        }
                    );

                }

                pipe.Add(
                    new BsonDocument{
                        {"$skip",  skip}
                    }
                );

                pipe.Add(
                    new BsonDocument{
                        {"$limit",  perPage}
                    }
                );

                List<AtomConcept> results = await _entities.Aggregate<AtomConcept>(pipe).ToListAsync();
                //var results = new List<AtomConcept>();

                if (strNameFilter.ToLower() == "all")
                    results = _entities.Find(e => e.Id != null && e.SquadId.ToLower() == squadId.ToLower()).ToList().Take(200).ToList();

                if (!(strNameFilter.ToLower() == "all"))
                    results = _entities.Find(e =>e.SquadId.ToLower() == squadId.ToLower()  &&  e.Name.ToLower().Contains(strNameFilter.ToLower())).ToList();

                if (isASale) results = results.Where(e => e.SquadId.ToLower() == squadId.ToLower()).ToList();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves the list of Entity/Buyers filtered by the optional parameter
        /// </summary>
        /// <param name="isASale"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="filterString"></param>
        /// <param name="DocumentTypeId"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? BuyersList, string? ErrorDescription)> GetBuyers(bool isASale, string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? filterString = null)
        {
            try
            {
                var pipe = new List<BsonDocument>();
                string strNameFilter = filterString == null ? "" : filterString;
                var skip = (page - 1) * perPage;

                if (strNameFilter.ToUpper() != "ALL" && strNameFilter.ToUpper() != "TODOS" && strNameFilter != "")
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

                if (!isASale)
                {
                    //Es compra
                    pipe.Add(
                    new BsonDocument
                    {
                        { "$match",
                            new BsonDocument{
                                { "DefaultEntityRole.EntityRoleClassifier.Name", new BsonRegularExpression("ORGANIZATION", "i") },
                                { "SquadId", squadId}
                            }
                        }
                    }
                    );
                }
                else
                {
                    //Es venta
                    pipe.Add(
                    new BsonDocument
                    {
                        { "$match",
                            new BsonDocument{
                                { "SquadId", squadId}
                            }
                        }
                    }
                    );
                }

                pipe.Add(
                    new BsonDocument{
                        {"$skip",  skip}
                    }
                );

                pipe.Add(
                    new BsonDocument{
                        {"$limit",  perPage}
                    }
                );

                List<AtomConcept> results = new();
                if (strNameFilter.ToLower() == "all")
                    results = _entities.Find(e => e.Id != null && e.SquadId == squadId).ToList();

                if (!(strNameFilter.ToLower() == "all"))
                    results = _entities.Find(e => e.Name.ToLower().Contains(strNameFilter.ToLower())).ToList();

                if (!isASale) results = results.Where(e =>e.SquadId !=null &&  e.SquadId.ToLower() == squadId.ToLower()).ToList();

                // List<AtomConcept> results = await _entities.Aggregate<AtomConcept>(pipe).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves the list of Entity/Carriers filtered by the optional parameter
        /// </summary>
        /// <param name="filterString"></param>
        /// <param name="DocumentTypeId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? CarriersList, string? ErrorDescription)> GetCarriers(string DocumentTypeId, string? filterString)
        {
            try
            {
                string strNameFilter = filterString == null ? "" : filterString;
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

                List<AtomConcept> results = await _entities.Aggregate<AtomConcept>(pipe).ToListAsync();



                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves the list of Entity/SalesPersons filtered by the optional parameter
        /// </summary>
        /// <param name="filterString"></param>
        /// <param name="DocumentTypeId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<AtomConcept>? SalesPersonsList, string? ErrorDescription)> GetSalesPersons(string DocumentTypeId, string? filterString)
        {
            try
            {
                string strNameFilter = filterString == null ? "" : filterString;
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


                List<AtomConcept> results = await _entities.Aggregate<AtomConcept>(pipe).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

     
    }
}
