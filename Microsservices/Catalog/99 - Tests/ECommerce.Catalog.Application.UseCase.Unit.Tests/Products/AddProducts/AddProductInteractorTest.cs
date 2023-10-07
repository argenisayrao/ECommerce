using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.UseCase.Ports.Out;
using ECommerce.Catalog.Application.UseCase.UseCase.AddProduct;
using Moq;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.AddProducts
{
    public class AddProductInteractorTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly AddProductInteractor _addProductInteractor;

        public AddProductInteractorTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _addProductInteractor = new AddProductInteractor(_productRepository.Object);
        }

        [Fact]
        public async Task ExecuteAsync_WhenProductPortInIsValid_Success()
        {
            var id = Guid.NewGuid();
            var portIn = new AddProductPortIn(id.ToString(),"Lapis", 1.00);
            
            AddProductPortOut portOut = await _addProductInteractor.ExecuteAsync(portIn);

            Assert.Equal(portIn.Name, portOut.Name);
            Assert.Equal(portIn.Value, portOut.Value);
            Assert.Equal(id.ToString(),portOut.Id);
        }

        [Fact]
        public async Task ExecuteAsync_WhenRepositoryReturnException_AddProductPortOutIsNull()
        {
            _productRepository.Setup(_ => _.AddAsync(It.IsAny<Product>())).Throws<Exception>();

            await Assert.ThrowsAsync<NullReferenceException>(() =>
                       _addProductInteractor.ExecuteAsync(It.IsAny<AddProductPortIn>()));
        }
    }
}
