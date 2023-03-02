using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Communications;
using SunttelTradePointB.Shared.Common;
using System.Net;
using System.Net.Http.Json;

namespace SunttelTradePointB.Client.Services.MasterTablesServices
{
    public class CommunicationService: ICommunicationChat
    {
        private readonly HttpClient _httpClient;
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
                if(ipAddress == null) 
                {
                    ipAddress = "127.0.0.0";
                }
                var channel = await _httpClient.GetFromJsonAsync<ChannelCommunicationsGroup>($"/api/CommunicationsManagement/GetChannelCommunicationsGroupById?userId={userId}&ipAdress={ipAddress}&channelCommunicationsGroupId={channelId}");
                return channel;
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
                var listGroups = await _httpClient.GetFromJsonAsync<List<ChannelCommunicationsGroup>>($"/api/CommunicationsManagement/GetChannelCommunicationsGroups?userId={userId}&ipAdress={ipAddress}");
                return listGroups;
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
                if(ipAddress == null)
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


    }
}
