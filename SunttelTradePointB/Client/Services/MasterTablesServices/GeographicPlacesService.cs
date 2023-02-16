using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Squad;
using System.Net.Http;
using System.Net.Http.Json;

namespace SunttelTradePointB.Client.Services.MasterTablesServices
{
   
    public class GeographicPlacesService : IGeographicPlaces
    {
        private readonly HttpClient _httpClient;

        List<Country>? countriesGlobalList;
        List<GeoRegion>? regionsGlobalList;
        List<City>? citiesGlobalList;

        public GeographicPlacesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        /// <summary>
        /// Get the list of Cities
        /// </summary>
        /// <param name="nameLike"></param>
        /// <param name="countryId"></param>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<City>?> GetCities(string RegionId, string nameLike= "",  bool forceRefresh = false)
        {
            try
            {
                if (forceRefresh || citiesGlobalList == null) {
                    citiesGlobalList = await _httpClient.GetFromJsonAsync<List<City>>($"api/GeographicPlaces/GetCities?regionId={RegionId.ToString()}");

                }

                if (citiesGlobalList != null)
                    return citiesGlobalList.FindAll(c => c.Name.Contains(nameLike)).ToList();
                else
                    return null;
            }
            catch(Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        /// <summary>
        /// Get the list of Countries
        /// </summary>
        /// <param name="nameLike"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Country>?> GetCountries(string? nameLike = null, bool forceRefresh = false)
        {
            try
            {
                if (forceRefresh || countriesGlobalList == null)
                {
                    countriesGlobalList = await _httpClient.GetFromJsonAsync<List<Country>>($"api/GeographicPlaces/GetCountries/{nameLike}");
                }

                nameLike = nameLike ?? string.Empty;

                if(countriesGlobalList!=null)
                    return countriesGlobalList.FindAll(c => c.Name.Contains(nameLike)).ToList();
                else
                    return null;


            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Get a list of Regions
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<GeoRegion>?> GetRegions(string countryId, string nameLike = "", bool forceRefresh = false)
        {
            try
            {
                if (forceRefresh || regionsGlobalList == null) {
                    regionsGlobalList = await _httpClient.GetFromJsonAsync<List<GeoRegion>>($"api/GeographicPlaces/GetRegions?countryId={countryId.ToString()}");
                }

                if (regionsGlobalList != null)
                    return regionsGlobalList.FindAll(c => c.Name.Contains(nameLike)).ToList();
                else
                    return null;
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                return null;
            }
        }
    }
}
