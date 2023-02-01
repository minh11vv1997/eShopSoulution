using eShopSolution.Application.Catalog.Products.Dto;
using eShopSolution.Application.CommentDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Services.admin
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest requestCr);
        Task<int> Update(ProductCreateRequest requestUp);
        Task<int> Delete(int productId);
        Task<List<ProductViewModel>> GetAll();
        Task<PagedResult<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize);
    }
}
