using Todoist_API.DTOs.Auth;
using Todoist_API.DTOs.Tags;
using Todoist_API.DTOs.Todos;

namespace Todoist_API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Todo, GetTodoDto>();
            CreateMap<AddTodoDto, Todo>().ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
            CreateMap<AddTagDto, Tag>();
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<UpdateTodoDto, Todo>().ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
            CreateMap<UpdateTagDto, Tag>();
            CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
        }
    }
}
