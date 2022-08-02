using Microsoft.EntityFrameworkCore;
using backend.Models;
namespace backend.Database
{
    public class sqlServerDbContext: DbContext
    {
        public sqlServerDbContext(DbContextOptions<sqlServerDbContext> options):base(options)
        {
            
        }

        public DbSet<User> Users {get; set;}
    }


}