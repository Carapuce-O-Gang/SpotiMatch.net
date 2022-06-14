using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotiMatch.Database.Entities;
using SpotiMatch.Logic.Services.Interfaces;
using SpotiMatch.Shared.Dtos;

namespace SpotiMatch.Api.Controllers
{
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

        [HttpPost]
        public async Task<ActionResult<UserDto>> Post(UserDto user)
        {
            UserDto addedUser = await UserService.AddUser(user, HttpContext.RequestAborted);
            
            if (addedUser == null)
            {
                NotFound();
            }

            return Ok(addedUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Put(int id, UserDto user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            UserDto updatedUser = await UserService.UpdateUser(user, HttpContext.RequestAborted);

            if (updatedUser == null)
            {
                NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool isDeleted = await UserService.DeleteUser(id, HttpContext.RequestAborted);

            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
