using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.UseCase.Ports.Out;
using ECommerce.Catalog.Application.UseCase.UseCase.AddProduct;
using ECommerce.Catalog.Application.DomainModel.Notifiables;
using Moq;
using Xunit;

namespace Ex.Arq.Hex.Application.DomainModel.Unit.Tests
{
    public class ProductTest : AbstractNotifiable
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly AddProductInteractor _addProductInteractor;

        private static readonly string over100Chars = new('-', 101);

        public ProductTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _addProductInteractor = new AddProductInteractor(_productRepository.Object);
        }

        [Fact]
        public void ExecuteAsync_WhenProductPortInIsValid_Success()
        {
            var id = Guid.NewGuid();
            var name = "Lápis";
            var value = 1.00;

            var product = new Product(id, name, value);

            Assert.False(Invalid);

            Assert.Equal(name, product.Name);
            Assert.Equal(value, product.Value);
            Assert.Equal(id, product.Id);
        }

        [Fact]
        public void ExecuteAsync_WhenProductIdGuidEmpty_Insuccess()
        {
            var id = Guid.Empty;
            var name = "Lápis";
            var value = 1.00;

            var product = new Product(id, name, value);

            Assert.True(product.Invalid);

            Assert.True(VerifyMessageWhenProductIdGuidEmpty(product));

            Assert.Equal(nameof(Product.Id), product.Notifications.ToList()[0].Property);
        }

        [Fact]
        public void ExecuteAsync_WhenProductNameContainsLessThanThreeLetters_Insuccess()
        {
            var id = Guid.NewGuid();
            var name = "Lá";
            var value = 1.00;

            var product = new Product(id, name, value);

            Assert.True(product.Invalid);

            Assert.True(VerifyMessageWhenProductNameContainsLessThanThreeLetters(product));

            Assert.Equal(nameof(Product.Name), product.Notifications.ToList()[0].Property);
        }

        [Fact]
        public void ExecuteAsync_WhenProductNameExceededOneHundredCharacters_Insuccess()
        {
            var id = Guid.NewGuid();
            var value = 1.00;

            var product = new Product(id, over100Chars, value);

            Assert.True(product.Invalid);

            Assert.True(VerifyWhenProductNameExceededOneHundredCharacters(product));

            Assert.Equal(nameof(Product.Name), product.Notifications.ToList()[0].Property);
        }

        [Fact]
        public void ExecuteAsync_WhenValueIsZero_Insuccess()
        {
            var id = Guid.NewGuid();
            var name = "Lápis";

            var product = new Product(id, name, 0);

            Assert.True(product.Invalid);

            Assert.True(VerifyWhenValueIsInvalid(product));

            Assert.Equal(nameof(Product.Value), product.Notifications.ToList()[0].Property);
        }

        private static bool VerifyMessageWhenProductIdGuidEmpty(Product product)
        {
            if (string.Format(NotificationMessage.PropertyInvalid, nameof(Product.Id)) ==
                product.Notifications.ToList()[0].Message)
                return true;

            return false;
        }

        private static bool VerifyMessageWhenProductNameContainsLessThanThreeLetters(Product product)
        {
            if (string.Format(NotificationMessage.HaveAtLeast, nameof(Product.Name), "3") ==
                product.Notifications.ToList()[0].Message)
                return true;

            return false;
        }


        private static bool VerifyWhenProductNameExceededOneHundredCharacters(Product product)
        {
            if (string.Format(NotificationMessage.ExceededCharacters, nameof(Product.Name), "100") ==
                product.Notifications.ToList()[0].Message)
                return true;

            return false;
        }

        private static bool VerifyWhenValueIsInvalid(Product product)
        {
            if (string.Format(NotificationMessage.PropertyInvalid, nameof(Product.Value)) ==
                product.Notifications.ToList()[0].Message)
                return true;

            return false;
        }

        public static IEnumerable<object[]> MockStringMaxLength100
        {
            get
            {
                yield return new object[] { null };
                yield return new object[] { "" };
                yield return new object[] { over100Chars };
            }
        }

    }
}