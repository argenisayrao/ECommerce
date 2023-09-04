using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.DeleteProductById;
using Moq;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.DeleteProductById
{
    public class DeleteProductByIdInteractorTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly DeleteProductByIdInteractor _deleteProductByIdInteractor;
        private readonly Guid _id;
        private readonly DeleteProductByIdPortIn _deleteProductPortIn;

        public DeleteProductByIdInteractorTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _deleteProductByIdInteractor = new DeleteProductByIdInteractor(_productRepository.Object);
            _id = Guid.NewGuid();
            _deleteProductPortIn = new DeleteProductByIdPortIn(_id);
        }
        [Fact]
        public void ExecuteAsync_ProductExistsInDataBase_Success()
        {
            Assert.IsNotType<Exception>(_deleteProductByIdInteractor.ExecuteAsync(_deleteProductPortIn));
            _productRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>()));
        }


        [Fact]
        public async void ExecuteAsync_WhenRepositoryReturnException_Exception()
        {
            _productRepository.Setup(_ => _.DeleteAsync(It.IsAny<Guid>())).Throws<Exception>();

            await Assert.ThrowsAsync<NullReferenceException>(() =>
                       _deleteProductByIdInteractor.ExecuteAsync(It.IsAny<DeleteProductByIdPortIn>()));
        }
    }
}
