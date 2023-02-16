using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces
{
    /// <summary>
    /// Interface to set the services to be render to deal with the Products, Services and all Transactional Items maintenance
    /// </summary>
    public interface ITransactionalItemsBack
    {

        /// <summary>
        /// Retrievs a list of transactional Items filtered by the parameters name, group, and code
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="nameLike"></param>
        /// <param name="groupName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<TransactionalItem>? TransactionalItemRelatedList, string? ErrorDescription)> GetTransactionItemList(int? page = 1, int? perPage = 10, string? nameLike = null, string? groupName = null, string? code = null);


        /// <summary>
        /// Generic function to retrieve details related to a Transactional Item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transactItemId"></param>
        /// <param name="transactionalItemDetailsSection"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<T>? TransactionalItemRelatedList, string? ErrorDescription)> GetTransactionalItemDetailsOf<T>(string transactItemId, TransactionalItemDetailsSection transactionalItemDetailsSection);


        /// <summary>
        /// Retrieves an object of a transactional Item by id
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, AtomConcept transactionalItem, string? ErrorDescription)> GetTransactionalItemById(string transactionalItemId);


        /// <summary>
        /// Updates/ Insert a new Transactional Item
        /// </summary>
        /// <param name="transactionalItem"></param>
        /// <param name="transactionalItemId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItem? transactionalItem, string? ErrorDescription)> SaveTransactionalItem(TransactionalItem transactionalItem, string transactionalItemId);


        /// <summary>
        /// Insert / Update a Transactional Item Characteristic
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemCharacteristicPair"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemCharacteristicPair? transactionalItemCharacteristicPairs, string? ErrorDescription)> SaveCharacteristics(string transactionalItemId, TransactionalItemCharacteristicPair transactionalItemCharacteristicPair);


        /// <summary>
        /// Insert / Update a Transactional Item Image
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactItemImage"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactItemImage? transactItemImage, string? ErrorDescription)> SaveImage(string transactionalItemId, TransactItemImage transactItemImage);


        /// <summary>
        /// Insert / Update a Transactional Item Production Step
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemProcessStep"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemProcessStep? transactionalItemProcessStep, string? ErrorDescription)> SaveProductionSpecs(string transactionalItemId, TransactionalItemProcessStep transactionalItemProcessStep);

        /// <summary>
        /// Insert / Update a Transactional Item Quality Parameter
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemQualityPair"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemQualityPair? transactionalItemQualityPair, string? ErrorDescription)> SaveQualityParameters(string transactionalItemId, TransactionalItemQualityPair transactionalItemQualityPair);

        /// <summary>
        /// Insert / Update a Transactional Packing Instructions
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="packingSpecs"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, PackingSpecs? packingSpecs, string? ErrorDescription)> SaveProductPackingSpecs(string transactionalItemId, PackingSpecs packingSpecs);


        /// <summary>
        /// Insert / Update a Transactional Item Tag
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemTag"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemTag? transactionalItemTag, string? ErrorDescription)> SaveTags(string transactionalItemId, TransactionalItemTag transactionalItemTag);

        /// <summary>
        /// Retrieves the list of Transactional Types that match with the filter condition in the parameter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<TransactionalItemType>?  transactionalItemTypes, string? ErrorDescription)> GetTransactionalItemTypes(string filterCondition);

        /// <summary>
        /// Retrieves a Transactional Type of the corresponding id
        /// </summary>
        /// <param name="TransactionalItemTypeId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemType? transactionalItemType, string? ErrorDescription)> GetTransactionalItemType(string TransactionalItemTypeId);

        /// <summary>
        /// Insert/ Updates a Transactional Type of the corresponding id
        /// </summary>
        /// <param name="TransactionalItemTypeId"></param>
        /// <param name="transactionalItemType"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemType? transactionalItemType, string? ErrorDescription)> SaveTransactionalItemType(string? TransactionalItemTypeId, TransactionalItemType transactionalItemType);


        /// <summary>
        /// Retrieves a list of boxes matching the filter criteria.
        /// If the criteria is empty it should list all
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<Box>?  boxes, string? ErrorDescription)> GetBoxes(string filterCondition);


        /// <summary>
        /// Retrieves the information of a particular box id
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, Box? box, string? ErrorDescription)> GetBoxeById(string boxId);

        /// <summary>
        /// Insert/ Updates Box information
        /// </summary>
        /// <param name="boxId"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, Box? box, string? ErrorDescription)> SaveBox(string? boxId, Box box);



        /// <summary>
        /// Retrieves a list of seasons matching the filter criteria.
        /// If the criteria is empty it should list all
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<SeasonBusiness>?  seasonBusinesses, string? ErrorDescription)> GetSeasons(string filterCondition);

        /// <summary>
        /// Retrieves the information of a particular season id
        /// </summary>
        /// <param name="seasonId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, SeasonBusiness? seasonBusiness, string? ErrorDescription)> GetSeason(string seasonId);

        /// <summary>
        /// Insert/ Updates season information
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="seasonBusiness"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, SeasonBusiness? seasonBusiness, string? ErrorDescription)> SaveSeason(string? seasonId, SeasonBusiness seasonBusiness);


    }
}
