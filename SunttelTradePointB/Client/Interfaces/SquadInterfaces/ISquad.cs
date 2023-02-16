using SunttelTradePointB.Shared.Squad;

namespace SunttelTradePointB.Client.Interfaces.SquadInterfaces
{
    public interface ISquad
    {

        Task<Squad> SquadInfo();
        Task<List<SystemTool>> SystemToolsByUser(Guid userId);

    }
}
