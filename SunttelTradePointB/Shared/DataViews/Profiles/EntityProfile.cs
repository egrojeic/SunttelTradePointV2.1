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
            CreateMap<ActorsImport, EntityActor>();
                
        }
    }
}
