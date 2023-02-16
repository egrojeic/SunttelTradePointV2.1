using SunttelTradePointB.Shared.Security;
using SunttelTradePointB.Shared.Squad;

namespace SunttelTradePointB.Server.Interfaces
{
    public interface ISquad
    {

        Task<Squad> SquadInfo();
        Task<List<SystemTool>> SystemToolsByUser(Guid userId);

    }
}
