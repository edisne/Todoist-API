namespace Todoist_API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Todo, GetTodoDto>();
            CreateMap<AddTodoDto, Todo>();
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();
        }
    }
}
