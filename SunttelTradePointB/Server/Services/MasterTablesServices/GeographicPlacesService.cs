using Microsoft.AspNetCore.Mvc.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;
using System.Collections.Generic;
using System.Drawing;

namespace SunttelTradePointB.Server.Services.MasterTablesServices
{

    /// <summary>
    /// Service implementing IGeographicPlaces which is intended to deal with CRUD operations of Geographic Places
    /// </summary>
    public class GeographicPlacesService : IGeographicPlaces
    {

        IMongoCollection<Country> _CountryCollection;
        IMongoCollection<GeoRegion> _RegionCollection;
        IMongoCollection<City> _CityCollection;
        IMongoCollection<Warehouse> _WarehouseCollection;


        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="config"></param>
        public GeographicPlacesService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);

            _CountryCollection = mongoDatabase.GetCollection<Country>("GeographicCountries");
            _RegionCollection = mongoDatabase.GetCollection<GeoRegion>("GeographicRegions");
            _CityCollection = mongoDatabase.GetCollection<City>("GeographicCities");
            _WarehouseCollection = mongoDatabase.GetCollection<Warehouse>("Warehouses");

        }


        /// <summary>
        /// Returns a list of Countries
        /// </summary>
        /// <param name="nameLike"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<Country>? CountryList, string? ErrorDescription)> GetCountries(string? nameLike = null)
        {
            try
            {
                string strNameFiler = nameLike == null ? "" : nameLike;
 ;
                List<Country> countriesList = await _CountryCollection.Find(c=>c.Name.Contains(strNameFiler)).SortBy(c=>c.Name).ToListAsync();
                return (true, countriesList, null);
            }
            catch (Exception e) {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        ///  Returns a list of Regions
        /// </summary>
        /// <param name="nameLike"></param>
        /// <param name="countryIso3"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<GeoRegion>? RegionList, string? ErrorDescription)> GetRegions(string? nameLike = "", string? countryIso3 = null, string? countryId = null)
        {

            try
            {
                string strNameFiler = nameLike == null ? "" : nameLike;
                string strCountryFiler = countryIso3 == null ? "" : countryIso3;
                string strCountryIdFiler = countryId == null ? "000000000000000000000000" : countryId;

                ;
                List<GeoRegion> regionsList = await _RegionCollection.Find(c => (strNameFiler.Length == 0 || (strNameFiler.Length > 0 && c.Name.Contains(strNameFiler))) 
                    & (strCountryFiler.Length ==0 || (strCountryFiler.Length> 0 & c.CountryRegion.CodeIso3 == strCountryFiler))
                    & (strCountryIdFiler == "000000000000000000000000" || (strCountryIdFiler != "000000000000000000000000" & (c.CountryRegion.Id == strCountryIdFiler)))

                    ).ToListAsync();
                return (true, regionsList, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }



       /// <summary>
       /// Get the list of cities
       /// </summary>
       /// <param name="nameLike"></param>
       /// <param name="countryIso3"></param>
       /// <param name="RegionId"></param>
       /// <returns></returns>
        public async Task<(bool IsSuccess, List<City>? CityList, string? ErrorDescription)> GetCities(string? nameLike = "", string? countryIso3 = "", string? RegionId = null)
        {

            try
            {
                string strNameFiler = nameLike == null ? "" : nameLike;
                string strCountryFiler = countryIso3 == null ? "" : countryIso3;
                string strRegionFiler = RegionId == null ? "000000000000000000000000" : RegionId;

                
                List<City> cityList = await _CityCollection.Find(c => (strNameFiler.Length == 0 || (strNameFiler.Length > 0 && c.Name.Contains(strNameFiler)))
                    & (strCountryFiler.Length == 0 || (strCountryFiler.Length >0 & c.RegionCity.CountryRegion.CodeIso3 == strCountryFiler))
                    & (strRegionFiler == "000000000000000000000000" || (strRegionFiler != "000000000000000000000000" & (c.RegionCity.Id == strRegionFiler)))

                    ).ToListAsync();
                return (true, cityList, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }


        /// <summary>
        /// Retrieves a list of Warehouses with the posibility to receive an optional paremeter
        /// </summary>
        /// <param name="nameLike"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<Warehouse>? warehouses, string? ErrorDescription)> GetWarehouses(string? nameLike = null)
        {
            try
            {
                string filter = nameLike == null ? "" : nameLike;

                if (filter.Length > 0)
                {
                    var pipeline = new[]
                    {
                    new BsonDocument(
                        "$match", new BsonDocument(
                            "Name", new BsonDocument("$regex", new BsonRegularExpression($"/{filter}/i"))
                        )
                     )
                };
                    List<Warehouse> results = await _WarehouseCollection.Aggregate<Warehouse>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var warehouses = await _WarehouseCollection.Find<Warehouse>(new BsonDocument()).ToListAsync();

                    if (warehouses == null || warehouses.Count == 0)
                    {
                        return (false, null, "Unpopulated Warehaouses");
                    }
                    else
                    {
                        return (true, warehouses, null);
                    }
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves a particular Warehouse by Id
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, Warehouse? warehouse, string? ErrorDescription)> GetWarehouse(string warehouseId)
        {
            try
            {
                var filterWarehouse = Builders<Warehouse>.Filter.Eq(x => x.Id, warehouseId);
                var resultWarehouse = await _WarehouseCollection.Find(filterWarehouse).FirstOrDefaultAsync();

                if (resultWarehouse == null)
                {
                    return (false, null, "Unpopulated Warehouses");
                }
                else
                {
                    return (true, resultWarehouse, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Inserts / Updates a Warehouse object
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, Warehouse? warehouse, string? ErrorDescription)> SaveWarehouse(string warehouseId, Warehouse warehouse)
        {
            try
            {
                if (warehouse.Id == null)
                {
                    warehouse.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterWarehouse = Builders<Warehouse>.Filter.Eq("_id", new ObjectId(warehouseId));
                var updateWarehouseOptions = new ReplaceOptions { IsUpsert = true };
                var resultWarehouse = await _WarehouseCollection.ReplaceOneAsync(filterWarehouse, warehouse, updateWarehouseOptions);

                return (true, warehouse, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
    }
}
