using Ex.Arq.Hex.Application.DomainModel;
using Ex.Arq.Hex.Application.DomainModel.Entities;
using Ex.Arq.Hex.Application.DomainModel.Exceptions;
using Ex.Arq.Hex.Application.UseCase.Constants;
using Ex.Arq.Hex.Application.UseCase.Exceptions;
using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.SearchProducts;
using Moq;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.SearchProducts
{
    public class SearchProductsInteractorTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly SearchProductsInteractor _searchProductsInteractor;

        public SearchProductsInteractorTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _searchProductsInteractor = new SearchProductsInteractor(_productRepository.Object);
        }

        [Fact]
        public async Task ExecuteAsync_WhenValidKey_Success()
        {
            var id = Guid.NewGuid();
            var name = "Mesa";
            double value = 1.0;

            var product = new Product(id, name, value);
            var portIn = new SearchProductsPortIn("mes");

            _productRepository.Setup(_ => _.SearchAsync(portIn.Key)).ReturnsAsync(new List<Product>() { product });

            var portOut = await _searchProductsInteractor.ExecuteAsync(portIn);

            Assert.Equal(id, portOut.ToList()[0].Id);
            Assert.Equal(name, portOut.ToList()[0].Name);
            Assert.Equal(value, portOut.ToList()[0].Value);
        }

        [Fact]
        public async Task ExecuteAsync_WhenRepositoryReturnException_Success()
        {
            _productRepository.Setup(_ => _.SearchAsync(It.IsAny<string>())).Throws<Exception>();

            await Assert.ThrowsAsync<NullReferenceException>(() =>
                       _searchProductsInteractor.ExecuteAsync(It.IsAny<SearchProductsPortIn>()));
        }
    }
}
