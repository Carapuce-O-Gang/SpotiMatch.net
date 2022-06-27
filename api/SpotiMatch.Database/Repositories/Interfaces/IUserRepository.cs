using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SpotiMatch.Database.Entities;

namespace SpotiMatch.Database.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public IQueryable<User> GetUsers();
        public Task<User> GetUser(int id, CancellationToken cancellationToken = default);
        public Task<User> AddUser(User user, CancellationToken cancellationToken = default);
        public Task<User> UpdateUser(User user, CancellationToken cancellationToken = default);
        public Task<bool> DeleteUser(int id, CancellationToken cancellationToken = default);
    }
}
