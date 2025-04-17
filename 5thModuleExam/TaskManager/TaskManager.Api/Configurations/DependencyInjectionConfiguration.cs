using TaskManager.Bll.Services;
using TaskManager.Repository.Services;

namespace TaskManager.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepositoryAdoNet>();
        //builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepositoryEFCore>();

        builder.Services.AddScoped<IToDoItemService, ToDoItemService>();

    }
}
