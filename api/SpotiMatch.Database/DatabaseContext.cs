using Microsoft.EntityFrameworkCore;
using SpotiMatch.Database.Entities;

namespace SpotiMatch.Database
{
    public class DatabaseContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DatabaseContext() {}

        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) {}
    }
}
