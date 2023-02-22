using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces
{
    public interface ITransactionalItems
    {

        /// <summary>
        /// Retrives a list with Transactional Items meeting search criteria
        /// </summary>
        /// <param name="nameLike"></param>
        /// <param name="className"></param>
        /// <param name="Code"></param>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        Task<List<TransactionalItem>> GetTransactionalItemsList(int? page = 1, int? perPage = 10, string? nameLike = null, string? className = null, string? Code = null, bool forceRefresh = false);

        /// <summary>
        /// Retrives a list with Actors Items meeting search criteria
        /// </summary>
        /// <param name="nameLike"></param>       
        /// <returns></returns>
        Task<List<Concept>> GetSelectorListEntityActors(string? nameLike = null);

        /// <summary>
        /// Retrives a list with ProductModel Items meeting search criteria
        /// </summary>
        /// <param name="transactionalItemId"></param>       
        /// <returns></returns>
        Task<List<ProductModel>> GetSelectorListEntityProductModel(string? transactionalItemId = null);

        /// <summary>
        /// etrives a list with Box To Sale Items
        /// </summary>        //   
        /// <returns></returns>
        Task<List<Box>> GetSelectorListEntityBoxToSale();

        /// <summary>
        /// etrives a list with Box To Buy Items
        /// </summary>        //   
        /// <returns></returns>
        Task<List<Box>> GetSelectorListEntityBoxToBuy();

        /// <summary>
        /// Retrives a list with Season Business Items meeting search criteria
        /// </summary>
        /// <param name="nameLike"></param>       
        /// <returns></returns>
        Task<List<SeasonBusiness>> GetSelectorListSeasonBusiness(string? nameLike = null);

        /// <summary>
        /// Retrives a list with Tag Items meeting search criteria
        /// </summary>
        /// <param name="nameLike"></param>       
        /// <returns></returns>
        Task<List<TransactionalItemTag>> GetSelectorListTag(string? nameLike = null);      

        /// <summary>
        /// Retrives a list with Path Images Items meeting search criteria
        /// </summary>
        /// <param name="transactionalItemId"></param>       
        /// <returns></returns>
        Task<List<TransactItemImage>> GetTransactionalItemDetailsPathImages(string? transactionalItemId = null);

        /// Retrives a list with Quality Parameters Items meeting search criteria
        /// </summary>
        /// <param name="transactionalItemId"></param>       
        /// <returns></returns>
        Task<List<TransactionalItemQualityPair>> GetTransactionalItemDetailsQualityParameters(string? transactionalItemId = null);

        /// Retrives a list with Process Step  Items meeting search criteria
        /// </summary>
        /// <param name="transactionalItemId"></param>       
        /// <returns></returns>
        Task<List<TransactionalItemProcessStep>> GetTransactionalItemDetailsProductionSpecs(string? transactionalItemId = null);

        /// Retrives a list with Packing Recipe Items meeting search criteria
        /// </summary>
        /// <param name="transactionalItemId"></param>       
        /// <returns></returns>
        Task<List<PackingSpecs>> GetTransactionalItemDetailsPackingRecipe(string? transactionalItemId = null);

        /// <summary>
        /// Retrives a list with Box meeting search criteria
        /// </summary>
        /// <param name="nameLike"></param>       
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        Task<List<Box>> GetBoxGetBoxTable( string? nameLike = null);

        /// <summary>
        /// Retrives Boxsed  with search criteria
        /// </summary>
        /// <param name="boxID"></param>     
        /// <returns></returns>
        Task<Box> GetBox(string boxID);


        /// <summary>
        /// Retrives a list with Seasons meeting search criteria
        /// </summary>
        /// <param name="nameLike"></param>       
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        Task<List<SeasonBusiness>> GetSeasonsTable(int? page = 1, int? perPage = 10, string? nameLike = null);

        /// <summary>
        /// Retrives Season  with search criteria
        /// </summary>
        /// <param name="seasonId"></param>     
        /// <returns></returns>
        Task<SeasonBusiness> GetSeason(string seasonId);

        /// <summary>
        /// Retrives a list with transactional item type meeting search criteria
        /// </summary>
        /// <param name="nameLike"></param>              
        /// <returns></returns>
        Task<List<TransactionalItemType>> GetTransactionalItemType( string? nameLike = null);

        /// <summary>
        /// Retrives a list with Transactional Statuses meeting search criteria
        /// </summary>
        /// <param name="nameLike"></param>              
        /// <returns></returns>
        Task<List<TransactionalItemStatus>> GetTransactionalStatusesTable(string? nameLike = null);

        /// save a Packing Spec
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="packingSpecs"></param>   
        /// <returns></returns>
        Task<bool> SaveProductPackingSpec(string? transactionalItemId, PackingSpecs? packingSpecs);

        /// save a Tag
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemTag"></param>   
        /// <returns></returns>
        Task<bool> SaveTags(string? transactionalItemId, TransactionalItemTag? transactionalItemTag);

        /// save a Production Specs
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemProcessStep"></param>   
        /// <returns></returns>
        Task<bool> SaveProductionSpecs(string? transactionalItemId, TransactionalItemProcessStep transactionalItemProcessStep);

        /// save a Image
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactItemImage"></param>   
        /// <returns></returns>
        Task<bool> SaveImage(string? transactionalItemId, TransactItemImage transactItemImage);

        /// Save Quality Parameters
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemQualityPair"></param>   
        /// <returns></returns>
        Task<bool> SaveQualityParameters(string transactionalItemId,TransactionalItemQualityPair transactionalItemQualityPair);

        /// Save Transactional Item
        /// </summary>      
        /// <param name="transactionalItem"></param>   
        /// <returns></returns>
        Task<TransactionalItem> SaveTransactionalItem(TransactionalItem transactionalItem);


        /// Save Season
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="season"></param>   
        /// <returns></returns>
        Task<bool> SaveSeason(SeasonBusiness season);

        /// Save Box
        /// </summary>
        /// <param name="box"></param>   
        /// <returns></returns>
        Task<bool> SaveBox(Box box);

        /// Save Transactional Item Type
        /// </summary>
        /// <param name="transactionalItemType"></param>   
        /// <returns></returns>
        Task<bool> SaveTransactionalItemType( TransactionalItemType transactionalItemType);

        /// Save Transactional Item Group
        /// </summary>       
        /// <param name="conceptGroup"></param>   
        /// <returns></returns>
        Task<bool> SaveTransactionalItemGroup( ConceptGroup conceptGroup);

        /// Save Status
        /// </summary>
        /// <param name="transactionalItemStatus"></param>   
        /// <returns></returns>
        Task<bool> SaveStatus(TransactionalItemStatus transactionalItemStatus);

        /// Upload Files
        /// </summary>
        /// <param name="multipartFormDataContent"></param>     
        /// <returns></returns>
        Task<string> UploadFiles(MultipartFormDataContent multipartFormDataContent);


    }
}

