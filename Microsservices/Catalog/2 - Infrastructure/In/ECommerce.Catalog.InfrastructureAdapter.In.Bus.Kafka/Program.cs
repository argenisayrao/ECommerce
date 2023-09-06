using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.Catalog.InfrastructureAdapter.In.Bus.Kafka.Consumers;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", true, true)
    .AddEnvironmentVariables();
var configuration = builder.Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {

        services.AddSingleton(configuration);

        var consumer = new ConsumerConfig
        {
            GroupId = configuration.GetConnectionString("KafkaGroupId"),
            BootstrapServers = configuration.GetConnectionString("KafkaBootstrapServers"),
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false,
            EnablePartitionEof = true
        };

        services.AddSingleton(consumer);

        services.AddAplicationWithAccessData(configuration);
        services.AddScoped<ConsumerProductCreated>();
        services.AddHostedService<ConsumerProductCreated>();
    })
    .Build();

await host.RunAsync();
