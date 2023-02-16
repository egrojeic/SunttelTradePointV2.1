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
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemType? transactionalItemType, string? ErrorDescription)> GetTransactionalItemType(string transactionalItemId);

        /// <summary>
        /// Insert /Updates a Transactional Item Type
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemType"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemType? transactionalItemType, string? ErrorDescription)> SaveTransactionalItemType(TransactionalItemType transactionalItemType);

        /// <summary>
        /// Retrieves a table with all Box types in the system
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<Box>? boxes, string? ErrorDescription)> GetBoxTable();


        /// <summary>
        /// Retrieves a particular box object
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, Box? box, string? ErrorDescription)> GetBox(string boxID);


        /// <summary>
        /// Insert / Updates a box
        /// </summary>
        /// <param name="boxId"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, Box? box, string? ErrorDescription)> SaveBox(Box box);

        /// <summary>
        /// Retrieves a table with all seasons in the system
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<SeasonBusiness>? seasons, string? ErrorDescription)> GetSeasonsTable();


        /// <summary>
        /// Retrieves the info of a particular season
        /// </summary>
        /// <param name="seasonId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, SeasonBusiness? season, string? ErrorDescription)> GetSeason(string seasonId);

        /// <summary>
        /// Insert / Udates a Season
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="season"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, SeasonBusiness? season, string? ErrorDescription)> SaveSeason(SeasonBusiness season);


        /// <summary>
        /// Retrieves a table with all possible Transactional States
        /// </summary>
        /// <returns></returns>
        Task<(bool IsSuccess, List<TransactionalItemStatus>?  transactionalItemStatuses, string? ErrorDescription)> GetTransactionalStatusesTable();


        /// <summary>
        /// Insert / Updates a Transactional Item Status
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="transactionalItemStatus"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemStatus? transactionalItemStatus, string? ErrorDescription)> SaveStatus(TransactionalItemStatus transactionalItemStatus);




        /// <summary>
        /// Retrives the list of Transactional Items Groups filtered by the parameter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<ConceptGroup>? transactionalItemGroups, string? ErrorDescription)> GetTransactionalItemGroups(string filterCondition);


        /// <summary>
        /// Retrives a particular Transactional Item Group
        /// </summary>
        /// <param name="transactionalItemGroupId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, ConceptGroup? transactionalItemGroup, string? ErrorDescription)> GetTransactionalItemGroup(string transactionalItemGroupId);


        /// <summary>
        /// Saves a particular Transactional Item Group
        /// </summary>
        /// <param name="transactionalItemGroupId"></param>
        /// <param name="transactionalItemGroup"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, ConceptGroup? transactionalItemGroup, string? ErrorDescription)> SaveTransactionalItemGroup(ConceptGroup transactionalItemGroup);

    }
}
