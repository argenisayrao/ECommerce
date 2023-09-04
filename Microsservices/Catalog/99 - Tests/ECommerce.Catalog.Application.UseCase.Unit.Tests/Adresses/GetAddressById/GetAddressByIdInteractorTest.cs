using Ex.Arq.Hex.Application.DomainModel.ValueObjects;
using Ex.Arq.Hex.Application.UseCase.Constants;
using Ex.Arq.Hex.Application.UseCase.Exceptions.Adresses.UseCases;
using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.GetAddressById;
using Moq;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.Adresses.GetAddressById
{
    public class GetAddressByIdInteractorTest
    {
        private readonly Mock<IAddressRepository> _addressRepository;
        private readonly GetAddressByIdInteractor _getAddressByIdInteractor;

        public GetAddressByIdInteractorTest()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _getAddressByIdInteractor = new GetAddressByIdInteractor(_addressRepository.Object);
        }

        [Fact]
        public async void ExecuteAsync_WhenAddressExists_Success()
        {
            var id = Guid.NewGuid();
            var country = "Brazil";
            var state = "Minas Gerais";
            var zipCode = "37200-000";
            var city = "Lavras";
            var street = "Perimetral";
            var number = 100;

            var getAddressByIdPortIn = new GetAddressByIdPortIn(id);
            var address = new Address(id, country, state, zipCode, city, street, number);

            _addressRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(address);

            var getAddressByIdPortOut = await _getAddressByIdInteractor.ExecuteAsync(getAddressByIdPortIn);

            Assert.Equal(id, getAddressByIdPortOut.Id); Guid.NewGuid();
            Assert.Equal(country, getAddressByIdPortOut.Country);
            Assert.Equal(state, getAddressByIdPortOut.State);
            Assert.Equal(zipCode, getAddressByIdPortOut.ZipCode);
            Assert.Equal(city, getAddressByIdPortOut.City);
            Assert.Equal(street, getAddressByIdPortOut.Street);
            Assert.Equal(number, getAddressByIdPortOut.Number);
        }

        [Fact]
        public async void ExecuteAsync_WhenAddressNotExists_GetAddressByIdInteractorException()
        {
            var id = Guid.NewGuid();

            var getAddressByIdPortIn = new GetAddressByIdPortIn(id);

            Exception exception = await Assert.ThrowsAsync<GetAddressByIdInteractorException>(
                () => _getAddressByIdInteractor.ExecuteAsync(getAddressByIdPortIn));

            Assert.Equal(UseCaseConstants.AddressNotExists,exception.Message);
        }

        [Fact]
        public async void ExecuteAsync_WhenRepositoryReturnException_Exception()
        {
            var id = Guid.NewGuid();
            var getAddressByIdPortIn = new GetAddressByIdPortIn(id);

            _addressRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(() => _getAddressByIdInteractor
            .ExecuteAsync(getAddressByIdPortIn));
        }
    }
}
