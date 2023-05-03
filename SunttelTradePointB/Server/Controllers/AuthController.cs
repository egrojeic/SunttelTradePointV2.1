using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SunttelTradePointB.Client.Interfaces.SquadInterfaces;
using SunttelTradePointB.Server.Interfaces;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Server.Interfaces.UserTracking;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Enums;
using SunttelTradePointB.Shared.Models;
using SunttelTradePointB.Shared.Security;
using SunttelTradePointB.Shared.SquadsMgr;
using Syncfusion.Blazor.RichTextEditor;

namespace SunttelTradePointB.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
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
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserTracking userTracking, IActorsNodes entityNodes, ISquadBack squad, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userTracking = userTracking;
            _entityNodes = entityNodes;
            _squad = squad;
            _roleManager = roleManager;
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
            var defaultSquad = await _squad.GetSquadIdByName(Guid.Parse(user.Id), request.SquadName);

            if(user.DefaultSquadId != defaultSquad)
            {
                user.DefaultSquadId = defaultSquad;
                await _userManager.UpdateAsync(user);
            }
            

            await _signInManager.SignInAsync(user, request.RememberMe);

           


            return Ok();
        }

        private async Task<(string entityId, string skinImage)> CheckUserEntity(string userId, string userName)
        {
            var response = await _entityNodes.GetEntityActorByUserId(userId, "127.0.0.1", userName);
            
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
            user.EntityID = parameters.EntityId;

            var result = await _userManager.CreateAsync(user, parameters.Password);
            if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

            
            return await Login(new LoginRequest
            {
                UserName = parameters.UserName,
                Password = parameters.Password
                 
            });
        }

        /// <summary>
        /// Register new User rol
        /// </summary>
        /// <param name="rolname"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterUserRol(string rolname)
        {
            try
            {
                var result = await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = rolname
                });

                if (result.Succeeded)
                {
                    return Ok();
                } else
                {
                    return BadRequest(result.Errors.FirstOrDefault()?.Description);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Register user with rol
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RegisterUser(RegisterRequest parameters)
        {
            try
            {
                var user = new ApplicationUser();
                user.UserName = parameters.UserName;
                //user.UserType = parameters.UserType;
                user.Email = parameters.Email;
                //user.EntityID = parameters.EntityId;

                var result = await _userManager.CreateAsync(user, parameters.Password);
                if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);
                var result2 = await _userManager.AddToRoleAsync(user, parameters.Rolname);
                if (!result2.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get a list of users by rolname
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsersByRol()
        {
            try
            {
                string rolname = "User";
                var list = await _userManager.GetUsersInRoleAsync(rolname);

                List<UserEntity> users = new List<UserEntity>();

                for (int i = 0; i < list.Count; i++)
                {
                    users.Add(new UserEntity()
                    {
                        Id = list[i].Id,
                        Name = list[i].UserName,
                        Email = list[i].Email
                    });
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

            List<SquadsByUser> squads = new List<SquadsByUser>();
            string LastSquadId = "";
            string EntityIdUser = "";

            if (User != null && User.Identity !=null && User.Identity.Name != null)
            {
                squads = await _squad.SquadInfo(User.Identity.Name);
                var userInfo = await _userManager.FindByNameAsync(User.Identity.Name);

                LastSquadId = (userInfo != null && userInfo.DefaultSquadId != null) ? userInfo.DefaultSquadId:"";
                EntityIdUser = (userInfo != null && userInfo.EntityID != null) ? userInfo.EntityID : "";
            }

            var response = await _entityNodes.GetEntityActorByUserId("sys", "127.0.0.1", User.Identity.Name);
            var skinImage = response.IsSuccess ? response.Item2.skinImage : "";



            return Ok(new CurrentUser
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                MySquads = squads,
                LastSquadId = LastSquadId,
                EntityId = EntityIdUser,
                SkinImageName = skinImage,
                Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
                

            });
        }


        /// <summary>
        /// Get a list of roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                List<IdentityRole> list = await _roleManager.Roles.ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRoles")]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                List<IdentityRole> list = await _roleManager.Roles.ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IdentityResult> DeleteUser(User user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IList<User>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

    }

}
