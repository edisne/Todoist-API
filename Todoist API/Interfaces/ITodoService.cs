using Microsoft.AspNetCore.Mvc;
using Todoist_API.DTOs.Todos;
using Todoist_API.Helpers;

namespace Todoist_API.Interfaces
{
    public interface ITodoService
    {
        Task<ServiceResponse<PagedList<GetTodoDto>>> GetAllTodos(UserParams userParams, string userId);
        Task<ServiceResponse<GetTodoDto>> GetTodoById(int id);
        Task<ServiceResponse<AddTodoDto>> AddTodo(AddTodoDto newTodo);
        Task<ServiceResponse<UpdateTodoDto>> UpdateTodo(UpdateTodoDto todo);
        Task<ServiceResponse<GetTodoDto>> DeleteTodo(int id);

    }
}
