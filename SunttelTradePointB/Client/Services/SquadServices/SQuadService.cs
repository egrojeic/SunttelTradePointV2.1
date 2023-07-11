
using SunttelTradePointB.Client.Interfaces.SquadInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Security;
using SunttelTradePointB.Shared.SquadsMgr;

using System.Net.Http.Json;

namespace SunttelTradePointB.Client.Services.SquadServices
{
    public class SQuadService : ISquad
    {

        private readonly HttpClient _httpClient;

        public SQuadService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<Squad> GetSquad(Guid squadId)
        {
            var result = await _httpClient.GetFromJsonAsync<Squad>($"api/squad/GetSquad/{squadId}");

            return result;
        }


        public async Task<(bool IsSuccess, Squad? squad, string? ErrorDescription)> SaveSquad(Squad squad)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                //var result = await _httpClient.PostAsJsonAsync<Squad>($"api/Squad/SaveSquad?userId={userId}&ipAdress={ipAddress}", squad);
                var result = await _httpClient.PostAsJsonAsync<Squad>($"api/Squad/SaveSquad/{userId}/{ipAddress}", squad);


                if (result.Content != null)
                {
                    var item = await result.Content.ReadFromJsonAsync<Squad>();

                    return (result.IsSuccessStatusCode, item, null);

                }
                else
                {
                    return (false, null, "Saving Error");
                }
            }
            catch(Exception ex)
            {
                return (false, null, ex.Message);
            }
            


            
        }

        public async Task<List<SquadsByUser>> SquadInfo(string userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<SquadsByUser>>($"api/squad/GetSquadInfo/{userId}");

            return result;

        }

        public async Task<List<SystemTool>> SystemToolsByUser(Guid userId)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<SystemTool>>($"api/Squad/GetSystemTools/{userId}/{UIClientGlobalVariables.ActiveSquad.ID}");
                return result;
            }
            catch
            {

                 return null;

            }
        }
    }
}
