using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces;
using SunttelTradePointB.Shared.Security;
using SunttelTradePointB.Shared.SquadsMgr;

namespace SunttelTradePointB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquadController : ControllerBase
    {

        ISquadBack _squad;

        public SquadController(ISquadBack squad)
        {
            _squad = squad;
        }


        [HttpGet("GetSquadInfo/{userId}")]
        public async Task<IActionResult> GetSquadInfo(string userId)
        {
            var response = await _squad.SquadInfo(userId);
            return Ok(response);
        }


        [HttpGet("GetSystemTools/{userId}")]
        public async Task<IActionResult> GetSystemTools(Guid userId)
        {
            var response = await _squad.SystemToolsByUser(userId);
            return Ok(response);
        }



        /// <summary>
        /// Retrieves the Squad by id  
        /// </summary>
        /// <param name="squadId"></param>
        /// <returns></returns>

        [HttpGet("GetSquad/{squadId}")]
        public async Task<IActionResult> GetSquad(Guid squadId)
        {
            var response = await _squad.GetSquad(squadId.ToString());
            return Ok(response);
        }


        /// <summary>
        /// Saves an Squad
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="squad"></param>
        /// <returns></returns>
        [HttpPost("SaveSquad/{userId}/{ipAdress}")]
        public async Task<IActionResult> SaveSquad(string userId, string ipAdress, Squad squad)
        {
            var response = await _squad.SaveSquad(userId, ipAdress, squad);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
                return NotFound(response);
        }


    }
}
