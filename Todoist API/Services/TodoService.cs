using Todoist_API.Interfaces;

namespace Todoist_API.Services
{
    public class TodoService : ITodoService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private List<Todo> _todoList = new List<Todo>()
        {
            new Todo() { Title = "Prvi todo"},
            new Todo() { Title = "Drugi todo" }
        };

        public TodoService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
      
        public async Task<ServiceResponse<IEnumerable<GetTodoDto>>> AddTodo(AddTodoDto newTodo)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetTodoDto>>();
            _todoList.Add(_mapper.Map<Todo>(newTodo));
            serviceResponse.Data = _todoList.Select(x => _mapper.Map<GetTodoDto>(x)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetTodoDto>>> GetAllTodos()
        {
            var serviceRespose = new ServiceResponse<IEnumerable<GetTodoDto>>();
            var dbTodos = await _context.Todos.ToListAsync();
            serviceRespose.Data = dbTodos.Select(x => _mapper.Map<GetTodoDto>(x)).ToList();
            return serviceRespose;
        }

        public async Task<ServiceResponse<GetTodoDto>> GetTodoById(int id)
        {
            var serviceResponse = new ServiceResponse<GetTodoDto>();
            var dbTodo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetTodoDto>(dbTodo);
            if (dbTodo is not null)
            {
                return serviceResponse;
            }
            throw new Exception("Todo not found");

        }
    }
}
