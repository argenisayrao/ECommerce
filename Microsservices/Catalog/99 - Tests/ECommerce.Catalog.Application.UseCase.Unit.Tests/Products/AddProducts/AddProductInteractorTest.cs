using Ex.Arq.Hex.Application.DomainModel;
using Ex.Arq.Hex.Application.DomainModel.Entities;
using Ex.Arq.Hex.Application.UseCase.Constants;
using Ex.Arq.Hex.Application.UseCase.Exceptions.Products.Ports;
using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.AddProducts;
using Moq;
using System;
using System.Threading.Tasks;
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
            var portIn = new AddProductPortIn("Lapis", 1.00);
            
            AddProductPortOut portOut = await _addProductInteractor.ExecuteAsync(portIn);

            Assert.Equal(portIn.Name, portOut.Name);
            Assert.Equal(portIn.Value, portOut.Value);
            Assert.NotEmpty(portOut.Id.ToString());
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
