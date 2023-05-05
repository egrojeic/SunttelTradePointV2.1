using Microsoft.AspNet.Identity.EntityFramework;
using SunttelTradePointB.Shared.Security;
using SunttelTradePointB.Shared.SquadsMgr;

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

        Task<List<UserRole>?> GetRoles();

        Task DeleteUser(string id);

        Task<UserEntity> GetUserById(string id);

        Task EditUserByAdmin(RegisterRequest registerRequest);

        Task<List<SystemTool>?> GetSystemsTools();

        Task RegisterRole(UserRole role);

        Task<UserRole?> GetRoleById(string id);
    }

}
