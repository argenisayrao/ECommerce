using Ex.Arq.Hex.Application.DomainModel;
using Ex.Arq.Hex.Application.DomainModel.Entities;
using Ex.Arq.Hex.Application.UseCase.Constants;
using Ex.Arq.Hex.Application.UseCase.Exceptions;
using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.DeleteProduct;
using Moq;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.DeleteProduct
{
    public class DeleteProductInteractorTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly DeleteProductInteractor _deleteProductInteractor;
        private readonly DeleteProductPortIn _deleteProductPortIn;

        public DeleteProductInteractorTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _deleteProductInteractor = new DeleteProductInteractor(_productRepository.Object);
            _deleteProductPortIn = new DeleteProductPortIn(Guid.NewGuid(), "Tesoura",4.00);
        }

        [Fact]
        public void ExecuteAsync_ProductExists_Success()
        {
            Assert.IsNotType<Exception>(_deleteProductInteractor.ExecuteAsync(_deleteProductPortIn));
            _productRepository.Verify(x => x.DeleteAsync(It.IsAny<Product>()));
        }


        [Fact]
        public async Task ExecuteAsync_WhenRepositoryReturnException_Exception()
        {
            _productRepository.Setup(_ => _.DeleteAsync(It.IsAny<Guid>())).Throws<Exception>();

            await Assert.ThrowsAsync<NullReferenceException>(() =>
                       _deleteProductInteractor.ExecuteAsync(It.IsAny<DeleteProductPortIn>()));
        }
    }
}
