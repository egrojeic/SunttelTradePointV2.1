using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SunttelTradePointB.Shared.ImportingData
{
    public class ActorsImport
    {
        public int LegacyId { get; set; }
        public string Codigo { get; set; }
        public string Name { get; set; }
        public string EMailAddres { get; set; }
        public string Address1 { get; set; }
        public string Country { get; set; }
        public string CountryState { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string GrupoClientesNombre { get; set; }
        public string GroupObjectId { get; set; }
        public string TypeActorId { get; set; }
        public string TypeActor { get; set; }
        public string TypeActorClassifier { get; set; }
        public int IDGrupoClientes { get; set; }

        public int FlagActivo { get; set; }

        public int ISPerson { get; set; }
    }

    public class IdentityList
    {
           
        public int ActorLegacyId { get; set; }
        public string Code { get; set; }
        public int ActorIssuerLegacyId { get; set; }
        public string ActorIssuerLegacyName { get; set; }

        public string IDObjectIdIssuer { get; set; }
    }

    public class AddressActorsImport
    {   
        public int IDClientes { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string City_LegacyId { get; set; }
        public string City_Code { get; set; }
        public string City_Name { get; set; }
        public int City_PaisLegacyID { get; set; }
        public int City_StateLegacyID {get;}
        public string StateName   { get; set; }
        public string StateCode { get; set; }
        public string CountryCode  { get; set; }
        public string SourceInfo { get; set; }



    }
}
