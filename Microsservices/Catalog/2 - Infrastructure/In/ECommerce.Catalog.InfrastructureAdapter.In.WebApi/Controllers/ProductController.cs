﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Text.Json;
using ECommerce.Catalog.InfrastructureAdapter.In.WebApi.DTOs;
using ECommerce.Catalog.Application.UseCase.Ports.In;
using ECommerce.Catalog.Application.UseCase.UseCase.AddProduct;
using ECommerce.Catalog.Application.UseCase.UseCase.GetProductById;
using ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct;

namespace ECommerce.Catalog.InfrastructureAdapter.In.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController : ControllerBase
    {
        private readonly IAddProductInteractor _addProduct;
        private readonly ISearchProductsInteractor _searchProducts;
        private readonly IGetProductByIdInteractor _getProductById;
        private readonly IMapper _mapper;


        public ProductController(IAddProductInteractor addProductInteractor,
            ISearchProductsInteractor searchProducts,
            IGetProductByIdInteractor getProductById,
            IMapper mapper)
        {
            _addProduct = addProductInteractor;
            _searchProducts = searchProducts;
            _getProductById = getProductById;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("add-product")]
        [SwaggerOperation("Add product")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Product added successfully", typeof(AddProductPortOut))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Product with invalid fields", typeof(AddProductPortOut))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddProduct(ProductDto product)
        {
            var addProductPortIn = _mapper.Map<AddProductPortIn>(product);
            var addProductPortOut = await _addProduct.ExecuteAsync(addProductPortIn);

            if (addProductPortOut.Success)
                return Ok(JsonSerializer.Serialize(addProductPortOut));

            return BadRequest(JsonSerializer.Serialize(addProductPortOut));
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

            if (searchProductsPortOut.Any())
                return Ok(JsonSerializer.Serialize(searchProductsPortOut));

            return NotFound();
        }
    }
}
