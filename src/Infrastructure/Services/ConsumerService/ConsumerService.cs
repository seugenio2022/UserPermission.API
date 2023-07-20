using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UserPermission.API.Application.Common.DTOs;
using UserPermission.API.Application.Queries.SyncQueries;
using UserPermission.API.Infrastructure.Common;

namespace UserPermission.API.Infrastructure.Services.ConsumerService
{
    public class ConsumerService : BackgroundService
    {
        private readonly ILogger<ConsumerService> logger;
        private readonly IConfiguration configuration;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMediator _mediator;

        public ConsumerService(
            ILogger<ConsumerService> logger,
            IConfiguration configuration,
            IServiceScopeFactory scopeFactory,
            IMediator mediator
          )
        {
            this.logger = logger;
            this.configuration = configuration;
            _scopeFactory = scopeFactory;
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            logger.LogInformation($"Kafka background task is starting.");

            cancellationToken.Register(() =>
                logger.LogInformation($" Kafka background task is stopping."));

            using (var consumer = new ConsumerBuilder<Ignore, string>(new ConsumerConfig
            {
                GroupId = $"{configuration.GetValue<string>("KafkaTopic")}_group",
                BootstrapServers = configuration.GetValue<string>("KafkaConnect"),
                AutoOffsetReset = AutoOffsetReset.Earliest
            }).Build())
            {
                consumer.Subscribe(configuration.GetValue<string>("KafkaTopic"));

                await Task.Run(() =>
                {
                    while (true)
                    {
                        var cr = consumer.Consume(default(CancellationToken));

                        if (!string.IsNullOrEmpty(cr.Message.Value))
                        {
                            var eventData = JsonConvert.DeserializeObject<PublishInputDto<PermissionDto>>(cr.Message.Value);

                            if (eventData.Operation == "request")
                            {
                                logger.LogInformation($"message request " + cr.Message.Value);
                                mediator.Send(new InsertPermissionCommand(eventData.Data)).GetAwaiter().GetResult();
                            }
                            if (eventData.Operation == "modify")
                            {
                                logger.LogInformation($"message modify " + cr.Message.Value);
                                mediator.Send(new UpdatePermissionCommand(eventData.Data)).GetAwaiter().GetResult();
                            }

                        }
                    }
                });

                consumer.Close();
            }

        }
    }
}
