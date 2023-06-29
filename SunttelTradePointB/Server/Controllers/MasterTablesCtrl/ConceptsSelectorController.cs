using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;

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
        /// <param name="roleIndex">1: Provider, 2: Customer, 3: Carrier, 4: Company, 5: User, 6: Employee</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListEntityActor")]
        public async Task<IActionResult> GetSelectorListEntityActor(string? filterString = null, BasicRolesFilter? roleIndex = null)
        {
                var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _selectorDatasource.GetSelectorListEntityActor(filterString,squadId, roleIndex);

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
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterCondition"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListEntityRoles")]
        public async Task<IActionResult> GetSelectorListEntityRoles(string? filterCondition = null, int? page = 1, int? perPage = 10)
        {

            var response = await _selectorDatasource.GetSelectorListEntityRoles(filterCondition, page, perPage);

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
        public async Task<IActionResult> GetSelectorListIdentificationTypes(string? filterName = null)
        {
            var response = await _selectorDatasource.GetSelectorListIdentificationTypes(filterName);

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


        /// <summary>
        /// Retrieves a list of EntitiyRelationship Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorEntitiyRelationshipTypes")]
        public async Task<IActionResult> GetSelectorEntitiyRelationshipTypes()
        {
            var response = await _selectorDatasource.GetSelectorEntitiyRelationshipTypes();

            if(response.IsSuccess)
            {
                return Ok(response.entitiyRelationshipTypes);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list of Pallet Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListPalletTypes")]
        public async Task<IActionResult> GetSelectorListPalletTypes(string? filterName = null)
        {
            var response = await _selectorDatasource.GetSelectorListPalletTypes(filterName);

            if (response.IsSuccess)
            {
                return Ok(response.palletTypes);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list of Entity Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListEntityTypes")]
        public async Task<IActionResult> GetSelectorListEntityTypes(string? filterName = null)
        {
            var response = await _selectorDatasource.GetSelectorListEntityTypes(filterName);

            if (response.IsSuccess)
            {
                return Ok(response.entityTypes);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves the posible values for an specific recipe modifier
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="modifierId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetProductRecipeQualityModifiersByModifierId")]
        public async Task<IActionResult> GetProductRecipeQualityModifiersByModifierId(string userId, string ipAddress, string modifierId)
        {
            var response = await _selectorDatasource.GetProductRecipeQualityModifiersByModifierId(userId, ipAddress, modifierId);

            if (response.IsSuccess)
            {
                return Ok(response.productRecipeQualityModifiers);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves thelist of products used as packing material
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListPackingMaterials")]
        public async Task<IActionResult> GetSelectorListPackingMaterials(string filterString)
        {
            var response = await _selectorDatasource.GetSelectorListPackingMaterials(filterString);

            if (response.IsSuccess)
            {
                return Ok(response.materialsList);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list of assembly types
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListAssemblyTypes")]
        public async Task<IActionResult> GetSelectorListAssemblyTypes(string filterString)
        {
              var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _selectorDatasource.GetSelectorListAssemblyTypes(filterString);

            if (response.IsSuccess)
            {
                return Ok(response.assemblyTypes);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        /// <summary>
        /// Retrives a selector list of label papers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSelectorListLabelPaper")]
        public async Task<IActionResult> GetSelectorListLabelPaper()
        {
            var response = await _selectorDatasource.GetSelectorListLabelPaper();

            if (response.IsSuccess)
            {
                return Ok(response.labelPapers) ;
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        /// <summary>
        /// Retrieves the list of Entity/Vendor filtered by the optional parameter
        /// </summary>
        /// <param name="isASale"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterString"></param>
        /// <param name="paginate"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetVendors")]
        public async Task<IActionResult> GetVendors(bool isASale, string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filterString = null, bool paginate = true)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _selectorDatasource.GetVendors(isASale, userId, ipAddress, squadId, page, perPage, filterString, paginate);

            if (response.IsSuccess)
            {
                return Ok(response.VendorsList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves the list of Entity/Vendor filtered by the optional parameter
        /// </summary>
        /// <param name="isASale"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterString"></param>
        /// <param name="paginate"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetBuyers")]
        public async Task<IActionResult> GetBuyers(bool isASale, string userId, string ipAddress, int? page = 1, int? perPage = 10, string? filterString = null,bool paginate = true)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];

            var response = await _selectorDatasource.GetBuyers(isASale, userId, ipAddress,squadId, page, perPage, filterString, paginate);

            if (response.IsSuccess)
            {
                return Ok(response.BuyersList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves the list of Entity/Vendor filtered by the optional parameter
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCarriers")]
        public async Task<IActionResult> GetCarriers(string filterString)
        {
              var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _selectorDatasource.GetSelectorListEntityActor(filterString,squadId, BasicRolesFilter.Carrier);

            if (response.IsSuccess)
            {
                return Ok(response.EntityActorList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves the list of Entity/Vendor filtered by the optional parameter
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetSalesPersons")]
        public async Task<IActionResult> GetSalesPersons(string filterString)
        {
              var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString() ?? ""; // Request.Headers["SquadId"];
            var response = await _selectorDatasource.GetSelectorListEntityActor(filterString,squadId, BasicRolesFilter.SalesPerson);

            if (response.IsSuccess)
            {
                return Ok(response.EntityActorList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

    }
}
