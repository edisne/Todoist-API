namespace Todoist_API.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}