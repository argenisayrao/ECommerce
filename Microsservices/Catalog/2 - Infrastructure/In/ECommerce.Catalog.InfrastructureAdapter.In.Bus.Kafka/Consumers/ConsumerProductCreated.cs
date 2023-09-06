using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ECommerce.Catalog.Application.UseCase.Ports.In;
using ECommerce.Catalog.Application.UseCase.UseCase.AddProduct;
using Microsoft.Extensions.Configuration;
using ECommerce.Catalog.Application.DomainModel.Entities;

namespace ECommerce.Catalog.InfrastructureAdapter.In.Bus.Kafka.Consumers
{
    internal class ConsumerProductCreated : BackgroundService
    {
        private readonly IAddProductInteractor _addProduct;
        private readonly ConsumerConfig _consumerConfig;
        private readonly IConfigurationRoot _configuration;

        public ConsumerProductCreated(IAddProductInteractor addProduct,
            ConsumerConfig consumer,
            IConfigurationRoot configuration)
        {
            _addProduct = addProduct ?? throw new ArgumentNullException(nameof(addProduct));
            _consumerConfig = consumer ?? throw new ArgumentNullException(nameof(consumer));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var topic = typeof(Product).Name;
            _ = Task.Factory.StartNew(async () =>
            {
                using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
                Console.WriteLine($"consuming data from {topic}");

                while (!stoppingToken.IsCancellationRequested)
                {
                    consumer.Subscribe(topic);
                    var consumedDataResult = consumer.Consume();
                    if (consumedDataResult.IsPartitionEOF) continue;
                    try
                    {
                        Console.WriteLine($"Consumed message '{consumedDataResult.Message.Value}' at: '{consumedDataResult.TopicPartitionOffset}'.");

                        var addProductPortIn = JsonConvert.DeserializeObject<AddProductPortIn>(consumedDataResult.Message.Value);

                        var addProductPortOut = await _addProduct.ExecuteAsync(addProductPortIn);

                        Console.WriteLine(JsonConvert.SerializeObject(addProductPortOut));             
                    }
                    catch
                    {
                        continue;
                    }

                    consumer.Commit();
                }
                Console.WriteLine($"Shutting down consumer: {consumer.Name}");
            }, stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            await Task.CompletedTask;
        }
    }
}
