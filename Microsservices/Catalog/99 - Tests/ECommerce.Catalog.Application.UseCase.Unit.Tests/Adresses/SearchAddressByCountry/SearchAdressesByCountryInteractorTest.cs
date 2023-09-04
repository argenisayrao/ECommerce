using Ex.Arq.Hex.Application.DomainModel.ValueObjects;
using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.SearchAdressesByCountry;
using Moq;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.Adresses.SearchAddressByCountry
{
    public class SearchAdressesByCountryInteractorTest
    {
        private readonly Mock<IAddressRepository> _addressRepository;
        private readonly SearchAdressesByCountryInteractor _searchAdressesByCountryInteractor;
        private readonly Guid _id;
        private readonly string _country;
        private readonly string _state;
        private readonly string _zipCode;
        private readonly string _city;
        private readonly string _street;
        private readonly int _number;

        public SearchAdressesByCountryInteractorTest()
        {            
            _addressRepository = new Mock<IAddressRepository>();
            _searchAdressesByCountryInteractor = new SearchAdressesByCountryInteractor(_addressRepository.Object);
            _id = Guid.NewGuid();
            _country = "Brazil";
            _state = "Minas Gerais";
            _zipCode = "37200-000";
            _city = "Lavras";
            _street = "Perimetral";
            _number = 100;
        }

        [Fact]
        public async Task ExecuteAsync_WhenValidKey_Success()
        {
            var address = new Address(_id, _country, _state, _zipCode, _city, _street, _number);
            var portIn = new SearchAddressByCountryPortIn("Bra");

            _addressRepository.Setup(_ => _.SearchByCountryAsync(portIn.Key))
                .ReturnsAsync(new List<Address>() { address,address });

            var portsOut = await _searchAdressesByCountryInteractor.ExecuteAsync(portIn);


            foreach(var portOut in portsOut)
            {
                Assert.Contains(portIn.Key, portOut.Country);                
            }
        }

        [Fact]
        public async Task ExecuteAsync_WhenRepositoryReturnException_Success()
        {
            _addressRepository.Setup(_ => _.SearchByCountryAsync(It.IsAny<string>())).Throws<Exception>();

            await Assert.ThrowsAsync<NullReferenceException>(() =>
                       _searchAdressesByCountryInteractor
                       .ExecuteAsync(It.IsAny<SearchAddressByCountryPortIn>()));
        }
    }
}
