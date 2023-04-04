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
        Task<List<Concept>> GetSelectorListEntityActors(string? nameLike = null, string roleName = null);

        /// <summary>
        /// Retrives a list with ProductModel Items meeting search criteria
        /// </summary>
        /// <param name="transactionalItemId"></param>       
        /// <returns></returns>
        Task<List<ProductModel>> GetSelectorListEntityProductModel(string? transactionalItemId = null);



        /// <summary>
        /// Retrives a item with ProductModel Items meeting search criteria
        /// </summary>
        /// <param name="transactionalItemId"></param>    
        /// <param name="productModelId"></param>    
        /// <returns></returns>
        Task<ProductModel> GetProductModelById(string productModelId);


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
        Task<List<Box>> GetBoxTable(string? nameLike = null);

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
        Task<TransactionalItemType> GetTransactionalItemType(string? nameLike = null);

        /// <summary>
        /// Retrives a list with Transactional Statuses meeting search criteria
        /// </summary>
        /// <param name="nameLike"></param>              
        /// <returns></returns>
        Task<List<TransactionalItemStatus>> GetTransactionalStatusesTable(string? nameLike = null);


        /// <summary>
        /// Retrives a list with transactional item status meeting search criteria
        /// </summary>
        /// <param name="filterString"></param>              
        /// <returns></returns>
        Task<TransactionalItemStatus> GetSelectorListPackingMaterials(string? filterString = null);

        /// <summary>
        /// Retrives a list with transactional item status meeting search criteria
        /// </summary>
        /// <param name="transactionalItemTypeId"></param>              
        /// <returns></returns>
        Task<List<RecipeModifier>> GetRecipeModifiersByTypeID(string? transactionalItemTypeId = null);

        /// <summary>
        /// Retrives a list with assembly type meeting search criteria
        /// </summary>
        /// <param name="filterString"></param>              
        /// <returns></returns>
        Task<List<AssemblyType>> GetSelectorListAssemblyTypes(string? filterString = null);

        /// <summary>
        /// Retrives a transactional item meeting search criteria
        /// </summary>
        /// <param name="transactionalItemId"></param>              
        /// <returns></returns>
        Task<TransactionalItem> GetTransactionalItemById(string? transactionalItemId = null);

        /// <summary>
        /// No parameter                
        /// <returns></returns>
        Task<List<Box>> GetBoxes();

        /// <summary>
        /// Retrives a list with label style meeting search criteria
        /// </summary>
        /// <param name="labelStyleId"></param>              
        /// <returns></returns>
        Task<LabelStyle> GetLabelStyle(string? labelStyleId = null);

        /// <summary>
        /// Retrives a list with concept group meeting search criteria
        /// </summary>
        /// <param name="filterCondition"></param>              
        /// <returns></returns>
        Task<List<ConceptGroup>> GetSelectorListTransactionalItemGroups(string? filterCondition = null);

        /// <summary>
        /// Retrives a list with label style  meeting search criteria
        /// </summary>
        /// <param name="filterCondition"></param>              
        /// <returns></returns>
        Task<List<LabelStyle>> GetLabelStyles(string? filterCondition = null);


        /// <summary>
        /// Retrives a list with product model  meeting search criteria
        /// </summary>
        /// <param name="transactionalItemId"></param>              
        /// <returns></returns>
        Task<List<ProductModel>> GetSelectorListTransactionalItemModels(string? transactionalItemId = null);


        /// <summary>
        /// Retrives a list with transactionalItem type  meeting search criteria
        /// </summary>
        /// <param name="filterCondition"></param>              
        /// <returns></returns>
        Task<List<TransactionalItemType>> GetSelectorListTransactionalItemTypes(string? filterCondition = null);

        /// <summary>
        /// Retrives a list with transactionalItem type  meeting search criteria
        /// </summary>
        /// <param name="modifierId"></param>              
        /// <returns></returns>
        Task<List<TransactionalItemType>> GetProductRecipeQualityModifiersByModifierId(string? modifierId = null);

        /// <summary>
        /// Retrives a list with transactionalItem characteristic pair  meeting search criteria
        /// </summary>
        /// <param name="transactionalItemTypeId"></param>              
        /// <returns></returns>
        Task<List<TransactionalItemCharacteristicPair>> GetCharacteristic(string? transactionalItemTypeId = null);

        /// <summary>
        /// Retrives a list with label paper  meeting search criteria
        /// </summary>
        /// <param name="filterCondition"></param>              
        /// <returns></returns>
        Task<List<LabelPaper>> GetSelectorListLabelPaper(string? filterCondition = null);

        /// <summary>
        /// Retrives a list with label paper  meeting search criteria
        /// </summary>
        /// <param name="labelPaperId"></param>              
        /// <returns></returns>
        Task<LabelPaper> GetConceptPaperId(string? labelPaperId = null);

        /// <summary>
        /// Retrives a Selector List of Packing Materials
        /// </summary>
        /// <param name="filterString"></param>              
        /// <returns></returns>
        Task<List<Concept>> GetConceptProduct(string? filterString = null);

        /// <summary>
        /// Retrives a list with transactionalItem type  meeting search criteria
        /// </summary>
        /// <param name="filterCondition"></param>              
        /// <returns></returns>
        Task<List<TransactionalItemType>> GetTransactionalItemTypes(string? filterCondition = null);



        /// <summary>
        /// Retrives a list with season business  meeting search criteria
        /// </summary>
        /// <param name="filterCondition"></param>              
        /// <returns></returns>
        Task<List<SeasonBusiness>> GetSeasons(string? filterCondition = null);


        /// <summary>
        /// Retrives a list with box meeting search criteria
        /// </summary>
        /// <param name="filterCondition"></param>              
        /// <returns></returns>
        Task<List<Box>> GetSelectorListBoxes(string? filterCondition = null);

        /// <summary>
        /// Save a  assembly type
        /// </summary>       
        /// <param name="assemblyType"></param>              
        /// <returns></returns>
        Task<bool> SaveAssemblyType(AssemblyType assemblyType);
        

        /// <summary>
        /// Save a  label paper
        /// </summary>       
        /// <param name="LabelPaper"></param>              
        /// <returns></returns>
        Task<bool> SaveConceptPaper(LabelPaper labelPaper);


        /// <summary>
        /// Save a  label style
        /// </summary>       
        /// <param name="labelStyle"></param>              
        /// <returns></returns>
        Task<bool> SaveLabelStyle(LabelStyle labelStyle);

        /// <summary>
        /// Save a  product model
        /// </summary>
        /// <param name="transactionalItemId"></param>              
        ///   /// <param name="productModel"></param>              
        /// <returns></returns>
        Task<bool> SaveTransactionalItemModels(string? transactionalItemId, ProductModel productModel);


        /// <summary>
        /// save a  characteristics
        /// </summary>
        /// <param name="transactionalItemId"></param>              
        /// <param name="transactionalItemCharacteristicPair"></param>              
        /// <returns></returns>
        Task<bool> SaveCharacteristics(string? transactionalItemId, TransactionalItemCharacteristicPair transactionalItemCharacteristicPair);



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
        Task<bool> SaveQualityParameters(string transactionalItemId, TransactionalItemQualityPair transactionalItemQualityPair);

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
        Task<bool> SaveTransactionalItemType(TransactionalItemType transactionalItemType);

        /// Save Transactional Item Group
        /// </summary>       
        /// <param name="conceptGroup"></param>   
        /// <returns></returns>
        Task<bool> SaveTransactionalItemGroup(ConceptGroup conceptGroup);

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


        /// <summary>
        /// Retrives a Transactional Item by transactionalItemId
        /// </summary>
        /// <param name="transactionalItemId"></param>              
        /// <param name="transactionalItemTagId"></param>              
        /// <returns></returns>
        Task<TransactionalItemTag> GetTagById(string transactionalItemId, string transactionalItemTagId);

        /// <summary>
        /// Retrives a Transactional Item Details Packing Recipe by transactionalItemId
        /// </summary>
        /// <param name="transactionalItemId"></param>              
        /// <param name="packingSpecsId"></param>              
        /// <returns></returns>
        Task<PackingSpecs> GetPackingRecipeById(string? transactionalItemId, string packingSpecsId);

        /// <summary>
        /// Retrives a Product Recipe Quality Modifiers By Modifier Id
        /// </summary>
        /// <param name="transactionalItemId"></param>              
        /// <param name="productionSpecsToId"></param>              
        /// <returns></returns>
        Task<TransactionalItemProcessStep> GetTransactionalItemDetailsProductionSpecsById(string transactionalItemId, string productionSpecsToId);


    }
}

