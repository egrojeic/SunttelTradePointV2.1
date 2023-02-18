using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Communications;

namespace SunttelTradePointB.Server.Interfaces.Communications
{

    /// <summary>
    /// Defines the methods of a a class intended to deal with storage
    /// data od messages
    /// </summary>
    public interface IMessagesValet
    {

        /// <summary>
        /// Retrieves a list of messages sent or received by an entity 
        /// filtered by its date and an optional filter sentence
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="startingDate"></param>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, List<CommunicationsMessage>? communicationsMessages, string? ErrorDescription)> GetMessagesOfAnEntity(string entityId, DateTime startingDate, string? filterCriteria = "");


        /// <summary>
        /// Saves a message
        /// </summary>
        /// <param name="communicationsMessage"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, CommunicationsMessage communicationsMessage, string? ErrorDescription)> SaveMessage(CommunicationsMessage communicationsMessage);


        /// <summary>
        /// Adds a message reaction
        /// </summary>
        /// <param name="messageReaction"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, MessageReaction messageReaction, string? ErrorDescription)> AddMessageReaction(MessageReaction messageReaction);


    }
}
