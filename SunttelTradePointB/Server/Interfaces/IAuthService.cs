using SunttelTradePointB.Shared.Security;

namespace SunttelTradePointB.Server.Interfaces
{
    public interface IAuthService
    {
        Task Login(LoginRequest loginRequest);
        Task Register(RegisterRequest registerRequest);
        Task Logout();
        Task<CurrentUser> CurrentUserInfo();
    }

}
