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

        IMongoCollection<EntityRole> _entityRole;
        IMongoCollection<EntityRole> _entityRoles;
        IMongoCollection<EntityType> _entityType;
        IMongoCollection<IdentificationType> _identificationType;
        IMongoCollection<IdentificationType> _identificationTypes;


        /// <summary>
        /// Constructor of the service which retrieves the connection string
        /// </summary>
        /// <param name="config"></param>
        public EntityActorsRelatedConceptsService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DatabaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DatabaseName);
            _entityRole = mongoDatabase.GetCollection<EntityRole>("Roles");
            _entityRoles = mongoDatabase.GetCollection<EntityRole>("Roles");
            _entityType = mongoDatabase.GetCollection<EntityType>("EntityTypes");
            _identificationType = mongoDatabase.GetCollection<IdentificationType>("IdentificationTypes");
            _identificationTypes = mongoDatabase.GetCollection<IdentificationType>("IdentificationTypes");
        }


        /// <summary>
        /// Retrieves info of a particular Entity Role
        /// </summary>
        /// <param name="entityRoleId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, EntityRole? entityRole, string? ErrorDescription)> GetEntityRole(string entityRoleId)
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
        /// <param name="entityTypeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, EntityType? entityType, string? ErrorDescription)> GetEntityType(string entityTypeId)
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
        /// <param name="identicationTypeId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, IdentificationType? identificationType, string? ErrorDescription)> GetIdentificationType(string identicationTypeId)
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
                return(false, null, e.Message);
            }
        }


        /// <summary>
        /// Insert / Updates Entity Role information
        /// </summary>
        /// <param name="entityRoleId"></param>
        /// <param name="entityRole"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, EntityRole? entityRole, string? ErrorDescription)> SaveEntityRole(string entityRoleId, EntityRole entityRole)
        {
            try
            {
                var filterEntityRole = Builders<EntityRole>.Filter.Eq("_id", new ObjectId(entityRoleId));
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
        /// <param name="entityTypeId"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, EntityType? entityType, string? ErrorDescription)> SaveEntityType(string entityTypeId, EntityType entityType)
        {
            try
            {
                var filterEntityType = Builders<EntityType>.Filter.Eq("_id", new ObjectId(entityTypeId));
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
        /// <param name="identicationTypeId"></param>
        /// <param name="identificationType"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, IdentificationType? identificationType, string? ErrorDescription)> SaveIdentificationType(string identicationTypeId, IdentificationType identificationType)
        {
            try
            {
                var filterIdentificationType = Builders<IdentificationType>.Filter.Eq("_id", new ObjectId(identicationTypeId));
                var updateIdentificationTypeOptions = new ReplaceOptions { IsUpsert = true };
                var resultIdentificationType = await _identificationType.ReplaceOneAsync(filterIdentificationType, identificationType, updateIdentificationTypeOptions);

                return (true, identificationType, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
    }
}
