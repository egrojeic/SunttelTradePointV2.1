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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetBox")]
        public async Task<IActionResult> GetBox(string boxID)
        {

            var response = await _transactionalItemsRelatedConcepts.GetBox(boxID);

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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSeason")]
        public async Task<IActionResult> GetSeason(string seasonId)
        {
            var response = await _transactionalItemsRelatedConcepts.GetSeason(seasonId);

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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemType")]
        public async Task<IActionResult> GetTransactionalItemType(string transactionalItemId)
        {
            var response = await _transactionalItemsRelatedConcepts.GetTransactionalItemType(transactionalItemId);

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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemGroups")]
        public async Task<IActionResult> GetTransactionalItemGroups(string filterCondition)
        {
            var response = await _transactionalItemsRelatedConcepts.GetTransactionalItemGroups(filterCondition);
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
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTransactionalItemGroup")]
        public async Task<IActionResult> GetTransactionalItemGroup(string transactionalItemGroupId)
        {
            var response = await _transactionalItemsRelatedConcepts.GetTransactionalItemGroup(transactionalItemGroupId);
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
        /// Saves a Box document. If it doesn't exists, it will be created
        /// </summary>
        /// <param name="boxId"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveBox")]
        public async Task<IActionResult> SaveBox(string? boxId, Box box)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveBox(box);
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
        /// <param name="seasonId"></param>
        /// <param name="season"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveSeason")]
        public async Task<IActionResult> SaveSeason(string? seasonId, SeasonBusiness season)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveSeason(season);
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
        /// <param name="statusId"></param>
        /// <param name="transactionalItemStatus"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveStatus")]
        public async Task<IActionResult> SaveStatus(string? statusId, TransactionalItemStatus transactionalItemStatus)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveStatus(transactionalItemStatus);
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
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemType"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTransactionalItemType")]
        public async Task<IActionResult> SaveTransactionalItemType(string? transactionalItemId, TransactionalItemType transactionalItemType)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveTransactionalItemType(transactionalItemType);
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
        /// <param name="transactionalItemGroupId"></param>
        /// <param name="transactionalItemGroup"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveTransactionalItemGroup")]
        public async Task<IActionResult> SaveTransactionalItemGroup(string? transactionalItemGroupId, ConceptGroup transactionalItemGroup)
        {
            var response = await _transactionalItemsRelatedConcepts.SaveTransactionalItemGroup(transactionalItemGroup);
            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemGroup);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }
    }
}
