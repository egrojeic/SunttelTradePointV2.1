using SunttelTradePointB.Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.Quality
{

    public class QualityTrafficLight: BasicConcept
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
    }

    public class QualityAction : BasicConcept
    {

    }

    public class QualityReportType : BasicConcept
    {

    }

    public class QualityParameterGroup : BasicConcept
    {

    }

    public class QualityAssuranceParameter : RecordItem
    {
        public string Name { get; set; }
        public QualityParameterGroup ParameterGroup { get; set; }

        public QualityReportType ReportType { get; set; }

    }

    public class QualityEvaluation: RecordItem
    {

        public QualityReportType QualityReportType { get; set; }
        public string SalesDocumentItemsDetailsId { get; set; }

        public DateTime InspectionDate { get; set; }

        [DisplayName("Quantity to Inspect")]
        public double QuantityToInspect { get; set; }
        [DisplayName("Quantity Inspected")]
        public double QuantityInspected { get; set; }
        
        [DisplayName("Quantity with Issues")]
        public double QuantityWithIssues { get; set; }

        public double Pack { get; set; }
        
        [DisplayName("Traffic Light Status")]
        public QualityTrafficLight TrafficLightStatus { get; set; }

        [DisplayName("Quality Action")]
        public QualityAction ActionToTake { get; set; }

        public bool OverKill { get; set; } = false;

        public List<QualityEvaluationDetail> EvaluationParameters { get; set; }

        public List<QualityEvaluationPhoto> QualityEvaluationImages { get; set; }
    }


    public class QualityEvaluationDetail: RecordItem
    {
        public string QualityEvaluationId { get; set; }

        public QualityAssuranceParameter Parameter { get; set; }

        public bool IsPresent { get; set; }

    }


    public class QualityEvaluationPhoto: RecordItem
    {

        public QualityParameterGroup ParameterGroup { get; set; }
        public string ImageName { get; set; }
    }


}
