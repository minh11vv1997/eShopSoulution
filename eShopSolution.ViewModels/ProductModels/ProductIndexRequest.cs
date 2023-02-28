using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.ProductModels
{
    public class ProductIndexRequest
    {
        public string keyword { get; set; }
        public int? categoryId { get; set; }

        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = 10;
    }
}