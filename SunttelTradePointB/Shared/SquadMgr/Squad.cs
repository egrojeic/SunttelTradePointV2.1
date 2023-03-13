using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.SquadsMgr
{
    public class Squad
    {
        public Guid ID { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Nombre { get; set; }

        [ObjectPropertyAttributes(ObjectPropertyAttributes.EditModes.None)]
        public string UrlDServer { get; set; }

        [ObjectPropertyAttributes(ObjectPropertyAttributes.EditModes.None)]
        public string UserD { get; set; }

        [ObjectPropertyAttributes(ObjectPropertyAttributes.EditModes.None)]
        public string PassD { get; set; }

        [DisplayName("Enabled")]
        public int FlagEnabled { get; set; }

        [ObjectPropertyAttributes(ObjectPropertyAttributes.EditModes.None)]
        public string EntityID { get; set; }
        public string SkinImage { get; set; }

        [ObjectPropertyAttributes(ObjectPropertyAttributes.EditModes.None)]
        public string IDAppUserOwner { get; set; }

        [DisplayName("Domain Name")]
        public string DomainName { get; set; }

        [ObjectPropertyAttributes(ObjectPropertyAttributes.EditModes.Details)]

        public List<SquadTag> Tags { get; set; }
    }

    public class SquadTag
    {
        public Guid ID { get; set; }
        public string IDSquads { get; set; }
        public string TagKey { get; set; }
        public string TagValue { get; set; }

    }

    public class SquadsByUser
    {
        public Guid ID { get; set; }

        public Guid IDSquads { get; set; }
        public string SquadName { get; set; }
        public string SkinImage { get; set; }
        public string UrlDServer { get; set; }
        public string UserD { get; set; }
        public string PassD { get; set; }

        public Guid IDAppUserOwner { get; set; }

        public string DomainName { get; set; }

    }
    
}
