using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
              new AppConfig() { Key = "HomeTitle", Value = "This is home page of eShopSolution" },
              new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of eShopSolution" },
              new AppConfig() { Key = "HomeDescription", Value = "This is description of eShopSolution" }
              );
            modelBuilder.Entity<Language>().HasData(
               new Language() { Id = "vi", Name = "Tiếng Việt", IsDefault = true },
               new Language() { Id = "en", Name = "English", IsDefault = false });

            modelBuilder.Entity<Category>().HasData(
              new Category()
              {
                  Id = 1,
                  IsShowOnHome = true,
                  ParenId = null,
                  SortOrder = 1,
                  Status = Status.Active
              },
            new Category()
            {
                Id = 2,
                IsShowOnHome = true,
                ParenId = null,
                SortOrder = 2,
                Status = Enums.Status.Active
            });
            modelBuilder.Entity<CategoryTranslation>().HasData(
                 new CategoryTranslation() { Id = 1, CategoryId = 1, Name = "Áo nam", LanguageId = "vi", SeoAlias = "ao-nam", SeoDescription = "Sản phẩm áo thời trang nam", SeoTitle = "Sản phẩm áo thời trang nam" },
                 new CategoryTranslation() { Id = 2, CategoryId = 1, Name = "Men Shirt", LanguageId = "en", SeoAlias = "men-shirt", SeoDescription = "The shirt products for men", SeoTitle = "The shirt products for men" },
                 new CategoryTranslation() { Id = 3, CategoryId = 2, Name = "Áo nữ", LanguageId = "vi", SeoAlias = "ao-nu", SeoDescription = "Sản phẩm áo thời trang nữ", SeoTitle = "Sản phẩm áo thời trang women" },
                 new CategoryTranslation() { Id = 4, CategoryId = 2, Name = "Women Shirt", LanguageId = "en", SeoAlias = "women-shirt", SeoDescription = "The shirt products for women", SeoTitle = "The shirt products for women" }
                   );
            modelBuilder.Entity<Product>().HasData(
           new Product()
           {
               Id = 1,
               DateCreated = DateTime.Now,
               OriginalPrice = 100000,
               Price = 200000,
               Stock = 0,
               ViewCount = 0,
               IsFeatured = true
           },
            new Product()
            {
                Id = 2,
                DateCreated = DateTime.Now,
                OriginalPrice = 200000,
                Price = 300000,
                Stock = 1,
                ViewCount = 0,
                IsFeatured = false
            }
           );
            modelBuilder.Entity<ProductImage>().HasData(
                new ProductImage()
                {
                    Id = 10,
                    ProductId = 1,
                    ImagePath = "/themes/images/products/b1.jpg",
                    Caption = "Thumbnail image",
                    IsDefault = true,
                    SortOrder = 1,
                    FileSize = 18759
                },
                new ProductImage()
                {
                    Id = 11,
                    ProductId = 2,
                    ImagePath = "/themes/images/products/b2.jpg",
                    Caption = "Thumbnail image",
                    IsDefault = true,
                    SortOrder = 4,
                    FileSize = 39972
                },
                 new ProductImage()
                 {
                     Id = 12,
                     ProductId = 2,
                     ImagePath = "/themes/images/products/6.jpg",
                     Caption = "Thumbnail image",
                     IsDefault = true,
                     SortOrder = 4,
                     FileSize = 39972
                 },
                   new ProductImage()
                   {
                       Id = 13,
                       ProductId = 1,
                       ImagePath = "/themes/images/products/7.jpg",
                       Caption = "Thumbnail image",
                       IsDefault = true,
                       SortOrder = 4,
                       FileSize = 5353
                   },
                   new ProductImage()
                   {
                       Id = 14,
                       ProductId = 1,
                       ImagePath = "/themes/images/products/9.jpg",
                       Caption = "Thumbnail image",
                       IsDefault = true,
                       SortOrder = 4,
                       FileSize = 231231
                   },
                   new ProductImage()
                   {
                       Id = 15,
                       ProductId = 2,
                       ImagePath = "/themes/images/products/8.jpg",
                       Caption = "Thumbnail image",
                       IsDefault = true,
                       SortOrder = 4,
                       FileSize = 231231
                   }
                );
            modelBuilder.Entity<ProductTranslation>().HasData(
                 new ProductTranslation()
                 {
                     Id = 1,
                     ProductId = 1,
                     Name = "Áo sơ mi nam trắng Việt Tiến",
                     LanguageId = "vi",
                     SeoAlias = "ao-so-mi-nam-trang-viet-tien",
                     SeoDescription = "Áo sơ mi nam trắng Việt Tiến",
                     SeoTitle = "Áo sơ mi nam trắng Việt Tiến",
                     Details = "Áo sơ mi nam trắng Việt Tiến",
                     Description = "Áo sơ mi nam trắng Việt Tiến"
                 },
                new ProductTranslation()
                {
                    Id = 2,
                    ProductId = 1,
                    Name = "Viet Tien Men T-Shirt",
                    LanguageId = "en",
                    SeoAlias = "viet-tien-men-t-shirt",
                    SeoDescription = "Viet Tien Men T-Shirt",
                    SeoTitle = "Viet Tien Men T-Shirt",
                    Details = "Viet Tien Men T-Shirt",
                    Description = "Viet Tien Men T-Shirt"
                },
                 new ProductTranslation()
                 {
                     Id = 3,
                     ProductId = 2,
                     Name = "Torado T-Shirt",
                     LanguageId = "vi",
                     SeoAlias = "Torado-t-shirt",
                     SeoDescription = "Torado Men T-Shirt",
                     SeoTitle = "Torado Men T-Shirt",
                     Details = "Torado Men T-Shirt",
                     Description = "Torado Men T-Shirt"
                 }

                );

            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductId = 1, CategoryId = 1 }
                );
            modelBuilder.Entity<Slide>().HasData(
             new Slide() { Id = 1, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 1, Url = "#", Image = "/themes/images/carousel/1.png", Status = Status.Active },
             new Slide() { Id = 2, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 2, Url = "#", Image = "/themes/images/carousel/2.png", Status = Status.Active },
             new Slide() { Id = 3, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 3, Url = "#", Image = "/themes/images/carousel/3.png", Status = Status.Active },
             new Slide() { Id = 4, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 4, Url = "#", Image = "/themes/images/carousel/4.png", Status = Status.Active },
             new Slide() { Id = 5, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 5, Url = "#", Image = "/themes/images/carousel/5.png", Status = Status.Active },
             new Slide() { Id = 6, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 6, Url = "#", Image = "/themes/images/carousel/6.png", Status = Status.Active }
             );

            // Thêm cho bảng admin
            var roleId = new Guid("D276B5EB-7E1D-4347-96BF-CE5931BDE593");
            var adminId = new Guid("4FF07EC1-1521-4F74-92E8-B03F26A72A96");
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole
                {
                    Id = roleId,
                    Name = "admin",
                    NormalizedName = "admin",
                    Description = "Administrator role"
                });
            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "minhvv@rikkeisoft.com",
                NormalizedEmail = "minhvv@rikkeisoft.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123456a@A"),
                SecurityStamp = string.Empty,
                FirstName = "Minh",
                LastName = "Vu",
                Dob = new DateTime(2023, 01, 02)
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}