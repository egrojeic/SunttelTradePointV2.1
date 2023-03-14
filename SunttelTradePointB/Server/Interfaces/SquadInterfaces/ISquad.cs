using SunttelTradePointB.Shared.Security;
using SunttelTradePointB.Shared.SquadsMgr;
using SunttelTradePointB.Shared.SquadsMgr;

namespace SunttelTradePointB.Server.Interfaces
{
    public interface ISquadBack
    {

        /// <summary>
        /// Retrieves the Squads related with a User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<SquadsByUser>> SquadInfo(string userId);


        /// <summary>
        /// Retrieves the System Tools by User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        Task<List<SystemTool>> SystemToolsByUser(Guid userId, string? squadId = "");
        
        /// <summary>
        /// Retrieves the Squad by Squad Name
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="SquadName"></param>
        /// <returns></returns>
        Task<string> GetSquadIdByName(Guid userId, string SquadName);


        /// <summary>
        /// Retrieves the Squad by Squad ID
        /// </summary>
        /// <param name="squadId"></param>
        /// <returns></returns>
        Task<Squad> GetSquad(string squadId);

        /// <summary>
        /// Saves a Squad
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squad"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, Squad? squad, string? error)> SaveSquad(string userId, string ipAdress, Squad squad);



    }
}
