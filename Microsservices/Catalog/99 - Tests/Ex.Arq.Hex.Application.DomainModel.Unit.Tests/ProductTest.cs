using Ex.Arq.Hex.Application.DomainModel.Entities;
using Ex.Arq.Hex.Application.DomainModel.Exceptions;
using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.AddProducts;
using Moq;
using Xunit;

namespace Ex.Arq.Hex.Application.DomainModel.Units.Tests
{
    public class ProductTest
    {
        private readonly Mock<IProductRepository> _productRepository;

        public ProductTest()
        {
            _productRepository = new Mock<IProductRepository>();
        }

        [Fact(DisplayName = "Product_Add_Success")]
        public async Task Product_Add_Success()
        {
            var portIn = new AddProductPortIn("Mesa", 1.99);            

            var interactor = new AddProductInteractor(_productRepository.Object);
            var portOut = await interactor.ExecuteAsync(portIn);

            Assert.Equal(portIn.Name, portOut.Name);
            Assert.Equal(portIn.Value, portOut.Value);
            Assert.NotEqual(Guid.Empty, portOut.Id);
        }

        [Theory]
        [InlineData("     ")]
        [InlineData("")]
        [InlineData(null)]
        public void Product_WhenInvalidParameters_Success(string name)
        {
            Assert.Throws<ProductException>(() =>
            new Product(Guid.NewGuid(), name, 15.00));
        }


    }
}