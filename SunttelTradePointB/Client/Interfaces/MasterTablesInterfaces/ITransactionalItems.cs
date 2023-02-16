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
        /// Retrives a list with Entity Groups Items meeting search criteria
        /// </summary>
        /// <param name="nameLike"></param>       
        /// <returns></returns>
        Task<List<ConceptGroup>> GetSelectorListEntityGroups(string? nameLike = null);

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

        /// save a Image
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemQualityPair"></param>   
        /// <returns></returns>
        Task<bool> SaveQualityParameters(string transactionalItemId,TransactionalItemQualityPair transactionalItemQualityPair);



      

    }
}
