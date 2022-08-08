using Microsoft.AspNetCore.Mvc;
using backend.Models;

namespace backend.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserContext userContext;
        public UserController(IUserContext userContext)
        {
            this.userContext = userContext;
        }


        [HttpGet]
        public async Task<ActionResult> getUsers()
        {
            return Ok(await userContext.GetUsers());
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] UserDto user)
        {
            var new_user = await userContext.AddUser(user);
            return Ok(new_user);
        }
    }
}