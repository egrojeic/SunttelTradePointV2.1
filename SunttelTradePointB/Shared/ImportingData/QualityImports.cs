using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.ImportingData
{
    internal class QualityImports
    {
    }

    public class QuatityParameterImports
    {
        public string SquadId { get; set; }
        public string ObjectId { get; set; }
        public string LegacyId { get; set; }
        public string Name { get; set; }
        public string ParameterGroup_Name { get; set; }
        public string ParameterGroup_Id { get; set; }
        public string ReportType_Id { get; set; }
        public string ReportType_Name { get; set; }

    }
}
