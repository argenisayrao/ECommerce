using Confluent.Kafka;
using ECommerce.Catalog.Application.DomainModel.Entities;
using Newtonsoft.Json;

namespace ECommerce.Catalog.Integration.Tests.Repositories
{
    public class ProducerEventProductCreated
    {
        public readonly ProducerConfig _config;
        public ProducerEventProductCreated(string bootstrapServers)
        {
            _config = new ProducerConfig { BootstrapServers = bootstrapServers };
        }

        public async Task SendEventProductCreated(Product product)
        {
            using var producer = new ProducerBuilder<Null, string>(_config).Build();
            {
                var message = JsonConvert.SerializeObject(product);

                _ = await producer.ProduceAsync(nameof(Product),
                    new Message<Null, string> { Value = message });
            }
        }
    }
}
