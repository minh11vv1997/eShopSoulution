﻿using eShopSolution.Application.Catalog.Services.admin;
using eShopSolution.Application.Catalog.Services.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.ProductImages;
using eShopSolution.ViewModels.ProductModels;
using eShopSoulution.Utilities.Excentions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _context;
        private readonly IStoregeService _storegeService;
        public ManageProductService(EShopDbContext context, IStoregeService storegeService)
        {
            _context = context;
            _storegeService = storegeService;
        }
        public async Task<int> Create(ProductCreateRequest requestCr)
        {
            var product = new Product()
            {
                Price = requestCr.Price,
                OriginalPrice = requestCr.OriginalPrice,
                Stock = requestCr.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = requestCr.Name,
                        Description = requestCr.Description,
                        Details = requestCr.Details,
                        SeoDescription = requestCr.SeoDescription,
                        SeoAlias = requestCr.SeoAlias,
                        SeoTitle = requestCr.SeoTitle,
                        LanguageId = requestCr.LanguageId
                    }
                }
            };
            // Save Images
            if (requestCr.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = requestCr.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(requestCr.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1,
                    }
                };
            }
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }
        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }
        public async Task<int> Update(ProductUpdateRequest requestUp)
        {
            var product = _context.Products.Find(requestUp.Id);
            var productTransilation = _context.ProductTranslations.FirstOrDefault(x => x.ProductId == requestUp.Id
            && x.LanguageId == requestUp.LanguageId);
            if (product == null || productTransilation == null) throw new EShopException($"Khong tim thay san pham : {requestUp.Id}");
            productTransilation.Name = requestUp.Name;
            productTransilation.SeoAlias = requestUp.SeoAlias;
            productTransilation.SeoDescription = requestUp.SeoDescription;
            productTransilation.SeoTitle = requestUp.SeoTitle;
            productTransilation.Description = requestUp.Description;
            productTransilation.Details = requestUp.Details;
            // Save Images
            if (requestUp.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(x => x.IsDefault == true && x.ProductId == requestUp.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = requestUp.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(requestUp.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = _context.Products.Find(productId);
            if (product == null) throw new EShopException($"Khong tim thay san pham : {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = _context.Products.Find(productId);
            if (product == null) throw new EShopException($"Khong tim thay san pham : {productId}");
            product.Stock += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EShopException($"Khong tim thay san pham : {productId}");
            }
            var images = _context.ProductImages.Where(x => x.ProductId == productId);
            foreach (var item in images)
            {
                await _storegeService.DeleteFileAsync(item.ImagePath);
            }
             _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }
        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            // B1: Select lấy dữ liệu
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.Name.Contains(request.Keyword)
                        select new { p, pt, pic };
            //B2: Filter : Lọc ra điều kiện tìm kiếm
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));
            }
            if (request.CateogoryIds.Count > 0)
            {
                query = query.Where(px => request.CateogoryIds.Contains(px.pic.CategoryId));
            }
            //B3 : Paging : phân trang
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(sp => new ProductViewModel()
                            {
                                Id = sp.p.Id,
                                Name = sp.pt.Name,
                                DateCreated = sp.p.DateCreated,
                                Description = sp.pt.Description,
                                Details = sp.pt.Details,
                                LanguageId = sp.pt.LanguageId,
                                OriginalPrice = sp.p.OriginalPrice,
                                Price = sp.p.Price,
                                SeoAlias = sp.pt.SeoAlias,
                                SeoDescription = sp.pt.SeoDescription,
                                SeoTitle = sp.pt.SeoTitle,
                                Stock = sp.p.Stock,
                                ViewCount = sp.p.ViewCount
                            }).ToListAsync();

            //B4: Select projection
            var pageResult = new PagedResult<ProductViewModel>()
            {
                TotaRecord = totalRow,
                Item = data
            };
            return pageResult;
        }
        private async Task<string> SaveFile(IFormFile formFile)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storegeService.SaveFileAsync(formFile.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest requestCrIm)
        {
            var productImage = new ProductImage()
            {
                Caption = requestCrIm.Caption,
                DateCreated = DateTime.Now,
                IsDefault = requestCrIm.IsDefault,
                ProductId = productId,
                SortOrder = requestCrIm.SortOrder
            };
            if (requestCrIm.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(requestCrIm.ImageFile);
                productImage.FileSize = requestCrIm.ImageFile.Length;
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<int> RemoteImage(int imageId)
        {
            var productImId = await _context.ProductImages.FindAsync(imageId);
            if (productImId == null) throw new EShopException($"Khong tim thay hinh anh : {imageId}");
            _context.ProductImages.Remove(productImId);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest requestUpm)
        {
            var productImg = await _context.ProductImages.FindAsync(imageId);
            if (requestUpm == null) throw new EShopException($"Khong tim thay anh san pham : {imageId}");
            if (requestUpm.ImageFile  != null)
            {
                productImg.ImagePath = await this.SaveFile(requestUpm.ImageFile);
                productImg.FileSize = requestUpm.ImageFile.Length;
            }
            _context.ProductImages.Update(productImg);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetListImage(int productImagesId)
        {
            var productIm = await _context.ProductImages.Where(x => x.ProductId == productImagesId) 
                            .Select(i => new ProductImageViewModel()
                            {
                                Id = i.Id,
                                ProductId = i.ProductId,
                                ImagePath = i.ImagePath,
                                FileSize = i.FileSize,
                                IsDefault = i.IsDefault,
                                DateCreated = i.DateCreated,
                                SortOrder = i.SortOrder
                            }).ToListAsync();
            return productIm;

        }

        public async Task<ProductImageViewModel> GetListImageById(int productImagesId)
        {
            var image =await  _context.ProductImages.FindAsync(productImagesId);
            if (image == null)
            {
                throw new EShopException($"Khong tim thay anh voi id {image}");
            }
            var viewModel = new ProductImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId,
                SortOrder = image.SortOrder
            };
            return viewModel;
        }
    }
}
