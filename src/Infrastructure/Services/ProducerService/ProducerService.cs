using Confluent.Kafka;
using Newtonsoft.Json;
using UserPermission.API.Application.Common.DTOs;
using UserPermission.API.Application.Common.Interfaces;

namespace UserPermission.API.Infrastructure.Services.ProducerService
{
    public class ProducerService : IProducerService
    {
        private readonly ProducerConfig _producerConfig;
        private readonly string _topic;

        public ProducerService(ProducerConfig producerConfig, string topic)
        {
            _producerConfig = producerConfig;
            _topic = topic;
        }
        public async Task PublishAsync<T>(PublishInputDto<T> dto)
        {
            using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
            {
                var serializedObject = JsonConvert.SerializeObject(dto);
                var result = await producer.ProduceAsync(_topic, new Message<Null, string> { Value = serializedObject });
                producer.Flush();

                if (result.Status == PersistenceStatus.NotPersisted) throw new Exception("Message Produce Error");

            }
        }
    }
}
