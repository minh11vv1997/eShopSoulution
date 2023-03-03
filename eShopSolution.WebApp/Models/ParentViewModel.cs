using eShopSolution.ViewModels.ProductModels;
using eShopSolution.ViewModels.SlideModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Models
{
    public class ParentViewModel
    {
        public List<SlideViewModel> SlideViewModel { get; set; }
        public List<ProductViewModel> FeaturedProducts { get; set; }
        public List<ProductViewModel> ListTipProduct { get; set; }
    }
}