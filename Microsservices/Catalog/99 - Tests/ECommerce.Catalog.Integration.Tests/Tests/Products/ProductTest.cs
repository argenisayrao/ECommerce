using Ex.Arq.Hex.Application.UseCase.Exceptions.Products.UseCases;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.AddProducts;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.DeleteProduct;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.DeleteProductById;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.GetProductById;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.SearchProducts;
using Ex.Arq.Hex.Application.UseCase.UseCase.Products.UpdateProducts;
using Ex.Arq.Hex.InfrastructureAdapter.Out.AccessData.EntityFramework.Contexts;
using Ex.Arq.Hex.InfrastructureAdapter.Out.AccessData.EntityFramework.Products.Repository;
using Ex.Arq.Hex.Unit.Integration.Attributes;
using Xunit;

namespace Ex.Arq.Hex.Unit.Integration.UseCase.Products
{
    [TestCaseOrderer("Ex.Arq.Hex.Unit.Integration.Orderer.PriorityOrderer", "Ex.Arq.Hex.Unit.Integration")]
    public static class ProductTest
    {
        private static ProductRepository productRepository;
        private static GetProductByIdInteractor getProductByIdInteractor;

        private static readonly List<string> _names =
            new() { "Mesa", "Cadeira" };

        private static readonly List<double> _values = new() { 200.00, 50.00 };
        private static readonly List<string> _ids = new();

        [Fact, TestPriority(1)]
        public static async Task Product_AddTwoProductsAndSetIds_Success()
        {
            productRepository = new(new AppDb());
            var addProductPortInMesa = new AddProductPortIn(_names[0], _values[0]);
            var addProductPortInCadeira = new AddProductPortIn(_names[1], _values[1]);

            var interactor = new AddProductInteractor(productRepository);

            var addProductPortOutMesa = await interactor.ExecuteAsync(addProductPortInMesa);
            var addProductPortOutCadeira = await interactor.ExecuteAsync(addProductPortInCadeira);


            Assert.Equal(addProductPortInMesa.Name, addProductPortOutMesa.Name);
            Assert.Equal(addProductPortInMesa.Value, addProductPortOutMesa.Value);
            Assert.NotEqual(Guid.Empty, addProductPortOutMesa.Id);

            Assert.Equal(addProductPortInCadeira.Name, addProductPortOutCadeira.Name);
            Assert.Equal(addProductPortInCadeira.Value, addProductPortOutCadeira.Value);
            Assert.NotEqual(Guid.Empty, addProductPortOutCadeira.Id);

            _ids.Add(addProductPortOutMesa.Id.ToString());
            _ids.Add(addProductPortOutCadeira.Id.ToString());
        }

        [Fact, TestPriority(2)]
        public static async Task Product_GetProductById_Success()
        {
            productRepository = new(new AppDb());
            var getProductByIdPortIn = new GetProductByIdPortIn(Guid.Parse(_ids[0]));

            var interactor = new GetProductByIdInteractor(productRepository);
            var portOut = await interactor.ExecuteAsync(getProductByIdPortIn);

            Assert.Equal(Guid.Parse(_ids[0]), portOut.Id);
            Assert.Equal(_names[0], portOut.Name);
            Assert.Equal(_values[0], portOut.Value);
        }


        [Fact, TestPriority(3)]
        public static async Task Product_SearchProduct_Success()
        {
            productRepository = new(new AppDb());
            string key = "Me";// Names[0].Substring(0,3);

            var portIn = new SearchProductsPortIn(key);

            var interactor = new SearchProductsInteractor(productRepository);
            var searchProductsInteractor = await interactor.ExecuteAsync(portIn);

            foreach (SearchProductPortOut product in searchProductsInteractor)
            {
                Assert.Contains(portIn.Key.ToLower(), product.Name.ToLower());
            }
        }

        [Fact, TestPriority(4)]
        public static async Task Product_UpdateProduct_Success()
        {
            productRepository = new(new AppDb());
            getProductByIdInteractor = new GetProductByIdInteractor(productRepository);

            _names[0] = "Escrivaninha";
            _values[0] = 150.00;
            var updateProductPortIn = new UpdateProductPortIn(Guid.Parse(_ids[0]), _names[0], _values[0]);
            var interactor = new UpdateProductInteractor(productRepository);

            await interactor.ExecuteAsync(updateProductPortIn);

            GetProductByIdPortOut getProductById = await getProductByIdInteractor.
                ExecuteAsync(new GetProductByIdPortIn(Guid.Parse(_ids[0])));

            Assert.Equal(Guid.Parse(_ids[0]), getProductById.Id);
            Assert.Equal(_names[0], getProductById.Name);
            Assert.Equal(_values[0], getProductById.Value);
        }

        [Fact, TestPriority(5)]
        public static async Task Product_DeleteProduct_Success()
        {
            productRepository = new(new AppDb());
            getProductByIdInteractor = new GetProductByIdInteractor(productRepository);

            var deleteProductPortIn = new DeleteProductPortIn(Guid.Parse(_ids[0]), _names[0], _values[0]);
            var interactor = new DeleteProductInteractor(productRepository);

            await interactor.ExecuteAsync(deleteProductPortIn);

            await Assert.ThrowsAsync<GetProductByIdInteractorException>(() => getProductByIdInteractor.
            ExecuteAsync(new GetProductByIdPortIn(Guid.Parse(_ids[0]))));
        }

        [Fact, TestPriority(6)]
        public static async Task Product_DeleteProductById_Success()
        {
            productRepository = new(new AppDb());
            getProductByIdInteractor = new GetProductByIdInteractor(productRepository);

            var deleteProductPortIn = new DeleteProductByIdPortIn(Guid.Parse(_ids[1]));
            var interactor = new DeleteProductByIdInteractor(productRepository);

            await interactor.ExecuteAsync(deleteProductPortIn);

            await Assert.ThrowsAsync<GetProductByIdInteractorException>(() => getProductByIdInteractor.
            ExecuteAsync(new GetProductByIdPortIn(Guid.Parse(_ids[1]))));
        }
    }
}
