using eShopSolution.Application.Catalog.Products.Dto;
using eShopSolution.Application.CommentDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Services.client
{
    public interface IPublicProductService
    {
        // Cac Action cua Client
        PagedResult<ProductViewModel> GetAllByCategoryId(int categoryId,int pageIndex,int pageSize);
    }
}
