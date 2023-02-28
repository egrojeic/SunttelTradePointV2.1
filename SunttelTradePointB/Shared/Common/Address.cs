using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.Common
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CityAddressRef { get; set; }
        public string ZipCode { get; set; }

        public bool DefaultAddress { get; set; }

        [BsonIgnoreIfNull]
        public City CityAddress { get; set; }

       

        public override string ToString()
        {

            string nameRegionToString = "";
            string nameCountry = "";

            if (CityAddress != null) {
                nameRegionToString = (CityAddress.RegionCity != null) ? CityAddress.RegionCity.Name : "";
                nameCountry = (CityAddress.RegionCity != null) ? CityAddress.RegionCity.CountryRegion.Name : "";

            }
            return $"{AddressLine1} - {nameRegionToString}, {nameCountry} ZIP: {ZipCode}";

            return $"{AddressLine1}";
        }
    }



    public class City {


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int LegacyId { get; set; }

        [DisplayName("City")]
        public string Name { get; set; }

        public string BaseLanguageName { get; set; }
        public GeoRegion RegionCity { get; set; }

        public int TimeZone { get; set; }

        public override string ToString()
        {
            string nameRegionToString = (RegionCity != null) ? RegionCity.Name:"";
            string nameCountry = (RegionCity != null) ? RegionCity.CountryRegion.Name:"";
            return $"{Name} - {nameRegionToString}, {nameCountry}";
        }
    }

    public class GeoRegion
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int LegacyId { get; set; }

        [DisplayName("Region")]
        public string Name { get; set; }

        public string BaseLanguageName { get; set; }

        public Country CountryRegion { get; set; }


    }

    public class Country
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int LegacyId { get; set; }
        public string CodeIso2 { get; set; }
        public string CodeIso3 { get; set; }

        [DisplayName("Country")]
        public string Name { get; set; }

        public string BaseLanguageName { get; set; }

        [BsonIgnoreIfNull]
        public List<IpRangeCountry> IpRange { get; set; }
    }


    public class IpRangeCountry {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string StartIp { get; set; }
        public string FinalIp { get; set; }

    }

}
