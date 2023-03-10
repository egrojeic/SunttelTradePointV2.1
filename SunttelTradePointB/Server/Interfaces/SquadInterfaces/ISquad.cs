using SunttelTradePointB.Shared.Security;
using SunttelTradePointB.Shared.SquadsMgr;
using SunttelTradePointB.Shared.SquadsMgr;

namespace SunttelTradePointB.Server.Interfaces
{
    public interface ISquadBack
    {

        Task<List<SquadsByUser>> SquadInfo(string userId);
        Task<List<SystemTool>> SystemToolsByUser(Guid userId);

    }
}
