using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Infrastructure.Data;
using Todo.Backend.Infrastructure.Repositories;
using Todo.Backend.Infrastructure.Services;
using Todo.Backend.Application.Abstractions;

namespace Todo.Backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var useInMemory = configuration.GetValue<bool>("UseInMemoryProvider");
        if (useInMemory)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("todo_in_memory"), ServiceLifetime.Scoped);
        }
        else
        {
            var conn = configuration.GetConnectionString("DefaultConnection") ?? "Data Source=todo.db";
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(conn), ServiceLifetime.Scoped);
        }

        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskService, TaskService>();

        return services;
    }
}
