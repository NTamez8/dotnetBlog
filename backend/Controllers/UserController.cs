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
    }
}