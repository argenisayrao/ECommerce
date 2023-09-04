using Ex.Arq.Hex.Application.DomainModel;
using Ex.Arq.Hex.Application.DomainModel.Entities;
using Ex.Arq.Hex.Application.UseCase.Constants;
using Ex.Arq.Hex.Application.UseCase.Exceptions.Products.Ports;
using Ex.Arq.Hex.Application.UseCase.Ports.In;
using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.GetProductById;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.SearchProducts;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.UpdateProducts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.UpdateProduct
{
    public class UpdateProductInteractorTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly UpdateProductInteractor _updateProductInteractor;

        public UpdateProductInteractorTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _updateProductInteractor = new UpdateProductInteractor(_productRepository.Object);
        }

        [Fact]
        public void ExecuteAsync_WhenValidProduct_Success()
        {
            var updateProductportIn = new UpdateProductPortIn(Guid.NewGuid(), "Tesoura", 4.00);

            Assert.IsNotType<Exception>(_updateProductInteractor.ExecuteAsync(updateProductportIn));
            _productRepository.Verify(x => x.UpdateAsync(It.IsAny<Product>()));
        }

        [Fact]
        public async Task ExecuteAsync_WhenRepositoryReturnException_Exception()
        {
            _productRepository.Setup(_ => _.DeleteAsync(It.IsAny<Guid>())).Throws<Exception>();

            await Assert.ThrowsAsync<NullReferenceException>(() =>
                              _updateProductInteractor.ExecuteAsync(It.IsAny<UpdateProductPortIn>()));
        }
    }
}
