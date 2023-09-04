using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.DeleteAddressById;
using Moq;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.Adresses.DeleteAddressById
{
    public class DeleteAddressByIdInteractorTest
    {
        private readonly Mock<IAddressRepository> _addressRepository;
        private readonly DeleteAddressByIdInteractor _deleteAddressByIdInteractor;

        public DeleteAddressByIdInteractorTest()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _deleteAddressByIdInteractor = new DeleteAddressByIdInteractor(_addressRepository.Object);
        }

        [Fact]
        public void ExecuteAsync_AddressExists_Success()
        {
            var deleteAddressByIdPortIn = new DeleteAddressByIdPortIn(Guid.NewGuid());

            Assert.IsNotType<Exception>(_deleteAddressByIdInteractor.ExecuteAsync(deleteAddressByIdPortIn));
            _addressRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>()));
        }


        [Fact]
        public async Task ExecuteAsync_WhenRepositoryReturnException_Exception()
        {
            _addressRepository.Setup(_ => _.DeleteAsync(It.IsAny<Guid>()))
                .Throws<Exception>();

            await Assert.ThrowsAsync<NullReferenceException>(() =>
                       _deleteAddressByIdInteractor.
                       ExecuteAsync(It.IsAny<DeleteAddressByIdPortIn>()));
        }
    }
}
