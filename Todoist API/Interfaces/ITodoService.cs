namespace Todoist_API.Interfaces
{
    public interface ITodoService
    {
        Task<ServiceResponse<IEnumerable<GetTodoDto>>> GetAllTodos();
        Task<ServiceResponse<GetTodoDto>> GetTodoById(int id);
        Task<ServiceResponse<IEnumerable<GetTodoDto>>> AddTodo(AddTodoDto newTodo);
    }
}
