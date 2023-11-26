using Microsoft.AspNetCore.Mvc;
using Todoist_API.DTOs.Todos;
using Todoist_API.Helpers;
using Todoist_API.Interfaces;

namespace Todoist_API.Services
{
    public class TodoService : ITodoService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public TodoService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
      
        public async Task<ServiceResponse<AddTodoDto>> AddTodo(AddTodoDto newTodo)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetTodoDto>>();
            if (newTodo is not null)
            {
                var dbTodo = _mapper.Map<Todo>(newTodo);
                _context.Todos.Add(dbTodo);
                var entriesWritten = await _context.SaveChangesAsync();

                if (entriesWritten > 0) return new ServiceResponse<AddTodoDto>
                {
                    Success = true,
                    Data = newTodo,
                    Message = "Successfully added new todo"
                };
                else return new ServiceResponse<AddTodoDto>
                {
                    Success = false,
                    Data = null,
                    Message = "Failed to add new todo"
                };
            }
            else return new ServiceResponse<AddTodoDto>
            {
                Success = false,
                Data = null,
                Message = "Invalid data"
            };
        }

        public async Task<ServiceResponse<PagedList<GetTodoDto>>> GetAllTodos(UserParams userParams, string userId)
        {
            var serviceRespose = new ServiceResponse<PagedList<GetTodoDto>>();
            var dbTodos = _context.Todos.Include(t => t.Tags).Where(t => t.UserId == userId).AsNoTracking();
            if (!string.IsNullOrEmpty(userParams.Search))
            {
                dbTodos = dbTodos.Where(t => t.Title.Contains(userParams.Search) 
                                        || t.Description.Contains(userParams.Search)
                                        || t.Project.Contains(userParams.Search)
                                        || t.Tags.Any(tag => tag.Title.Contains(userParams.Search)));
            }
            if (!string.IsNullOrEmpty(userParams.Tag)) 
            {
                dbTodos = dbTodos.Where(t => t.Tags.Any(tag => tag.Title == userParams.Tag));
            }
            var query = dbTodos.Select(x => _mapper.Map<GetTodoDto>(x)).AsNoTracking();
            serviceRespose.Data = await PagedList<GetTodoDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
            return serviceRespose;
        }

        public async Task<ServiceResponse<GetTodoDto>> GetTodoById(int id)
        {
            var serviceResponse = new ServiceResponse<GetTodoDto>();
            var dbTodo = await _context.Todos.Include(t => t.Tags).FirstOrDefaultAsync(x => x.Id == id);
            
            if (dbTodo is not null)
            {
                serviceResponse.Success = true; 
                serviceResponse.Data = _mapper.Map<GetTodoDto>(dbTodo);
                return serviceResponse;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<UpdateTodoDto>> UpdateTodo(UpdateTodoDto todo)
        {
            var dbTodo = _context.Todos.Find(todo.Id);
            if (dbTodo is not null)
            {
                _mapper.Map(todo, dbTodo);
                await _context.SaveChangesAsync();

                return new ServiceResponse<UpdateTodoDto>
                {
                    Success = true,
                    Data = todo
                };
            }
            return new ServiceResponse<UpdateTodoDto> { Success = false, Message = "Todo not found" };
        }

        public async Task<ServiceResponse<GetTodoDto>> DeleteTodo(int id)
        {
            if (id > 0)
            {
                var dbTodo = await _context.Todos.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
                if (dbTodo == null)
                {
                    return new ServiceResponse<GetTodoDto>
                    {
                        Success = false,
                        Message = "Todo Not found"
                    };
                }
                _context.Remove(dbTodo);
                var entries = await _context.SaveChangesAsync();
                if (entries > 0)
                {
                    return new ServiceResponse<GetTodoDto>
                    {
                        Success = true,
                        Message = "Succesfully deleted todo"
                    }; 
                }
                else
                {
                    return new ServiceResponse<GetTodoDto>
                    {
                        Success = false,
                        Message = "Error while deleting todo"
                    };
                }
                
            }
            return new ServiceResponse<GetTodoDto>
            {
                Success = false,
                Message = "Invalid data"
            };
        }
    }
}
