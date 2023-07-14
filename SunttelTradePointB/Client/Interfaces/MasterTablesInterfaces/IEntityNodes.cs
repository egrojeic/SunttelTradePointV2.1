using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Communications;

namespace SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces
{
    public interface IEntityNodes
    {
        public Address? tempAddress { get; set; }

        public City? tempCity { get; set; }
        public GeoRegion tempRegion { get; set; }
        public Country tempCountry { get; set; }
        public string SkinImage
        {
            get;
            set;
        }

        public string Host { get { return "https://localhost:7186/uploads/entityImages"; } }

        public enum UploadingFileTyoe
        {
            ActorItemImage,
            EntityImage
        }



        /// <summary>
        /// Returns a list of faces of entity node/actors
        /// </summary>
        /// <param name="nameLike">Letters/Words that could be contained on the actor's name</param>
        /// <param name="entityType">Name of the Entity type</param>
        /// <param name="entityCode">Identification Code of the actor</param>
        /// <returns></returns>
        Task<List<EntityActor>> GetEntityActorFaceList(string? nameLike = null);


        /// <summary>
        /// Retrieves an entity actor looked by id
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        Task<EntityActor> GetEntityActor(string entityActorId);


        /// <summary>
        /// Insert a new Entity Actor in the DataBase
        /// </summary>
        /// <param name="EntityActorId"></param>
        /// <param name="entityActor"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, string? EntityId, string? error)> SaveEntity(string? EntityActorId, EntityActor entityActor);

        /// <summary>
        /// Updates the info of an Entity
        /// </summary>
        /// <param name="entityActor"></param>
        /// <returns></returns>
        Task<EntityActor> UpdateEntityActor(EntityActor entityActor);

        /// <summary>
        /// Upload the info of an list Entity item in format CSV
        /// </summary>
        /// <param name="entityActor"></param>
        /// <returns></returns>
        Task<List<EntityActor>> EntityImport(MultipartFormDataContent file);


        /// <summary>
        /// Retrieves a list of addresses of an actor
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        Task<List<Address>> GetEntityActorAddressList(string entityActorId);

        /// <summary>
        /// Retrives the different related concepts to an Entity/Node
        /// </summary>
        /// <typeparam name="Concept"></typeparam>
        /// <param name="entityActorId"></param>
        /// <param name="entityDetailsSection"></param>
        /// <returns></returns>
        Task<List<Concept>> GetEntityDetailsOf(string entityActorId, EntityDetailsSection entityDetailsSection);

        /// <summary>
        /// Retrives the different related concepts to an Channel Communications Group
        /// </summary>
        /// <typeparam name="ChannelCommunicationsGroup"></typeparam>
        /// <param name="channelCommunicationsGroupId"></param>
        /// <returns></returns>
        Task<ChannelCommunicationsGroup> GetChannelCommunicationsGroupById(string channelCommunicationsGroupId);

        /// <summary>
        /// Retrives the different related concepts to an Channel Communications Group 
        /// </summary>     
        ///  No parameter
        /// <returns></returns>
        Task<List<ChannelCommunicationsGroup>> GetChannelCommunicationsGroups();


        /// <summary>
        /// Returns the list of possible Entity Roles
        /// </summary>
        /// <returns></returns>
        Task<List<EntityRole>> EntityRolesList();



        /// <summary>
        /// Retrives the different related concepts to an ChannelCommunicationsGroup
        /// </summary>
        /// <typeparam name="ChannelCommunicationsGroup"></typeparam>     
        /// <param name="channelCommunicationsGroup"></param>
        /// <returns></returns>
        Task<ChannelCommunicationsGroup> SaveChannelCommunicationsGroup(ChannelCommunicationsGroup channelCommunicationsGroup);

        /// <summary>
        /// Delete a EntityType
        /// </summary>  
        /// <param name="palletTypeId"></param>
        /// <returns></returns>
        Task<bool> DeleteEntityTypeById(string palletTypeId);

        /// <summary>
        /// Delete a EntityGroup
        /// </summary>  
        /// <param name="entityGroupId"></param>
        /// <returns></returns>
        Task<bool> DeleteEntityGroupById(string entityGroupId);

        /// <summary>
        /// Delete a EntityRole
        /// </summary>  
        /// <param name="entityRoleId"></param>
        /// <returns></returns>
        Task<bool> DeleteEntityRoleById(string entityRoleId);

        /// <summary>
        /// Delete a PalletType
        /// </summary>  
        /// <param name="palletTypeId"></param>
        /// <returns></returns>
        Task<bool> DeletePalletTypeById(string palletTypeId);

        /// <summary>
        /// Delete the ShippingSetup of a Entity
        /// </summary>
        /// <param name="EntityId"></param>
        /// <param name="shippingInfoId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, string? ErrorDescription)> DeleteShippingSetup(string EntityId, string shippingInfoId);

        /// <summary>
        ///  Delete the ShippingAddress of a Entity
        /// </summary>
        /// <param name="EntityId"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public Task<(bool IsSuccess, string? ErrorDescription)> DeleteEntityAddress(string EntityId, string addressId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<List<PalletType>> GetPalletTypeList(string filter = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterGroup"></param>
        /// <returns></returns>
        public Task<List<EntityRole>> GetListEntityRoles(string filterGroup);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterGroup"></param>
        /// <returns></returns>
        public Task<List<EntityGroup>> GetListEntityGroups(string filterGroup);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<List<IdentificationType>> GetIdentificationTypes(string filter = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificationTypeId"></param>
        /// <returns></returns>
        public Task<bool> DeleteIdentificationTypeById(string identificationTypeId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<List<EntityType>> GetEntityTypeList(string filter = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public Task<Country> NewCountry(Country country);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geoRegion"></param>
        /// <returns></returns>
        public Task<GeoRegion> NewGeoRegion(GeoRegion geoRegion);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public Task<City> NewCity(City? city);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Task<Address> NewEntityActorAddress(Address address);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipartFormDataContent"></param>
        /// <returns></returns>
        public Task<string> UploadFiles(MultipartFormDataContent multipartFormDataContent);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="atomConcept"></param>
        /// <returns></returns>
        public Task<bool> SaveEntityType(AtomConcept atomConcept);
      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palletType"></param>
        /// <returns></returns>
        public Task SavePalletType(PalletType palletType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identification"></param>
        /// <returns></returns>
        public Task<bool> SaveIdentificationType(AtomConcept identification);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <returns></returns>
        public Task<TransactItemImage> GetImage(string? transactionalItemId = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <param name="skinImage"></param>
        /// <returns></returns>
        public Task<bool> SaveEntitySkinImage(string entityActorId, string skinImage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityActor"></param>
        /// <returns></returns>
        public Task<EntityActor> NewEntityActor(EntityActor entityActor);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <returns></returns>
        public Task<List<ConceptGroup>> GetEntityGroups(string filterCondition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EntityActorId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public Task<bool> SaveEntityAddress(string EntityActorId, Address address);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EntityActorId"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Task SavePhone(string EntityActorId, PhoneNumber phoneNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EntityActorId"></param>
        /// <param name="identificationEntity"></param>
        /// <returns></returns>
        public Task SaveIdentificationCode(string EntityActorId, IdentificationEntity identificationEntity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityGroupId"></param>
        /// <param name="entityGroup"></param>
        /// <returns></returns>
        public Task<bool> SaveEntityGroup(string entityGroupId, EntityGroup entityGroup);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="electronicAddressId"></param>
        /// <param name="electronicAddress"></param>
        /// <returns></returns>
        public Task SaveElectronicAddress(string electronicAddressId, ElectronicAddress electronicAddress);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shippinSetupId"></param>
        /// <param name="shippingInfo"></param>
        /// <returns></returns>
        public Task SaveShippingSetup(string shippinSetupId, ShippingInfo shippingInfo);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="commercialConditionId"></param>
        /// <param name="comercialConditions"></param>
        /// <returns></returns>
        public Task<bool> SaveCommercialConditions(string commercialConditionId, EntitiesCommercialRelationShip comercialConditions);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="entityRole"></param>
        /// <returns></returns>
        public Task<bool> SaveEntityRole(string roleId, EntityRole entityRole);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public Task<EntitiesCommercialRelationShip> GetCommercialRelationShip(string entityActorId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Task<EntityRole> GetRoleById(string roleId);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<AtomConcept>> GetListElectronicAddress();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public Task<List<Address>> GetEntityDetailsAddressList(string entityId);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="electronicAddressId"></param>
        /// <returns></returns>
        public Task<ElectronicAddress> GetElectronicAddressById(string electronicAddressId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shippingId"></param>
        /// <returns></returns>
        public Task<List<ShippingInfo>> GetShippingInfos(string shippingId);
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commercialConditionId"></param>
        /// <returns></returns>
        public Task<List<EntitiesCommercialRelationShip>> GetCommercialConditions(string commercialConditionId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<EntitiyRelationshipType>> GetEntityRelationType();
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public Task<List<ElectronicAddress>> GetEntityElectronicAddress(string entityActorId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public Task<List<IdentificationEntity>> GetEntityIdentifierList(string entityId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public Task<List<PhoneNumber>> GetPhoneNumber(string entityActorId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Task<EntityGroup> GetEntityGroup(string groupId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public Task<List<IdentificationType>> GetDetailsIdentifiers(string entityActorId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificationId"></param>
        /// <returns></returns>
        public Task<AtomConcept> GetIdentificationTypeById(string identificationId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<PalletType>> GetPalletTypes();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shippingInfoId"></param>
        /// <returns></returns>
        public Task<ShippingInfo> GetShippingSetUpById(string shippingInfoId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityTypeId"></param>
        /// <returns></returns>
        public Task<AtomConcept> GetEntityTypeById(string entityTypeId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palletTypeId"></param>
        /// <returns></returns>
        public Task<PalletType> GetPalleTypeById(string palletTypeId);


    }
}
