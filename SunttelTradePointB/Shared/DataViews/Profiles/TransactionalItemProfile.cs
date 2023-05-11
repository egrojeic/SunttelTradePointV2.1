using AutoMapper;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.ImportingData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.DataViews.Profiles
{
    internal class TransactionalItemProfile : Profile
    {
        public TransactionalItemProfile() 
        {
            CreateMap<ProductsImport, TransactionalItem>();


        }
    }
}
