using Microsoft.AspNetCore.Mvc;
using Todoist_API.Interfaces;

namespace Todoist_API.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("{email}/{password}")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> Login(string email, string password)
        {
            var user = await _userService.Login(email, password);
            if (user.Success)
            {
                return Ok(user);
            } else
            {
                return BadRequest(user);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> GetSingle(int id)
        {
            return Ok(await _userService.GetUserById(id));
        }
    }
}
