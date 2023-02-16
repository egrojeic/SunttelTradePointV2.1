using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.Squad
{
    public class Squad
    {
        public Guid ID { get; set; }
        public string Nombre { get; set; }

        public string UrlDServer { get; set; }

        public string UserD { get; set; }
        public string PassD { get; set; }

        public int FlagEnabled { get; set; }


    }
}
