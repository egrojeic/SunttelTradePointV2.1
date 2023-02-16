using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.ImportingData
{
    public class ImportCountries
    {
        public int LegacyId { get; set; }
        public string CodeIso2 { get; set; }
        public string CodeIso3 { get; set; }
        public string Name { get; set; }

    }

    public class ImportRegions
    {
        public int LegacyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CodeIso2 { get; set; }
    }

    public class ImportCities
    {
        public int LegacyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public string Region { get; set; }
        public string CodeIso2 { get; set; }
    }


}
