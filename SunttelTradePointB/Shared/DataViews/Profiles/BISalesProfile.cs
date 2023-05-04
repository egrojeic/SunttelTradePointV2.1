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
            CreateMap<CommercialDocument, BISalesConsolidated>();
        }
    }
}
