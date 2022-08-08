using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using backend.Database;
using backend.Models;
namespace backend.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly sqlServerDbContext dbContext;

        private readonly IConfiguration configuration;

        public AuthController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            sqlServerDbContext dbContext,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDto possibleUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Some required fields are missing");
            }

            var userExists = await userManager.FindByEmailAsync(possibleUser.Email);
            if(userExists != null)
            {
                return BadRequest("User exists");
            }

            User newUser = new User(){
                Name = possibleUser.Name,
                Email = possibleUser.Email,
                UserName = possibleUser.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await userManager.CreateAsync(newUser,possibleUser.Password);
            if(result.Succeeded) return Ok("User Created");
            return BadRequest("Failed to create user");
        }
    }
}