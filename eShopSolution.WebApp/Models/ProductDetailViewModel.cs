using eShopSolution.ViewModels.CategoryModel;
using eShopSolution.ViewModels.ProductImages;
using eShopSolution.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Models
{
    public class ProductDetailViewModel
    {
        public CategoryViewModel Category { get; set; }
        public ProductViewModel Products { get; set; }
        public List<ProductViewModel> ReLatedProducts { get; set; }
        public List<ProductImageViewModel> ProductImages { get; set; }
    }
}