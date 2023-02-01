using eShopSolution.Application.Catalog.Products.Dto;
using eShopSolution.Application.Catalog.Services.client;
using eShopSolution.Application.CommentDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        public PagedResult<ProductViewModel> GetAllByCategoryId(int categoryId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
