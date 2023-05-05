using SunttelTradePointB.Shared.SquadsMgr;

namespace SunttelTradePointB.Shared.Security
{
    public class UserRole
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<SystemTool> SystemTools { get; set; }

        public override string ToString() { return Name; }
    }
}
