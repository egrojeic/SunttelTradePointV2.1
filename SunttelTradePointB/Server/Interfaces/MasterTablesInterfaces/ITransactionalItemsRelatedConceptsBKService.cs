using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces
{

    /// <summary>
    /// Interface to deal with related concepts to Transactional Items
    /// </summary>
    public interface ITransactionalItemsRelatedConceptsBKService
    {
        /// <summary>
        /// Retrieves a particular Transaction Item type object
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemType? transactionalItemType, string? ErrorDescription)> GetTransactionalItemType(string userId, string ipAddress, string transactionalItemId);

        /// <summary>
        /// Insert /Updates a Transactional Item Type
        /// </summary>
        /// <param name="transactionalItemType"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemType? transactionalItemType, string? ErrorDescription)> SaveTransactionalItemType(string userId, string ipAddress, TransactionalItemType transactionalItemType);

        /// <summary>
        /// Retrieves a table with all Box types in the system
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<Box>? boxes, string? ErrorDescription)> GetBoxTable();


        /// <summary>
        /// Retrieves a particular box object
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, Box? box, string? ErrorDescription)> GetBox(string userId, string ipAddress, string boxID);


        /// <summary>
        /// Insert / Updates a box
        /// </summary>
        /// <param name="box"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, Box? box, string? ErrorDescription)> SaveBox(string userId, string ipAddress, Box box);

        /// <summary>
        /// Retrieves a table with all seasons in the system
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<SeasonBusiness>? seasons, string? ErrorDescription)> GetSeasonsTable();


        /// <summary>
        /// Retrieves the info of a particular season
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, SeasonBusiness? season, string? ErrorDescription)> GetSeason(string userId, string ipAddress, string seasonId);

        /// <summary>
        /// Insert / Udates a Season
        /// </summary>
        /// <param name="season"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, SeasonBusiness? season, string? ErrorDescription)> SaveSeason(string userId, string ipAddress, SeasonBusiness season);


        /// <summary>
        /// Retrieves a table with all possible Transactional States
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<TransactionalItemStatus>?  transactionalItemStatuses, string? ErrorDescription)> GetTransactionalStatusesTable();


        /// <summary>
        /// Insert / Updates a Transactional Item Status
        /// </summary>
        /// <param name="transactionalItemStatus"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemStatus? transactionalItemStatus, string? ErrorDescription)> SaveStatus(string userId, string ipAddress, TransactionalItemStatus transactionalItemStatus);




        /// <summary>
        /// Retrives the list of Transactional Items Groups filtered by the parameter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<ConceptGroup>? transactionalItemGroups, string? ErrorDescription)> GetTransactionalItemGroups(string userId, string ipAddress, string filterCondition);


        /// <summary>
        /// Retrives a particular Transactional Item Group
        /// </summary>
        /// <param name="transactionalItemGroupId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, ConceptGroup? transactionalItemGroup, string? ErrorDescription)> GetTransactionalItemGroup(string userId, string ipAddress, string transactionalItemGroupId);


        /// <summary>
        /// Saves a particular Transactional Item Group
        /// </summary>
        /// <param name="transactionalItemGroup"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, ConceptGroup? transactionalItemGroup, string? ErrorDescription)> SaveTransactionalItemGroup(string userId, string ipAddress, ConceptGroup transactionalItemGroup);

        /// <summary>
        /// Saves (INSERT/UPDATE) a Transactional Item Process Step
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="transactionalItemProcessStep"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemProcessStep?  transactionalItemProcessStep, string? ErrorDescription)> SaveTransactionalItemProcessStep(string userId, string ipAddress, string transactionalItemTypeId, TransactionalItemProcessStep transactionalItemProcessStep);


        /// <summary>
        /// Saves (INSERT/UPDATE) a Transactional Item Type Characteristic
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="transactionalItemTypeCharacteristic"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemTypeCharacteristic?  transactionalItemTypeCharacteristic, string? ErrorDescription)> SaveTransactionalItemTypeCharacteristic(string userId, string ipAddress, string transactionalItemTypeId, TransactionalItemTypeCharacteristic transactionalItemTypeCharacteristic);

        /// <summary>
        /// Saves (INSERT/UPDATE) a Transactional Item Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="transactionalItemQuality"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemQuality?  transactionalItemQuality, string? ErrorDescription)> SaveTransactionalItemQuality(string userId, string ipAddress, string transactionalItemTypeId, TransactionalItemQuality  transactionalItemQuality);

        /// <summary>
        /// Saves (INSERT/UPDATE) a Recipe Modifier
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="recipeModifier"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, RecipeModifier?  recipeModifier, string? ErrorDescription)> SaveRecipeModifier(string userId, string ipAddress, string transactionalItemTypeId, RecipeModifier recipeModifier);


        /// <summary>
        /// Retrieves a particular assembly type by its ID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="assemblyTypeId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, AssemblyType? assemblyType, string? ErrorDescription)> GetAssemblyTypeByID(string userId, string ipAddress, string assemblyTypeId);


        /// <summary>
        /// Saves an assembly type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="assemblyType"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, AssemblyType? assemblyType, string? ErrorDescription)> SaveAssemblyType(string userId, string ipAddress, AssemblyType assemblyType);


        /// <summary>
        /// Retrieves information of a particular Label Style by its id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="labelStyleId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, LabelStyle?  labelStyle, string? ErrorDescription)> GetLabelStyle(string userId, string ipAddress, string labelStyleId);

        /// <summary>
        /// Save label style
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="labelStyle"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, LabelStyle? labelStyle, string? ErrorDescription)> SaveLabelStyle(string userId, string ipAddress, LabelStyle labelStyle);

        
        /// <summary>
        /// Retrieves the list of  Label Styles
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="filterString"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<LabelStyle>? labelStyles, string? ErrorDescription)> GetLabelStyles(string userId, string ipAddress, string? filterString);


    }
}
