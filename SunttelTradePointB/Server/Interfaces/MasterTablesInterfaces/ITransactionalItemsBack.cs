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
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="squadId"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="nameLike"></param>
        /// <param name="groupName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<TransactionalItem>? TransactionalItemRelatedList, string? ErrorDescription)> GetTransactionItemList(string userId, string ipAddress, string squadId, int? page = 1, int? perPage = 10, string? nameLike = null, string? groupName = null, string? code = null);


        /// <summary>
        /// Generic function to retrieve details related to a Transactional Item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transactItemId"></param>
        /// <param name="transactionalItemDetailsSection"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<T>? TransactionalItemRelatedList, string? ErrorDescription)> GetTransactionalItemDetailsOf<T>(string userId, string ipAddress, string transactItemId, TransactionalItemDetailsSection transactionalItemDetailsSection);


        /// <summary>
        /// Retrieves an object of a transactional Item by id
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, AtomConcept transactionalItem, string? ErrorDescription)> GetTransactionalItemById(string userId, string ipAddress, string transactionalItemId);


        /// <summary>
        /// Updates/ Insert a new Transactional Item
        /// </summary>
        /// <param name="transactionalItem"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItem? transactionalItem, string? ErrorDescription)> SaveTransactionalItem(string userId, string ipAddress, TransactionalItem transactionalItem);


        /// <summary>
        /// Insert / Update a Transactional Item Characteristic
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemCharacteristicPair"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemCharacteristicPair? transactionalItemCharacteristicPairs, string? ErrorDescription)> SaveCharacteristics(string userId, string ipAddress, string transactionalItemId, TransactionalItemCharacteristicPair transactionalItemCharacteristicPair);


        /// <summary>
        /// Insert / Update a Transactional Item Image
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactItemImage"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactItemImage? transactItemImage, string? ErrorDescription)> SaveImage(string userId, string ipAddress, string transactionalItemId, TransactItemImage transactItemImage);


        /// <summary>
        /// Insert / Update a Transactional Item Production Step
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemProcessStep"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemProcessStep? transactionalItemProcessStep, string? ErrorDescription)> SaveProductionSpecs(string userId, string ipAddress, string transactionalItemId, TransactionalItemProcessStep transactionalItemProcessStep);

        /// <summary>
        /// Insert / Update a Transactional Item Quality Parameter
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemQualityPair"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemQualityPair? transactionalItemQualityPair, string? ErrorDescription)> SaveQualityParameters(string userId, string ipAddress, string transactionalItemId, TransactionalItemQualityPair transactionalItemQualityPair);

        /// <summary>
        /// Insert / Update a Transactional Packing Instructions
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="packingSpecs"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, PackingSpecs? packingSpecs, string? ErrorDescription)> SaveProductPackingSpecs(string userId, string ipAddress, string transactionalItemId, PackingSpecs packingSpecs);


        /// <summary>
        /// Insert / Update a Transactional Item Tag
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemTag"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemTag? transactionalItemTag, string? ErrorDescription)> SaveTags(string userId, string ipAddress, string transactionalItemId, TransactionalItemTag transactionalItemTag);

        /// <summary>
        /// Retrieves the list of Transactional Types that match with the filter condition in the parameter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<TransactionalItemType>?  transactionalItemTypes, string? ErrorDescription)> GetTransactionalItemTypes(string userId, string ipAddress, string filterCondition);

        /// <summary>
        /// Retrieves a Transactional Type of the corresponding id
        /// </summary>
        /// <param name="TransactionalItemTypeId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemType? transactionalItemType, string? ErrorDescription)> GetTransactionalItemType(string userId, string ipAddress, string TransactionalItemTypeId);

        /// <summary>
        /// Insert/ Updates a Transactional Type of the corresponding id
        /// </summary>
        /// <param name="transactionalItemType"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, TransactionalItemType? transactionalItemType, string? ErrorDescription)> SaveTransactionalItemType(string userId, string ipAddress, TransactionalItemType transactionalItemType);


        /// <summary>
        /// Retrieves a list of boxes matching the filter criteria.
        /// If the criteria is empty it should list all
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<Box>?  boxes, string? ErrorDescription)> GetBoxes(string userId, string ipAddress, string filterCondition);


        /// <summary>
        /// Retrieves the information of a particular box id
        /// </summary>
        /// <param name="boxId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, Box? box, string? ErrorDescription)> GetBoxeById(string userId, string ipAddress, string boxId);

        /// <summary>
        /// Insert/ Updates Box information
        /// </summary>
        /// <param name="box"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, Box? box, string? ErrorDescription)> SaveBox(string userId, string ipAddress, Box box);



        /// <summary>
        /// Retrieves a list of seasons matching the filter criteria.
        /// If the criteria is empty it should list all
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<SeasonBusiness>?  seasonBusinesses, string? ErrorDescription)> GetSeasons(string userId, string ipAddress, string filterCondition);

        /// <summary>
        /// Retrieves the information of a particular season id
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, SeasonBusiness? seasonBusiness, string? ErrorDescription)> GetSeason(string userId, string ipAddress, string seasonId);

        /// <summary>
        /// Insert/ Updates season information
        /// </summary>
        /// <param name="seasonBusiness"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, SeasonBusiness? seasonBusiness, string? ErrorDescription)> SaveSeason(string userId, string ipAddress, SeasonBusiness seasonBusiness);


        /// <summary>
        /// Retrieves the Models of a Transactional Items
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="trasnsactionalItemId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<ProductModel>? productModels, string? ErrorDescription)> GetListModelsByTransactionalItemId(string userId, string ipAddress, string trasnsactionalItemId);

        /// <summary>
        /// Retrieves a list with the different posble process for a Transactional Item Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<TransactionalItemProcessStep>?  transactionalItemProcessSteps, string? ErrorDescription)> GetTransactionalItemProcessStepsByTypeID(string userId, string ipAddress, string transactionalItemTypeId);

        /// <summary>
        /// Retrieves a list with the different possible characterisitics for a Transactional Item Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<TransactionalItemTypeCharacteristic>?  transactionalItemTypeCharacteristics, string? ErrorDescription)> GetTransactionalItemTypeCharacteristicByTypeID(string userId, string ipAddress, string transactionalItemTypeId);


        /// <summary>
        /// Retrieves a list with the different possible Quality Parameters for a Transactional Item Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<TransactionalItemQuality>? transactionalItemQualities, string? ErrorDescription)> GetQualityParametersByTypeID(string userId, string ipAddress, string transactionalItemTypeId);


        /// <summary>
        /// Retrieves a list with the different possible Quality Parameters for a Transactional Item Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<RecipeModifier>?  recipeModifiers, string? ErrorDescription)> GetRecipeModifiersByTypeID(string userId, string ipAddress, string transactionalItemTypeId);


        /// <summary>
        /// Saves (INSERT/UPDATE) A transactional item model
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemId"></param>
        /// <param name="productModel"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, ProductModel? productModel, string? ErrorDescription)> SaveTransactionalItemModels(string userId, string ipAddress, string transactionalItemId, ProductModel productModel);
    }
}
