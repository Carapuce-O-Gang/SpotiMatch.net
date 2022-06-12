using System.Collections.Generic;
using SpotiMatch.Database.Entities;

namespace SpotiMatch.Database.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetUsers();
        public User GetUser(int id);
        public User AddUser(User employee);
        public User UpdateUser(User employee);
        public bool DeleteUser(int id);
    }
}
