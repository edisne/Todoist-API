using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todoist_API.Interfaces;
using System.Security.Cryptography;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Todoist_API.DTOs.Auth;


namespace Todoist_API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> Register(RegisterDto registerDto)
        {
            var serviceResponse = new ServiceResponse<UserDto>();
            if (await this.UserExists(registerDto.Username!))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Username already taken";
                serviceResponse.Data = null;

                return BadRequest(serviceResponse);
            }

            var user = _mapper.Map<User>(registerDto);

            user.UserName = registerDto.Username!.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);


            if (!result.Succeeded)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Failed to register user: [{string.Join(" ", result.Errors.Select(error => error.Description))}]";
                serviceResponse.Data = null;
                return BadRequest(serviceResponse);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) {
                return BadRequest(serviceResponse);
            }

            var data = new UserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
            };

            serviceResponse.Success = true;
            serviceResponse.Message = "Sucessfully registered";
            serviceResponse.Data = data;

            return Ok(serviceResponse);

        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> Login (LoginDto loginDto) {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            var serviceResponse = new ServiceResponse<UserDto>()
            {
                Message = "Invalid username or password",
                Success = false,
                Data = null
            };
            
            if (user is null) return Unauthorized();

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) return Unauthorized();

            var loggedInUser = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                LastName = user.LastName,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email
            };

            return Ok(new ServiceResponse<UserDto>
            {
                Data = loggedInUser,
                Success = true,
                Message = "Loggin successful"
            });
        }


        private async Task<bool> UserExists (string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username);
        }
    }
}
