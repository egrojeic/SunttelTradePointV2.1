using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Components.Authorization;
using SunttelTradePointB.Shared.Security;
using SunttelTradePointB.Shared.SquadsMgr;
using System.Security.Claims;
using System.Text.Json;

namespace SunttelTradePointB.Client.Services
{
    public class CustomStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService api;
        private CurrentUser _currentUser;

        public CustomStateProvider(IAuthService api)
        {
            this.api = api;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userInfo = await GetCurrentUser();


                if (userInfo.IsAuthenticated)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, _currentUser.UserName),
                        new Claim("SkingImage", "test"),


                    }.Concat(_currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));


                    identity = new ClaimsIdentity(claims, "Server authentication");



                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task<CurrentUser> GetCurrentUser()
        {
            if (_currentUser != null && _currentUser.IsAuthenticated) return _currentUser;
            _currentUser = await api.CurrentUserInfo();

            UIClientGlobalVariables.UserName = _currentUser.UserName;
            UIClientGlobalVariables.CurrentUserSquads = _currentUser.MySquads;

            var defaultSquadUserId = _currentUser.LastSquadId;

            var EntityUserId = _currentUser.EntityId;

            UIClientGlobalVariables.ActiveSquad = _currentUser.MySquads.Where(s => s.ID == Guid.Parse(defaultSquadUserId)).FirstOrDefault();

            UIClientGlobalVariables.EntityUserId = EntityUserId ?? "";

            UIClientGlobalVariables.UserSkinImage = _currentUser.SkinImageName;



            return _currentUser;
        }
        public async Task Logout()
        {
            await api.Logout();
            _currentUser = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task Login(LoginRequest loginParameters)
        {
            await api.Login(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task Register(RegisterRequest registerParameters)
        {
            await api.Register(registerParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task RegisterUserByAdmin(RegisterRequest registerParameters)
        {
            await api.RegisterUserByAdmin(registerParameters);
        }

        public async Task RegisterRole(UserRole role)
        {
            await api.RegisterRole(role);
        }
        
        public async Task EditRoleSystemTools(UserRole role)
        {
            await api.EditRoleSystemTools(role);
        }

        public async Task<List<UserEntity>> GetUsersByRolname(string rolname)
        {
            return await api.GetUsersByRolname(rolname);
        }

        public async Task<List<UserRole>?> GetRoles()
        {
            return await api.GetRoles();
        }

        public async Task<List<SystemTool>?> GetSystemTools()
        {
            return await api.GetSystemsTools();
        }

        public async Task DeleteUser(string id)
        {
            await api.DeleteUser(id);
        }

        public async Task DeleteRole(string id)
        {
            await api.DeleteRole(id);
        }

        public async Task<UserEntity> GetUserById(string id)
        {
            return await api.GetUserById(id);
        }

        public async Task<UserRole?> GetRoleById(string id)
        {
            return await api.GetRoleById(id);
        }

        public async Task EditUserByAdmin(RegisterRequest registerParameters)
        {
            await api.EditUserByAdmin(registerParameters);
        }
    }
}
