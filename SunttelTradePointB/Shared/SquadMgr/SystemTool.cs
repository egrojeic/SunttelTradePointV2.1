using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.SquadsMgr
{
    public class SystemTool
    {

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }


        public int IDToolSets { get; set; }

        public int DisplayOrder { get; set; }

        public string OptionRef { get; set; }

        [NotMapped]
        public virtual string IconName { get; set; }


        [ForeignKey("IDToolSets")]
        public ToolSet? ToolSetsNavigation { get; set; }

        public override string ToString() { return Name; }

    }

    public class ToolSet
    {
       

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public virtual string? collapseState { get; set; }

        [NotMapped]
        public virtual string IconName { get; set; }
    }
}
