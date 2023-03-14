using SunttelTradePointB.Shared.Communications;

namespace SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces
{
    public interface ICommunicationChat
    {
        Task<ChannelCommunicationsGroup> GetChannelCommunicationsGroupById(string channelId);
        Task<List<ChannelCommunicationsGroup>> GetChannelCommunicationsGroups();
        Task<bool> SaveChannelCommunicationsGroup(ChannelCommunicationsGroup channelCommunicationsGroup);

        /// <summary>
        /// Retrives a list with CommunicationsMessage Items meeting search criteria
        /// </summary>
        /// <param name="channelCommunicationGroupId"></param>
        /// <param name="startingDate"></param>
        /// <param name="filterCriteria"></param>       
        /// <returns></returns>
        Task<List<CommunicationsMessage>> GetMessagesOfAnEntity(string channelCommunicationGroupId, string? startingDate = null, string? filterCriteria = null);
    }
}
