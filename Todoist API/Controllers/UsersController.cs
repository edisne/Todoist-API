using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todoist_API.Interfaces;

namespace Todoist_API.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> GetSingle(string id)
        {
            return Ok(await _userService.GetUserById(id));
        }
    }
}
