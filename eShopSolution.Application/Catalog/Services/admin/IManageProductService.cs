using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.ProductImages;
using eShopSolution.ViewModels.ProductModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Services.admin
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest requestCr);
        Task<int> Update(ProductUpdateRequest requestUp);
        Task<int> Delete(int productId);
        Task<bool> UpdatePrice(int productId, decimal newPrice); //Cập nhật lại giá
        Task<bool> UpdateStock(int productId, int addedQuantity);// Cập nhật lại số lượng
        Task AddViewCount(int productId); // thêm 1 sản phẩm vào ds
        // Task<List<ProductViewModel>> GetAll();
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        //Task<PagedResult<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize, List<int> CategoryId);
        // Thêm mới ảnh
        Task<int> AddImage(int productId, ProductImageCreateRequest requestCrIm);
        Task<int> RemoteImage(int imageId);
        Task<int> UpdateImage(int imageId,ProductImageUpdateRequest requestUpm);
        Task<List<ProductImageViewModel>> GetListImage(int productImagesId);
        Task<ProductImageViewModel> GetListImageById(int productImagesId);
    }
}
