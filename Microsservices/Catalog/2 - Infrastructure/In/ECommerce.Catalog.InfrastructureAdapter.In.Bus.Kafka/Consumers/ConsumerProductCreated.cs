using AutoMapper;
using Confluent.Kafka;

using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Newtonsoft.Json;
using ECommerce.Catalog.Application.UseCase.Ports.In;
using ECommerce.Catalog.Application.UseCase.UseCase.AddProduct;
using Microsoft.Extensions.Configuration;

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
            var running = true;

            using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();

            {
                consumer.Subscribe(_configuration.GetConnectionString("KafkaTopic"));

                var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (running)
                    {
                        try
                        {
                            var cr = consumer.Consume(cts.Token);
                            Console.WriteLine(cr.Message.Value);

                            if (cr is not null && cr.Message.Value.Contains("Name")) ;
                            {
                                Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");

                                var addProductPortIn = JsonConvert.DeserializeObject<AddProductPortIn>(cr.Message.Value);

                                var addProductPortOut = await _addProduct.ExecuteAsync(addProductPortIn);

                                Console.WriteLine(JsonConvert.SerializeObject(addProductPortOut));
                            }
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    running = false;
                    consumer.Close();
                }
            }
        }
    }
}
