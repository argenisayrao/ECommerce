using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.GetAddressById;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.Adresses.GetAddressById
{
    public class GetAddressByIdPortInTest
    {
        [Fact]
        public void GetAddressByIdPortIn_TestConstructor_Success()
        {
            var id = Guid.NewGuid();
            
            var getAddressByIdPortIn = new GetAddressByIdPortIn(id);

            Assert.Equal(id, getAddressByIdPortIn.Id);
        }
    }
}
