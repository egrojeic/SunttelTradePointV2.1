using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Communications;
using SunttelTradePointB.Shared.Common;
using System.Net;
using System.Net.Http.Json;

namespace SunttelTradePointB.Client.Services.MasterTablesServices
{
    public class CommunicationService : ICommunicationChat
    {
        private readonly HttpClient _httpClient;
        public List<CommunicationsMessage> communicationsMessages { get; set; } = new();
        public CommunicationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        ///Communications Management 
        ///

        ///Gets
        public async Task<ChannelCommunicationsGroup> GetChannelCommunicationsGroupById(string channelId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == null)
                {
                    ipAddress = "127.0.0.0";
                }

                var responseMessage = await Gethttp($"/api/CommunicationsManagement/GetChannelCommunicationsGroupById?userId={userId}&ipAdress={ipAddress}&channelCommunicationsGroupId={channelId}");
                var item = await responseMessage.Content.ReadFromJsonAsync<ChannelCommunicationsGroup>();

                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<ChannelCommunicationsGroup>> GetChannelCommunicationsGroups()
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == null)
                {
                    ipAddress = "127.0.0.0";
                }

                var responseMessage = await Gethttp($"/api/CommunicationsManagement/GetChannelCommunicationsGroups?userId={userId}&ipAdress={ipAddress}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<ChannelCommunicationsGroup>>();


                return list;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<List<CommunicationsMessage>> GetMessagesOfAnEntity(string channelCommunicationGroupId, string? startingDate = null, string? filterCriteria = null)
        {

            try
            {
                HttpResponseMessage responseMessage = null;
                if (startingDate == null && filterCriteria == null) responseMessage = await Gethttp($"/api/CommunicationsManagement/GetMessagesOfAnEntity?userId={UIClientGlobalVariables.UserId}&ipAdress={UIClientGlobalVariables.PublicIpAddress}&channelCommunicationGroupId={channelCommunicationGroupId}");
                if (startingDate != null && filterCriteria != null) responseMessage = await Gethttp($"/api/CommunicationsManagement/GetMessagesOfAnEntity?userId={UIClientGlobalVariables.UserId}&ipAdress={UIClientGlobalVariables.PublicIpAddress}&channelCommunicationGroupId={channelCommunicationGroupId}&filterCriteria={filterCriteria}&startingDate={startingDate}");
                if (startingDate != null && filterCriteria == null) responseMessage = await Gethttp($"/api/CommunicationsManagement/GetMessagesOfAnEntity?userId={UIClientGlobalVariables.UserId}&ipAdress={UIClientGlobalVariables.PublicIpAddress}&channelCommunicationGroupId={channelCommunicationGroupId}&filterCriteria={filterCriteria}");
                if (startingDate == null && filterCriteria != null) responseMessage = await Gethttp($"/api/CommunicationsManagement/GetMessagesOfAnEntity?userId={UIClientGlobalVariables.UserId}&ipAdress={UIClientGlobalVariables.PublicIpAddress}&channelCommunicationGroupId={channelCommunicationGroupId}&startingDate={startingDate}");
                var list = await responseMessage.Content.ReadFromJsonAsync<List<CommunicationsMessage>>();

                return list;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        ///Post
        public async Task<bool> SaveChannelCommunicationsGroup(ChannelCommunicationsGroup channelCommunicationsGroup)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == null)
                {
                    ipAddress = "127.0.0.0";
                }
                var result = await _httpClient.PostAsJsonAsync($"/api/CommunicationsManagement/SaveChannelCommunicationsGroup?userId={userId}&ipAdress={ipAddress}", channelCommunicationsGroup);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

        public async Task<HttpResponseMessage> Gethttp(string Url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, Url);
                request.Headers.Add("SquadId", UIClientGlobalVariables.ActiveSquad.ID.ToString());

                var response = await _httpClient.SendAsync(request);

                System.Diagnostics.Debug.WriteLine(response.IsSuccessStatusCode);
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else { return null; }

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                System.Diagnostics.Debug.WriteLine(errMessage);

            }
            return null;

        }

    }
}
