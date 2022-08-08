using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using backend.Database;
using backend.Models;
using System.Text;
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

        [HttpPost("register")]
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

        // test password is 1234@Nicolas
        // test email is this@that.com
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Some required fields are missing");
            }

            var userExists = await userManager.FindByEmailAsync(loginInformation.Email);
            if(userExists != null && await userManager.CheckPasswordAsync(userExists,loginInformation.Password))
            {

                var jwtToken = await GenerateJWTTokenAsync(userExists);
                return Ok(jwtToken);
            }

            return Unauthorized("Failed to login with the credentials given try again");
        }

        private async Task<AuthToken> GenerateJWTTokenAsync(User user)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(secret,SecurityAlgorithms.HmacSha256)
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new AuthToken(){
                Token = jwtToken,
                ExpiresAt = token.ValidTo
            };
        }
    }
}