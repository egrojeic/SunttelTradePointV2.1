using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItems")]
        public async Task<IActionResult> GetTransactionalItems(int? page = 1, int? perPage = 10, string? filterName = null)
        {

            var response = await _transactionalItems.GetTransactionItemList(page, perPage, filterName);

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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemDetailsPackingRecipe")]
        public async Task<IActionResult> GetTransactionalItemDetailsPackingRecipe(string transactionalItemId)
        {

            //var response = await _entityNodes.GetEntityActorAddressList(EntityId);
            var response = await _transactionalItems.GetTransactionalItemDetailsOf<PackingSpecs>(transactionalItemId, TransactionalItemDetailsSection.PackingRecipe);

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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemDetailsProductionSpecs")]
        public async Task<IActionResult> GetTransactionalItemDetailsProductionSpecs(string transactionalItemId)
        {

            var response = await _transactionalItems.GetTransactionalItemDetailsOf<TransactionalItemProcessStep>(transactionalItemId, TransactionalItemDetailsSection.ProductionSpecs);

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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemDetailsQualityParameters")]
        public async Task<IActionResult> GetTransactionalItemDetailsQualityParameters(string transactionalItemId)
        {

            var response = await _transactionalItems.GetTransactionalItemDetailsOf<TransactionalItemQualityPair>(transactionalItemId, TransactionalItemDetailsSection.QualityParameters);

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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemDetailsPathImages")]
        public async Task<IActionResult> GetTransactionalItemDetailsPathImages(string transactionalItemId)
        {
            var response = await _transactionalItems.GetTransactionalItemDetailsOf<TransactItemImage>(transactionalItemId, TransactionalItemDetailsSection.PathImages);

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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemDetailsTags")]
        public async Task<IActionResult> GetTransactionalItemDetailsTags(string transactionalItemId)
        {
            try
            {
                var response = await _transactionalItems.GetTransactionalItemDetailsOf<TransactionalItemTag>(transactionalItemId, TransactionalItemDetailsSection.Tags);
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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemById")]
        public async Task<IActionResult> GetTransactionalItemById(string transactionalItemId)
        {
            var response = await _transactionalItems.GetTransactionalItemById(transactionalItemId);

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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemType")]
        public async Task<IActionResult> GetTransactionalItemType(string transactionalItemTypeId)
        {
            var response = await _transactionalItems.GetTransactionalItemType(transactionalItemTypeId);

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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemTypes")]
        public async Task<IActionResult> GetTransactionalItemTypes(string? filterCondition = null)
        {
            var response = await _transactionalItems.GetTransactionalItemTypes(filterCondition);

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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetBoxeById")]
        public async Task<IActionResult> GetBoxeById(string boxId)
        {
            var response = await _transactionalItems.GetBoxeById(boxId);

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
        public async Task<IActionResult> GetBoxes(string? filterCondition = null)
        {
            var response = await _transactionalItems.GetBoxes(filterCondition);

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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSeason")]
        public async Task<IActionResult> GetSeason(string seasonId)
        {
            var response = await _transactionalItems.GetSeason(seasonId);

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
        public async Task<IActionResult> GetSeasons(string? filterCondition = null)
        {
            var response = await _transactionalItems.GetSeasons(filterCondition);

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
        /// Updates/ Insert a new Transactional Item
        /// </summary>
        /// <param name="transactionalItem"></param>
        /// <param name="transactionalItemId"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTransactionalItem")]
        public async Task<IActionResult> SaveTransactionalItem(TransactionalItem transactionalItem, string transactionalItemId)
        {
            var response = await _transactionalItems.SaveTransactionalItem(transactionalItem, transactionalItemId);

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
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveCharacteristics")]
        public async Task<IActionResult> SaveCharacteristics(string transactionalItemId, TransactionalItemCharacteristicPair transactionalItemCharacteristicPair)
        {
            var response = await _transactionalItems.SaveCharacteristics(transactionalItemId, transactionalItemCharacteristicPair);

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
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveImage")]
        public async Task<IActionResult> SaveImage(string transactionalItemId, TransactItemImage transactItemImage)
        {
            var response = await _transactionalItems.SaveImage(transactionalItemId, transactItemImage);

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
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveProductionSpecs")]
        public async Task<IActionResult> SaveProductionSpecs(string transactionalItemId, TransactionalItemProcessStep transactionalItemProcessStep)
        {
            var response = await _transactionalItems.SaveProductionSpecs(transactionalItemId, transactionalItemProcessStep);

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
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveQualityParameters")]
        public async Task<IActionResult> SaveQualityParameters(string transactionalItemId, TransactionalItemQualityPair transactionalItemQualityPair)
        {
            var response = await _transactionalItems.SaveQualityParameters(transactionalItemId, transactionalItemQualityPair);

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
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveProductPackingSpecs")]
        public async Task<IActionResult> SaveProductPackingSpecs(string transactionalItemId, PackingSpecs packingSpecs)
        {
            var response = await _transactionalItems.SaveProductPackingSpecs(transactionalItemId, packingSpecs);

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
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTags")]
        public async Task<IActionResult> SaveTags(string transactionalItemId, TransactionalItemTag transactionalItemTag)
        {

                
            var response = await _transactionalItems.SaveTags(transactionalItemId, transactionalItemTag);

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
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="transactionalItemType"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTransactionalItemType")]
        public async Task<IActionResult> SaveTransactionalItemType(string? transactionalItemTypeId, TransactionalItemType transactionalItemType)
        {
            var response = await _transactionalItems.SaveTransactionalItemType(transactionalItemTypeId, transactionalItemType);

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
        /// <param name="boxId"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveBox")]
        public async Task<IActionResult> SaveBox(string? boxId, Box box)
        {
            var response = await _transactionalItems.SaveBox(boxId, box);

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
        /// <param name="seasonId"></param>
        /// <param name="seasonBusiness"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveSeason")]
        public async Task<IActionResult> SaveSeason(string? seasonId, SeasonBusiness seasonBusiness)
        {
            var response = await _transactionalItems.SaveSeason(seasonId, seasonBusiness);

            if (response.IsSuccess)
            {
                return Ok(response.seasonBusiness);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

    }
}
