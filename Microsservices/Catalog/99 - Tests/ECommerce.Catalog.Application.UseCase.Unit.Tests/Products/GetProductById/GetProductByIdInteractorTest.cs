using Ex.Arq.Hex.Application.DomainModel;
using Ex.Arq.Hex.Application.DomainModel.Entities;
using Ex.Arq.Hex.Application.DomainModel.Exceptions;
using Ex.Arq.Hex.Application.UseCase.Constants;
using Ex.Arq.Hex.Application.UseCase.Exceptions;
using Ex.Arq.Hex.Application.UseCase.Exceptions.Products.UseCases;
using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.GetProductById;
using Moq;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.GetProductById
{
    public class GetProductByIdInteractorTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly GetProductByIdInteractor _getProductByIdInteractor;
        private readonly Guid _id;
        private readonly GetProductByIdPortIn _productPortIn;

        public GetProductByIdInteractorTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _getProductByIdInteractor = new GetProductByIdInteractor(_productRepository.Object);
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

            Assert.Equal(_id, productPortOut.Id);
            Assert.Equal(name, productPortOut.Name);
            Assert.Equal(value, productPortOut.Value);
        }

        [Fact]
        public async Task ExecuteAsync_WhenProductNotExists_GetProductByIdInteractorException()
        {
            var productPortIn = new GetProductByIdPortIn(Guid.NewGuid());

            _productRepository.Setup(_ => _.GetByIdAsync(_id));

            Exception exception = await Assert.ThrowsAsync<GetProductByIdInteractorException>(() =>
             _getProductByIdInteractor.ExecuteAsync(productPortIn));

            Assert.Equal(UseCaseConstants.ProductNotExists, exception.Message);
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
