using Ex.Arq.Hex.Application.DomainModel.ValueObjects;
using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.UpdateAddress;
using Moq;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.Adresses.UpdateAddress
{
    public class UpdateAddressInteractorTest
    {
        private readonly Mock<IAddressRepository> _addressRepository;
        private readonly UpdateAddressInteractor _interactor;

        public UpdateAddressInteractorTest()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _interactor = new UpdateAddressInteractor(_addressRepository.Object);
        }

        [Fact]
        public void ExecuteAsync_WhenValidProduct_Success()
        {
            var id = Guid.NewGuid();
            var country = "Brazil";
            var state = "Minas Gerais";
            var zipCode = "37200-000";
            var city = "Lavras";
            var street = "Perimetral";
            var number = 100;

            var updateAddressPortIn = new UpdateAddressPortIn(id, country, state,
                zipCode, city, street, number);

            Assert.IsNotType<Exception>(_interactor.ExecuteAsync(updateAddressPortIn));
            _addressRepository.Verify(x => x.UpdateAsync(It.IsAny<Address>()));
        }

        [Fact]
        public async Task InteractorAsync_WhenRepositoryReturnException_Exception()
        {
            _addressRepository.Setup(_ => _.DeleteAsync(It.IsAny<Guid>())).Throws<Exception>();

            await Assert.ThrowsAsync<NullReferenceException>(() =>
                              _interactor.ExecuteAsync(It.IsAny<UpdateAddressPortIn>()));
        }
    }
}
