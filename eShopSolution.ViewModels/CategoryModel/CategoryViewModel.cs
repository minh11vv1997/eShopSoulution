using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.CategoryModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}