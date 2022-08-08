using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace backend.Models
{
    public class User: IdentityUser
    {
        public string Name {get; set;}

    }

    public class UserDto
    {
        public string Name {get; set;}

        [Required]
        public string Password {get; set;}

        [Required]
        public string Email {get; set;}

        [Required]
        public string UserName {get; set;}
    }

}