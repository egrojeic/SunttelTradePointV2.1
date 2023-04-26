
using SunttelTradePointB.Shared.Sales;
using SunttelTradePointB.Shared.Security;
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
