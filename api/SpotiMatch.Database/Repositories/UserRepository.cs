using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<User> GetUsers()
        {
            return DatabaseContext.Users.ToList();
        }

        public User GetUser(int id)
        {
            User user = DatabaseContext.Users.Find(id) 
                ?? throw new ArgumentNullException("User not found");

            return user;
        }

        public User AddUser(User user)
        {
            DatabaseContext.Users.Add(user);
            DatabaseContext.SaveChanges();

            return user;
        }

        public User UpdateUser(User user)
        {
            User userToUpdate = DatabaseContext.Users.Find(user.Id)
                ?? throw new ArgumentNullException("User not found");

            DatabaseContext.Users.Update(user);
            DatabaseContext.SaveChanges();

            return user;
        }

        public bool DeleteUser(int id)
        {
            User userToDelete = DatabaseContext.Users.Find(id)
                ?? throw new ArgumentNullException("User not found");

            DatabaseContext.Users.Remove(userToDelete);
            DatabaseContext.SaveChanges();

            return true;
        }
    }
}
