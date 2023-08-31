using Ecommerce.Core.Domain.Helpers.Exceptions;
using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.UseCase.Ports.Out;
using ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB.Constants;
using MongoDB.Driver;
using Polly;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.IO;

namespace ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;
        protected readonly FilterDefinitionBuilder<Product> _filterBuilder;
        private readonly Policy _retryPolicy;

        public ProductRepository(IMongoDatabase collection)
        {
            _collection = collection.GetCollection<Product>("Products");
            _filterBuilder = Builders<Product>.Filter;

            _retryPolicy = Policy
                .Handle<MongoWriteException>()
                .Or<MongoBulkWriteException>()
                .Or<MongoCommandException>()
                .Or<MongoExecutionTimeoutException>()
                .WaitAndRetry(ConstantsMongo.NumberOfTries, retryAttempt =>
                TimeSpan.FromSeconds(CalculateTimeSpamForRetryInterval(retryAttempt)));
        }
         
        public async Task AddAsync(Product product)
        {
            try
            {
                await _retryPolicy.Execute(async () =>
                {
                    await _collection.InsertOneAsync(product);
                });
            }
            catch (Exception errorAtInsertInDatabase)
            {
                throw new ApplicationCoreException($"don't possible insert data in MongoDb." +
                    $" Database: '{ConstantsMongo.MongoDataBaseName}'," +
                    $" Colection: Products',Data:{Newtonsoft.Json.JsonConvert.SerializeObject(product)}", errorAtInsertInDatabase);
            }
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Product>> SearchAsync(string key)
        {
            throw new NotImplementedException();
        }

        private static double CalculateTimeSpamForRetryInterval(int retryAttempt)
        {
            return Math.Pow(ConstantsMongo.TimeBaseForRetryInterval, retryAttempt);
        }
    }
}
