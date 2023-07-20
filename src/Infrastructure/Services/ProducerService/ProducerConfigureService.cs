using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserPermission.API.Application.Common.Interfaces;

namespace UserPermission.API.Infrastructure.Services.ProducerService
{
    public static class ProducerConfigureService
    {
        public static IServiceCollection AddProducerService(this IServiceCollection services, IConfiguration configuration)
        {

            CreateTopic(configuration);

            services.AddSingleton<IProducerService>(provider =>
            {
                return new ProducerService(new ProducerConfig
                {
                    BootstrapServers = configuration.GetValue<string>("KafkaConnect")
                }, configuration.GetValue<string>("KafkaTopic"));
            });
            return services;
        }

        private static void CreateTopic(IConfiguration configuration)
        {
            var config = new AdminClientConfig
            {
                BootstrapServers = configuration.GetValue<string>("KafkaConnect")
            };

            using (var adminClient = new AdminClientBuilder(config).Build())
            {
                var topicName = configuration.GetValue<string>("KafkaTopic");
                var replicationFactor = 1;
                var numPartitions = 1;

                var topicMetadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));

                var exist = topicMetadata.Topics.Exists(t => t.Topic.Equals(topicName, StringComparison.OrdinalIgnoreCase));
                if(exist) 
                { 
                    Console.WriteLine($"Topic already exists"); 
                    return; 
                }

                var topicSpecification = new TopicSpecification
                {
                    Name = topicName,
                    ReplicationFactor = (short)replicationFactor,
                    NumPartitions = numPartitions
                };

                try
                {
                    adminClient.CreateTopicsAsync(new[] { topicSpecification }).GetAwaiter().GetResult();
                }
                catch (CreateTopicsException e)
                {
                    Console.WriteLine($"Error al crear el topic '{topicName}': {e.Results[0].Error.Reason}");
                }
            }
        }
    }
}
