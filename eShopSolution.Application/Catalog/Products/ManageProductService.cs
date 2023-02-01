using eShopSolution.Application.Catalog.Products.Dto;
using eShopSolution.Application.Catalog.Services.admin;
using eShopSolution.Application.CommentDto;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _context;
        public ManageProductService(EShopDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(ProductCreateRequest requestCr)
        {
            var product = new Product() {
                Price = requestCr.Price
            };

            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Update(ProductCreateRequest requestUp)
        {
            throw new NotImplementedException();
        }
        public async Task<int> Delete(int productId)
        {
            throw new NotImplementedException();
        }
        Task<List<ProductViewModel>> IManageProductService.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<PagedResult<ProductViewModel>> IManageProductService.GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
