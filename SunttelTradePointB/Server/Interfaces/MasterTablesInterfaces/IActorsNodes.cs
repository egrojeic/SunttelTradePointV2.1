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
        /// <param name="nameLike"></param>
        /// <param name="entityType"></param>
        /// <param name="entityCode"></param>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<EntityActor>? ActorsNodesList, string? ErrorDescription)> GetEntityActorFaceList(string userId, string ipAdress, string? nameLike = null, string? entityType = null, string? entityCode = null);


        /// <summary>
        /// Retrieves an entity actor looked by id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, AtomConcept entityActorResponse, string? ErrorDescription)> GetEntityActorById(string userId, string ipAdress, string entityActorId);


        /// <summary>
        /// Retrieves any list of details of an Entity from an array in the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="entityDetailsSection"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<T>? EntityRelatedList, string? ErrorDescription)> GetEntityDetailsOf<T>(string userId, string ipAdress, string entityActorId, EntityDetailsSection entityDetailsSection);


        /// <summary>
        /// Returns a list of posible Roles of that could be adpoted by an Entity
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<EntityRole>? rolesList, string? ErrorDescription)> EntityRolesListSelector();



        /// <summary>
        /// Saves an Entity/Actor document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, EntityActor? entityActorResponse, string? ErrorDescription)> SaveEntity(string userId, string ipAdress, EntityActor entity);

        /// <summary>
        /// Saves an address of an entity. If it exists, it'll be updated, otherwise it 'll be inserted in the array
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, Address? entityAddress, string? ErrorDescription)> SaveEntityAddress(string userId, string ipAdress, string entityActorId, Address address);

        /// <summary>
        /// Saves a phone number of an entity. If it exists, it'll be updated, otherwise it 'll be inserted in the array
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, PhoneNumber? phoneNumber, string? ErrorDescription)> SavePhone(string userId, string ipAdress, string entityActorId, PhoneNumber phoneNumber);


        /// <summary>
        /// Saves an Identification Code of an entity. If it exists, it'll be updated, otherwise it 'll be inserted in the array
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="identificationEntity"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, IdentificationEntity? identificationEntity, string? ErrorDescription)> SaveIdentificationCode(string userId, string ipAdress, string entityActorId, IdentificationEntity identificationEntity);


        /// <summary>
        /// Retrieves a list of Entity Groups filtered by the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="filterCondition"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, List<EntityGroup>? entityGroup, string? ErrorDescription)> GetEntityGroups(string userId, string ipAdress, string? filterCondition = null);

        /// <summary>
        /// Retrieves a Entity Group of the id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityGroupId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, EntityGroup?  entityGroup, string? ErrorDescription)> GetEntityGroup(string userId, string ipAdress, string entityGroupId);

        /// <summary>
        /// Insert / Updates Entity Group info
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityGroupId"></param>
        /// <param name="conceptGroup"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, EntityGroup? entityGroup, string? ErrorDescription)> SaveEntityGroup(string userId, string ipAdress, string entityGroupId, EntityGroup conceptGroup);

        /// <summary>
        /// Insert / Updates an electronic address of an entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="electronicAddress"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, ElectronicAddress? electronicAddress, string? ErrorDescription)> SaveElectronicAddress(string userId, string ipAdress, string entityActorId, ElectronicAddress electronicAddress);


        /// <summary>
        /// Retrives the list of electronic addresses of an Actor
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, List<ElectronicAddress>?  electronicAddresses, string? ErrorDescription)> GetElectronicAddresses(string userId, string ipAdress, string entityActorId);



        /// <summary>
        /// Retrieves a particular electroica addres give by its id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="electronicAddressId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, ElectronicAddress? electronicAddress, string? ErrorDescription)> GetElectronicAddressById(string userId, string ipAdress, string electronicAddressId);


        /// <summary>
        /// Insert / Updates a shipping setup
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="shippingInfo"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, ShippingInfo? shippingInfo, string? ErrorDescription)> SaveShippingSetup(string userId, string ipAdress, string entityActorId, ShippingInfo shippingInfo);

        /// <summary>
        /// Retrieves a particular Shipping condition addres give by its id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="shippingInfoId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, ShippingInfo? shippingInfo, string? ErrorDescription)> GetShippingSetupById(string userId, string ipAdress, string shippingInfoId);


        /// <summary>
        /// Saves commercial conditions of an Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="entitiesCommercialRelationShip"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, EntitiesCommercialRelationShip?  entitiesCommercialRelationShip, string? ErrorDescription)> SaveCommercialConditions(string userId, string ipAdress, string entityActorId, EntitiesCommercialRelationShip entitiesCommercialRelationShip);

        /// <summary>
        /// Retrieves a particular Commercial relationship give by its id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entitiesCommercialRelationShipId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, EntitiesCommercialRelationShip? entitiesCommercialRelationShip, string? ErrorDescription)> GetEntitiesCommercialRelationShipById(string userId, string ipAdress, string entitiesCommercialRelationShipId);


        /// <summary>
        /// Retrieves the relation of all Shipping info records related with an Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, List<ShippingInfo>  shippingInfos, string? ErrorDescription)> GetShippingSetup(string userId, string ipAdress, string entityActorId);


        /// <summary>
        /// Retrieves the list of the different commercial conditions for an Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, List<EntitiesCommercialRelationShip>  entitiesCommercialRelationShips, string? ErrorDescription)> GetCommercialConditiosOfEntity(string userId, string ipAdress, string entityActorId);



    }
}
