using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Database;

namespace backend.Models
{
    public class sqlServerUserContext: IUserContext
    {
        private readonly sqlServerDbContext context;

        public sqlServerUserContext(sqlServerDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await context.Users.ToListAsync();
            return users;
        }


        public async Task<User> AddUser(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }
    }


}