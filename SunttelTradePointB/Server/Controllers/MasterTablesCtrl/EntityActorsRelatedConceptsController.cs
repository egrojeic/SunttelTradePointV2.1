using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Server.Controllers.MasterTablesCtrl
{
    /// <summary>
    /// Controller to deal with related concepts to Entity Actors
    /// </summary>
    [Tags("Entity Actors Related Concepts")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EntityActorsRelatedConceptsController : ControllerBase
    {

        private IEntitiesRelatedConcepts _entitiesRelatedConcepts;
        private readonly ILogger<EntityActorsRelatedConceptsController> _logger;
        IConfiguration config;


        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="_config"></param>
        /// <param name="entitiesRelatedConcepts"></param>
        public EntityActorsRelatedConceptsController(ILogger<EntityActorsRelatedConceptsController> logger, IConfiguration _config, IEntitiesRelatedConcepts entitiesRelatedConcepts)
        {
            _logger = logger;
            config = _config;
            _entitiesRelatedConcepts = entitiesRelatedConcepts;
        }


        /// <summary>
        ///  Retrieves a Entity Role Object
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityRoleId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetEntityRole")]
        public async Task<IActionResult> GetEntityRole(string userId, string ipAdress, string entityRoleId)
        {
            var response = await _entitiesRelatedConcepts.GetEntityRole(userId, ipAdress, entityRoleId);

            if (response.IsSuccess)
            {
                return Ok(response.entityRole);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list of Entity Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetEntityRoles")]
        public async Task<IActionResult> GetEntityRoles()
        {
            var response = await _entitiesRelatedConcepts.GetEntityRoles();

            if (response.IsSuccess)
            {
                return Ok(response.entityRoles);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a Entity Type object
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetEntityType")]
        public async Task<IActionResult> GetEntityType(string userId, string ipAdress, string entityTypeId)
        {
            var response = await _entitiesRelatedConcepts.GetEntityType(userId, ipAdress, entityTypeId);

            if (response.IsSuccess)
            {
                return Ok(response.entityType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves an Identification Type object
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="identicationTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetIdentificationType")]
        public async Task<IActionResult> GetIdentificationType(string userId, string ipAdress, string identicationTypeId)
        {
            var response = await _entitiesRelatedConcepts.GetIdentificationType(userId, ipAdress, identicationTypeId);

            if (response.IsSuccess)
            {
                return Ok(response.identificationType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list of Identification Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetIdentificationTypes")]
        public async Task<IActionResult> GetIdentificationTypes()
        {
            var response = await _entitiesRelatedConcepts.GetIdentificationTypes();

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
        /// Retrieves a Pallet Type by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="palletTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetPalletType")]
        public async Task<IActionResult> GetPalletType(string userId, string ipAdress, string palletTypeId)
        {
            var response = await _entitiesRelatedConcepts.GetPalletType(userId, ipAdress, palletTypeId);

            if (response.IsSuccess)
            {
                return Ok(response.palletType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Saves an Entity Role document. If it doesn't exists, it will be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityRole"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveEntityRole")]
        public async Task<IActionResult> SaveEntityRole(string userId, string ipAdress, EntityRole entityRole)
        {
            var response = await _entitiesRelatedConcepts.SaveEntityRole(userId, ipAdress, entityRole);

            if (response.IsSuccess)
            {
                return Ok(response.entityRole);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Saves an Entity Type document. If it doesn't exists, it will be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveEntityType")]
        public async Task<IActionResult> SaveEntityType(string userId, string ipAdress, EntityType entityType)
        {
            var response = await _entitiesRelatedConcepts.SaveEntityType(userId, ipAdress, entityType);

            if (response.IsSuccess)
            {
                return Ok(response.entityType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Saves an Identification Type document. If it doesn't exists, it will be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="identificationType"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveIdentificationType")]
        public async Task<IActionResult> SaveIdentificationType(string userId, string ipAdress, IdentificationType identificationType)
        {
            var response = await _entitiesRelatedConcepts.SaveIdentificationType(userId, ipAdress, identificationType);

            if (response.IsSuccess)
            {
                return Ok(response.identificationType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Insert / Update a Pallet Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="palletType"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SavePalletType")]
        public async Task<IActionResult> SavePalletType(string userId, string ipAdress, PalletType palletType)
        {
            var response = await _entitiesRelatedConcepts.SavePalletType(userId, ipAdress, palletType);

            if (response.IsSuccess)
            {
                return Ok(response.palletType);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }
    }
}
