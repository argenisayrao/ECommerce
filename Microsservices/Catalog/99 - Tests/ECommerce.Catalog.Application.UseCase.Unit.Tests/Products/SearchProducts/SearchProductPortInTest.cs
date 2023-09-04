using Ex.Arq.Hex.Application.UseCase.Exceptions.Products.Ports;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.SearchProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ex.Arq.Hex.Application.UseCase.Units.Tests.SearchProducts
{
    public class SearchProductPortInTest
    {
        [Fact]
        public void SearchProductPortIn_WhenValidKey_Success()
        {
            var key = "Mes";
            var searchProductPortIn = new SearchProductsPortIn(key);

            Assert.Equal(key, searchProductPortIn.Key);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void SearchProductPortIn_WhenInvalidKey_Exception(string key)
        {
            Assert.Throws<SearchProductsPortInException>(() =>
                 new SearchProductsPortIn(key));
        }
    }
}
