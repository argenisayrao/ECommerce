using Ex.Arq.Hex.Application.UseCase.BasePort;
using Ex.Arq.Hex.Application.UseCase.Exceptions.Adresses.Ports;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.Adresses.BaseAddress
{
    public class BaseSearchAddressByPortTest
    {
        [Fact]
        public void BaseSearchAddressByPortn_WhenValidKey_Success()
        {
            var key = "Mes";
            var searchProductPortIn = new BaseSearchAddressByPort(key);

            Assert.Equal(key, searchProductPortIn.Key);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void BaseSearchAddressByPort_WhenInvalidKey_Exception(string key)
        {
            Assert.Throws<BaseSearchAddressByPortException>(() =>
                 new BaseSearchAddressByPort(key));
        }
    }
}
