using Todoist_API.Interfaces;

namespace Todoist_API.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context; 
        }

        public async Task<ServiceResponse<UserDto>> GetUserById(string id)
        {
            var serviceResponse = new ServiceResponse<UserDto>();
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<UserDto>(dbUser);
            if (dbUser is not null)
            {
                return serviceResponse;
            }
            throw new Exception("User not found");

        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
        }
    }
}
