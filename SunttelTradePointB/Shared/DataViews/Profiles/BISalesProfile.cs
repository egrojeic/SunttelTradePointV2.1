using AutoMapper;
using SunttelTradePointB.Shared.DataViews.BI;
using SunttelTradePointB.Shared.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.DataViews.Profiles
{
    public class BISalesProfile: Profile
    {
        public BISalesProfile()
        {
            CreateMap<CommercialDocument, BISalesConsolidated>()
                .ForMember(dest => dest.DeliveryAddressCountry, opt => opt.MapFrom(src => src.DeliveryAddress.CityAddress.RegionCity.CountryRegion.Name))
                .ForMember(dest => dest.DeliveryAddressState, opt => opt.MapFrom(src => src.DeliveryAddress.CityAddress.RegionCity.Name))
                .ForMember(dest => dest.DeliveryAddressCity, opt => opt.MapFrom(src => src.DeliveryAddress.CityAddress.Name))
                .ForMember(dest => dest.BuyerCommercialGroup, opt => opt.MapFrom(src => src.Buyer.Groups[0].Name));
        }
    }
}
