using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Client.Shared.EntityShareComponents.RelatedConcepts;
using SunttelTradePointB.Server.Interfaces.Communications;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Communications;
using Syncfusion.PdfExport;

namespace SunttelTradePointB.Server.Services.Communications
{

    /// <summary>
    /// Implements methods to deal with Messages in the system
    /// </summary>
    public class MessageValet : IMessagesValet
    {

        IMongoCollection<CommunicationsMessage> _messagedb;
        IMongoCollection<ChannelCommunicationsGroup> _communicationChannelGroup;

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
            _communicationChannelGroup = mongoDatabase.GetCollection<ChannelCommunicationsGroup>("CommunicationsChannels");


        }


        

        /// <summary>
        /// Retrieves a particular communication channel group
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="channelCommunicationsGroupId"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, ChannelCommunicationsGroup? channelCommunicationsGroup, string? ErrorDescription)> GetChannelCommunicationsGroupById(string userId, string ipAdress, string channelCommunicationsGroupId)
        {
            try
            {
                var filter = Builders<ChannelCommunicationsGroup>.Filter.Eq(x => x.Id, channelCommunicationsGroupId);
                var result = await _communicationChannelGroup.Find(filter).FirstOrDefaultAsync();

                if (result == null)
                {
                    return (false, null, "Record NOT Found");
                }
                else
                {
                    return (true, result, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Retrieves all communication channels relevant for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<ChannelCommunicationsGroup>? channelCommunicationsGroups, string? ErrorDescription)> GetChannelCommunicationsGroups(string userId, string ipAdress)
        {
            try
            {
                string strNameFiler = ""; // entityId == null ? "" : entityId;

                var pipeline = new List<BsonDocument>();

                //pipeline.Add(
                //    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(strNameFiler)))
                //);

               
               

                pipeline.Add(
                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "SkinImageName", 1 },
                                { "Name", 1 }
                            }
                        }
                    }
                );

                List<ChannelCommunicationsGroup> results = await _communicationChannelGroup.Aggregate<ChannelCommunicationsGroup>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }



        /// <summary>
        /// Retrieves a list of messages sent or received by an entity 
        /// filtered by its date and an optional filter sentence
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="startingDate"></param>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<CommunicationsMessage>? communicationsMessages, string? ErrorDescription)> GetMessagesOfAnEntity(string entityId, string ipAdress, DateTime? startingDate = null, string? filterCriteria = "")
        {
            try
            {
                string strNameFiler = filterCriteria == null ? "" : filterCriteria;
               
                var pipeline = new List<BsonDocument>();

                if (!(strNameFiler.ToLower() == "all" || strNameFiler.ToLower() == "todos") && strNameFiler !="")
                {
                    pipeline.Add(
                    new BsonDocument{
                        { "$match",  new BsonDocument {
                            { "$text",
                                new BsonDocument {
                                    { "$search",strNameFiler },
                                    { "$language","english" },
                                    { "$caseSensitive",false },
                                    { "$diacriticSensitive",false }
                                }
                            }
                        }
                        }
                    }
                );
                }

                DateTime startDate = startingDate?? DateTime.Now.AddDays(-1);

                pipeline.Add(
                        new BsonDocument(
                            "$match",
                            new BsonDocument("SendDateTime",
                            new BsonDocument("$gt", new BsonDateTime(startDate)))

                        ));
               

                List<CommunicationsMessage> results = await _messagedb.Aggregate<CommunicationsMessage>(pipeline).ToListAsync();

                //var results = await _entityActorsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Saves (INSERT/UPDATE) a communication channel group
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="channelCommunicationsGroup"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, ChannelCommunicationsGroup? channelCommunicationsGroup, string? ErrorDescription)> SaveChannelCommunicationsGroup(string userId, string ipAdress, ChannelCommunicationsGroup channelCommunicationsGroup)
        {
            try
            {
                if (channelCommunicationsGroup.Id == null)
                    channelCommunicationsGroup.Id = ObjectId.GenerateNewId().ToString();

                var filter = Builders<ChannelCommunicationsGroup>.Filter.Eq("_id", new ObjectId(channelCommunicationsGroup.Id));
                var update = new ReplaceOptions { IsUpsert = true };
                var result = await _communicationChannelGroup.ReplaceOneAsync(filter, channelCommunicationsGroup, update);

                return (true, channelCommunicationsGroup, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
            
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
                {
                    communicationsMessage.Id = ObjectId.GenerateNewId().ToString();
                }

                communicationsMessage.SendDateTime=DateTime.UtcNow;

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
