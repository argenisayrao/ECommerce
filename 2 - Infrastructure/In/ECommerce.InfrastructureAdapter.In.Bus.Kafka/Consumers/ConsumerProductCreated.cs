using AutoMapper;
using Confluent.Kafka;
using ECommerce.Application.UseCase.UseCase.AddProduct;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Newtonsoft.Json;
using ECommerce.Application.UseCase.Ports.In;

namespace ECommerce.InfrastructureAdapter.In.Bus.Kafka.Consumers
{
    internal class ConsumerProductCreated : BackgroundService
    {
        private readonly IAddProductInteractor _addProduct;

        public ConsumerProductCreated(IAddProductInteractor addProduct)
        {
            _addProduct = addProduct ?? throw new ArgumentNullException(nameof(addProduct));
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "127.0.0.1:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var running = true;

            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();

            {
                c.Subscribe("test-topic");

                var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (running)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);
                            Console.WriteLine(cr.Message.Value);

                            if (cr is not null && cr.Message.Value.Contains("Name"));
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
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    running = false;
                    c.Close();
                }
            }
        }
    }
}
