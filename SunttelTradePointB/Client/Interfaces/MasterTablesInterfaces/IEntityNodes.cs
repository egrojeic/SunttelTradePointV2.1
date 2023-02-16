using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces
{
    public interface IEntityNodes
    {
        /// <summary>
        /// Returns a list of faces of entity node/actors
        /// </summary>
        /// <param name="nameLike">Letters/Words that could be contained on the actor's name</param>
        /// <param name="entityType">Name of the Entity type</param>
        /// <param name="entityCode">Identification Code of the actor</param>
        /// <returns></returns>
        Task<List<EntityActor>> GetEntityActorFaceList(string? nameLike = null, string? entityType = null, string? entityCode = null, bool forceRefresh = false);


        /// <summary>
        /// Retrieves an entity actor looked by id
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        Task<EntityActor> GetEntityActorById(string entityActorId);


        /// <summary>
        /// Insert a new Entity Actor in the DataBase
        /// </summary>
        /// <param name="entityActor"></param>
        /// <returns></returns>
        Task<EntityActor> CreateNewEntityActor(EntityActor entityActor);

        /// <summary>
        /// Updates the info of an Entity
        /// </summary>
        /// <param name="entityActor"></param>
        /// <returns></returns>
        Task<EntityActor> UpdateEntityActor(EntityActor entityActor);


        /// <summary>
        /// Retrieves a list of addresses of an actor
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        Task<List<Address>> GetEntityActorAddressList(string entityActorId);

        /// <summary>
        /// Retrives the different related concepts to an Entity/Node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityActorId"></param>
        /// <param name="entityDetailsSection"></param>
        /// <returns></returns>
        Task<List<T>> GetEntityDetailsOf<T>(string entityActorId, EntityDetailsSection entityDetailsSection);


        /// <summary>
        /// Returns the list of possible Entity Roles
        /// </summary>
        /// <returns></returns>
        Task<List<EntityRole>> EntityRolesList();
    }
}
