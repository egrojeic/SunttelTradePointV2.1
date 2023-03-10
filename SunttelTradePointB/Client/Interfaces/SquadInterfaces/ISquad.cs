using SunttelTradePointB.Shared.SquadsMgr;

namespace SunttelTradePointB.Client.Interfaces.SquadInterfaces
{
    public interface ISquad
    {

        Task<List<SquadsByUser>> SquadInfo(string userId);
        Task<List<SystemTool>> SystemToolsByUser(Guid userId);

    }
}
