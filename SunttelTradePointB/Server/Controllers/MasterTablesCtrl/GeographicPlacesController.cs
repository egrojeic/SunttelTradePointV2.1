using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Server.Interfaces.UserTracking;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Security;
using System.Net;
using System.Reflection;

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
        private readonly IUserTracking _userTracking;

      

        private IGeographicPlaces _geographicPlaces;
        private readonly ILogger<GeographicPlacesController> _logger;
        IConfiguration config;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="_config"></param>
        /// <param name="geographicPlaces"></param>
        /// <param name="userTracking"></param>
        public GeographicPlacesController(ILogger<GeographicPlacesController> logger, IConfiguration _config, IGeographicPlaces geographicPlaces, IUserTracking userTracking)
        {
            _logger = logger;
            config = _config;
            _geographicPlaces = geographicPlaces;
            _userTracking = userTracking;

        }

        ///// <summary>
        ///// Saves Activity
        ///// </summary>
        ///// <param name="methodName"></param>
        ///// <returns></returns>
        //public async Task<ActionResult> SaveActivity(string methodName)
        //{
        //    // Record user activity
        //    var userActivity = new UserActivity
        //    {
        //        UserId = User.Identity.Name,
        //        ControllerName = "GeographicPlaces",
        //        MethodName = methodName,
        //        TimeStamp = DateTime.UtcNow
        //    };
        //    await _userTracking.SaveUserActivityByController(userActivity);

        //    return Ok();
        //}

        /// <summary>
        /// Returns all countries in the Database
        /// </summary>
        /// <param name="filterName"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCountries")]
        public async Task<IActionResult> GetCountries(string? filterName = null)
        {

            //await SaveActivity("GetCountries");
            var response = await _geographicPlaces.GetCountries(filterName);

            if (response.IsSuccess)
            {
                return Ok(response.CountryList);
            }
            else
                return NotFound(response.ErrorDescription);
        }

        /// <summary>
        ///  Gets the Country ID, Name and Language of the country matching the IP
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCountryByIPAddress")]
        public async Task<IActionResult> GetCountryByIPAddress(string ipAddress)
        {

            //await SaveActivity("GetCountries");
            var response = await _geographicPlaces.GetCountryByIPAddress(ipAddress);

            if (response.IsSuccess)
            {
                return Ok(response.country);
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
        /// <param name="entityId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="nameLike"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetWarehouses")]
        public async Task<IActionResult> GetWarehouses(string entityId, string ipAdress, string? nameLike = null)
        {
            var response = await _geographicPlaces.GetWarehouses(entityId, ipAdress, nameLike);

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
        /// <param name="entityId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetWarehouse")]
        public async Task<IActionResult> GetWarehouse(string entityId, string ipAdress, string warehouseId)
        {
            var response = await _geographicPlaces.GetWarehouse(entityId, ipAdress, warehouseId);

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
        /// <param name="entityId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveWarehouse")]
        public async Task<IActionResult> SaveWarehouse(string entityId, string ipAdress, Warehouse warehouse)
        {
            var response = await _geographicPlaces.SaveWarehouse(entityId, ipAdress, warehouse);

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
