using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductViewModel>> GetPagingProduct(GetManageProductPagingRequest request);

        Task<bool> CreateProduct(ProductCreateRequest request);

        Task<bool> UpdateProduct(ProductUpdateRequest request);

        Task<bool> CategoryAssign(int id, CategoryAssignRequest request);

        Task<ProductViewModel> GetByIdCategory(int id, string languageId);

        Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take);

        Task<List<ProductViewModel>> GetListTipdProducts(string languageId, int take);
    }
}