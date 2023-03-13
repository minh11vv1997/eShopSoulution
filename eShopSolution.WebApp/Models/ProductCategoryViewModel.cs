using eShopSolution.ViewModels.CategoryModel;
using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Models
{
    public class ProductCategoryViewModel
    {
        public CategoryViewModel Category { get; set; }
        public PagedResult<ProductViewModel> Products { get; set; }
    }
}