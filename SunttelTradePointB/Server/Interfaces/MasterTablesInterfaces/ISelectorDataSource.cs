using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces
{

    /// <summary>
    /// Interface for a servoce intended to provide Data in order to fill comboboxes, lists, or
    /// any other control used to be a selector of a field in a Document form
    /// </summary>
    public interface ISelectorDataSource
    {

        /// <summary>
        /// Retrieves the list of Entity/Nodes/Actors filtered by the optional parameter or/and by role
        /// </summary>
        /// <param name="filterString"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<AtomConcept>? EntityActorList, string? ErrorDescription)> GetSelectorListEntityActor(string? filterString, BasicRolesFilter? roleName = null);
     
        /// <summary>
        /// Retrives the different address of an Entity
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<AtomConcept>? EntityActorAddressList, string? ErrorDescription)> GetSelectorListEntityAddress(string? entityId);


       

        /// <summary>
        /// Retrieves the list of Entity Groups filtered by teh parameter
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<AtomConcept>? EntityGroupsList, string? ErrorDescription)> GetSelectorListEntityGroups(string? filterString);

        /// <summary>
        /// Retrieves the list of Entity  Roles
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterString"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<AtomConcept>? EntityRolesList, string? ErrorDescription)> GetSelectorListEntityRoles(string? filterString, int? page = 1, int? perPage = 10);


        /// <summary>
        /// Retrieves the list of Seasons
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<AtomConcept>? SeasonsList, string? ErrorDescription)> GetSelectorListSeasonBusiness();


        /// <summary>
        /// Retrieves the list of Transactional Item Groups
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<ConceptGroup>? TransactionalItemGroupsList, string? ErrorDescription)> GetSelectorListTransactionalItemGroups(string? filterString);


        /// <summary>
        /// Retrieves the list of Transactional Item Types
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<AtomConcept>? TransactionalItemTypesList, string? ErrorDescription)> GetSelectorListTransactionalItemTypes();

        /// <summary>
        /// Retrieves a list of Boxes
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<Box>? boxes, string? ErrorDescription)> GetSelectorListBoxes();

        /// <summary>
        /// Retrieves a the list of the different models are already registered for a transactional item
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<AtomConcept>? transactionalItemModels, string? ErrorDescription)> GetSelectorListTransactionalItemModels(string transactionalItemId);


        /// <summary>
        /// Returns a list of the different Identification Types in the system
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<IdentificationType>? identificationTypes, string? ErrorDescription)> GetSelectorListIdentificationTypes(string? filterName = null);


        /// <summary>
        /// Retrives Id, and name of electronic Address which group is Network Platform
        /// db.EntityNodes.find({"Groups.Name": {"$in": ["Network Platforms"]}}, {"Name": 1})
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<AtomConcept>? ElectronicAddressEntities, string? ErrorDescription)> GetSelectorElectronicAddressEntities();

        /// <summary>
        /// Retrieves a list of EntitiyRelationship Types
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<EntitiyRelationshipType>?   entitiyRelationshipTypes, string? ErrorDescription)> GetSelectorEntitiyRelationshipTypes();


        /// <summary>
        /// Retrieves a list of Pallet Types
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<PalletType>? palletTypes, string? ErrorDescription)> GetSelectorListPalletTypes();

        /// <summary>
        /// Retrieves a list of Entity Types
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<EntityType>?  entityTypes, string? ErrorDescription)> GetSelectorListEntityTypes(string? filterName = null);





        /// <summary>
        /// Retrieves the posible values for an specific recipe modifier
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="modifierId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<ProductRecipeQualityModifier>? productRecipeQualityModifiers, string? ErrorDescription)> GetProductRecipeQualityModifiersByModifierId(string userId, string ipAddress, string modifierId);

        /// <summary>
        /// Retrieves thelist of products used as packing material
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<AtomConcept>? materialsList, string? ErrorDescription)> GetSelectorListPackingMaterials(string filterString);


        /// <summary>
        /// Retrieves a list of assembly types
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<AtomConcept>? assemblyTypes, string? ErrorDescription)> GetSelectorListAssemblyTypes(string filterString);



        /// <summary>
        /// Retrieves a list of papers fro labels
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<AtomConcept>? labelPapers, string? ErrorDescription)> GetSelectorListLabelPaper();

    }
}
