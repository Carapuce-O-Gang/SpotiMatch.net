using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SpotiMatch.Database.Entities;
using SpotiMatch.Database.Repositories.Interfaces;

namespace SpotiMatch.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly DatabaseContext DatabaseContext = new DatabaseContext();

        public UserRepository(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public IQueryable<User> GetUsers()
        {
            return DatabaseContext.Users.AsQueryable();
        }

        public async Task<User> GetUser(int id, CancellationToken cancellationToken)
        {
            User user = await DatabaseContext.Users.FindAsync(id, cancellationToken)
                ?? throw new ArgumentNullException("User not found");

            return user;
        }

        public async Task<User> AddUser(User user, CancellationToken cancellationToken)
        {
            await DatabaseContext.Users.AddAsync(user, cancellationToken);
            DatabaseContext.SaveChanges();

            return user;
        }

        public async Task<User> UpdateUser(User user, CancellationToken cancellationToken)
        {
            User userToUpdate = await DatabaseContext.Users.FindAsync(user.Id, cancellationToken) 
                ?? throw new ArgumentNullException("User not found");

            DatabaseContext.Users.Update(user);
            await DatabaseContext.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task<bool> DeleteUser(int id, CancellationToken cancellationToken)
        {
            User userToDelete = await DatabaseContext.Users.FindAsync(id)
                ?? throw new ArgumentNullException("User not found");

            DatabaseContext.Users.Remove(userToDelete);
            await DatabaseContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
