using MongoDB.Bson;
using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.Communications;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Communications;

namespace SunttelTradePointB.Server.Services.Communications
{

    /// <summary>
    /// Implements methods to deal with Messages in the system
    /// </summary>
    public class MessageValet : IMessagesValet
    {

        IMongoCollection<CommunicationsMessage> _messagedb;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        public MessageValet(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _messagedb = mongoDatabase.GetCollection<CommunicationsMessage>("CommunicationsMessages");


        }


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

        /// <summary>
        /// Saves (Insert/Update) a message
        /// </summary>
        /// <param name="communicationsMessage"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, CommunicationsMessage communicationsMessage, string? ErrorDescription)> SaveMessage(CommunicationsMessage communicationsMessage)
        {
            try
            {
                if (communicationsMessage.Id == null)
                    communicationsMessage.Id = ObjectId.GenerateNewId().ToString();

                var filterCommunicationsMessage = Builders<CommunicationsMessage>.Filter.Eq("_id", new ObjectId(communicationsMessage.Id));
                var updateCommunicationsMessage = new ReplaceOptions { IsUpsert = true };
                var resultTransactionalItemType = await _messagedb.ReplaceOneAsync(filterCommunicationsMessage, communicationsMessage, updateCommunicationsMessage);

              return (true, communicationsMessage, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
    }
}
