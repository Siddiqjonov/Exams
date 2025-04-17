using Microsoft.EntityFrameworkCore;
using TaskManager.Dal;
using TaskManager.Repository.Settings;

namespace TaskManager.Api.Configurations;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
        var sqlDBConnectionString = new SqlDBConnectionString(connectionString);

        builder.Services.AddSingleton<SqlDBConnectionString>(sqlDBConnectionString);

        builder.Services.AddDbContext<MainContext>(options => options.UseSqlServer(connectionString));
    }
}
