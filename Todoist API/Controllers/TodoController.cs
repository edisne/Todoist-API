using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Todoist_API.DTOs.Todos;
using Todoist_API.Extensions;
using Todoist_API.Helpers;
using Todoist_API.Interfaces;
using Todoist_API.Services;

namespace Todoist_API.Controllers
{
    [Authorize]
    public class TodoController : BaseController
    {
        private readonly ITodoService _todoService;
        private readonly IUserService _userService;

        public TodoController(ITodoService todoService, IUserService userService)
        {
            _todoService = todoService;
            _userService = userService;
        }
        private static Todo todo = new Todo();


        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<PagedList<GetTodoDto>>>> Get([FromQuery] UserParams userParams)
        {

            var todos = await _todoService.GetAllTodos(userParams, User.GetUserId());

            Response.AddPaginationHeader(new PaginationHeader(todos.Data.CurrentPage, todos.Data.PageSize, 
                    todos.Data.TotalCount, todos.Data.TotalPages));

            if (todos is null) {
                return NotFound();
            }
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetTodoDto>>> GetSingle(int id)
        {
            var todo = await _todoService.GetTodoById(id);
            if (todo is null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<AddTodoDto>>> AddTodo(AddTodoDto newTodo)
        {
            var addAction = await _todoService.AddTodo(newTodo);

            if (!addAction.Success)
            {
                return BadRequest(addAction.Message); 
            }
            return Ok(addAction);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<UpdateTodoDto>>> UpdateTodo([FromBody] UpdateTodoDto updatedTodo)
        {
            var updateAction = await _todoService.UpdateTodo(updatedTodo);

            if(!updateAction.Success)
            {
                return BadRequest(updateAction.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo (int id)
        {
            var deleteAction = await _todoService.DeleteTodo(id);
            if (!deleteAction.Success)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
