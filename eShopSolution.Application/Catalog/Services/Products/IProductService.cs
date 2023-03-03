using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.ProductImages;
using eShopSolution.ViewModels.ProductModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Services.Products
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest requestCr);

        Task<int> Update(ProductUpdateRequest requestUp);

        Task<int> Delete(int productId);

        Task<bool> UpdatePrice(int productId, decimal newPrice); //Cập nhật lại giá

        Task<bool> UpdateStock(int productId, int addedQuantity);// Cập nhật lại số lượng

        Task AddViewCount(int productId); // thêm 1 sản phẩm vào ds

        Task<ProductViewModel> GetById(int productId, string languageId); // Lấy theo ID

        // Task<List<ProductViewModel>> GetAll();
        Task<PagedResult<ProductViewModel>> GetAllPagingClient(GetPublicProductPagingRequest request);

        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        // Thêm mới ảnh
        Task<int> AddImage(int productId, ProductImageCreateRequest requestCrIm);

        Task<int> RemoteImage(int imageId);

        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest requestUpm);

        Task<List<ProductImageViewModel>> GetListImage(int productImagesId);

        Task<ProductImageViewModel> GetImageById(int productImagesId);

        // Phần Iterface của client cho trang ngoài.
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request);

        Task<List<ProductViewModel>> GetFeatureProduct(string languageId, int take);

        Task<List<ProductViewModel>> GetListTipProduct(string languageId, int take);

        Task<bool> CategoryAssign(int id, CategoryAssignRequest request);
    }
}