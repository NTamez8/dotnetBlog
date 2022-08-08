using Microsoft.EntityFrameworkCore;
using backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace backend.Database
{
    public class sqlServerDbContext: IdentityDbContext<User>
    {
        public sqlServerDbContext(DbContextOptions<sqlServerDbContext> options):base(options)
        {
            
        }

        public DbSet<User> Users {get; set;}

        public DbSet<BlogPost> Blogs {get; set;}
    }


}