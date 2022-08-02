using Microsoft.EntityFrameworkCore;
using backend.Models;
namespace backend.Database
{
    class sqliteDbContext: DbContext
    {
        public sqliteDbContext(DbContextOptions<sqliteDbContext> options):base(options)
        {
            
        }

        public DbSet<User> Users {get; set;}
    }


}