using Ex.Arq.Hex.Application.DomainModel.ValueObjects;
using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.DeleteAddress;
using Moq;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.Adresses.DeleteAddress
{
    public class DeleteAddressInteractorTest
    {
        private readonly Mock<IAddressRepository> _addressRepository;
        private readonly DeleteAddressInteractor _deleteAddressInteractor;

        public DeleteAddressInteractorTest()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _deleteAddressInteractor = new DeleteAddressInteractor(_addressRepository.Object);
        }

        [Fact]
        public void ExecuteAsync_AddressExists_Success()
        {
            var id = Guid.NewGuid();
            var country = "Brazil";
            var state = "Minas Gerais";
            var zipCode = "37200-000";
            var city = "Lavras";
            var street = "Perimetral";
            var number = 100;
            var deleteAddressPortIn = new DeleteAddressPortIn(id,country,state,zipCode,city,street,number);
            
            Assert.IsNotType<Exception>(_deleteAddressInteractor.ExecuteAsync(deleteAddressPortIn));
            _addressRepository.Verify(x => x.DeleteAsync(It.IsAny<Address>()));
        }


        [Fact]
        public async Task ExecuteAsync_WhenRepositoryReturnException_Exception()
        {
            _addressRepository.Setup(_ => _.DeleteAsync(It.IsAny<Guid>()))
                .Throws<Exception>();

            await Assert.ThrowsAsync<NullReferenceException>(() =>
                       _deleteAddressInteractor.
                       ExecuteAsync(It.IsAny<DeleteAddressPortIn>()));
        }
    }
}
