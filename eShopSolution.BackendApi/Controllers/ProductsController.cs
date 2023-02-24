using eShopSolution.Application.Catalog.Services.Products;
using eShopSolution.ViewModels.ProductImages;
using eShopSolution.ViewModels.ProductModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    // Chuẩn RestAPi : api/products
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // HttGet mặc định : URL : http://localhost:products
        [HttpGet]
        public async Task<IActionResult> Get(string languageId)
        {
            var product = await _productService.GetAll(languageId);
            return Ok(product);
        }

        // HttGet thứ 2 Get thì sẽ trùng nên phải đặt Arias
        //  URL : http://localhost:products?pageIndex=1&pageSize=10&CategoryId=
        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAllPagingById(string languageId, [FromQuery] GetPublicProductPagingRequest request)
        {
            var products = await _productService.GetAllByCategoryId(languageId, request);
            return Ok(products);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _productService.GetAllPaging(request);
            return Ok(products);
        }

        //  URL : http://localhost:product/1
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetByID(int productId, string languageId)
        {
            var product = await _productService.GetById(productId, languageId);
            if (product == null)
            {
                return BadRequest("Can not find prudct");
            }
            return Ok(product);
        }

        //Create : thêm mới
        [HttpPost]
        [Consumes("multipart/form-data")] //Chấp nhận truyền form lên
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _productService.Create(request);
            if (productId == 0)
            {
                return BadRequest();
            }
            // Lấy ra productID truyền về cho product
            var product = await _productService.GetById(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetByID), new { id = productId }, product);
        }

        //Update : sửa cả bản ghi
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affecterResult = await _productService.Update(request);
            if (affecterResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        // Upadate 1 phần của bản ghi
        [HttpPatch("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var isSuccessful = await _productService.UpdatePrice(productId, newPrice);
            if (isSuccessful)
            {
                return Ok();
            }
            return BadRequest();
        }

        //Delete : xóa
        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affecterResult = await _productService.Delete(productId);
            if (affecterResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        // Images
        [HttpPost("{productId}/{images}")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _productService.AddImage(productId, request);
            if (imageId == 0)
            {
                return BadRequest();
            }
            // Lấy ra images cho từng sản phẩm product
            var images = await _productService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, images);
        }

        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            var productImageId = await _productService.GetImageById(imageId);
            if (productImageId == null)
            {
                return BadRequest("Cannot fint productImages");
            }
            return Ok(productImageId);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateImage(imageId, request);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> RemoteImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.RemoteImage(imageId);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}