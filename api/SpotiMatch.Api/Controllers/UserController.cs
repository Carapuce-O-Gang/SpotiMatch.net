using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using SpotiMatch.Database.Repositories.Interfaces;
using SpotiMatch.Database.Entities;

namespace SpotiMatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository UserRepository;

        public UserController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            List<User> users = await Task.FromResult(UserRepository.GetUsers());

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> Get(int id)
        {

            User user = await Task.FromResult(UserRepository.GetUser(id));

            if (user == null)
            {
                NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            User postedUser = await Task.FromResult(UserRepository.AddUser(user));
            
            if (postedUser == null)
            {
                NotFound();
            }

            return Ok(postedUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            User updatedUser = await Task.FromResult(UserRepository.UpdateUser(user));

            if (updatedUser == null)
            {
                NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool isDeleted = await Task.FromResult(UserRepository.DeleteUser(id));

            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
