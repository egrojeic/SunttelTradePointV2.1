using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Client.Shared.EntityShareComponents.RelatedConcepts;
using SunttelTradePointB.Server.Interfaces.Communications;
using SunttelTradePointB.Server.Interfaces.UserTracking;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Communications;
using SunttelTradePointB.Shared.Security;
using Syncfusion.PdfExport;

namespace SunttelTradePointB.Server.Services.UserTracking
{

    /// <summary>
    /// Class implementig IUserTracking
    /// </summary>
    public class UserTrackingService : IUserTracking
    {


        IMongoCollection<UserActivity> _userctivityByController;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        public UserTrackingService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _userctivityByController = mongoDatabase.GetCollection<UserActivity>("UserActivityByController");
         

        }

        /// <summary>
        /// INSERT USER ACTIVITY LOG BY CONTOLLER
        /// </summary>
        /// <param name="userActivity"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, UserActivity? userActivity, string? ErrorDescription)> SaveUserActivityByController(UserActivity userActivity)
        {
            try
            {
                await _userctivityByController.InsertOneAsync(userActivity);
                return (true, userActivity, null);
            }
            catch(Exception e)
            {
                return (false, null, e.Message);
            }
            
        }
    }
}
