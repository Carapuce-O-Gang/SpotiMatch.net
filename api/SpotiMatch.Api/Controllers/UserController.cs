using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            IEnumerable<UserDto> users = await UserService.GetUsers(HttpContext.RequestAborted);

            if (users == null || !users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {

            UserDto user = await UserService.GetUser(id, HttpContext.RequestAborted);

            if (user == null)
            {
                NotFound();
            }

            return Ok(user);
        }
    }
}
