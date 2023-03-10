using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SunttelTradePointB.Client.Interfaces.SquadInterfaces;
using SunttelTradePointB.Server.Interfaces;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Server.Interfaces.UserTracking;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Models;
using SunttelTradePointB.Shared.Security;
using SunttelTradePointB.Shared.SquadsMgr;

namespace SunttelTradePointB.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserTracking _userTracking;

        private ISquadBack _squad;
        private IActorsNodes _entityNodes;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="userTracking"></param>
        /// <param name="entityNodes"></param>
        /// <param name="squad"></param>
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserTracking userTracking, IActorsNodes entityNodes, ISquadBack squad)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userTracking = userTracking;
            _entityNodes = entityNodes;
            _squad = squad;



        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return BadRequest("User does not exist");
            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!singInResult.Succeeded) return BadRequest("Invalid password");


            var responseCheck = await CheckUserEntity(user.Id, user.UserName);

            user.SkingImage = responseCheck.skinImage;
            user.EntityID = responseCheck.entityId;

            await _signInManager.SignInAsync(user, request.RememberMe);

           


            return Ok();
        }

        private async Task<(string entityId, string skinImage)> CheckUserEntity(string userId, string userName)
        {
            var response = await _entityNodes.GetEntityActorByUserId(userId, "127.0.0.1", userId);
            
            if(response.IsSuccess)
            {
                return(response.Item2.entityId, response.Item2.skinImage);
            }
            else
            {
                EntityActor entityActor = new EntityActor
                {
                    SunttelUserId = userId,
                    Name = userName,
                    SkinImageName = ""
                };
                var responseCreation = await _entityNodes.SaveEntity(userId, "127.0.0.1", entityActor);
                
                if(responseCreation.IsSuccess && responseCreation.entityActorResponse != null)
                {
                    return (responseCreation.entityActorResponse.Id, "");
                }
                else
                {
                    return ("", "");
                }
                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest parameters)
        {
            var user = new ApplicationUser();
            user.UserName = parameters.UserName;
            var result = await _userManager.CreateAsync(user, parameters.Password);
            if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

            
            return await Login(new LoginRequest
            {
                UserName = parameters.UserName,
                Password = parameters.Password
                 
            });
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }


        [HttpGet]
        [ActionName("CurrentUserInfo")]
        public async Task<IActionResult> CurrentUserInfo()
        {

            var squads = await _squad.SquadInfo(User.Identity.Name);

            return Ok(new CurrentUser
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                MySquads = squads,
                Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
                

            });
        }


    }

}
