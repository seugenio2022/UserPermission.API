using UserPermission.API.Application.Common.Interfaces;
using UserPermission.API.Infrastructure.Persistence;
using UserPermission.API.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<UserPermissionDbContext>(options =>
                options.UseInMemoryDatabase("UserPermission.APIDb"));
        }
        else
        {
            services.AddDbContext<UserPermissionDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(UserPermissionDbContext).Assembly.FullName)));
        }

        services.AddScoped<UserPermissionDbContextInitialiser>();

        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}
