using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using ECommerce.Catalog.Application.UseCase.Ports.In;
using ECommerce.Catalog.Application.UseCase.UseCase.GetProductById;
using ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct;

namespace ECommerce.Catalog.InfrastructureAdapter.In.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController : ControllerBase
    {
        private readonly ISearchProductsInteractor _searchProducts;
        private readonly IGetProductByIdInteractor _getProductById;


        public ProductController(ISearchProductsInteractor searchProducts,
            IGetProductByIdInteractor getProductById)
        {
            _searchProducts = searchProducts;
            _getProductById = getProductById;
        }

        [HttpGet]
        [Route("get-product-by-id")]
        [SwaggerOperation("Get product by id")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Indicates that the product was found", typeof(GetProductByIdPortOut))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Indicates that the product was not found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var getProductByIdIn = new GetProductByIdPortIn(id);

            var getProductByIdPortOut = await _getProductById.ExecuteAsync(getProductByIdIn);

            if (getProductByIdPortOut.IsExists)
                return Ok(getProductByIdPortOut);

            return NotFound();
        }

        [HttpGet]
        [Route("search-products")]
        [SwaggerOperation("Search for product by name")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Indicates that the product was found", typeof(List<SearchProductsPortOut>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Indicates that the key used in the search is null or empty")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Indicates that the product was not found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SearchProduct(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return BadRequest();
            }

            var searchProductsPortIn = new SearchProductsPortIn(key);
            var searchProductsPortOut = await _searchProducts.ExecuteAsync(searchProductsPortIn);

            if (searchProductsPortOut.SearchProductPortOut.Any())
                return Ok(searchProductsPortOut);

            return NotFound();
        }
    }
}
