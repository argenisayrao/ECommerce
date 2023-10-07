using Ecommerce.Core.Domain;
using Ecommerce.Core.Domain.Helpers.Exceptions;
using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.UseCase.Ports.Out;
using ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct;
using ECommerce.Catalog.Application.UseCase.Util;
using ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB.Pagination;
using MongoDB.Driver;
using Polly;
using System.Text;

namespace ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB
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
                    $" Colection: Products',Data:{Newtonsoft.Json.JsonConvert.SerializeObject(product)} Message:{errorAtInsertInDatabase}", errorAtInsertInDatabase);
            }
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var filter = _filterBuilder.Where(x => x.Id == id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<PageListDto<Product>> SearchAsyncByName(SearchProductFilter filter)
        {
            var filterData = _filterBuilder.Empty;

            if (filter.Name.HasValue())
                filterData = _filterBuilder.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

            return await PaginationExtension.GetPaged<Product>(_collection,
                filterData,
                filter.Page,
                filter.PageSize,
                nameof(Product.Name));
        }

        private static double CalculateTimeSpamForRetryInterval(int retryAttempt)
        {
            return Math.Pow(ConstantsMongo.TimeBaseForRetryInterval, retryAttempt);
        }
    }
}
