using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpotiMatch.Shared.Dtos;

namespace SpotiMatch.Logic.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthDto> Login(LoginDto login, CancellationToken cancellationToken = default);
        Task<UserDto> Register(RegisterDto register, CancellationToken cancellationToken = default);
    }
}
