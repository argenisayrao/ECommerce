﻿using ECommerce.Application.DomainModel.Entities;
using ECommerce.Application.UseCase.Ports.In;
using ECommerce.Application.UseCase.Ports.Out;

namespace ECommerce.Application.UseCase.UseCase.SearchProduct
{
    public class SearchProductsInteractor: ISearchProductsInteractor
    {
        private readonly IProductRepository _productRepository;

        public SearchProductsInteractor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IReadOnlyCollection<SearchProductsPortOut>> ExecuteAsync(SearchProductsPortIn portIn)
        {
            var searchProductsPortOut = new List<SearchProductsPortOut>();

            var products = await _productRepository.SearchAsync(portIn.Key);

            foreach (Product product in products)
            {
                searchProductsPortOut.Add(
                    new SearchProductsPortOut(product.Id,
                                             product.Name,
                                             product.Value));
            }

            return searchProductsPortOut;
        }
    }
}
