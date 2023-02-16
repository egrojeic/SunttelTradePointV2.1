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

        ISquad _squad;

        public SquadController(ISquad squad)
        {
            _squad = squad;
        }


        [HttpGet]
        public async Task<IActionResult> GetSquadInfo()
        {
            var response = await _squad.SquadInfo();
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
