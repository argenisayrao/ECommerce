using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.UseCase.Ports.Out;
using ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct;
using ECommerce.Catalog.Application.UseCase.Util;
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

            _productRepository.Setup(_ => _.SearchAsyncByName(new SearchProductFilter(portIn))).ReturnsAsync(new PageListDto<Product>() { Items = new List<Product>() { product } });

            var portOut = await _searchProductsInteractor.ExecuteAsync(portIn);

            Assert.Equal(id.ToString(), portOut.Items[0].Id);
            Assert.Equal(name, portOut.Items[0].Name);
            Assert.Equal(value, portOut.Items[0].Value);
        }

        [Fact]
        public async Task ExecuteAsync_WhenRepositoryReturnException_Success()
        {
            _productRepository.Setup(_ => _.SearchAsyncByName(It.IsAny<SearchProductFilter>())).Throws<Exception>();

            await Assert.ThrowsAsync<NullReferenceException>(() =>
                       _searchProductsInteractor.ExecuteAsync(It.IsAny<SearchProductsPortIn>()));
        }
    }
}
