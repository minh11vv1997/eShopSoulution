using eShopSolution.Application.Catalog.Services.client;
using eShopSolution.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.ProductModels;

namespace eShopSolution.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly EShopDbContext _context;

        public PublicProductService(EShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> GetAll(string languageId)
        {
            // 1: Select và kết nối 
            var query = from product in _context.Products
                        join productTranslition in _context.ProductTranslations on product.Id equals productTranslition.ProductId
                        join productCategory in _context.ProductInCategories on product.Id equals productCategory.ProductId
                        join category in _context.Categories on product.Id equals category.Id
                        where productTranslition.LanguageId == languageId
                        select new { product, productTranslition, productCategory };

            var data = await query.Select(sp => new ProductViewModel()
            {
                Id = sp.product.Id,
                Name = sp.productTranslition.Name,
                DateCreated = sp.product.DateCreated,
                Description = sp.productTranslition.Description,
                Details = sp.productTranslition.Details,
                LanguageId = sp.productTranslition.LanguageId,
                OriginalPrice = sp.product.OriginalPrice,
                Price = sp.product.Price,
                SeoAlias = sp.productTranslition.SeoAlias,
                SeoDescription = sp.productTranslition.SeoDescription,
                SeoTitle = sp.productTranslition.SeoTitle,
                Stock = sp.product.Stock,
                ViewCount = sp.product.ViewCount
            }).ToListAsync();
            return data;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request)
        {
            // 1: Select và kết nối 
            var query = from product in _context.Products
                        join productTranslition in _context.ProductTranslations on product.Id equals productTranslition.ProductId
                        join productCategory in _context.ProductInCategories on product.Id equals productCategory.ProductId
                        join category in _context.Categories on product.Id equals category.Id
                        where productTranslition.LanguageId == languageId
                        select new { product, productTranslition, productCategory };

            // Lọc dữ liệu cần tìm kiếm
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.productCategory.CategoryId == request.CategoryId);
            }
            // 3: Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .Select(sp => new ProductViewModel()
                                  {
                                      Id = sp.product.Id,
                                      Name = sp.productTranslition.Name,
                                      DateCreated = sp.product.DateCreated,
                                      Description = sp.productTranslition.Description,
                                      Details = sp.productTranslition.Details,
                                      LanguageId = sp.productTranslition.LanguageId,
                                      OriginalPrice = sp.product.OriginalPrice,
                                      Price = sp.product.Price,
                                      SeoAlias = sp.productTranslition.SeoAlias,
                                      SeoDescription = sp.productTranslition.SeoDescription,
                                      SeoTitle = sp.productTranslition.SeoTitle,
                                      Stock = sp.product.Stock,
                                      ViewCount = sp.product.ViewCount
                                  }).ToListAsync();
            // 4: Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotaRecord = totalRow,
                Item = data
            };
            return pagedResult;
        }

    }
}
