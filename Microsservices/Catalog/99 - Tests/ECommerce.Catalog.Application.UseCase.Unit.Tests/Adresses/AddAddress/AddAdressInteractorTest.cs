using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.Application.DomainModel.ValueObjects;
using Moq;
using Xunit;
using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.AddAddress;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.Adresses.AddAddress
{
    public class AddAdressInteractorTest
    {
        private readonly Mock<IAddressRepository> _addressRepository;
        private readonly AddAddressInteractor _addAddressInteractor;
        private readonly string _country;
        private readonly string _state;
        private readonly string _zipCode;
        private readonly string _city;
        private readonly string _street;
        private readonly int _number;

        public AddAdressInteractorTest()
        {
            _addressRepository = new Mock<IAddressRepository>();
            _addAddressInteractor = new AddAddressInteractor(_addressRepository.Object);
            _country = "Brazil";
            _state = "Minas Gerais";
            _zipCode = "37200-000";
            _city = "Lavras";
            _street = "Perimetral";
            _number = 100;
        }

        [Fact]
        public async Task ExecuteAsync_WhenSuccessAtAddAddress_ReturnAddAddressPortOut()
        {
            var addAddressPortIn = new AddAddressPortIn(_country, _state, _zipCode, _city, _street, _number);

            AddAddressPortOut addAddressPortOut = await _addAddressInteractor.ExecuteAsync(addAddressPortIn);

            Assert.NotEmpty(addAddressPortOut.Id.ToString());
            Assert.Equal(_country, addAddressPortOut.Country);
            Assert.Equal(_state, addAddressPortOut.State);
            Assert.Equal(_zipCode, addAddressPortOut.ZipCode);
            Assert.Equal(_city, addAddressPortOut.City);
            Assert.Equal(_street, addAddressPortOut.Street);
            Assert.Equal(_number, addAddressPortOut.Number);
        }

        [Fact]
        public async Task ExecuteAsync_WhenReturnExceptionFromRepository_Exception()
        {
            _addressRepository.Setup(_ => _.AddAsync (It.IsAny<Address>())).Throws<Exception>();

           await Assert.ThrowsAsync<NullReferenceException>(() =>
                 _addAddressInteractor.ExecuteAsync(It.IsAny<AddAddressPortIn>()));
        }
    }
}
