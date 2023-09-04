using Ex.Arq.Hex.Application.UseCase.Exceptions.Adresses.Ports;
using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.SearchAdressesByCountry;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.Adresses.SearchAddressByCountry
{
    public class SearchAddressByCountryPortInTest
    {
        [Fact]
        public void SearchAddressPortIn_WhenValidKey_Success()
        {
            var key = "Bra";
            var searchProductPortIn = new SearchAddressByCountryPortIn(key);

            Assert.Equal(key, searchProductPortIn.Key);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void SearchAddressPortIn_WhenInvalidKey_Exception(string key)
        {
            Assert.Throws<BaseSearchAddressByPortException>(() =>
                 new SearchAddressByCountryPortIn(key));
        }
    }
}
