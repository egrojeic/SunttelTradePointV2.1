using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.Common
{

    /// <summary>
    /// Class Intended to describe storage places or facilities
    /// </summary>
    public class Warehouse: AtomConcept
    {
        [DisplayName("Parent Warehouse")]
        [BsonIgnoreIfNull]
        public Warehouse WareHouseParent { get; set; }

        [DisplayName("Associated Entity")]
        [BsonIgnoreIfNull]
        public AtomConcept? AssociatedEntity { get; set; }

        [DisplayName("External")]
        public bool IsExternal { get; set; }

        [DisplayName("Transformation")]
        public bool IsTransformationFacility { get; set; }

        [DisplayName("Scan Simulated")]
        public bool ScanSimulated { get; set; }

        [DisplayName("Inventory Controlled")]
        public bool IsInventoryControlled { get; set; }

        [DisplayName("Inventory Item Type")]
        public AtomConcept InventoryTransactionalItemType { get; set; }

        [DisplayName("Address")]
        public Address WarehouseAddress { get; set; }

        [DisplayName("Contact")]
        public AtomConcept? ContactPerson { get; set; }

        [DisplayName("Is Enabled")]
        public bool IsActive { get; set; }
    }
}
