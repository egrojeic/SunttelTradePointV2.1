using SunttelTradePointB.Shared.SquadsMgr;

namespace SunttelTradePointB.Client.Interfaces.SquadInterfaces
{
    public interface ISquad
    {

        Task<List<SquadsByUser>> SquadInfo(string userId);
        Task<List<SystemTool>> SystemToolsByUser(Guid userId);

        Task<Squad> GetSquad(Guid squadId);

        Task<(bool IsSuccess, Squad? squad, string? ErrorDescription)> SaveSquad(Squad squad);


    }
}
