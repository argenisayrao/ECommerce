using Ex.Arq.Hex.Application.DomainModel.Messages;
using Ex.Arq.Hex.Application.UseCase.BasePort;
using Ex.Arq.Hex.Application.UseCase.Exceptions.Adresses.Ports;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.Adresses.BaseAddress
{
    public class BaseAddressPortTest
    { 
        private readonly Guid _id;
        private readonly string _country;
        private readonly string _state;
        private readonly string _zipCode;
        private readonly string _city;
        private readonly string _street;
        private readonly int _number;

        public BaseAddressPortTest()
        {
            _id = Guid.NewGuid();
            _country = "Brazil";
            _state = "Minas Gerais";
            _zipCode = "37200-000";
            _city = "Lavras";
            _street = "Perimetral";
            _number = 100;
        }

        [Fact]
        public void Address_WhenValidParameters_Success()
        {
            var address = new BaseAddressPort(_id, _country, _state, _zipCode, _city, _street, _number);

            Assert.NotEmpty(address.Id.ToString());

            Assert.Equal(_id, address.Id);
            Assert.Equal(_country, address.Country);
            Assert.Equal(_state, address.State);
            Assert.Equal(_zipCode, address.ZipCode);
            Assert.Equal(_city, address.City);
            Assert.Equal(_street, address.Street);
            Assert.Equal(_number, address.Number);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Address_WhenInvalidCountry_AddressExcpetionWithMessageInvalidCountry(string country)
        {
            Exception exception = Assert.Throws<BaseAddressPortException>(() =>
            new BaseAddressPort(_id, country, _state, _zipCode, _city, _street, _number));

            Assert.Equal(AddressMessages.InvalidCountry, exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Address_WhenInvalidState_AddressExcpetionWithMessageInvalidState(string state)
        {
            Exception exception = Assert.Throws<BaseAddressPortException>(() =>
            new BaseAddressPort(_id, _country, state, _zipCode, _city, _street, _number));

            Assert.Equal(AddressMessages.InvalidState, exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [InlineData("37200000")]
        [InlineData("3720-000")]
        [InlineData("372000-000")]
        [InlineData("37200-00")]
        [InlineData("37200-0000")]
        public void Address_WhenInvalidZipCode_AddressExcpetionWithMessageInvalidZipCode(string zipCode)
        {
            Exception exception = Assert.Throws<BaseAddressPortException>(() =>
            new BaseAddressPort(_id, _country, _state, zipCode, _city, _street, _number));

            Assert.Equal(AddressMessages.InvalidZipCode, exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Address_WhenInvalidCity_AddressExcpetionWithMessageInvalidCity(string city)
        {
            Exception exception = Assert.Throws<BaseAddressPortException>(() =>
            new BaseAddressPort(_id, _country, _state, _zipCode, city, _street, _number));

            Assert.Equal(AddressMessages.InvalidCity, exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Address_WhenInvalidCity_AddressExcpetionWithMessageInvalidStreet(string street)
        {
            Exception exception = Assert.Throws<BaseAddressPortException>(() =>
            new BaseAddressPort(_id, _country, _state, _zipCode, _city, street, _number));

            Assert.Equal(AddressMessages.InvalidStreet, exception.Message);
        }

        [Fact]
        public void Address_WhenInvalidCity_AddressExcpetionWithMessageInvalidNumber()
        {
            int number = 0;
            Exception exception = Assert.Throws<BaseAddressPortException>(() =>
            new BaseAddressPort(_id, _country, _state, _zipCode, _city, _street, number));

            Assert.Equal(AddressMessages.InvalidNumber, exception.Message);
        }
    }
}
