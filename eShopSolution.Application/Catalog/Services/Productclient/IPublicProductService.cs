using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Services.Productclient
{
    public interface IPublicProductService
    {
        // Cac Action cua Client
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request);

        Task<List<ProductViewModel>> GetAll(string languageId);
    }
}