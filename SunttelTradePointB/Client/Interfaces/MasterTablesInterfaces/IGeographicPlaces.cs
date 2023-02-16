using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces
{

    /// <summary>
    /// Author: Jorge Isaza
    /// Description: Interface to controll CRUD Operations on Geographic Places
    /// </summary>
    public interface IGeographicPlaces
    {
        /// <summary>
        /// Get a list of Countries 
        /// </summary>
        /// <param name="nameLike"></param>
        /// <returns></returns>
        Task<List<Country>?> GetCountries(string? nameLike = null, bool forceRefresh = false);

       /// <summary>
       /// Get a lsit of regions
       /// </summary>
       /// <param name="nameLike"></param>
       /// <param name="countryIso3"></param>
       /// <param name="countryId"></param>
       /// <returns></returns>
        Task<List<GeoRegion>?> GetRegions(string countryId, string nameLike = "", bool forceRefresh = false);


       
        /// <summary>
        /// Get a list of Cities
        /// </summary>
        /// <param name="nameLike"></param>
        /// <param name="countryId"></param>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        Task<List<City>?> GetCities(string RegionId, string nameLike = "", bool forceRefresh = false);


    }
}
