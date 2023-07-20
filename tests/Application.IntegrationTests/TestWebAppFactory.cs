using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest;
using UserPermission.API.Application.Common.Interfaces;
using UserPermission.API.Application.Common.Interfaces.RepositoryRead;
using UserPermission.API.Infrastructure.Persistence.RepositoryRead;
using UserPermission.API.Infrastructure.Services.ConsumerService;
using UserPermission.API.Infrastructure.Services.ElasticService;
using UserPermission.API.Infrastructure.Services.ProducerService;

namespace UserPermission.API.Application.IntegrationTests;
public class TestWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder, services) =>
        {

            services
                .Remove<DbContextOptions<Infrastructure.Persistence.UserPermissionDbContext>>()
                .Remove<IHostedService>()
                .Remove<IProducerService>()
                .Remove<IRepositoryRead>()
                .Remove<IElasticClient>()
                .AddElasticService(builder.Configuration)
                .AddScoped<IRepositoryRead, RepositoryRead>()
                .AddDbContext<Infrastructure.Persistence.UserPermissionDbContext>((sp, options) =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly(typeof(Infrastructure.Persistence.UserPermissionDbContext).Assembly.FullName)))
                .AddProducerService(builder.Configuration);
        });
    }
}
