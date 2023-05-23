using MongoDB.Bson;
using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Server.Services.MasterTablesServices
{

    /// <summary>
    /// Implements a service with the end-points to deal with related concepts to Entities
    /// </summary>
    public class EntityActorsRelatedConceptsService : IEntitiesRelatedConcepts
    {
        IMongoCollection<EntityActor> _entityActorsCollection;
        IMongoCollection<EntityRole> _entityRole;
        IMongoCollection<EntityRole> _entityRoles;
        IMongoCollection<EntityType> _entityType;
        IMongoCollection<EntityGroup> _entityGroup;
        IMongoCollection<IdentificationType> _identificationType;
        IMongoCollection<IdentificationType> _identificationTypes;
        IMongoCollection<PalletType> _palletType;
        

        /// <summary>
        /// Constructor of the service which retrieves the connection string
        /// </summary>
        /// <param name="config"></param>
        public EntityActorsRelatedConceptsService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DatabaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DatabaseName);
            _entityActorsCollection = mongoDatabase.GetCollection<EntityActor>("EntityNodes");
            _entityRole = mongoDatabase.GetCollection<EntityRole>("Roles");
            _entityRoles = mongoDatabase.GetCollection<EntityRole>("Roles");
            _entityType = mongoDatabase.GetCollection<EntityType>("EntityTypes");
            _identificationType = mongoDatabase.GetCollection<IdentificationType>("IdentificationTypes");
            _identificationTypes = mongoDatabase.GetCollection<IdentificationType>("IdentificationTypes");
            _palletType = mongoDatabase.GetCollection<PalletType>("PalletTypes");
            _entityGroup = mongoDatabase.GetCollection<EntityGroup>("EntityGroups");
        }


        /// <summary>
        /// Retrieves info of a particular Entity Role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityRoleId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, EntityRole? entityRole, string? ErrorDescription)> GetEntityRole(string userId, string ipAdress, string entityRoleId)
        {
            try
            {
                var filterEntityRole = Builders<EntityRole>.Filter.Eq(x => x.Id, entityRoleId);
                var resultEntityRole = await _entityRole.Find(filterEntityRole).FirstOrDefaultAsync();
                if (resultEntityRole == null)
                {
                    return (false, null, "Unpopulated Entity Roles");
                }
                else
                {
                    return (true, resultEntityRole, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a list of Entity Roles
        /// </summary>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<EntityRole>? entityRoles, string? ErrorDescription)> GetEntityRoles()
        {
            try
            {
                var entityRoles = await _entityRoles.Find<EntityRole>(new BsonDocument()).ToListAsync();
                if (entityRoles == null || entityRoles.Count == 0)
                {
                    return (false, null, "Unpopulated Entity Roles");
                }
                else
                {
                    return (true, entityRoles, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves info of a particular Entity Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityTypeId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, EntityType? entityType, string? ErrorDescription)> GetEntityType(string userId, string ipAdress, string entityTypeId)
        {
            try
            {
                var filterEntityType = Builders<EntityType>.Filter.Eq(x => x.Id, entityTypeId);
                var resultEntityType = await _entityType.Find(filterEntityType).FirstOrDefaultAsync();
                if (resultEntityType == null)
                {
                    return (false, null, "Unpopulated Entity Types");
                }
                else
                {
                    return (true, resultEntityType, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves info of a particular Identification Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="identicationTypeId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, IdentificationType? identificationType, string? ErrorDescription)> GetIdentificationType(string userId, string ipAdress, string identicationTypeId)
        {
            try
            {
                var filterIdentificationType = Builders<IdentificationType>.Filter.Eq(x => x.Id, identicationTypeId);
                var resultIdentificationType = await _identificationType.Find(filterIdentificationType).FirstOrDefaultAsync();
                if (resultIdentificationType == null)
                {
                    return (false, null, "Unpopulated Identification Types");
                }
                else
                {
                    return (true, resultIdentificationType, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a list of Identificaion Types
        /// </summary>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<IdentificationType>? identificationTypes, string? ErrorDescription)> GetIdentificationTypes()
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
        /// Retrieves a Pallet Type by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="palletTypeId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, PalletType? palletType, string? ErrorDescription)> GetPalletType(string userId, string ipAdress, string palletTypeId)
        {
            try
            {
                var filterPalletType = Builders<PalletType>.Filter.Eq(x => x.Id, palletTypeId);
                var resultPalletType = await _palletType.Find(filterPalletType).FirstOrDefaultAsync();
                if (resultPalletType == null)
                {
                    return (false, null, "Unpopulated Pallet Types");
                }
                else
                {
                    return (true, resultPalletType, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Insert / Updates Entity Role information
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityRole"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, EntityRole? entityRole, string? ErrorDescription)> SaveEntityRole(string userId, string ipAdress, EntityRole entityRole)
        {
            try
            {
                if (entityRole.Id == null)
                    entityRole.Id = ObjectId.GenerateNewId().ToString();

                var filterEntityRole = Builders<EntityRole>.Filter.Eq("_id", new ObjectId(entityRole.Id));
                var updateEntityRolesOptions = new ReplaceOptions { IsUpsert = true };
                var resultEntityRoles = await _entityRoles.ReplaceOneAsync(filterEntityRole, entityRole, updateEntityRolesOptions);

                return (true, entityRole, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Insert / Updates Entity Type information
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, EntityType? entityType, string? ErrorDescription)> SaveEntityType(string userId, string ipAdress, EntityType entityType)
        {
            try
            {
                if (entityType.Id == null)
                {
                    entityType.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterEntityType = Builders<EntityType>.Filter.Eq("_id", new ObjectId(entityType.Id));
                var updateEntityTypeOptions = new ReplaceOptions { IsUpsert = true };
                var resultEntityType = await _entityType.ReplaceOneAsync(filterEntityType, entityType, updateEntityTypeOptions);

                return (true, entityType, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Insert / Saves Identification Type information
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="identificationType"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, IdentificationType? identificationType, string? ErrorDescription)> SaveIdentificationType(string userId, string ipAdress, IdentificationType identificationType)
        {
            try
            {
                if (identificationType.Id == null)
                    identificationType.Id = ObjectId.GenerateNewId().ToString();

                var filterIdentificationType = Builders<IdentificationType>.Filter.Eq("_id", new ObjectId(identificationType.Id));
                var updateIdentificationTypeOptions = new ReplaceOptions { IsUpsert = true };
                var resultIdentificationType = await _identificationType.ReplaceOneAsync(filterIdentificationType, identificationType, updateIdentificationTypeOptions);

                return (true, identificationType, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        ///  Insert / Update a Pallet Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="palletType"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, PalletType? palletType, string? ErrorDescription)> SavePalletType(string userId, string ipAdress, PalletType palletType)
        {
            try
            {
                if (palletType.Id == null)
                {
                    palletType.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPalletType = Builders<PalletType>.Filter.Eq("_id", new ObjectId(palletType.Id));
                var updatePalletTypeOptions = new ReplaceOptions { IsUpsert = true };
                var resultPalletType = await _palletType.ReplaceOneAsync(filterPalletType, palletType, updatePalletTypeOptions);

                return (true, palletType, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Delete an EntityType not associated with Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditTypeId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteEntityTypeById(string userId, string ipAddress, string squadId, string? creditTypeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("TypeOfConcept._id", new ObjectId(creditTypeId))
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

                var resultCount = _entityActorsCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _entityType.DeleteOne(s => s.Id == creditTypeId);
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
        /// Delete an EntityGroup not associated with Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="entityGroupId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteEntityGroupById(string userId, string ipAddress, string squadId, string? entityGroupId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("Groups._id", new ObjectId(entityGroupId))
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

                var resultCount = _entityActorsCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _entityGroup.DeleteOne(s => s.Id == entityGroupId);
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
        /// Delete an EntityRole not associated with Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="entityRoleId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteEntityRoleById(string userId, string ipAddress, string squadId, string? entityRoleId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("DefaultEntityRole._id", new ObjectId(entityRoleId))
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

                var resultCount = _entityActorsCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _entityRole.DeleteOne(s => s.Id == entityRoleId);
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
        /// Delete an PalletType not associated with Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="palletTypeId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeletePalletTypeById(string userId, string ipAddress, string squadId, string? palletTypeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("PalletTypeShipping._id", new ObjectId(palletTypeId))
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

                var resultCount = _entityActorsCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _palletType.DeleteOne(s => s.Id == palletTypeId);
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
        /// Delete an identificationType not associated with Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="identificationTypeId"></param>       
        /// <returns></returns>
        public async Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteIdentificationTypeById(string userId, string ipAddress, string squadId, string? identificationTypeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();


                pipeline.Add(
                  new BsonDocument("$match",
                  new BsonDocument("PalletTypeShipping._id", new ObjectId(identificationTypeId))
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

                var resultCount = _entityActorsCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
                int count = resultCount != null && resultCount.Elements != null ? resultCount.Elements.Count() : 0;


                if (count <= 0)
                {
                    var result = _identificationType.DeleteOne(s => s.Id == identificationTypeId);
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
