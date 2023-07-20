using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace UserPermission.API.Infrastructure.Services.ElasticService
{
    public static class ElasticConfigureService
    {
        public static IServiceCollection AddElasticService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IElasticClient>(provider =>
            {
                var connectionSettings = new ConnectionSettings(new Uri(configuration.GetValue<string>("ElasticSearchConnection")))
                    .DefaultIndex(configuration.GetValue<string>("ElasticSearchDefaultIndex"));

                return new ElasticClient(connectionSettings);
            });
            return services;
        }
    }
}
