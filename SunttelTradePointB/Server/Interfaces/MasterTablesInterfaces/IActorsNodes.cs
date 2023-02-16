using SunttelTradePointB.Server.Migrations;
using SunttelTradePointB.Server.Services.MasterTablesServices;
using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces
{
    /// <summary>
    /// Author: Jorge Isaza
    /// Description: Interface to controll CRUD Operations on Entity Nodes
    /// </summary>
    public interface IActorsNodes
    {

        /// <summary>
        /// Returns a list of faces of entity node/actors
        /// </summary>
        /// <param name="nameLike">Letters/Words that could be contained on the actor's name</param>
        /// <param name="entityType">Name of the Entity type</param>
        /// <param name="entityCode">Identification Code of the actor</param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<EntityActor>? ActorsNodesList, string? ErrorDescription)> GetEntityActorFaceList(string? nameLike = null, string? entityType = null, string? entityCode = null);


        /// <summary>
        /// Retrieves an entity actor looked by id
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, AtomConcept entityActorResponse, string? ErrorDescription)> GetEntityActorById(string entityActorId);


        /// <summary>
        /// Retrieves any list of details of an Entity from an array in the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityActorId"></param>
        /// <param name="entityDetailsSection"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<T>? EntityRelatedList, string? ErrorDescription)> GetEntityDetailsOf<T>(string entityActorId, EntityDetailsSection entityDetailsSection);


        /// <summary>
        /// Returns a list of posible Roles of that could be adpoted by an Entity
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<EntityRole>? rolesList, string? ErrorDescription)> EntityRolesListSelector();



        /// <summary>
        /// Saves an Entity/Actor document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, EntityActor? entityActorResponse, string? ErrorDescription)> SaveEntity(EntityActor entity, string entityActorId);

        /// <summary>
        /// Saves an address of an entity. If it exists, it'll be updated, otherwise it 'll be inserted in the array
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, Address? entityAddress, string? ErrorDescription)> SaveEntityAddress(string entityActorId, Address address);


        /// <summary>
        ///  Saves a phone number of an entity. If it exists, it'll be updated, otherwise it 'll be inserted in the array
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, PhoneNumber? phoneNumber, string? ErrorDescription)> SavePhone(string entityActorId, PhoneNumber phoneNumber);


        /// <summary>
        /// Saves an Identification Code of an entity. If it exists, it'll be updated, otherwise it 'll be inserted in the array
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <param name="identificationEntity"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, IdentificationEntity? identificationEntity, string? ErrorDescription)> SaveIdentificationCode(string entityActorId, IdentificationEntity identificationEntity);


        /// <summary>
        /// Retrieves a list of Entity Groups filtered by the parameter
        /// If the parameter is empty it should returns all
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, List<EntityGroup>? entityGroup, string? ErrorDescription)> GetEntityGroups(string? filterCondition = null);

        /// <summary>
        /// Retrieves a Entity Group of the id
        /// </summary>
        /// <param name="entityGroupId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, EntityGroup?  entityGroup, string? ErrorDescription)> GetEntityGroup(string entityGroupId);

       /// <summary>
       /// Insert / Updates Entity Group info
       /// </summary>
       /// <param name="entityGroupId"></param>
       /// <param name="conceptGroup"></param>
       /// <returns></returns>
        public Task<(bool IsSuccess, EntityGroup? entityGroup, string? ErrorDescription)> SaveEntityGroup(string entityGroupId, EntityGroup conceptGroup);

        /// <summary>
        /// Insert / Updates an electronic address of an entity
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <param name="electronicAddress"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, ElectronicAddress? electronicAddress, string? ErrorDescription)> SaveElectronicAddress(string entityActorId, ElectronicAddress electronicAddress);


        /// <summary>
        /// Retrieves a particular electroica addres give by its id
        /// </summary>
        /// <param name="electronicAddressId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, ElectronicAddress? electronicAddress, string? ErrorDescription)> GetElectronicAddressById(string electronicAddressId);


        /// <summary>
        ///  Insert / Updates a shipping setup
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <param name="shippingInfo"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, ShippingInfo? shippingInfo, string? ErrorDescription)> SaveShippingSetup(string entityActorId, ShippingInfo shippingInfo);

        /// <summary>
        /// Retrieves a particular Shipping condition addres give by its id
        /// </summary>
        /// <param name="shippingInfoId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, ShippingInfo? shippingInfo, string? ErrorDescription)> GetShippingSetupById(string shippingInfoId);


        /// <summary>
        /// Saves commercial conditions of an Entity
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <param name="entitiesCommercialRelationShip"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, EntitiesCommercialRelationShip?  entitiesCommercialRelationShip, string? ErrorDescription)> SaveCommercialConditions(string entityActorId, EntitiesCommercialRelationShip entitiesCommercialRelationShip);

        /// <summary>
        /// Retrieves a particular Commercial relationship give by its id
        /// </summary>
        /// <param name="entitiesCommercialRelationShipId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, EntitiesCommercialRelationShip? entitiesCommercialRelationShip, string? ErrorDescription)> GetEntitiesCommercialRelationShipById(string entitiesCommercialRelationShipId);


    }
}
