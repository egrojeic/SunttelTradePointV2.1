using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.ImportingData
{
    public class SeasonsImports
    {
        public string SeasonObjectId { get; set; }
        public int ID  {get; set;}
        public string Nombre   {get; set;}
        public int FlagActivo  {get; set;}
        public int FlagHollyDay     {get; set;}
        public int IDClasesTemporadas {get; set;}
        public int FlagDespachoDomingo { get; set; }


    }
}
