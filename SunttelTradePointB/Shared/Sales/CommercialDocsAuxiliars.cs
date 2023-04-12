using MongoDB.Bson.Serialization.Attributes;
using SunttelTradePointB.Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.Sales
{
    public class AddItemCommercialDocument
    {


        public int RelevatGrade { get; set; }
        
        [DisplayName("Item")]
        public Concept TransactionalItem { get; set; }

        [DisplayName("Qty Available")]
        public double QtyAvailable { get; set; }

        [DisplayName("Qty to Add")]
        public double QtyToAdd { get; set; }

        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }

        [DisplayName("Chargeable Units")]
        public string ChargeableUnitsName { get; set; }
        
        [DisplayName("Taxable Unit Price")]
        public double TaxableUnitPrice { get; set; }

        [BsonIgnoreIfNull]
        [DisplayName("Pull Date")]
        public DateTime PullDate { get; set; }

        [BsonIgnoreIfNull]
        [DisplayName("Pull Date Formated")]
        public string FormatedPullDate { get; set; }


    }
}
