using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Server.Controllers.MasterTablesCtrl
{

    /// <summary>
    /// Controller to deal with Products, Services, and MAterials maintenance operations
    /// </summary>
    [Tags("Transactional Items Maintenance")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionalItemsController : ControllerBase
    {
        private ITransactionalItemsBack _transactionalItems;
        private readonly ILogger<TransactionalItemsController> _logger;
        IConfiguration config;

        /// <summary>
        /// Class Constructor injecting the TransactionalItems Service for the back-end
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="_config"></param>
        /// <param name="transactionalItemsBack"></param>
        public TransactionalItemsController(ILogger<TransactionalItemsController> logger, IConfiguration _config, ITransactionalItemsBack transactionalItemsBack)
        {
            _logger = logger;
            config = _config;
            _transactionalItems = transactionalItemsBack;
        }


        /// <summary>
        /// Retrives a list of Transactional Items matching a search criteria
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItems")]
        public async Task<IActionResult> GetTransactionalItems(string userId, string ipAddress,  int? page = 1, int? perPage = 10, string? filterName = null)
        {
            var squadId = "";

            if (Request.Headers.TryGetValue("X-Custom-Header", out var customHeaderValue))
            {
                squadId = Request.Headers["SquadId"];

            }

            var response = await _transactionalItems.GetTransactionItemList(userId, ipAddress, squadId, page, perPage, squadId, filterName);

            if (response.IsSuccess)
            {
                return Ok(response.TransactionalItemRelatedList);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrieves the Packing Recipe of a particular Product ID
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemDetailsPackingRecipe")]
        public async Task<IActionResult> GetTransactionalItemDetailsPackingRecipe(string userId, string ipAddress, string transactionalItemId)
        {

            //var response = await _entityNodes.GetEntityActorAddressList(EntityId);
            var response = await _transactionalItems.GetTransactionalItemDetailsOf<PackingSpecs>(userId, ipAddress, transactionalItemId, TransactionalItemDetailsSection.PackingRecipe);

            if (response.IsSuccess)
            {
                return Ok(response.TransactionalItemRelatedList);
            }
            else
                return NotFound(response.ErrorDescription);

        }


        /// <summary>
        /// Retreives Information about the proccesses needed to produce the transactional item
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemDetailsProductionSpecs")]
        public async Task<IActionResult> GetTransactionalItemDetailsProductionSpecs(string userId, string ipAddress, string transactionalItemId)
        {

            var response = await _transactionalItems.GetTransactionalItemDetailsOf<TransactionalItemProcessStep>(userId, ipAddress, transactionalItemId, TransactionalItemDetailsSection.ProductionSpecs);

            if (response.IsSuccess)
            {
                return Ok(response.TransactionalItemRelatedList);
            }
            else
                return NotFound(response.ErrorDescription);

        }

        /// <summary>
        /// Retrieves information about quality standards the Transactional Item should meet
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemDetailsQualityParameters")]
        public async Task<IActionResult> GetTransactionalItemDetailsQualityParameters(string userId, string ipAddress, string transactionalItemId)
        {

            var response = await _transactionalItems.GetTransactionalItemDetailsOf<TransactionalItemQualityPair>(userId, ipAddress, transactionalItemId, TransactionalItemDetailsSection.QualityParameters);

            if (response.IsSuccess)
            {
                return Ok(response.TransactionalItemRelatedList);
            }
            else
                return NotFound(response.ErrorDescription);

        }


        /// <summary>
        /// Retrieves the different paths to images related to the transactional item provided as parameter
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemDetailsPathImages")]
        public async Task<IActionResult> GetTransactionalItemDetailsPathImages(string userId, string ipAddress, string transactionalItemId)
        {
            var response = await _transactionalItems.GetTransactionalItemDetailsOf<TransactItemImage>(userId, ipAddress, transactionalItemId, TransactionalItemDetailsSection.PathImages);

            if (response.IsSuccess)
            {
                return Ok(response.TransactionalItemRelatedList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves the tags related to the Transactional Item
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemDetailsTags")]
        public async Task<IActionResult> GetTransactionalItemDetailsTags(string userId, string ipAddress, string transactionalItemId)
        {
            try
            {
                var response = await _transactionalItems.GetTransactionalItemDetailsOf<TransactionalItemTag>(userId, ipAddress, transactionalItemId, TransactionalItemDetailsSection.Tags);
                if (response.IsSuccess)
                {
                    return Ok(response.TransactionalItemRelatedList);
                }
                else
                    return NotFound(response.ErrorDescription);
            }
            catch (Exception e) {
                string errMessage = e.Message;
                return NotFound("Error");
            }


           
        }

        /// <summary>
        /// Retrieves an object of a transactional Item by id
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemById")]
        public async Task<IActionResult> GetTransactionalItemById(string userId, string ipAddress, string transactionalItemId)
        {
            var response = await _transactionalItems.GetTransactionalItemById(userId, ipAddress, transactionalItemId);

            if (response.IsSuccess)
            {
                return Ok(response.transactionalItem);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrieves a Transactional Item Type object by Id
        /// </summary>
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemType")]
        public async Task<IActionResult> GetTransactionalItemType(string userId, string ipAddress, string transactionalItemTypeId)
        {
            var response = await _transactionalItems.GetTransactionalItemType(userId, ipAddress, transactionalItemTypeId);

            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list of Transactional Item Types with the posibility to receive an optional paremeter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemTypes")]
        public async Task<IActionResult> GetTransactionalItemTypes(string userId, string ipAddress, string? filterCondition = null)
        {
            var response = await _transactionalItems.GetTransactionalItemTypes(userId, ipAddress, filterCondition);

            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemTypes);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a Box by Id
        /// </summary>
        /// <param name="boxId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetBoxeById")]
        public async Task<IActionResult> GetBoxeById(string userId, string ipAddress, string boxId)
        {
            var response = await _transactionalItems.GetBoxeById(userId, ipAddress, boxId);

            if (response.IsSuccess)
            {
                return Ok(response.box);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list of Box Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetBoxes")]
        public async Task<IActionResult> GetBoxes(string userId, string ipAddress, string? filterCondition = null)
        {
            var response = await _transactionalItems.GetBoxes(userId, ipAddress, filterCondition);

            if (response.IsSuccess)
            {
                return Ok(response.boxes);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a Seasson by Id
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSeason")]
        public async Task<IActionResult> GetSeason(string userId, string ipAddress, string seasonId)
        {
            var response = await _transactionalItems.GetSeason(userId, ipAddress, seasonId);

            if (response.IsSuccess)
            {
                return Ok(response.seasonBusiness);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list of Seassons
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSeasons")]
        public async Task<IActionResult> GetSeasons(string userId, string ipAddress, string? filterCondition = null)
        {
            var response = await _transactionalItems.GetSeasons(userId, ipAddress, filterCondition);

            if(response.IsSuccess)
            {
                return Ok(response.seasonBusinesses);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list with the different posble process for a Transactional Item Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemProcessStepsByTypeID")]
        public async Task<IActionResult> GetTransactionalItemProcessStepsByTypeID(string userId, string ipAddress, string transactionalItemTypeId)
        {
            var response = await _transactionalItems.GetTransactionalItemProcessStepsByTypeID(userId, ipAddress, transactionalItemTypeId);

            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemProcessSteps);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list with the different possible characterisitics for a Transactional Item Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemTypeCharacteristicByTypeID")]
        public async Task<IActionResult> GetTransactionalItemTypeCharacteristicByTypeID(string userId, string ipAddress, string transactionalItemTypeId)
        {
            var response = await _transactionalItems.GetTransactionalItemTypeCharacteristicByTypeID(userId, ipAddress, transactionalItemTypeId);

            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemTypeCharacteristics);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list with the different possible Quality Parameters for a Transactional Item Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetQualityParametersByTypeID")]
        public async Task<IActionResult> GetQualityParametersByTypeID(string userId, string ipAddress, string transactionalItemTypeId)
        {
            var response = await _transactionalItems.GetQualityParametersByTypeID(userId, ipAddress, transactionalItemTypeId);

            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemQualities);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list with the different possible Quality Parameters for a Transactional Item Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRecipeModifiersByTypeID")]
        public async Task<IActionResult> GetRecipeModifiersByTypeID(string userId, string ipAddress, string transactionalItemTypeId)
        {
            var response = await _transactionalItems.GetRecipeModifiersByTypeID(userId, ipAddress, transactionalItemTypeId);

            if (response.IsSuccess)
            {
                return Ok(response.recipeModifiers);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Updates/ Insert a new Transactional Item
        /// </summary>
        /// <param name="transactionalItem"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTransactionalItem")]
        public async Task<IActionResult> SaveTransactionalItem(string userId, string ipAddress, TransactionalItem transactionalItem)
        {
            var response = await _transactionalItems.SaveTransactionalItem(userId, ipAddress, transactionalItem);

            if (response.IsSuccess)
            {
                return Ok(response.transactionalItem);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Insert / Update a Transactional Item Characteristic
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemCharacteristicPair"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveCharacteristics")]
        public async Task<IActionResult> SaveCharacteristics(string userId, string ipAddress, string transactionalItemId, TransactionalItemCharacteristicPair transactionalItemCharacteristicPair)
        {
            var response = await _transactionalItems.SaveCharacteristics(userId, ipAddress, transactionalItemId, transactionalItemCharacteristicPair);

            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemCharacteristicPairs);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Insert / Update a Transactional Item Image
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactItemImage"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveImage")]
        public async Task<IActionResult> SaveImage(string userId, string ipAddress, string transactionalItemId, TransactItemImage transactItemImage)
        {
            var response = await _transactionalItems.SaveImage(userId, ipAddress, transactionalItemId, transactItemImage);

            if (response.IsSuccess)
            {
                return Ok(response.transactItemImage);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Insert / Update a Transactional Item Production Step
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemProcessStep"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveProductionSpecs")]
        public async Task<IActionResult> SaveProductionSpecs(string userId, string ipAddress, string transactionalItemId, TransactionalItemProcessStep transactionalItemProcessStep)
        {
            var response = await _transactionalItems.SaveProductionSpecs(userId, ipAddress, transactionalItemId, transactionalItemProcessStep);

            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemProcessStep);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Insert / Update a Transactional Item Quality Parameter
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemQualityPair"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveQualityParameters")]
        public async Task<IActionResult> SaveQualityParameters(string userId, string ipAddress, string transactionalItemId, TransactionalItemQualityPair transactionalItemQualityPair)
        {
            var response = await _transactionalItems.SaveQualityParameters(userId, ipAddress, transactionalItemId, transactionalItemQualityPair);

            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemQualityPair);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Insert / Update a Transactional Packing Instructions
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="packingSpecs"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveProductPackingSpecs")]
        public async Task<IActionResult> SaveProductPackingSpecs(string userId, string ipAddress, string transactionalItemId, PackingSpecs packingSpecs)
        {
            var response = await _transactionalItems.SaveProductPackingSpecs(userId, ipAddress, transactionalItemId, packingSpecs);

            if (response.IsSuccess)
            {
                return Ok(response.packingSpecs);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Insert / Update a Transactional Item Tag
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemTag"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTags")]
        public async Task<IActionResult> SaveTags(string userId, string ipAddress, string transactionalItemId, TransactionalItemTag transactionalItemTag)
        {

                
            var response = await _transactionalItems.SaveTags(userId, ipAddress, transactionalItemId, transactionalItemTag);

            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemTag);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Insert / Update a Transactional Item Type
        /// </summary>
        /// <param name="transactionalItemType"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTransactionalItemType")]
        public async Task<IActionResult> SaveTransactionalItemType(string userId, string ipAddress, TransactionalItemType transactionalItemType)
        {
            var response = await _transactionalItems.SaveTransactionalItemType(userId, ipAddress, transactionalItemType);

            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Insert / Update a Box Type
        /// </summary>
        /// <param name="box"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveBox")]
        public async Task<IActionResult> SaveBox(string userId, string ipAddress, Box box)
        {
            var response = await _transactionalItems.SaveBox(userId, ipAddress, box);

            if(response.IsSuccess)
            {
                return Ok(response.box);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Insert / Update a Business Season object
        /// </summary>
        /// <param name="seasonBusiness"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveSeason")]
        public async Task<IActionResult> SaveSeason(string userId, string ipAddress, SeasonBusiness seasonBusiness)
        {
            var response = await _transactionalItems.SaveSeason(userId, ipAddress, seasonBusiness);

            if (response.IsSuccess)
            {
                return Ok(response.seasonBusiness);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        /// <summary>
        /// Retrieves Characteristics of a Transactional Item
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemDetailsItemCharacteristics")]
        public async Task<IActionResult> GetTransactionalItemDetailsItemCharacteristics(string userId, string ipAddress, string transactionalItemId)
        {

            var response = await _transactionalItems.GetTransactionalItemDetailsOf<TransactionalItemCharacteristicPair>(userId, ipAddress, transactionalItemId, TransactionalItemDetailsSection.Characteristics);

            if (response.IsSuccess)
            {
                return Ok(response.TransactionalItemRelatedList);
            }
            else
                return NotFound(response.ErrorDescription);

        }

        /// <summary>
        /// Saves (INSERT/UPDATE) A transactional item model
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemId"></param>
        /// <param name="productModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTransactionalItemModels")]
        public async Task<IActionResult> SaveTransactionalItemModels(string userId, string ipAddress, string transactionalItemId, ProductModel productModel)
        {
            var response = await _transactionalItems.SaveTransactionalItemModels(userId, ipAddress, transactionalItemId, productModel);

            if (response.IsSuccess)
            {
                return Ok(response.productModel);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

    }
}
