using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.HasKey(x => new { x.CategoryId, x.ProductId });
            builder.ToTable("ProductInCategories");
            //Tạo khóa ngoại giữa 2 bảng n-n (product vs category)
            builder.HasOne(p => p.Product).WithMany(pc => pc.ProductInCategories)
                .HasForeignKey(c => c.ProductId);
            builder.HasOne(p => p.Category).WithMany(pc => pc.ProductInCategories)
               .HasForeignKey(c => c.CategoryId);
        }
    }
}
