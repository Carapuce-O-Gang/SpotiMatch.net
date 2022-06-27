using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SpotiMatch.Shared.Dtos;
using SpotiMatch.Logic.Services.Interfaces;

namespace SpotiMatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService AuthService;

        public AuthController(IAuthService authService)
        {
            AuthService = authService;
        }

        [HttpPost("/api/login")]
        public async Task<ActionResult<AuthDto>> Login(LoginDto login)
        {
            AuthDto auth = await AuthService.Login(login, HttpContext.RequestAborted);

            if (auth == null)
            {
                Unauthorized();
            }

            return Ok(auth);
        }

        [HttpPost("/api/register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            UserDto registeredUser = await AuthService.Register(register, HttpContext.RequestAborted);

            if (registeredUser == null)
            {
                StatusCode(500);
            }

            return Ok(registeredUser);
        }
    }
}
