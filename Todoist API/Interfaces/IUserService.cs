namespace Todoist_API.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<UserDto>> GetUserById(int id);
        Task<ServiceResponse<UserDto>> Login(string email, string password);

    }
}
