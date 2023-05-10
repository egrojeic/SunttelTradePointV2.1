using AutoMapper;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.DataViews.BI;
using SunttelTradePointB.Shared.ImportingData;
using SunttelTradePointB.Shared.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.DataViews.Profiles
{
    internal class EntityProfile : Profile
    {
        public EntityProfile() 
        {
            CreateMap<ActorsImport, EntityActor>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Codigo))
                .ForPath(dest => dest.InvoicingAddress.AddressLine1, opt => opt.MapFrom(src => src.Address1))
                .ForPath(dest => dest.InvoicingAddress.AddressLine1, opt => opt.MapFrom(src => src.Address1))
                .ForPath(dest => dest.InvoicingAddress.ZipCode, opt => opt.MapFrom(src => src.Address1))
                
                .ForPath(dest => dest.DefaultEntityRole.Id , opt => opt.MapFrom(src => src.TypeActorId))
                .ForPath(dest => dest.DefaultEntityRole.Name, opt => opt.MapFrom(src => src.TypeActor))
                .ForPath(dest => dest.DefaultEntityRole.EntityRoleClassifier.Id, opt => opt.MapFrom(src => src.TypeActorClassifier));
        }
    }
}
