using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.UseCase.Ports.Out;
using ECommerce.Catalog.Application.UseCase.Services.Interfaces;
using ECommerce.Catalog.Application.UseCase.UseCase.GetProductById;
using Moq;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.GetProductById
{
    public class GetProductByIdInteractorTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<ICachingService> _cache;
        private readonly GetProductByIdInteractor _getProductByIdInteractor;      

        private readonly Guid _id;
        private readonly GetProductByIdPortIn _productPortIn;

        public GetProductByIdInteractorTest()
        {
            _productRepository = new();
            _cache = new();
            _getProductByIdInteractor = new GetProductByIdInteractor(_productRepository.Object,_cache.Object);
            _id = Guid.NewGuid();
            _productPortIn = new GetProductByIdPortIn(_id);
        }

        [Fact]
        public async Task ExecuteAsync_WhenProductExists_Success()
        {
            var name = "Lapis";
            double value = 1.0;
            
            var product = new Product(_id, name, value);

            _productRepository.Setup(_ => _.GetByIdAsync(_id)).ReturnsAsync(product);

            GetProductByIdPortOut productPortOut = await _getProductByIdInteractor.ExecuteAsync(_productPortIn);

            Assert.Equal(_id.ToString(), productPortOut.Id);
            Assert.Equal(name, productPortOut.Name);
            Assert.Equal(value, productPortOut.Value);
        }

        [Fact]
        public async Task ExecuteAsync_WhenProductNotExists_ProductExistsIsFalse()
        {
            var productPortIn = new GetProductByIdPortIn(Guid.NewGuid());

            _productRepository.Setup(_ => _.GetByIdAsync(_id));

            var getProductByIdPortOut = await _getProductByIdInteractor.ExecuteAsync(productPortIn);

            Assert.False(getProductByIdPortOut.IsExists);
        }

        [Fact]
        public async Task ExecuteAsync_WhenRepositoryReturnException_Exception()
        {
            var productPortIn = new GetProductByIdPortIn(Guid.NewGuid());

            _productRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception());

            Exception exception = await Assert.ThrowsAsync<Exception>(() =>
             _getProductByIdInteractor.ExecuteAsync(productPortIn));
        }
    }
}
