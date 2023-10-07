using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct;
using ECommerce.Catalog.Application.UseCase.Util;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB.Pagination
{
    public static class PaginationExtension
    {
        public static async Task<PageListDto<T>> GetPaged<T>(IMongoCollection<T> collection,
            FilterDefinition<T> filterData, int page, int pageSize, string sort)
        {
            var result = new PageListDto<T>
            {
                Page = page,
                PageSize = pageSize,
                Total = Convert.ToInt32(collection.CountDocuments(filterData))                
            };

            var pageCount = (double)result.Total / pageSize;
            result.TotalPages = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Items = await collection.Find(filterData)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync();

            return result;
        }
    }
}