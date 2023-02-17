using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SunttelTradePointB.Shared.Common
{

    /// <summary>
    /// Enumerator of the different details tables
    /// </summary>
    public enum EntityDetailsSection
    {
        /// <summary>
        /// List of Shipping to address
        /// </summary>
        AddressList = 0,

        /// <summary>
        /// List of the different identifications codes provided by different issuers
        /// </summary>
        IdentifiersList = 1,

        /// <summary>
        /// User names to contact with the entity through different social media and other electronic comunication
        /// </summary>
        ElectronicAddressLis = 2,
        /// <summary>
        /// Phone Directory
        /// </summary>
        PhoneDirectory = 3,

        /// <summary>
        /// Information about 
        /// </summary>
        ShippingSetupDetails = 4,

        /// <summary>
        /// List describing credit limit, credit time limit, and other comercial conditions 
        /// </summary>
        ComercialConditions = 5
    }

    [BsonIgnoreExtraElements]
    public class EntityActor: Concept
    {

        [DisplayName("First Name")]
        [BsonIgnoreIfNull]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [BsonIgnoreIfNull]
        public string LastName { get; set; }

        public List<IdentificationEntity> Identifications { get; set; }

        [BsonIgnoreIfNull]

        [DisplayName("Invoicing Address")]
        public Address InvoicingAddress { get; set; }

        [DisplayName("Role by Default")]
        public EntityRole DefaultEntityRole { get; set; }
        public List<EntityTag> Tags { get; set; }


        // Contact Information
        [DisplayName("Sunttel User Name")]
        public string SunttelUserId { get; set; }

        [DisplayName("EMail")]
        public string EMailAddress { get; set; }

        [DisplayName("Address List")]
        public List<Address> AddressList { get; set; }

        [DisplayName("Electronic Addresses")]
        public List<ElectronicAddress> ElectronicAddresses { get; set; }

        [DisplayName("Phone Numbers")]
        public List<PhoneNumber> PhoneNumbers { get; set; }

        [BsonIgnoreIfNull]
        [DisplayName("Shipping Setup")]
        public List<ShippingInfo> ShippingInformation { get; set; }

        public string GetFirstPhone
        {
            get
            {
                if (PhoneNumbers == null)
                {
                    return "NO PHONE";
                }
                else
                {
                    return PhoneNumbers[0].Number;
                }
            }

        }

        

        public List<EntitiesCommercialRelationShip> EntitiesRelationShips { get; set; }

        public EntityActor()
        {
         
            this.Identifications = new List<IdentificationEntity>();
           
            this.AddressList = new List<Address>();
            this.ElectronicAddresses = new List<ElectronicAddress>();

            this.DefaultEntityRole = new EntityRole();
            this.Tags = new List<EntityTag>();
        }

    }

    public class EntityType: ConceptType
    {
       
    }


    public class EntityTag
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }



    public class ElectronicAddress {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [DisplayName("Address Key")]
        public string AddressKey { get; set; }

        [DisplayName("Emitter")]
        public AtomConcept EmitterEntity { get; set; }
    }

    public class EntityGroup: ConceptGroup
    {
        
        public string Description { get; set; }


    }

#nullable enable
    public class EntityRole: AtomConcept
    {

        /// <summary>
        /// Entity role which is the parent of the current role
        /// </summary>
        [BsonIgnoreIfNull]
        public EntityRole? EntityRoleClassifier { get; set; }

        public override string ToString()
        {
            return GetInnerName(this);
        }

        private string GetInnerName(EntityRole entityRole) {

            if (entityRole.EntityRoleClassifier == null)
            {
                return entityRole.Name;
            }
            else {
                return GetInnerName(entityRole.EntityRoleClassifier) + " - " + entityRole.Name;
            }
        }
    }
#nullable disable
    public class PhoneNumber
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [DisplayName("Zone")]
        public int ZoneCode { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }

    }

    public class ShippingInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [DisplayName("Sender")]
        public Concept Sender { get; set; }


        [DisplayName("Carrier")]
        public Concept Carrier { get; set; }

        [DisplayName("Delivery Address")]
        public Address DeliveryAddress { get; set; }

        [DisplayName("Cut Off (24H)")]
        public int CutOff24Hour { get; set; }

        [DisplayName("Cut Off (minutes)")]
        public int CutOffMinute { get; set; }

        [DisplayName("Delay (days)")]
        public int DelayTimeDays { get; set; }

        [DisplayName("Pallet Type")]
        public PalletType PalletTypeShipping { get; set; }

        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }


    }

    public class PalletType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }


    public class EntitiesCommercialRelationShip
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [DisplayName("Related Entity")]
        public AtomConcept RelatedEntity { get; set; }

        [DisplayName("Credit Limit (Days)")]
        public int LimitCreditDays { get; set; }

        [DisplayName("Credit Amount (Days)")]
        public double LimitCreditAmount { get; set; }

        [DisplayName("Relationship type")]
        public EntitiyRelationshipType EntitiyCommercialRelationShipType { get; set; }
        

    }


    public class EntitiyRelationshipType: AtomConcept
    {

    }

    public class EntitiyRelationshipStatus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

    }



}
