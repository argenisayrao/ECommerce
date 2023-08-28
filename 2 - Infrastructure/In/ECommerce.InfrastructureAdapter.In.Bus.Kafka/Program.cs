using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using ECommerce.InfrastructureAdapter.Out.AccessData;
using ECommerce.Application.UseCase.UseCase.AddProduct;
using ECommerce.InfrastructureAdapter.In.Bus.Kafka.Dto;
using AutoMapper;
using ECommerce.InfrastructureAdapter.In.Bus.Kafka.Consumers;
using Microsoft.Extensions.DependencyInjection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddApplicationWithAccessData();
        services.AddScoped<ConsumerProductCreated>();
        services.AddHostedService<ConsumerProductCreated>();
    })
    .Build();

await host.RunAsync();
