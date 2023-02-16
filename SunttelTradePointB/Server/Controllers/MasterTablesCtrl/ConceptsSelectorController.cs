﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;

namespace SunttelTradePointB.Server.Controllers.MasterTablesCtrl
{

    /// <summary>
    /// Outlets to get Concept Lists to be consumed for Selectors
    /// </summary>
    [Tags("Outlets to get Concept Lists to be consumed for Selectors")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConceptsSelectorController : ControllerBase
    {
        private ISelectorDataSource _selectorDatasource;
        private readonly ILogger<ConceptsSelectorController> _logger;
        IConfiguration config;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="_config"></param>
        /// <param name="selectorDataSource"></param>
        public ConceptsSelectorController(ILogger<ConceptsSelectorController> logger, IConfiguration _config, ISelectorDataSource selectorDataSource)
        {
            _logger = logger;
            config = _config;
            _selectorDatasource = selectorDataSource;
        }


        /// <summary>
        /// Retrieves the list of Entity/Nodes/Actors filtered by the optional parameter
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListEntityActor")]
        public async Task<IActionResult> GetSelectorListEntityActor(string? filterString = null)
        {

            var response = await _selectorDatasource.GetSelectorListEntityActor(filterString);

            if (response.IsSuccess)
            {
                return Ok(response.EntityActorList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves a list the list of addreses of an Entity
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListEntityAddress")]
        public async Task<IActionResult> GetSelectorListEntityAddress(string entityId)
        {

            var response = await _selectorDatasource.GetSelectorListEntityAddress(entityId);

            if (response.IsSuccess)
            {
                return Ok(response.EntityActorAddressList);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrieves Entity Groups matching a filter pattern
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListEntityGroups")]
        public async Task<IActionResult> GetSelectorListEntityGroups(string filterCondition)
        {

            var response = await _selectorDatasource.GetSelectorListEntityGroups(filterCondition);

            if (response.IsSuccess)
            {
                return Ok(response.EntityGroupsList);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrives the list of possible Entity Roles
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListEntityRoles")]
        public async Task<IActionResult> GetSelectorListEntityRoles(string filterCondition)
        {

            var response = await _selectorDatasource.GetSelectorListEntityRoles(filterCondition);

            if (response.IsSuccess)
            {
                return Ok(response.EntityRolesList);
            }
            else
                return NotFound(response.ErrorDescription);

        }

        /// <summary>
        /// Retrives the list of seasosns
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListSeasonBusiness")]
        public async Task<IActionResult> GetSelectorListSeasonBusiness()
        {

            var response = await _selectorDatasource.GetSelectorListSeasonBusiness();

            if (response.IsSuccess)
            {
                return Ok(response.SeasonsList);
            }
            else
                return NotFound(response.ErrorDescription);

        }

        /// <summary>
        /// Retrieves the list of Transactional Items groups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListTransactionalItemGroups")]
        public async Task<IActionResult> GetSelectorListTransactionalItemGroups(string filterCondition)
        {

            var response = await _selectorDatasource.GetSelectorListTransactionalItemGroups(filterCondition);

            if (response.IsSuccess)
            {
                return Ok(response.TransactionalItemGroupsList);
            }
            else
                return NotFound(response.ErrorDescription);

        }


        /// <summary>
        /// Retrieves a list of Transactional Item Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListTransactionalItemTypes")]
        public async Task<IActionResult> GetSelectorListTransactionalItemTypes()
        {

            var response = await _selectorDatasource.GetSelectorListTransactionalItemTypes();

            if (response.IsSuccess)
            {
                return Ok(response.TransactionalItemTypesList);
            }
            else
                return NotFound(response.ErrorDescription);

        }

        /// <summary>
        /// Retrieves a list of Boxes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListBoxes")]
        public async Task<IActionResult> GetSelectorListBoxes()
        {

            var response = await _selectorDatasource.GetSelectorListBoxes();

            if (response.IsSuccess)
            {
                return Ok(response.boxes);
            }
            else
                return NotFound(response.ErrorDescription);

        }

        /// <summary>
        /// Retrieves a the list of the different models are already registered for a transactional item
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListTransactionalItemModels")]
        public async Task<IActionResult> GetSelectorListTransactionalItemModels(string transactionalItemId)
        {

            var response = await _selectorDatasource.GetSelectorListTransactionalItemModels(transactionalItemId);

            if (response.IsSuccess)
            {
                return Ok(response.transactionalItemModels);
            }
            else
                return NotFound(response.ErrorDescription);

        }


        /// <summary>
        /// Returns a list of the different Identification Types in the system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListIdentificationTypes")]
        public async Task<IActionResult> GetSelectorListIdentificationTypes()
        {
            var response = await _selectorDatasource.GetSelectorListIdentificationTypes();

            if (response.IsSuccess)
            {
                return Ok(response.identificationTypes);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrives Id, and name of electronic Address which group is Network Platform
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorElectronicAddressEntities")]
        public async Task<IActionResult> GetSelectorElectronicAddressEntities()
        {
            var response = await _selectorDatasource.GetSelectorElectronicAddressEntities();

            if (response.IsSuccess)
            {
                return Ok(response.ElectronicAddressEntities);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }
    }
}
