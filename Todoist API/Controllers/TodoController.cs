using Microsoft.AspNetCore.Mvc;
using Todoist_API.Interfaces;

namespace Todoist_API.Controllers
{
    public class TodoController : BaseController
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }
        private static Todo todo = new Todo();
        

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetTodoDto>>>> Get()
        {
            return Ok(await _todoService.GetAllTodos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetTodoDto>>> GetSingle(int id)
        {
            return Ok(await _todoService.GetTodoById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<IEnumerable<AddTodoDto>>>> AddTodo(AddTodoDto newTodo)
        {
            return Ok(await _todoService.AddTodo(newTodo));
        }
    }
}
