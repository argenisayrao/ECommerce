using Ex.Arq.Hex.Application.UseCase.UseCase.Adresses.DeleteAddressById;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.Adresses.DeleteAddressById
{
    public class DeleteAddressByIdPortInTest
    {
        [Fact]
        public void GetAddressByIdPortIn_TestConstructor_Success()
        {
            var id = Guid.NewGuid();

            var deleteAddressByIdPortIn = new DeleteAddressByIdPortIn(id);

            Assert.Equal(id, deleteAddressByIdPortIn.Id);
        }
    }
}
