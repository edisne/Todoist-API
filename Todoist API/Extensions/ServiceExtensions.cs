using Microsoft.AspNetCore.Identity;
using Todoist_API.Controllers;
using Todoist_API.Interfaces;

namespace Todoist_API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Enable CORS
            services.AddCors();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
