using SunttelTradePointB.Shared.SquadsMgr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.ImportingData
{
    public class QCDocumentsImport
    {
        public string SquadId { get; set; }
        public string LegacyObjectId { get; set; }
        public string LegacyId { get; set; }
        public string QualityReportType_ObjectId { get; set; }
        public string QualityReportType_Name { get; set; }
        public string AWBDetail_ObjectId { get; set; }
        public string SalesDocumentItemsDetailsId_ObjectId { get; set; }
        public string Code { get; set; }
        public DateTime InspectionDate { get; set; }
        public double QuantityInOrder { get; set; }
        public double QuantityToInspect { get; set; }
        public double QuantityInspected { get; set; }
        public double QuantityWithIssues { get; set; }
        public string ActionToTake_ObjectId { get; set; }
        public string ActionToTake_Name { get; set; }
        public string TrafficLightStatus_ObjectId { get; set; }
        public string TrafficLightStatus_Name { get; set; }
        public int OverKill { get; set; }
        public int Pack { get; set; }
    }
}
