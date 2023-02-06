using eShopSolution.Application.Catalog.Services.admin;
using eShopSolution.Application.Catalog.Services.client;
using eShopSolution.ViewModels.ProductModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;
        public ProductController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }
        // HttGet mặc định : URL : http://localhost:product
        [HttpGet]
        public async Task<IActionResult> Get(string languageId)
        {
            var product = await _publicProductService.GetAll(languageId);
            return Ok(product);
        }
        // HttGet thứ 2 Get thì sẽ trùng nên phải đặt Arias
        //  URL : http://localhost:product/public-paging
        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategoryId(request);
            return Ok(products);
        }
        //  URL : http://localhost:product/1
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetByID(int productId, string languageId)
        {
            var product = await _manageProductService.GetById(productId, languageId);
            if (product == null)
            {
                return BadRequest("Can not find prudct");
            }
            return Ok(product);
        }
        //Create : thêm mới
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            var productId = await _manageProductService.Create(request);
            if (productId == 0)
            {
                return BadRequest();
            }
            // Lấy ra productID truyền về cho product
            var product = await _manageProductService.GetById(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetByID),new { id = productId }, product);
        }
        //Update : sửa
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest request)
        {
            var affecterResult = await _manageProductService.Update(request);
            if (affecterResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
        //Delete : xóa
        [HttpPut("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var affecterResult = await _manageProductService.Delete(id);
            if (affecterResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("price/{id}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int id, decimal newPrice)
        {
            var isSuccessful = await _manageProductService.UpdatePrice(id,newPrice);
            if (isSuccessful)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
