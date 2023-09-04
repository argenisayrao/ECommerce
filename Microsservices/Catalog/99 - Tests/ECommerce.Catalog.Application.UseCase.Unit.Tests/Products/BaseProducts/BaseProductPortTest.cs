using Ex.Arq.Hex.Application.UseCase.BasePort;
using Ex.Arq.Hex.Application.UseCase.Exceptions.Products.Ports;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.BaseProducts
{
    public class BaseProductPortTest
    {
        [Fact]
        public void BaseProduct_WhenValidParameters_Success()
        {
            var id = Guid.NewGuid();
            var name = "lapis";
            double value = 15.00;

            var baseProductPort =  new BaseProductPort(id, name, value);

            Assert.Equal(id, baseProductPort.Id);
            Assert.Equal(name, baseProductPort.Name);
            Assert.Equal(value, baseProductPort.Value);
        }

        [Theory]
        [InlineData("     ")]
        [InlineData("")]
        [InlineData(null)]
        public void BaseProduct_WhenInvalidParameters_Success(string name)
        {
            Assert.Throws<BaseProductPortException>(() =>
            new BaseProductPort(Guid.NewGuid(), name, 15.00));
        }
    }
}
