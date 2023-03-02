using SunttelTradePointB.Shared.Communications;
using SunttelTradePointB.Shared.Security;

namespace SunttelTradePointB.Server.Interfaces.UserTracking
{

    /// <summary>
    /// Class intended to track the user activity
    /// </summary>
    public interface IUserTracking
    {

        /// <summary>
        /// INSERT USER ACTIVITY LOG BY CONTOLLER
        /// </summary>
        /// <param name="userActivity"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, UserActivity? userActivity, string? ErrorDescription)> SaveUserActivityByController(UserActivity  userActivity);


    }
}
