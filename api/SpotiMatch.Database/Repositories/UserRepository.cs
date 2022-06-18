using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            User user = await DatabaseContext.Users.FindAsync(new object[] { id }, cancellationToken)
                ?? throw new ArgumentNullException("User not found");

            return user;
        }

        public async Task<User> AddUser(User user, CancellationToken cancellationToken)
        {
            DatabaseContext.Users.Add(user);
            await DatabaseContext.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task<User> UpdateUser(User user, CancellationToken cancellationToken)
        {
            User userToUpdate = await DatabaseContext.Users.FindAsync(new object[] { user.Id }, cancellationToken)
                ?? throw new ArgumentNullException("User not found");

            DatabaseContext.Entry(userToUpdate).State = EntityState.Modified;
            await DatabaseContext.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task<bool> DeleteUser(int id, CancellationToken cancellationToken)
        {
            User userToDelete = await DatabaseContext.Users.FindAsync(new object[] { id })
                ?? throw new ArgumentNullException("User not found");

            DatabaseContext.Users.Remove(userToDelete);
            await DatabaseContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
