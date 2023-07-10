using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.SquadsMgr;

namespace SunttelTradePointB.Shared.Security
{
    public class UserRole: IdentityRole<String>
    {
      
      
        public Guid SquadsId { get; set; }

        public List<SystemTool> SystemTools { get; set; }

        public override string ToString() { return Name; }


      
    }
}
