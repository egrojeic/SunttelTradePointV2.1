using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Server.Controllers.MasterTablesCtrl;
using SunttelTradePointB.Server.Interfaces.Communications;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Communications;

namespace SunttelTradePointB.Server.Controllers.Communications
{

    /// <summary>
    /// Controller to deal with communications
    /// </summary>
    [Tags("Communications Management")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommunicationsManagementController : ControllerBase
    {

        private IMessagesValet  _messagesValet;
        private readonly ILogger<CommunicationsManagementController> _logger;
        IConfiguration config;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="_config"></param>
        /// <param name="messagesValet"></param>
        public CommunicationsManagementController(ILogger<CommunicationsManagementController> logger, IConfiguration _config, IMessagesValet messagesValet)
        {
            _logger = logger;
            config = _config;
            _messagesValet = messagesValet;
        }

        /// <summary>
        /// Saves (INSERT/UPDATE) a communication channel group
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="channelCommunicationsGroup"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveChannelCommunicationsGroup")]
        public async Task<IActionResult> SaveChannelCommunicationsGroup(string userId, string ipAdress, ChannelCommunicationsGroup channelCommunicationsGroup)
        {
            var response = await _messagesValet.SaveChannelCommunicationsGroup(userId, ipAdress, channelCommunicationsGroup);

            if (response.IsSuccess)
            {
                return Ok(response.channelCommunicationsGroup);
            }
            else
            {
                return NotFound(response.ErrorDescription);
            }
        }

        /// <summary>
        /// Retrieves a particular communication channel group
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="channelCommunicationsGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetChannelCommunicationsGroupById")]
        public async Task<IActionResult> GetChannelCommunicationsGroupById(string userId, string ipAdress, string channelCommunicationsGroupId)
        {

            var response = await _messagesValet.GetChannelCommunicationsGroupById(userId, ipAdress, channelCommunicationsGroupId);

            if (response.IsSuccess)
            {
                return Ok(response.channelCommunicationsGroup);
            }
            else
                return NotFound(response.ErrorDescription);

        }


        /// <summary>
        /// Retrieves all communication channels relevant for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetChannelCommunicationsGroups")]
        public async Task<IActionResult> GetChannelCommunicationsGroups(string userId, string ipAdress)
        {

            var response = await _messagesValet.GetChannelCommunicationsGroups(userId, ipAdress);

            if (response.IsSuccess)
            {
                return Ok(response.channelCommunicationsGroups);
            }
            else
                return NotFound(response.ErrorDescription);

        }


    }
}
