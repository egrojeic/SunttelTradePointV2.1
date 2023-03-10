using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Server.Interfaces;
using SunttelTradePointB.Shared.Security;

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

        

    }
}
