using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunttelTradePointB.Shared.SquadsMgr;

namespace SunttelTradePointB.Shared.Security
{
    public class CurrentUser
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public Dictionary<string, string> Claims { get; set; }


        public string? LastSquadId { get; set; } = "";

        public string? EntityId { get; set; } = "";

        public List<SquadsByUser>? MySquads { get; set; }
    }
}
