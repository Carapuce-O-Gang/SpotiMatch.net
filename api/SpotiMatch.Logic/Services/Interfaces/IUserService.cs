using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SpotiMatch.Shared.Dtos;

namespace SpotiMatch.Logic.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken = default);

        Task<UserDto> GetUser(int id, CancellationToken cancellationToken = default);

        Task<UserDto> UpdateUser(UserDto user, CancellationToken cancellationToken = default);

        Task<bool> DeleteUser(int id, CancellationToken cancellationToken = default);
    }
}
