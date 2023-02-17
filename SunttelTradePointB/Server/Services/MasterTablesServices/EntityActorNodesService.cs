using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json.Bson;
using Radzen.Blazor.Rendering;
using SharpCompress.Writers;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Server.Migrations;
using SunttelTradePointB.Shared.Common;
using System.Net;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Xml.Linq;

namespace SunttelTradePointB.Server.Services.MasterTablesServices
{


    /// <summary>
    /// implemnts interface to deal with Entity/Nodes information
    /// </summary>
    public class EntityActorNodesService : IActorsNodes
    {

        IMongoCollection<EntityActor> _entityActorsCollection;
        IMongoCollection<EntityRole> _rolesCollection;
        IMongoCollection<BsonDocument> _generalEntityCollection;
        IMongoCollection<EntityGroup> _entityGroup;


        /// <summary>
        /// Initialize the component passing the configuration to get the connection string
        /// </summary>
        /// <param name="config"></param>
        public EntityActorNodesService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _entityActorsCollection = mongoDatabase.GetCollection<EntityActor>("EntityNodes");

            _generalEntityCollection = mongoDatabase.GetCollection<BsonDocument>("EntityNodes");


            _rolesCollection = mongoDatabase.GetCollection<EntityRole>("Roles");

            _entityActorsCollection = mongoDatabase.GetCollection<EntityActor>("EntityNodes");
            _entityGroup = mongoDatabase.GetCollection<EntityGroup>("EntityGroups");
        }



        /// <summary>
        /// Retrieves the whole object of an Entity/Actor
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, AtomConcept entityActorResponse, string? ErrorDescription)> GetEntityActorById(string entityActorId)
        {
            try
            {
                var pipeline = new[] {

                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(entityActorId))),

                     new BsonDocument {
                        { "$lookup",
                            new BsonDocument {
                                { "from", "GeographicCities" },
                                { "localField", "InvoicingAddress.CityAddressRef" },
                                { "foreignField", "_id" },
                                { "as", "InvoicingAddress.CityAddress" }
                            }
                        }
                    },

                     new BsonDocument(
                        "$unwind",
                        "$InvoicingAddress.CityAddress")
                    ,

                };

                var resultPrev = await _entityActorsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();


                EntityActor result = resultPrev.Select(d => BsonSerializer.Deserialize<EntityActor>(d)).ToList()[0];


                return (true, result, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrives the list 
        /// </summary>
        /// <param name="nameLike"></param>
        /// <param name="entityType"></param>
        /// <param name="entityCode"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<EntityActor>? ActorsNodesList, string? ErrorDescription)> GetEntityActorFaceList(string? nameLike = null, string? entityType = null, string? entityCode = null)
        {

            try
            {
                string strNameFiler = nameLike == null ? "" : nameLike;


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
                        { "$lookup",
                            new BsonDocument {
                                { "from", "GeographicCities" },
                                { "localField", "InvoicingAddress.CityAddressRef" },
                                { "foreignField", "_id" },
                                { "as", "InvoicingAddress.CityAddress" }
                            }
                        }
                    },
                    new BsonDocument(
                        "$unwind",
                        "$InvoicingAddress.CityAddress")
                    ,
                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "AddressList", 0 }
                            }
                        }
                    }
                };



                List<EntityActor> results = await _entityActorsCollection.Aggregate<EntityActor>(pipeline).ToListAsync();

                //var results = await _entityActorsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }



        /// <summary>
        /// Retrieves an list of details from an array in the Entity Collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityActorId"></param>
        /// <param name="entityDetailsSection"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<T>? EntityRelatedList, string? ErrorDescription)> GetEntityDetailsOf<T>(string entityActorId, EntityDetailsSection entityDetailsSection)
        {
            string detailsArrayListName = "";

            switch (entityDetailsSection)
            {
                case EntityDetailsSection.AddressList:
                    detailsArrayListName = "AddressList";
                    break;

                case EntityDetailsSection.IdentifiersList:
                    detailsArrayListName = "Identifications";
                    break;
                case EntityDetailsSection.ElectronicAddressLis:
                    detailsArrayListName = "ElectronicAddresses";
                    break;
                case EntityDetailsSection.ComercialConditions:
                    detailsArrayListName = "ElectronicAddresses";
                    break;
                case EntityDetailsSection.PhoneDirectory:
                    detailsArrayListName = "PhoneNumbers";
                    break;
                default:
                    return (false, null, "Not option Found");
            }

            if (entityActorId.Length == 0)
                return (false, null, "Not valid Entity Id");
            try
            {
                var pipeline = new[] {

                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(entityActorId))),

                    new BsonDocument("$project", new BsonDocument(){
                        { $"{detailsArrayListName}", 1},
                        { "_id", 0}
                    }),

                    new BsonDocument("$unwind", $"${detailsArrayListName}"),
                       new BsonDocument(
                       "$replaceRoot",
                            new BsonDocument("newRoot", $"${detailsArrayListName}")),
                };



                if (detailsArrayListName == "AddressList")
                {
                    Array.Resize(ref pipeline, pipeline.Length + 1);
                    pipeline[pipeline.Length - 1] =
                        new BsonDocument {
                        { "$lookup",
                            new BsonDocument {
                                { "from", "GeographicCities" },
                                { "localField", "CityAddressRef" },
                                { "foreignField", "_id" },
                                { "as", "CityAddress" }
                            }
                        }
                    };
                    Array.Resize(ref pipeline, pipeline.Length + 1);
                    pipeline[pipeline.Length - 1] = new BsonDocument { { "$unwind", "$CityAddress" } };
                }
                var result = await _entityActorsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();


                switch (entityDetailsSection)
                {
                    case EntityDetailsSection.AddressList:
                        var listDocs = result.Select(d => BsonSerializer.Deserialize<Address>(d)).ToList();
                        return (true, listDocs.Cast<T>().ToList(), null);

                    case EntityDetailsSection.IdentifiersList:
                        detailsArrayListName = "Identifications";
                        var listDocs2 = result.Select(d => BsonSerializer.Deserialize<IdentificationEntity>(d)).ToList();
                        return (true, listDocs2.Cast<T>().ToList(), null);

                    case EntityDetailsSection.ElectronicAddressLis:
                        detailsArrayListName = "ElectronicAddresses";
                        var listDocs3 = result.Select(d => BsonSerializer.Deserialize<ElectronicAddress>(d)).ToList();
                        return (true, listDocs3.Cast<T>().ToList(), null);

                    case EntityDetailsSection.ComercialConditions:
                        detailsArrayListName = "EntitiesCommercialRelationShip";
                        var listDocs4 = result.Select(d => BsonSerializer.Deserialize<EntitiesCommercialRelationShip>(d)).ToList();
                        return (true, listDocs4.Cast<T>().ToList(), null);

                    case EntityDetailsSection.PhoneDirectory:
                        detailsArrayListName = "PhoneNumbers";
                        var listDocs5 = result.Select(d => BsonSerializer.Deserialize<PhoneNumber>(d)).ToList();
                        return (true, listDocs5.Cast<T>().ToList(), null);
                    default:
                        return (false, null, "Option Not Found");
                }


            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves the list of possible Entity roles
        /// </summary>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<EntityRole>? rolesList, string? ErrorDescription)> EntityRolesListSelector()
        {
            var roles = await _rolesCollection.Find<EntityRole>(new BsonDocument())
                .Sort(Builders<EntityRole>.Sort.Ascending("EntityRoleClassifier.Name").Ascending("Name"))
                .ToListAsync();

            if (roles == null || roles.Count == 0)
            {

                return (false, null, "Unpopulated Roles");

            }
            else
            {
                return (true, roles, null);
            }
        }


        /// <summary>
        /// Saves an Entity/Actor document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, EntityActor? entityActorResponse, string? ErrorDescription)> SaveEntity(EntityActor entity, string entityActorId)
        {
            try
            {
                if (entity.InvoicingAddress.Id == null)
                {
                    entity.InvoicingAddress.Id = ObjectId.GenerateNewId().ToString();
                }

                var filter = Builders<EntityActor>.Filter.Eq("_id", new ObjectId(entityActorId));

                var updateOptions = new ReplaceOptions { IsUpsert = true };

                var result = await _entityActorsCollection.ReplaceOneAsync(filter, entity, updateOptions);

                return (true, entity, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Saves an address of an entity. If it exists, it'll be updated, otherwise it 'll be inserted in the array
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, Address? entityAddress, string? ErrorDescription)> SaveEntityAddress(string entityActorId, Address address)
        {
            try
            {
                if (address.Id == null)
                    address.Id = ObjectId.GenerateNewId().ToString();

                if (address.Id == "")
                    address.Id = ObjectId.GenerateNewId().ToString();

                var filterPrev = Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId);
                var resultPrev = await _entityActorsCollection.Find(filterPrev).FirstOrDefaultAsync();

                if (entityActorId.Length > 0 && resultPrev != null && resultPrev.AddressList.Any(x => x.Id == address.Id))
                {
                    //Update Element
                    var filter = Builders<EntityActor>.Filter.And(
                        Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId),
                        Builders<EntityActor>.Filter.ElemMatch(x => x.AddressList, y => y.Id == address.Id)
                    );
                    var update = Builders<EntityActor>.Update.Set(x => x.AddressList[-1], address);
                    await _entityActorsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add Element

                    var filter = Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId);
                    var update = Builders<EntityActor>.Update.AddToSet(x => x.AddressList, address);
                    await _entityActorsCollection.UpdateOneAsync(filter, update);
                }



                return (true, address, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }

        /// <summary>
        /// Saves a phone number of an entity. If it exists, it'll be updated, otherwise it 'll be inserted in the array
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, PhoneNumber? phoneNumber, string? ErrorDescription)> SavePhone(string entityActorId, PhoneNumber phoneNumber)
        {
            try
            {
                if (phoneNumber.Id == null)
                {
                    phoneNumber.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPrev = Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId);
                var resultPrev = await _entityActorsCollection.Find(filterPrev).FirstOrDefaultAsync();

                if (resultPrev != null && resultPrev.PhoneNumbers.Any(x => x.Id == phoneNumber.Id))
                {
                    //Update Element
                    var filter = Builders<EntityActor>.Filter.And(
                        Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId),
                        Builders<EntityActor>.Filter.ElemMatch(x => x.PhoneNumbers, y => y.Id == phoneNumber.Id)
                    );
                    var update = Builders<EntityActor>.Update.Set(x => x.PhoneNumbers[-1], phoneNumber);
                    await _entityActorsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add Element
                    var filter = Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId);
                    var update = Builders<EntityActor>.Update.AddToSet(x => x.PhoneNumbers, phoneNumber);
                    await _entityActorsCollection.UpdateOneAsync(filter, update);
                }

                return (true, phoneNumber, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Saves an identification Code of an entity. If it exists, it'll be updated, otherwise it 'll be inserted in the array
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <param name="identificationEntity"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, IdentificationEntity? identificationEntity, string? ErrorDescription)> SaveIdentificationCode(string entityActorId, IdentificationEntity identificationEntity)
        {
            try
            {
                if (identificationEntity.Id == null)
                    identificationEntity.Id = ObjectId.GenerateNewId().ToString();

                var filterPrev = Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId);
                var resultPrev = await _entityActorsCollection.Find(filterPrev).FirstOrDefaultAsync();

                if (resultPrev != null && resultPrev.Identifications.Any(x => x.Code == identificationEntity.Code))
                {
                    //Update Element
                    var filter = Builders<EntityActor>.Filter.And(
                        Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId),
                        Builders<EntityActor>.Filter.ElemMatch(x => x.Identifications, y => y.Code == identificationEntity.Code)
                    );
                    var update = Builders<EntityActor>.Update.Set(x => x.Identifications[-1], identificationEntity);
                    await _entityActorsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add Element
                    var filter = Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId);
                    var update = Builders<EntityActor>.Update.AddToSet(x => x.Identifications, identificationEntity);
                    await _entityActorsCollection.UpdateOneAsync(filter, update);
                }

                return (true, identificationEntity, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a list of Entity Groups with the posibility to receive an optional paremeter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<EntityGroup>? entityGroup, string? ErrorDescription)> GetEntityGroups(string? filterCondition = null)
        {
            try
            {
                string filter = filterCondition == null ? "" : filterCondition;

                if (filter.Length > 0)
                {
                    var pipeline = new[]
                    {
                        new BsonDocument(
                            "$match", new BsonDocument(
                                "Name", new BsonDocument("$regex", new BsonRegularExpression($"/{filter}/i"))
                            )
                        )
                    };

                    List<EntityGroup> results = await _entityGroup.Aggregate<EntityGroup>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var entityGroups = await _entityGroup.Find<EntityGroup>(new BsonDocument()).ToListAsync();

                    if (entityGroups == null || entityGroups.Count == 0)
                    {
                        return (false, null, "Unpopulated Entity Groups");
                    }
                    else
                    {
                        return (true, entityGroups, null);
                    }
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a particular Entity Group object by Id
        /// </summary>
        /// <param name="entityGroupId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, EntityGroup? entityGroup, string? ErrorDescription)> GetEntityGroup(string entityGroupId)
        {
            try
            {
                var filterEntityGroup = Builders<EntityGroup>.Filter.Eq(x => x.Id, entityGroupId);
                var resultEntityGroup = await _entityGroup.Find(filterEntityGroup).FirstOrDefaultAsync();

                if (resultEntityGroup == null)
                {
                    return (false, null, "Unpopulated Entity Groups");
                }
                else
                {
                    return (true, resultEntityGroup, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Inserts / Updates an Entity Group object
        /// </summary>
        /// <param name="entityGroupId"></param>
        /// <param name="entityGroup"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, EntityGroup? entityGroup, string? ErrorDescription)> SaveEntityGroup(string entityGroupId, EntityGroup entityGroup)
        {
            try
            {
                var filterEntityGroup = Builders<EntityGroup>.Filter.Eq("_id", new ObjectId(entityGroupId));
                var updateEntityOptions = new ReplaceOptions { IsUpsert = true };
                var resultEntityGroup = await _entityGroup.ReplaceOneAsync(filterEntityGroup, entityGroup, updateEntityOptions);

                return (true, entityGroup, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Inserts / Updates Electronic Address in an Entity Actor
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <param name="electronicAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, ElectronicAddress? electronicAddress, string? ErrorDescription)> SaveElectronicAddress(string entityActorId, ElectronicAddress electronicAddress)
        {
            try
            {
                if (electronicAddress.Id == null) {
                    electronicAddress.Id = ObjectId.GenerateNewId().ToString();
                }


                var filterPrev = Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId);
                var resultPrev = await _entityActorsCollection.Find(filterPrev).FirstOrDefaultAsync();

                if (entityActorId.Length > 0 && resultPrev != null && resultPrev.ElectronicAddresses.Any(x => x.Id == electronicAddress.Id))
                {
                    //Update Element
                    var filter = Builders<EntityActor>.Filter.And(
                        Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId),
                        Builders<EntityActor>.Filter.ElemMatch(x => x.ElectronicAddresses, y => y.Id == electronicAddress.Id)
                    );
                    var update = Builders<EntityActor>.Update.Set(x => x.ElectronicAddresses[-1], electronicAddress);
                    await _entityActorsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add Element
                    var filter = Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId);
                    var update = Builders<EntityActor>.Update.AddToSet(x => x.ElectronicAddresses, electronicAddress);
                    await _entityActorsCollection.UpdateOneAsync(filter, update);
                }

                return (true, electronicAddress, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Inserts / Updates Shipping Setup in an Entity Actor
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <param name="shippingInfo"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, ShippingInfo? shippingInfo, string? ErrorDescription)> SaveShippingSetup(string entityActorId, ShippingInfo shippingInfo)
        {
            try
            {
                if (shippingInfo.Id == null)
                    shippingInfo.Id = ObjectId.GenerateNewId().ToString();


                var filterPrev = Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId);
                var resultPrev = await _entityActorsCollection.Find(filterPrev).FirstOrDefaultAsync();

                if (entityActorId.Length > 0 && resultPrev != null && resultPrev.ShippingInformation.Any(x => x.Id == shippingInfo.Id))
                {
                    //Update Element
                    var filter = Builders<EntityActor>.Filter.And(
                        Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId),
                        Builders<EntityActor>.Filter.ElemMatch(x => x.ShippingInformation, y => y.Id == shippingInfo.Id)
                    );
                    var update = Builders<EntityActor>.Update.Set(x => x.ShippingInformation[-1], shippingInfo);
                    await _entityActorsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add Element
                    var filter = Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId);
                    var update = Builders<EntityActor>.Update.AddToSet(x => x.ShippingInformation, shippingInfo);
                    await _entityActorsCollection.UpdateOneAsync(filter, update);
                }

                return (true, shippingInfo, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Inserts / Updates Commercial Conditions in an Entity Actor
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <param name="entitiesCommercialRelationShip"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, EntitiesCommercialRelationShip? entitiesCommercialRelationShip, string? ErrorDescription)> SaveCommercialConditions(string entityActorId, EntitiesCommercialRelationShip entitiesCommercialRelationShip)
        {
            try
            {
                if (entitiesCommercialRelationShip.Id == null)
                    entitiesCommercialRelationShip.Id = ObjectId.GenerateNewId().ToString();


                var filterPrev = Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId);
                var resultPrev = await _entityActorsCollection.Find(filterPrev).FirstOrDefaultAsync();

                if (entityActorId.Length > 0 && resultPrev != null && resultPrev.EntitiesRelationShips.Any(x => x.Id == entitiesCommercialRelationShip.Id))
                {
                    //Update Element
                    var filter = Builders<EntityActor>.Filter.And(
                        Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId),
                        Builders<EntityActor>.Filter.ElemMatch(x => x.EntitiesRelationShips, y => y.Id == entitiesCommercialRelationShip.Id)
                    );
                    var update = Builders<EntityActor>.Update.Set(x => x.EntitiesRelationShips[-1], entitiesCommercialRelationShip);
                    await _entityActorsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add Element
                    var filter = Builders<EntityActor>.Filter.Eq(x => x.Id, entityActorId);
                    var update = Builders<EntityActor>.Update.AddToSet(x => x.EntitiesRelationShips, entitiesCommercialRelationShip);
                    await _entityActorsCollection.UpdateOneAsync(filter, update);
                }

                return (true, entitiesCommercialRelationShip, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves an Electronic Address filtered by Id
        /// </summary>
        /// <param name="electronicAddressId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, ElectronicAddress? electronicAddress, string? ErrorDescription)> GetElectronicAddressById(string electronicAddressId)
        {
            try
            {
                var pipeline = new[] {

                    new BsonDocument(
                        "$unwind",
                        "$ElectronicAddresses"),

                    new BsonDocument("$match", new BsonDocument("ElectronicAddresses._id", new ObjectId(electronicAddressId))),

                    new BsonDocument(
                        "$project", new BsonDocument("ElectronicAddresses", 1)
                    ),

                    new BsonDocument(
                        "$replaceRoot", new BsonDocument("newRoot", "$ElectronicAddresses")
                    )
                };

                var resultPrev = await _entityActorsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();

                ElectronicAddress result = resultPrev.Select(d => BsonSerializer.Deserialize<ElectronicAddress>(d)).ToList()[0];

                return (true, result, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves Shipping Information filtered by Id
        /// </summary>
        /// <param name="shippingInfoId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, ShippingInfo? shippingInfo, string? ErrorDescription)> GetShippingSetupById(string shippingInfoId)
        {
            try
            {
                var pipeline = new[] {

                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(shippingInfoId))),

                     new BsonDocument {
                        { "$lookup",
                            new BsonDocument {
                                { "from", "EntityNodes" },
                                { "localField", "ShippingInformation.Id" },
                                { "foreignField", "_id" },
                                { "as", "ShippingInformation.CityAddress" }//Revisar
                            }
                        }
                    },

                     new BsonDocument(
                        "$unwind",
                        "ShippingInformation.CityAddress")//Revisar
                    ,

                };

                var resultPrev = await _entityActorsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();


                ShippingInfo result = resultPrev.Select(d => BsonSerializer.Deserialize<ShippingInfo>(d)).ToList()[0];//Revisar


                return (true, result, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves Entities Relationships filtered by Id
        /// </summary>
        /// <param name="entitiesCommercialRelationShipId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, EntitiesCommercialRelationShip? entitiesCommercialRelationShip, string? ErrorDescription)> GetEntitiesCommercialRelationShipById(string entitiesCommercialRelationShipId)
        {
            try
            {
                var pipeline = new[] {

                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(entitiesCommercialRelationShipId))),

                     new BsonDocument {
                        { "$lookup",
                            new BsonDocument {
                                { "from", "EntityNodes" },
                                { "localField", "EntitiesRelationShips.Id" },
                                { "foreignField", "_id" },
                                { "as", "EntitiesRelationShips.CityAddress" }//Revisar
                            }
                        }
                    },

                     new BsonDocument(
                        "$unwind",
                        "EntitiesRelationShips.CityAddress")//Revisar
                    ,

                };

                var resultPrev = await _entityActorsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();


                EntitiesCommercialRelationShip result = resultPrev.Select(d => BsonSerializer.Deserialize<EntitiesCommercialRelationShip>(d)).ToList()[0];
                //Revisar ^^^


                return (true, result, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrives the list of electronic addresses of an Actor
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<ElectronicAddress>? electronicAddresses, string? ErrorDescription)> GetElectronicAddresses(string entityActorId)
        {
            try
            {
                var pipeline = new[] {

                    new BsonDocument(
                        "$unwind",
                        "$ElectronicAddresses"),

                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(entityActorId))),

                    new BsonDocument(
                        "$project", new BsonDocument("ElectronicAddresses", 1)
                    ),

                    new BsonDocument(
                        "$replaceRoot", new BsonDocument("newRoot", "$ElectronicAddresses")
                    )
                };

                var resultPrev = await _entityActorsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();

                List<ElectronicAddress> result = resultPrev.Select(d => BsonSerializer.Deserialize<List<ElectronicAddress>>(d)).ToList()[0];

                return (true, result, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
    }
}
