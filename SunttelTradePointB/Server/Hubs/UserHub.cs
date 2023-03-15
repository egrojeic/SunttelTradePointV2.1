using Microsoft.AspNetCore.SignalR;
using SunttelTradePointB.Server.Interfaces.Communications;
using SunttelTradePointB.Server.Services.Communications;
using SunttelTradePointB.Shared.Communications;

namespace SunttelTradePointB.Server.Hubs
{

    /// <summary>
    /// Author: Jorge Isaza
    /// Creation Date: 02/05/2023
    /// 
    /// Hub to centralize communications 
    /// </summary>
    public class UserHub: Hub
    {

        private IMessagesValet _messagesValet;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messagesValet"></param>
        public UserHub(IMessagesValet messagesValet)
        {
            _messagesValet = messagesValet;
        }

        /// <summary>
        /// Number of users connected
        /// </summary>
        public static int ConnectedUsers { get; set; }


        /// <summary>
        /// Updates the number of users connected
        /// </summary>
        /// <returns></returns>
        public async Task NotifyUserConnected()
        {
            try
            {
                await Clients.All.SendAsync("updateConnectedUsers", ConnectedUsers.ToString());

            }
            catch(Exception e) {
            
                string message = e.Message; 
            }
        }

        /// <summary>
        /// Overrides method to control when a user is connected
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            // Add your own code here.
            // For example: in a chat application, record the association between
            // the current connection ID and user name, and mark the user as online.
            // After the code in this method completes, the client is informed that
            // the connection is established; for example, in a JavaScript client,
            // the start().done callback is executed.
            await Groups.AddToGroupAsync(Context.ConnectionId, "General");
            ConnectedUsers++;

            string usName = Context.UserIdentifier;

            await NotifyUserConnected();

            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Overrides method to control when a user is disconnected
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ConnectedUsers--;
            await NotifyUserConnected();

            await base.OnDisconnectedAsync(exception);
        }


        /// <summary>
        /// A user un-registered sends a message (Just for testing)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, CommunicationsMessage message)
        {
            try
            {
                await _messagesValet.SaveMessage(message);

                //await _messagesValet.SaveMessage(new CommunicationsMessage
                //{
                //    Message = message,
                //    MessageTypeId = CommunicationsMessageType.ChatMessage
                //}); 

                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
