
using SunttelTradePointB.Client.Interfaces.SquadInterfaces;
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
        public async Task<List<SquadsByUser>> SquadInfo(string userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<SquadsByUser>>($"api/squad/GetSquadInfo/{userId}");

            return result;

        }

        public async Task<List<SystemTool>> SystemToolsByUser(Guid userId)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<SystemTool>>($"api/Squad/GetSystemTools/{userId}");
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo.
                return result;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo.
            }
            catch
            {
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo.
                return null;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo.
            }
        }
    }
}
