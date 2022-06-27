using System;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SpotiMatch.Logic.Services.Interfaces;
using SpotiMatch.Shared.Dtos;

namespace SpotiMatch.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            ClaimsPrincipal currentUser = HttpContext.User;

            if (!currentUser.HasClaim(c => c.Type == "Id"))
            {
                return StatusCode(400);
            }

            int userId = Int32.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);

            UserDto user = await UserService.GetUser(userId, HttpContext.RequestAborted);

            if (user == null)
            {
                NotFound();
            }

            return Ok(user);
        }
    }
}
