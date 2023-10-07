using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.UseCase.Services.Interfaces;
using ECommerce.Catalog.Application.UseCase.UseCase.GetProductById;
using ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct;
using ECommerce.Catalog.InfrastructureAdapter.In.Bus.Kafka.Constants;
using ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB;
using ECommerce.Catalog.Integration.Tests.Attributes;
using ECommerce.Catalog.Integration.Tests.Helpers;
using ECommerce.Catalog.Integration.Tests.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MongoDB.Driver;
using Moq;
using System.Diagnostics;
using Xunit;

namespace ECommerce.Catalog.Integration.Tests.Tests.Products
{
    [TestCaseOrderer("Ex.Arq.Hex.Unit.Integration.Orderer.PriorityOrderer", "Ex.Arq.Hex.Unit.Integration")]
    public class ProductTest
    {
        private static ProductRepository productRepository;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<Product> _collection;
        private readonly Mock<ICachingService> _cache;
        private static GetProductByIdInteractor _getProductByIdInteractor;
        private static IMongoDatabase collection;
        private readonly IConfigurationRoot _configuration = AppSettingsHelper.GetAppSettings();
        private readonly ProducerEventProductCreated _producer;

        private static readonly List<string> _names =
            new() { "Mesa", "Cadeira" };

        private static readonly List<double> _values = new() { 200.00, 50.00 };
        private static readonly List<string> _ids = new();

        private string testAppSettingsFileDirectory = Path.Combine(SolutionPathHelper.TryGetSolutionDirectoryInfo().FullName,
                     @"Microsservices\Catalog\2 - Infrastructure\In\ECommerce.Catalog.InfrastructureAdapter.In.Bus.Kafka");
        private string binAppSettingsFileDirectoryAndName = Path.Combine(SolutionPathHelper.TryGetSolutionDirectoryInfo().FullName,
                @"Microsservices\Catalog\2 - Infrastructure\In\ECommerce.Catalog.InfrastructureAdapter.In.Bus.Kafka\bin\Debug\net7.0\appsettings.json");


        public ProductTest()
        {
            _mongoDatabase = new MongoClient(_configuration.GetConnectionString(ConstantsMongo.MongoDBConnection))
               .GetDatabase(ConstantsMongo.MongoDataBaseName);
            _collection = _mongoDatabase.GetCollection<Product>("Products");
            _getProductByIdInteractor = new GetProductByIdInteractor(new ProductRepository(_mongoDatabase), _cache.Object);
            _producer = new ProducerEventProductCreated
                (_configuration.GetConnectionString(ConstantsKafka.KafkaBootstrapServers));
        }

        protected Process StartApplication(string testAppSettingsFileName)
        {
            File.Copy($"{testAppSettingsFileDirectory}\\{testAppSettingsFileName}", binAppSettingsFileDirectoryAndName, true);
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = GetPathAppExe();
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardOutput = true;

            return Process.Start(processStartInfo);
        }

        [Fact, TestPriority(1)]
        public async Task Product_AddTwoProductsAndSetIds_Success()
        {
            var idMesa = Guid.NewGuid();
            var idCadeira = Guid.NewGuid();

            var mesa = new Product(idMesa, _names[0], _values[0]);
            var cadeira = new Product(idCadeira, _names[1], _values[1]);

            productRepository = new(_mongoDatabase);

            await Task.Delay(1000);
            await _producer.SendEventProductCreated(cadeira);
            await _producer.SendEventProductCreated(mesa);
            await Task.Delay(1000);

            var mesaFromDatabase = await _getProductByIdInteractor.ExecuteAsync(new GetProductByIdPortIn(idMesa));
            var cadeiraFromDatabase = await _getProductByIdInteractor.ExecuteAsync(new GetProductByIdPortIn(idCadeira));


            Assert.Equal(mesa.Name, mesaFromDatabase.Name);
            Assert.Equal(mesa.Value, mesaFromDatabase.Value);
            Assert.Equal(idMesa.ToString(), mesaFromDatabase.Id);

            Assert.Equal(cadeira.Name, cadeiraFromDatabase.Name);
            Assert.Equal(cadeira.Value, cadeiraFromDatabase.Value);
            Assert.Equal(idCadeira.ToString(), cadeiraFromDatabase.Id);

            _ids.Add(mesa.Id.ToString());
            _ids.Add(cadeira.Id.ToString());
        }

        private string GetPathAppExe()
        {
            return Path.Combine(SolutionPathHelper.TryGetSolutionDirectoryInfo().FullName,
                @"Microsservices\Catalog\2 - Infrastructure\In\ECommerce.Catalog.InfrastructureAdapter.In.Bus.Kafka\bin\Debug\net7.0\ECommerce.Catalog.InfrastructureAdapter.In.Bus.Kafka.exe");
        }

        [Fact, TestPriority(2)]
        public async Task Product_GetProductById_Success()
        {
            productRepository = new(_mongoDatabase);
            var getProductByIdPortIn = new GetProductByIdPortIn(Guid.Parse(_ids[0]));

            var interactor = new GetProductByIdInteractor(productRepository, _cache.Object);
            var portOut = await interactor.ExecuteAsync(getProductByIdPortIn);

            Assert.Equal(_ids[0], portOut.Id);
            Assert.Equal(_names[0], portOut.Name);
            Assert.Equal(_values[0], portOut.Value);
        }


        [Fact, TestPriority(3)]
        public async Task Product_SearchProduct_Success()
        {
            productRepository = new(_mongoDatabase);
            List<string> key = new List<string> { "Me", "me" };

            key.ForEach(async key  => 
            {
                var portIn = new SearchProductsPortIn(key);

                var interactor = new SearchProductsInteractor(productRepository);
                var pagination = await interactor.ExecuteAsync(portIn);

                pagination.Items.ToList().ForEach(product =>
                {
                    Assert.Contains(portIn.Name.ToLower(), product.Name.ToLower());
                });
            });            
        }
    }
}
