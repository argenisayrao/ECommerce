using Confluent.Kafka;
using Ecommerce.Core.Domain.Helpers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Infrastructure.Out.Bus.Kafka
{
    public abstract class KafkaRepository
    {
        protected readonly string ConnectionString;
        protected readonly string ApplicationGroup;
        protected KafkaRepository(string connectionString, string applicationGroup)
        {
            ApplicationGroup = applicationGroup;
            ConnectionString = connectionString;
        }

        public async Task PublishEvent<TData>(TData data, CancellationToken cancellationToken = default)
        {
            await ProducerAsync(data, cancellationToken);
        }
        
        protected async Task ProducerAsync<TData>(TData data, CancellationToken cancellationToken = default)
        {
            var kafkaTransferDataDTO = new KafkaTransferDataDTO<TData>(data);

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = ConnectionString
            };

            var producer = new ProducerBuilder<string, string>(producerConfig).Build();
            try
            {
                await producer.ProduceAsync(kafkaTransferDataDTO.Topic, new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = kafkaTransferDataDTO.MessageContent }, cancellationToken);
            }
            catch (Exception kafkaError)
            {
                throw new ApplicationCoreException($"don't possible publish data to Apache Kafka. Topic: '{kafkaTransferDataDTO.Topic}', Data: {kafkaTransferDataDTO.MessageContent}", kafkaError);
            }
        }
    }
}
