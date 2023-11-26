namespace Todoist_API.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<UserDto>> GetUserById(string id);
        Task<User> GetUserByUsername(string username);
    }
}
