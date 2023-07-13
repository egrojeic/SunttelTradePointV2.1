
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using SunttelTradePointB.Server.Data;
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
        private readonly RoleManager<UserRole> _roleManager;
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
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserTracking userTracking, IActorsNodes entityNodes, ISquadBack squad, RoleManager<UserRole> roleManager)
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

            if (user.DefaultSquadId != defaultSquad)
            {
                user.DefaultSquadId = defaultSquad;
                await _userManager.UpdateAsync(user);
            }

            try
            {
                await _signInManager.SignInAsync(user, request.RememberMe);

            }catch(Exception ex)
            {
                string errDesc = ex.Message;
            }





            return Ok();
        }

        private async Task<(string entityId, string skinImage)> CheckUserEntity(string userId, string userName)
        {
            var response = await _entityNodes.GetEntityActorByUserId(userId, "127.0.0.1", userName);

            if (response.IsSuccess)
            {
                return (response.Item2.entityId, response.Item2.skinImage);
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

                if (responseCreation.IsSuccess && responseCreation.entityActorResponse != null)
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
        /// <param name="userRole"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterUserRole([FromBody] UserRole userRole)
        {
            try
            {
                userRole.Id = ObjectId.GenerateNewId().ToString();
                var result = await _roleManager.CreateAsync(new UserRole
                {
                    Id = userRole.Id,
                    Name = userRole.Name,
                    NormalizedName = userRole.Name?.ToUpper(),
                    SquadsId = userRole.SquadsId
                });;

                if (result.Succeeded)
                {

                    bool resp = await _squad.RegisterRoleSystemTools(userRole);
                    if (!resp) return BadRequest("Error Saving System Tools for The Role");
                    return Ok();
                }
                else
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
        /// Get permissions for the roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSystemTools()
        {
            try
            {
                List<SystemTool> tools = await _squad.GetSystemTools();
                return Ok(tools);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                if (parameters.Roles != null && parameters.Roles.Count > 0)
                {
                    foreach (var role in parameters.Roles)
                    {
                        var result2 = await _userManager.AddToRoleAsync(user, role.Name);
                        if (!result2.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("EditUser")]
        public async Task<IActionResult> EditUser([FromBody]RegisterRequest parameters, [FromQuery] string userId, [FromQuery] string ipAdress)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(parameters.Id);
                if (user is null) return BadRequest("User not found");
                user.UserName = parameters.UserName;
                //user.UserType = parameters.UserType;
                user.Email = parameters.Email;
                //user.EntityID = parameters.EntityId;

                IdentityResult resp = await _userManager.UpdateAsync(user);
                if (resp.Succeeded)
                {
                    if (parameters.Password != "NOTCHANGE")
                    {
                        bool passwordUpdated = await ResetPassword(user, parameters.Password);

                        if (!passwordUpdated) return BadRequest("Error Updating password");

                    }

                    if (parameters.Roles.Any())
                    {
                        bool rolesUpdated = await UpdateRoles(parameters);

                        if (!rolesUpdated) return BadRequest("Error Updating roles");

                    }

                    return Ok();
                }
                else
                {
                    return BadRequest("Error trying to update user");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> ResetPassword(ApplicationUser user, string password)
        {
            try
            {
                var newPassword = _userManager.PasswordHasher.HashPassword(user!, password);
                user.PasswordHash = newPassword;
                IdentityResult resp = await _userManager.UpdateAsync(user);
                if (!resp.Succeeded) return false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> UpdateRoles(RegisterRequest parameters)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(parameters.Id);
                if(user is null) return false;

                // Eliminar el rol anterior
                var rolesAnteriores = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, rolesAnteriores);

                foreach (UserRole rol in parameters.Roles)
                {
                    // Agregar el nuevo rol
                    await _userManager.AddToRoleAsync(user, rol.Name);

                }

                // Guardar los cambios en la base de datos
                IdentityResult resp = await _userManager.UpdateAsync(user);
                if (!resp.Succeeded) return false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
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
                var usersInRole = await _userManager.Users
                    .Where(user => user.Active)
                    .Select(user => new UserEntity
                    {
                        Id = user.Id,
                        Name = user.UserName,
                        Email = user.Email
                    })
                    .ToListAsync();

                return Ok(usersInRole);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] string id)
        {
            try
            {
                var UserDB = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (UserDB is null) return BadRequest("User not found");

                UserEntity user = new UserEntity()
                {
                    Id = UserDB.Id,
                    Name = UserDB.UserName,
                    Email = UserDB.Email
                };

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update existing User rol
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("EditRoleSystemTools")]
        public async Task<IActionResult> EditRoleSystemTools([FromBody] UserRole userRole)
        {
            try
            {
                // Buscamos el rol existente
                var existingRole = await _roleManager.FindByIdAsync(userRole.Id);
                if (existingRole == null)
                {
                    return BadRequest("Rol not found");
                }

                // Actualizamos el nombre del rol
                existingRole.Name = userRole.Name;
                var updateResult = await _roleManager.UpdateAsync(existingRole);
                if (!updateResult.Succeeded)
                {
                    return BadRequest(updateResult.Errors.FirstOrDefault()?.Description);
                }

                bool SystemToolsUpdated = await _squad.UpdateRoleSystemTools(userRole);

                if (!SystemToolsUpdated) return BadRequest("Error Updating System tools relation");
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete existing User rol
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteRole")]
        public async Task<IActionResult> DeleteRole([FromQuery] string id)
        {
            try
            {
                // Buscamos el rol existente
                var existingRole = await _roleManager.FindByIdAsync(id);
                if (existingRole == null)
                {
                    return BadRequest("Rol not found");
                }

                // Borramos el rol
                var deleteResult = await _roleManager.DeleteAsync(existingRole);
                if (!deleteResult.Succeeded)
                {
                    return BadRequest(deleteResult.Errors.FirstOrDefault()?.Description);
                }

                

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Get Role By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRoleById")]
        public async Task<IActionResult> GetRoleById([FromQuery] string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return NotFound();

                UserRole rolDB = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);

                if (rolDB is null) return BadRequest(" not found");

                UserRole rol = new UserRole
                {
                    Id = rolDB.Id,
                    Name = rolDB.Name,
                    SystemTools = await _squad.SystemToolsByRole(id)
                }; 

                return Ok(rol);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get Roles By User Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRolesByUserId")]
        public async Task<IActionResult> GetRolesByUserId([FromQuery] string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(roles);
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
            try
            {

                List<Squad> squads = new List<Squad>();
                string LastSquadId = "";
                string EntityIdUser = "";
                string skinImage = "";

                if (User != null && User.Identity != null && User.Identity.Name != null)
                {
                    squads = await _squad.SquadInfo(User.Identity.Name);
                    var userInfo = await _userManager.FindByNameAsync(User.Identity.Name);

                    LastSquadId = (userInfo != null && userInfo.DefaultSquadId != null) ? userInfo.DefaultSquadId : "";
                    EntityIdUser = (userInfo != null && userInfo.EntityID != null) ? userInfo.EntityID : "";

                    var response = await _entityNodes.GetEntityActorByUserId("sys", "127.0.0.1", User.Identity.Name);
                    skinImage = response.IsSuccess ? response.Item2.skinImage : "";

                }

                System.Security.Claims.ClaimsPrincipal curuser = this.User;

                var idcurrentuser = _userManager.GetUserId(curuser);

                // Avoid sending duplicate keys
                var claimsDictionary = User.Claims.GroupBy(c => c.Type)
                                  .ToDictionary(g => g.Key, g => g.First().Value);


                return Ok(new CurrentUser
                {
                    IDUser = idcurrentuser,
                    IsAuthenticated = User.Identity.IsAuthenticated,
                    UserName = User.Identity.Name,
                    MySquads = squads,
                    LastSquadId = LastSquadId,
                    EntityId = EntityIdUser,
                    SkinImageName = skinImage,
                    Claims = claimsDictionary


                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Get the list of roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                List<UserRole> roles = await _roleManager.Roles.ToListAsync();
              

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] string id)
        {
            try
            {
                ApplicationUser? user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user is null) return BadRequest("User not found");
                user.Active = false;
                IdentityResult response = await _userManager.UpdateAsync(user);
                if (response.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(response.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
