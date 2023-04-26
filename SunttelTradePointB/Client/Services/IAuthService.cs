using SunttelTradePointB.Shared.Security;

namespace SunttelTradePointB.Client.Services
{
    public interface IAuthService
    {
        Task Login(LoginRequest loginRequest);
        Task Register(RegisterRequest registerRequest);
        Task Logout();
        Task<CurrentUser> CurrentUserInfo();

        Task RegisterUserByAdmin(RegisterRequest registerRequest);

        Task<List<UserEntity>> GetUsersByRolname(string rolname);
    }

}
