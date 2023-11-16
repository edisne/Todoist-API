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

        public async Task<ServiceResponse<UserDto>> Login(string email, string password)
        {
            var serviceResponse = new ServiceResponse<UserDto>();
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            serviceResponse.Data = _mapper.Map<UserDto>(dbUser);
            if (dbUser is not null)
            {
                serviceResponse.Success = true;
                
            } else
            {
                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = "User not found";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<UserDto>> GetUserById(int id)
        {
            var serviceResponse = new ServiceResponse<UserDto>();
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id.ToString());
            serviceResponse.Data = _mapper.Map<UserDto>(dbUser);
            if (dbUser is not null)
            {
                return serviceResponse;
            }
            throw new Exception("User not found");

        }
    }
}
