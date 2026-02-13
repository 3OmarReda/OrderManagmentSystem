using ApplicationLayer.Dtos.Products;
using ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Validation;
using PresentationLayer.Validation.Products;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDto dto)
        {
            if (dto is null)
                return BadRequest("Product data is required.");

            var validator = new AddProductValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Message = "Invalid Product Data",
                    Errors = errors
                });
            }

            var result = await _productService.AddProductAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Error.Message);

            return Ok(new { Message = "Product added successfully!" });
        }

        [HttpPut("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] UpdateProductDto dto)
        {
            if (productId == Guid.Empty)
                return BadRequest("Invalid Product Id.");

            var validator = new UpdateProductValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Message = "Invalid Product Data",
                    Errors = errors
                });
            }

            var result = await _productService.UpdateProductAsync(productId, dto);

            if (!result.IsSuccess)
                return BadRequest(result.Error.Message);

            return Ok(new { Message = "Product updated successfully!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsWithPaginationDto dto)
        {
            var validator = new GetAllProductsWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Message = "Invalid Pagination Data",
                    Errors = errors
                });
            }

            var result = await _productService.GetAllProducts(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Error.Message);

            return Ok(result.Data);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            if (productId == Guid.Empty)
                return BadRequest("Invalid Product Id.");

            var result = await _productService.GetProductByIdAsync(productId);

            if (!result.IsSuccess)
                return NotFound(result.Error.Message);

            return Ok(result.Data);
        }
    }
}
