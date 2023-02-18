using SunttelTradePointB.Server.Interfaces.Communications;
using SunttelTradePointB.Shared.Communications;

namespace SunttelTradePointB.Server.Services.Communications
{

    /// <summary>
    /// Implements methods to deal with Messages in the system
    /// </summary>
    public class MessageValet : IMessagesValet
    {

        /// <summary>
        /// Retrieves a list of messages sent or received by an entity 
        /// filtered by its date and an optional filter sentence
        /// </summary>
        /// <param name="messageReaction"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<(bool IsSuccess, MessageReaction messageReaction, string? ErrorDescription)> AddMessageReaction(MessageReaction messageReaction)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="startingDate"></param>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<(bool IsSuccess, List<CommunicationsMessage>? communicationsMessages, string? ErrorDescription)> GetMessagesOfAnEntity(string entityId, DateTime startingDate, string? filterCriteria = "")
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, CommunicationsMessage communicationsMessage, string? ErrorDescription)> SaveMessage(CommunicationsMessage communicationsMessage)
        {
            throw new NotImplementedException();
        }
    }
}
