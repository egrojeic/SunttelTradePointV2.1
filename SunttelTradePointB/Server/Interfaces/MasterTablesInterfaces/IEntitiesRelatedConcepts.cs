using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces
{

    /// <summary>
    /// Interface that set the end porints of services of related concepts to Entities
    /// </summary>
    public interface IEntitiesRelatedConcepts
    {

        /// <summary>
        /// Gets an entity object
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityTypeId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, EntityType? entityType, string? ErrorDescription)> GetEntityType(string userId, string ipAdress, string entityTypeId);


        /// <summary>
        /// Retrieves an object representing an identification type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="identicationTypeId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, IdentificationType? identificationType, string? ErrorDescription)> GetIdentificationType(string userId, string ipAdress, string identicationTypeId);

        /// <summary>
        /// Retrives the list of Identification Types
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<IdentificationType>? identificationTypes, string? ErrorDescription)> GetIdentificationTypes();


        /// <summary>
        /// Retrieves an object of an entity role by its id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityRoleId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, EntityRole? entityRole, string? ErrorDescription)> GetEntityRole(string userId, string ipAdress, string entityRoleId);

        /// <summary>
        /// Retrieves a list with all entity roles in the system
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<EntityRole>? entityRoles, string? ErrorDescription)> GetEntityRoles();


        /// <summary>
        /// Inserts/ Updates an Entity Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, EntityType? entityType, string? ErrorDescription)> SaveEntityType(string userId, string ipAdress, EntityType entityType);


        /// <summary>
        /// Inserts/ Updates an Identification Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="identificationType"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, IdentificationType? identificationType, string? ErrorDescription)> SaveIdentificationType(string userId, string ipAdress, IdentificationType identificationType);



        /// <summary>
        /// Inserts/ Updates an Entity Role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityRole"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, EntityRole? entityRole, string? ErrorDescription)> SaveEntityRole(string userId, string ipAdress, EntityRole entityRole);

        /// <summary>
        /// Insert / Update a Pallet Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="palletType"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, PalletType? palletType, string? ErrorDescription)> SavePalletType(string userId, string ipAdress, PalletType palletType);


        /// <summary>
        /// Retrieves a Pallet Type by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="palletTypeId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, PalletType? palletType, string? ErrorDescription)> GetPalletType(string userId, string ipAdress, string palletTypeId);



        /// <summary>
        /// Delete an EntityType not associated with Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="creditTypeId"></param>       
        /// <returns></returns>
        Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteEntityTypeById(string userId, string ipAddress, string squadId, string? creditTypeId);


        /// <summary>
        /// Delete an EntityGroup not associated with Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="entityGroupId"></param>       
        /// <returns></returns>
        Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteEntityGroupById(string userId, string ipAddress, string squadId, string? entityGroupId);



        /// <summary>
        /// Delete an EntityRole not associated with Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="entityRoleId"></param>       
        /// <returns></returns>
        Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteEntityRoleById(string userId, string ipAddress, string squadId, string? entityRoleId);

        /// <summary>
        /// Delete an PalletType not associated with Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="palletTypeId"></param>       
        /// <returns></returns>
        Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeletePalletTypeById(string userId, string ipAddress, string squadId, string? palletTypeId);

        /// <summary>
        /// Delete an identificationType not associated with Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="identificationTypeId"></param>       
        /// <returns></returns>
        Task<(bool IsSuccess, bool iCanRemoveIt, string? ErrorDescription)> DeleteIdentificationTypeById(string userId, string ipAddress, string squadId, string? identificationTypeId);

    }
}
