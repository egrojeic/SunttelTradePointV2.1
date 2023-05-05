using Microsoft.AspNet.Identity.EntityFramework;
using SunttelTradePointB.Shared.Security;
using SunttelTradePointB.Shared.SquadsMgr;
using System.Net.Http.Json;

namespace SunttelTradePointB.Client.Services
{

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private string basepath = "api/Auth/Name";

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CurrentUser> CurrentUserInfo()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<CurrentUser>("api/Auth/CurrentUserInfo");

                return result;
            }
            catch (Exception ex)
            {
                var errDesc = ex.Message;
                return null;
            }

        }

        public async Task<UserEntity?> GetUserById(string id)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<UserEntity>($"api/Auth/GetUserById?id={id}");
                return result;
            }
            catch (Exception ex)
            {
                var errDesc = ex.Message;
                return null;
            }

        }

        public async Task<UserRole?> GetRoleById(string id)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<UserRole>($"api/Auth/GetRoleById?id={id}");
                return result;
            }
            catch (Exception ex)
            {
                var errDesc = ex.Message;
                return null;
            }

        }

        public async Task Login(LoginRequest loginRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync("api/auth/logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegisterRequest registerRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task RegisterUserByAdmin(RegisterRequest registerRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/registerUser", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task RegisterRole(UserRole role)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/RegisterUserRole", role);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task EditUserByAdmin(RegisterRequest registerRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/EditUser", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task EditRoleSystemTools(UserRole registerRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/EditRoleSystemTools", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task DeleteUser(string id)
        {
            var result = await _httpClient.DeleteAsync($"api/auth/DeleteUser?id={id}");
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task DeleteRole(string id)
        {
            var result = await _httpClient.DeleteAsync($"api/auth/DeleteRole?id={id}");
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<List<UserRole>?> GetRoles()
        {
            try
            {
                string path = basepath.Replace("Name", "GetRoles");
                var responseMessage = await Gethttp($"{path}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<UserRole>>();
                return list != null ? list : new List<UserRole>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;
            }
        }

        public async Task<List<SystemTool>?> GetSystemsTools()
        {
            try
            {
                string path = basepath.Replace("Name", "GetSystemTools");
                var responseMessage = await Gethttp($"{path}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<SystemTool>>();
                return list != null ? list : new List<SystemTool>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;
            }
        }

        //public async Task<List<UserEntity>> GetUsersByRolname(string rolname)
        //{
        //    try
        //    {
        //        var result = await _httpClient.GetAsync("api/Auth/GetUsersByRol");

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        var errDesc = ex.Message;
        //        return null;
        //    }

        //}

        public async Task<List<UserEntity>> GetUsersByRolname(string rolname)
        {
            try
            {
                string path = basepath.Replace("Name", "GetUsersByRol");
                var responseMessage = await Gethttp($"{path}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<UserEntity>>();
                return list != null ? list : new List<UserEntity>();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<HttpResponseMessage> Gethttp(string Url)
        {
            try
            {

                var SquadId = UIClientGlobalVariables.ActiveSquad;
                var ReplaceIdUser = UIClientGlobalVariables.UserId;
                var ReplacePublicIpAddress = UIClientGlobalVariables.PublicIpAddress;

                //Url = Url.Replace("*Id", ReplaceIdUser ?? "000").Replace("*Ip", ReplacePublicIpAddress ?? "000");

                var request = new HttpRequestMessage(HttpMethod.Get, Url);

                if (SquadId != null) request.Headers.Add("SquadId", SquadId.IDSquads.ToString());
                if (SquadId == null) request.Headers.Add("SquadId", "0000000000");
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else { return null; }

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;

            }


        }


    }
}
