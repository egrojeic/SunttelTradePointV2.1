using SunttelTradePointB.Shared.Communications;

namespace SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces
{
    public interface ICommunicationChat
    {
        Task<ChannelCommunicationsGroup> GetChannelCommunicationsGroupById(string channelId);
        Task<List<ChannelCommunicationsGroup>> GetChannelCommunicationsGroups();
        Task<bool> SaveChannelCommunicationsGroup(ChannelCommunicationsGroup channelCommunicationsGroup);

    }
}
