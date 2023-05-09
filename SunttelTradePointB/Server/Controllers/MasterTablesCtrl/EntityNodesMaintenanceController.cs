using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.Common;
using System.Globalization;

namespace SunttelTradePointB.Server.Controllers.MasterTablesCtrl
{

    /// <summary>
    /// Author: Jorge Isaza
    /// Description: Controller intended to manage CRUD operations on Entity Nodes Table Maintenance
    /// </summary>
    [Tags("Entity Nodes Table Maintenance")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EntityNodesMaintenanceController : ControllerBase
    {

        private IActorsNodes _entityNodes;
        private readonly ILogger<EntityNodesMaintenanceController> _logger;
        IConfiguration config;

        /// <summary>
        /// Constructor Class 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="_config"></param>
        /// <param name="entityNodes"></param>
        public EntityNodesMaintenanceController(ILogger<EntityNodesMaintenanceController> logger, IConfiguration _config, IActorsNodes entityNodes)
        {
            _logger = logger;
            config = _config;
            _entityNodes = entityNodes;
        }


        /// <summary>
        /// Returns a list of entities with a name like the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetEntityNodes")]
        public async Task<IActionResult> GetEntityNodes(string userId, string ipAdress, string? filterName = null)
        {

            var response = await _entityNodes.GetEntityActorFaceList(userId, ipAdress, filterName);

            if (response.IsSuccess)
            {
                return Ok(response.ActorsNodesList);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrieves the list of shipping address of an Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="EntityId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetEntityDetailsAddressList")]
        public async Task<IActionResult> GetEntityDetailsAddressList(string userId, string ipAdress, string EntityId) {

           
            var response = await _entityNodes.GetEntityDetailsOf<Address>(userId, ipAdress, EntityId,  EntityDetailsSection.AddressList);

            if (response.IsSuccess)
            {
                return Ok(response.EntityRelatedList);
            }
            else
                return NotFound(response.ErrorDescription);

        }


        /// <summary>
        /// Retrieves the phone numbers list
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="EntityId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetEntityDetailsPhoneDirectory")]
        public async Task<IActionResult> GetEntityDetailsPhoneDirectory(string userId, string ipAdress, string EntityId)
        {
            var response = await _entityNodes.GetEntityDetailsOf<PhoneNumber>(userId, ipAdress, EntityId,  EntityDetailsSection.PhoneDirectory);

            if (response.IsSuccess)
            {
                return Ok(response.EntityRelatedList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves Identifier list of an Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="EntityId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetEntityDetailsIdentifiersList")]
        public async Task<IActionResult> GetEntityDetailsIdentifiersList(string userId, string ipAdress, string EntityId)
        {
            var response = await _entityNodes.GetEntityDetailsOf<IdentificationEntity>(userId, ipAdress, EntityId, EntityDetailsSection.IdentifiersList);

            if (response.IsSuccess)
            {
                return Ok(response.EntityRelatedList);
            }
            else
                return NotFound(response.ErrorDescription);
        }



        /// <summary>
        /// Retrives the possible roles for Entities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("EntityRolesListSelector")]
        public async Task<IActionResult> EntityRolesListSelector()
        {
            var response = await _entityNodes.EntityRolesListSelector();

            if (response.IsSuccess)
            {
                return Ok(response.rolesList);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrieves an Entity/Actor object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetEntityActorById")]
        public async Task<IActionResult> GetEntityActorById(string userId, string ipAdress, string entityActorId)
        {
            var response = await _entityNodes.GetEntityActorById(userId, ipAdress, entityActorId);

            if (response.IsSuccess)
            {
                return Ok(response.entityActorResponse);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Retrieves an Entity Group object by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetEntityGroup")]
        public async Task<IActionResult> GetEntityGroup(string userId, string ipAdress, string entityGroupId)
        {
            var response = await _entityNodes.GetEntityGroup(userId, ipAdress, entityGroupId);

            if(response.IsSuccess)
            {
                return Ok(response.entityGroup);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a list of Entity Groups
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filterCondition"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetEntityGroups")]
        public async Task<IActionResult> GetEntityGroups(string userId, string ipAdress, int? page = 1, int? perPage = 10, string? filterCondition = null)
        {
            var result = await _entityNodes.GetEntityGroups(userId, ipAdress, page, perPage, filterCondition);

            if (result.IsSuccess)
            {
                return Ok(result.entityGroup);
            }
            else
            {
                return NotFound(result.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrives the list of electronic addresses of an Actor
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetElectronicAddresses")]
        public async Task<IActionResult> GetElectronicAddresses(string userId, string ipAdress, string entityActorId)
        {
            var result = await _entityNodes.GetElectronicAddresses(userId, ipAdress, entityActorId);

            if(result.IsSuccess)
            {
                return Ok(result.electronicAddresses);
            }
            else
            {
                return NotFound(result.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves the relation of all Shipping info records related with an Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetShippingSetup")]
        public async Task<IActionResult> GetShippingSetup(string userId, string ipAdress, string entityActorId)
        {
            var result = await _entityNodes.GetShippingSetup(userId, ipAdress, entityActorId);

            if (result.IsSuccess)
            {
                return Ok(result.shippingInfos);
            }
            else
            {
                return NotFound(result.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves the list of the different commercial conditions for an Entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCommercialConditiosOfEntity")]
        public async Task<IActionResult> GetCommercialConditiosOfEntity(string userId, string ipAdress, string entityActorId)
        {
            var result = await _entityNodes.GetCommercialConditiosOfEntity(userId, ipAdress, entityActorId);

            if (result.IsSuccess)
            {
                return Ok(result.entitiesCommercialRelationShips);
            }
            else
            {
                return NotFound(result.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves an Electronic Address filtered by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="electronicAddressId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetElectronicAddressById")]
        public async Task<IActionResult> GetElectronicAddressById(string userId, string ipAdress, string electronicAddressId)
        {
            var result = await _entityNodes.GetElectronicAddressById(userId, ipAdress, electronicAddressId);

            if(result.IsSuccess)
            {
                return Ok(result.electronicAddress);
            }
            else
            {
                return NotFound(result.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves Shipping Information filtered by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="shippingInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetShippingSetupById")]
        public async Task<IActionResult> GetShippingSetupById(string userId, string ipAdress, string shippingInfoId)
        {
            var result = await _entityNodes.GetShippingSetupById(userId, ipAdress, shippingInfoId);

            if(result.IsSuccess)
            {
                return Ok(result.shippingInfo);
            }
            else
            {
                return NotFound(result.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves Entities Relationships filtered by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entitiesCommercialRelationShipId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetEntitiesCommercialRelationShipById")]
        public async Task<IActionResult> GetEntitiesCommercialRelationShipById(string userId, string ipAdress, string entitiesCommercialRelationShipId)
        {
            var result = await _entityNodes.GetEntitiesCommercialRelationShipById(userId, ipAdress, entitiesCommercialRelationShipId);

            if(result.IsSuccess)
            {
                return Ok(result.entitiesCommercialRelationShip);
            }
            else
            {
                return NotFound(result.ErrorDescription);
            }
        }


        /// <summary>
        /// Saves an Entity/Actor document. If it doesn't exists, it'll be created
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entity"></param>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveEntity")]
        public async Task<IActionResult> SaveEntity(string userId, string ipAdress, EntityActor entity)
        {
            var response = await _entityNodes.SaveEntity(userId, ipAdress, entity);

            if (response.IsSuccess)
            {
                return Ok(response.entityActorResponse);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an address of an entity. If it exists, it'll be updated, otherwise it 'll be inserted in the array
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveEntityAddress")]
        public async Task<IActionResult> SaveEntityAddress(string userId, string ipAdress, string entityActorId, Address address)
        {
            var response = await _entityNodes.SaveEntityAddress(userId, ipAdress, entityActorId, address);

            if (response.IsSuccess)
            {
                return Ok(response.entityAddress);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves a phone number of an entity. If it exists, it'll be updated, otherwise it 'll be inserted in the array
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SavePhone")]
        public async Task<IActionResult> SavePhone(string userId, string ipAdress, string entityActorId, PhoneNumber phoneNumber)
        {
            var response = await _entityNodes.SavePhone(userId, ipAdress, entityActorId, phoneNumber);

            if (response.IsSuccess)
            {
                return Ok(response.phoneNumber);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        /// Saves an Identification Code of an entity. If it exists, it'll be updated, otherwise it 'll be inserted in the array
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="identificationEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveIdentificationCode")]
        public async Task<IActionResult> SaveIdentificationCode(string userId, string ipAdress, string entityActorId, IdentificationEntity identificationEntity)
        {
            var response = await _entityNodes.SaveIdentificationCode(userId, ipAdress, entityActorId, identificationEntity);

            if (response.IsSuccess)
            {
                return Ok(response.identificationEntity);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Insert / Update an Entity Group
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityGroup"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveEntityGroup")]
        public async Task<IActionResult> SaveEntityGroup(string userId, string ipAdress, EntityGroup entityGroup)
        {
            var response = await _entityNodes.SaveEntityGroup(userId, ipAdress, entityGroup);

            if(response.IsSuccess)
            {
                return Ok(response.entityGroup);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Inserts / Updates Electronic Address in an Entity Actor
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="electronicAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveElectronicAddress")]
        public async Task<IActionResult> SaveElectronicAddress(string userId, string ipAdress, string entityActorId, ElectronicAddress electronicAddress)
        {
            var response = await _entityNodes.SaveElectronicAddress(userId, ipAdress, entityActorId, electronicAddress);

            if(response.IsSuccess)
            {
                return Ok(response.electronicAddress);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Inserts / Updates Shipping Setup in an Entity Actor
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="shippingInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveShippingSetup")]
        public async Task<IActionResult> SaveShippingSetup(string userId, string ipAdress, string entityActorId, ShippingInfo shippingInfo)
        {
            var response = await _entityNodes.SaveShippingSetup(userId, ipAdress, entityActorId, shippingInfo);

            if (response.IsSuccess)
            {
                return Ok(response.shippingInfo);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Inserts / Updates Commercial Conditions in an Entity Actor
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="entitiesCommercialRelationShip"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveCommercialConditions")]
        public async Task<IActionResult> SaveCommercialConditions(string userId, string ipAdress, string entityActorId, EntitiesCommercialRelationShip entitiesCommercialRelationShip)
        {
            var response = await _entityNodes.SaveCommercialConditions(userId, ipAdress, entityActorId, entitiesCommercialRelationShip);

            if (response.IsSuccess)
            {
                return Ok(response.entitiesCommercialRelationShip);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Updates SkinImage of an entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <param name="skinImage"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveEntitySkinImage")]
        public async Task<IActionResult> SaveEntitySkinImage(string userId, string ipAdress, string entityActorId, string skinImage)
        {
            var response = await _entityNodes.SaveEntitySkinImage(userId, ipAdress, entityActorId, skinImage);

            if (response.IsSuccess)
            {
                return Ok(response.imageName);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        // Para importar desde un archivo csv
        /// <summary>
        /// Upload file csv a entity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveEntitiesCSV")]
        public async Task<IActionResult> SaveEntitiesCSV(string userId, string ipAddress, IFormFile file)
        {
            var customHeaderValue = Request.Headers["SquadId"];
            var squadId = customHeaderValue.ToString().ToUpper() ?? ""; // Request.Headers["SquadId"];
            var response = await _entityNodes.SaveEntitiesCSV(userId, ipAddress, squadId, file);

            if (response.IsSuccess)
            {
                return Ok(response.ActorsNodesList);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

    }
}
