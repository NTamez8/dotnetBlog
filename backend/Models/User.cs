using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class User: IdentityUser
    {
        public string Name {get; set;}

    }

    public class UserDto
    {
        public string Name {get; set;}
        public string Password {get; set;}
        public string Email {get; set;}
    }
}