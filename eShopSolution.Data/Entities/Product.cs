using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace eShopSolution.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Stock { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }
        // Thêm thuộc tính để kết nối n-n giữa 2 bảng product and category
        public List<ProductInCategory> ProductInCategories { get; set; }
        // Thuộc tính kết nối 1-n giữa 2 bảng OrderDetails và Product
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Cart> Carts { get; set; }

        public List<ProductTranslation> ProductTranslations { get; set; }

        public List<ProductImage> ProductImages { get; set; }


    }
}
