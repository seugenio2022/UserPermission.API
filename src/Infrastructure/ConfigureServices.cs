using UserPermission.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserPermission.API.Infrastructure.Common;
using UserPermission.API.Infrastructure.Services.ProducerService;
using UserPermission.API.Infrastructure.Services.ElasticService;
using UserPermission.API.Application.Common.Interfaces.RepositoryWrite;
using UserPermission.API.Infrastructure.Persistence.RepositoryWrite;
using Microsoft.Extensions.DependencyInjection;
using UserPermission.API.Application.Common.Interfaces.RepositoryRead;
using UserPermission.API.Infrastructure.Persistence.RepositoryRead;
using UserPermission.API.Infrastructure.Services.ConsumerService;

namespace UserPermission.API.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<UserPermissionDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(UserPermissionDbContext).Assembly.FullName)));

        services.AddScoped<UserPermissionDbContextInitialiser>();

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddElasticService(configuration);
        services.AddScoped<IRepositoryRead, RepositoryRead>();
        services.AddProducerService(configuration);
        services.AddHostedService<ConsumerService>();



        return services;
    }
}
