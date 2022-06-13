using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SpotiMatch.Shared.Dtos;

namespace SpotiMatch.Logic.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken = default);

        Task<UserDto> GetUser(CancellationToken cancellationToken = default);

        Task<UserDto> AddUser(CancellationToken cancellationToken = default);

        Task<UserDto> UpdateUser(CancellationToken cancellationToken = default);

        Task<bool> DeleteUser(CancellationToken cancellationToken = default);
    }
}
