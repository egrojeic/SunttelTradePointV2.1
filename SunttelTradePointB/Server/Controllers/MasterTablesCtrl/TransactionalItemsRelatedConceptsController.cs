using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Server.Controllers.MasterTablesCtrl
{
    /// <summary>
    /// Controller to deal with related concepts to Transactional Items
    /// </summary>
    [Tags("Transactional Items Related Concepts")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionalItemsRelatedConceptsController : ControllerBase
    {

        private ITransactionalItemsRelatedConceptsBKService _transactionalItemsRelatedConcepts;
        private readonly ILogger<TransactionalItemsRelatedConceptsController> _logger;
        IConfiguration config;


        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="_config"></param>
        /// <param name="transactionalItemsRelatedConcepts"></param>
        public TransactionalItemsRelatedConceptsController(ILogger<TransactionalItemsRelatedConceptsController> logger, IConfiguration _config, ITransactionalItemsRelatedConceptsBKService transactionalItemsRelatedConcepts)
        {
            _logger = logger;
            config = _config;
            _transactionalItemsRelatedConcepts  = transactionalItemsRelatedConcepts;
        }


        /// <summary>
        /// Retrieves a box object
        /// </summary>
        /// <param name="boxID"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetBox")]
        public async Task<IActionResult> GetBox(string userId, string ipAddress, string boxID)
        {

            var response = await _transactionalItemsRelatedConcepts.GetBox(userId, ipAddress, boxID);

            if (response.IsSuccess)
            {
                return Ok(response.box);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrieves a list of Boxes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetBoxTable")]
        public async Task<IActionResult> GetBoxTable()
        {
            var response = await _transactionalItemsRelatedConcepts.GetBoxTable();

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
        /// Retrieves a SeasonBusiness object
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSeason")]
        public async Task<IActionResult> GetSeason(string userId, string ipAddress, string seasonId)
        {
            var response = await _transactionalItemsRelatedConcepts.GetSeason(userId, ipAddress, seasonId);

            if (response.IsSuccess)
            {
                return Ok(response.season);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list of Season Business
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSeasonsTable")]
        public async Task<IActionResult> GetSeasonsTable()
        {
            var response = await _transactionalItemsRelatedConcepts.GetSeasonsTable();

            if (response.IsSuccess)
            {
                return Ok(response.seasons);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a TransactionalItemType object
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemType")]
        public async Task<IActionResult> GetTransactionalItemType(string userId, string ipAddress, string transactionalItemId)
        {
            var response = await _transactionalItemsRelatedConcepts.GetTransactionalItemType(userId, ipAddress, transactionalItemId);

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
        /// Retrieves a list of Transactional Statuses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalStatusesTable")]
        public async Task<IActionResult> GetTransactionalStatusesTable()
        {
            var response = await _transactionalItemsRelatedConcepts.GetTransactionalStatusesTable();
            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemStatuses);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list of Transactional Item Groupd with a specified condition
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemGroups")]
        public async Task<IActionResult> GetTransactionalItemGroups(string userId, string ipAddress, string filterCondition)
        {
            var response = await _transactionalItemsRelatedConcepts.GetTransactionalItemGroups(userId, ipAddress, filterCondition);
            if(response.IsSuccess)
            {
                return Ok(response.transactionalItemGroups);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a particular TransactionalItemGroup
        /// </summary>
        /// <param name="transactionalItemGroupId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemGroup")]
        public async Task<IActionResult> GetTransactionalItemGroup(string userId, string ipAddress, string transactionalItemGroupId)
        {
            var response = await _transactionalItemsRelatedConcepts.GetTransactionalItemGroup(userId, ipAddress, transactionalItemGroupId);
            if(response.IsSuccess)
            {
                return Ok(response.transactionalItemGroup);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a particular assembly type by its ID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="assemblyTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAssemblyTypeByID")]
        public async Task<IActionResult> GetAssemblyTypeByID(string userId, string ipAddress, string assemblyTypeId)
        {
            var response = await _transactionalItemsRelatedConcepts.GetAssemblyTypeByID(userId, ipAddress, assemblyTypeId);
            if (response.IsSuccess)
            {
                return Ok(response.assemblyType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Saves a Box document. If it doesn't exists, it will be created
        /// </summary>
        /// <param name="box"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveBox")]
        public async Task<IActionResult> SaveBox(string userId, string ipAddress, Box box)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveBox(userId, ipAddress, box);
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
        /// Saves a SeasonBusiness document. If it doesn't exists, it will be created
        /// </summary>
        /// <param name="season"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveSeason")]
        public async Task<IActionResult> SaveSeason(string userId, string ipAddress, SeasonBusiness season)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveSeason(userId, ipAddress, season);
            if (response.IsSuccess)
            {
                return Ok(response.season);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Saves a TransactionalItemStatus document. If it doesn't exists, it will be created
        /// </summary>
        /// <param name="transactionalItemStatus"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveStatus")]
        public async Task<IActionResult> SaveStatus(string userId, string ipAddress, TransactionalItemStatus transactionalItemStatus)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveStatus(userId, ipAddress, transactionalItemStatus);
            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemStatus);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Saves a TransactionalItemType document. If it doesn't exists, it will be created
        /// </summary>
        /// <param name="transactionalItemType"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTransactionalItemType")]
        public async Task<IActionResult> SaveTransactionalItemType(string userId, string ipAddress, TransactionalItemType transactionalItemType)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveTransactionalItemType(userId, ipAddress, transactionalItemType);
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
        /// Insert / Updates Transactional Item Group
        /// </summary>
        /// <param name="transactionalItemGroup"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTransactionalItemGroup")]
        public async Task<IActionResult> SaveTransactionalItemGroup(string userId, string ipAddress, ConceptGroup transactionalItemGroup)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveTransactionalItemGroup(userId, ipAddress, transactionalItemGroup);
            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemGroup);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Saves (INSERT/UPDATE) a Transactional Item Process Step
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="transactionalItemProcessStep"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTransactionalItemProcessStep")]
        public async Task<IActionResult> SaveTransactionalItemProcessStep(string userId, string ipAddress, string transactionalItemTypeId, TransactionalItemProcessStep transactionalItemProcessStep)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveTransactionalItemProcessStep(userId, ipAddress, transactionalItemTypeId, transactionalItemProcessStep);
            if(response.IsSuccess)
            {
                return Ok(response.transactionalItemProcessStep);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Saves (INSERT/UPDATE) a Transactional Item Type Characteristic
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="transactionalItemTypeCharacteristic"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTransactionalItemTypeCharacteristic")]
        public async Task<IActionResult> SaveTransactionalItemTypeCharacteristic(string userId, string ipAddress, string transactionalItemTypeId, TransactionalItemTypeCharacteristic transactionalItemTypeCharacteristic)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveTransactionalItemTypeCharacteristic(userId, ipAddress, transactionalItemTypeId, transactionalItemTypeCharacteristic);
            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemTypeCharacteristic);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Saves (INSERT/UPDATE) a Transactional Item Quality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="transactionalItemQuality"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTransactionalItemQuality")]
        public async Task<IActionResult> SaveTransactionalItemQuality(string userId, string ipAddress, string transactionalItemTypeId, TransactionalItemQuality transactionalItemQuality)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveTransactionalItemQuality(userId, ipAddress, transactionalItemTypeId, transactionalItemQuality);
            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemQuality);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Saves (INSERT/UPDATE) a Recipe Modifier
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <param name="recipeModifier"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveRecipeModifier")]
        public async Task<IActionResult> SaveRecipeModifier(string userId, string ipAddress, string transactionalItemTypeId, RecipeModifier recipeModifier)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveRecipeModifier(userId, ipAddress, transactionalItemTypeId, recipeModifier);
            if (response.IsSuccess)
            {
                return Ok(response.recipeModifier);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Saves an assembly type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="assemblyType"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveAssemblyType")]
        public async Task<IActionResult> SaveAssemblyType(string userId, string ipAddress, AssemblyType assemblyType)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveAssemblyType(userId, ipAddress, assemblyType);
            if (response.IsSuccess)
            {
                return Ok(response.assemblyType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }
    }
}
