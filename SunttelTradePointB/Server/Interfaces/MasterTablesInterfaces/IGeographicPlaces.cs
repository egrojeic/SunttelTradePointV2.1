using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.SquadsMgr;

namespace SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces
{

    /// <summary>
    /// Author: Jorge Isaza
    /// Description: Interface to controll CRUD Operations on Geographic Places
    /// </summary>
    public interface IGeographicPlaces
    {

        /// <summary>
        /// Returns a list of Countries
        /// </summary>
        /// <param name="nameLike"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<Country>? CountryList, string? ErrorDescription)> GetCountries(string? nameLike = null);


        /// <summary>
        /// Retrieves the country matching with the IP parameter
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, Country?  country, string? ErrorDescription)> GetCountryByIPAddress(string ipAddress);


        /// <summary>
        ///  Returns a list of Regions
        /// </summary>
        /// <param name="nameLike"></param>
        /// <param name="countryIso3"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<GeoRegion>? RegionList, string? ErrorDescription)> GetRegions(string? nameLike = null, string? countryIso3 = null, string? countryId = null);


        /// <summary>
        /// Returns a list of Cities
        /// </summary>
        /// <param name="nameLike"></param>
        /// <param name="countryId">ID of the country of the listed Cities</param>
        /// <param name="RegionId">ID of the Region of the listed regions</param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<City>? CityList, string? ErrorDescription)> GetCities(string? nameLike = null, string? countryId = null, string? RegionId = null);


        /// <summary>
        /// Retrieves the list of warehouses.
        /// If the filter condition is empty, it shoukd return all
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="nameLike"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<Warehouse>?  warehouses, string? ErrorDescription)> GetWarehouses(string entityId, string ipAdress,string squadId, string? nameLike = null);

        /// <summary>
        ///  Retrieves a perticular warehouse
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, Warehouse? warehouse, string? ErrorDescription)> GetWarehouse(string entityId, string ipAdress,string squadId, string warehouseId);

        /// <summary>
        /// Insert/ Updates a Warehouse
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, Warehouse? warehouse, string? ErrorDescription)> SaveWarehouse(string entityId, string ipAdress, Warehouse warehouse);


    }
}
