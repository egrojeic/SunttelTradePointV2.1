using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Server.Controllers.MasterTablesCtrl
{

    /// <summary>
    /// Author: Jorge Isaza
    /// Description: Controller intended to manage CRUD operations on Geographic Places Table Maintenance
    /// </summary>
    [Tags("Geographic Places Table Maintenance")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GeographicPlacesController : ControllerBase
    {

        private IGeographicPlaces _geographicPlaces;
        private readonly ILogger<GeographicPlacesController> _logger;
        IConfiguration config;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="_config"></param>
        public GeographicPlacesController(ILogger<GeographicPlacesController> logger, IConfiguration _config, IGeographicPlaces geographicPlaces)
        {
            _logger = logger;
            config = _config;
            _geographicPlaces = geographicPlaces;
        }


        /// <summary>
        /// Returns all countries in the Database
        /// </summary>
        /// <param name="filterName"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCountries")]
        public async Task<IActionResult> GetCountries(string? filterName = null)
        {
            
            var response = await _geographicPlaces.GetCountries(filterName);

            if (response.IsSuccess)
            {
                return Ok(response.CountryList);
            }
            else
                return NotFound(response.ErrorDescription);
        }



        /// <summary>
        /// Get the list of regions
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="countryIso3"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRegions")]
        public async Task<IActionResult> GetRegions(string? countryId = null, string ? filterName = null, string? countryIso3 = null)
        {

            var response = await _geographicPlaces.GetRegions(filterName, countryIso3, countryId);

            if (response.IsSuccess)
            {
                return Ok(response.RegionList);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Get a list of cities
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="countryIso3"></param>
        /// <param name="regionId">Region BSON Id</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCities")]
        public async Task<IActionResult> GetCities(string? regionId = null, string ? filterName = null, string? countryIso3 = null)
        {

            var response = await _geographicPlaces.GetCities(filterName, countryIso3, regionId);

            if (response.IsSuccess)
            {
                return Ok(response.CityList);
            }
            else
                return NotFound(response.ErrorDescription);
        }


        /// <summary>
        /// Retrieves a list of Warehouses with the posibility to receive an optional paremeter
        /// </summary>
        /// <param name="nameLike"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetWarehouses")]
        public async Task<IActionResult> GetWarehouses(string? nameLike = null)
        {
            var response = await _geographicPlaces.GetWarehouses(nameLike);

            if (response.IsSuccess)
            {
                return Ok(response.warehouses);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Retrieves a particular Warehouse by Id
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetWarehouse")]
        public async Task<IActionResult> GetWarehouse(string warehouseId)
        {
            var response = await _geographicPlaces.GetWarehouse(warehouseId);

            if (response.IsSuccess)
            {
                return Ok(response.warehouse);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }


        /// <summary>
        /// Inserts / Updates a Warehouse object
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveWarehouse")]
        public async Task<IActionResult> SaveWarehouse(string warehouseId, Warehouse warehouse)
        {
            var response = await _geographicPlaces.SaveWarehouse(warehouseId, warehouse);

            if(response.IsSuccess)
            {
                return Ok(response.warehouse);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }
    }
}
