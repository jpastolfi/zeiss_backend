using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;
using zeiss_api.DTOs;
using zeiss_api.Models;
using zeiss_api.Services;


namespace zeiss_api.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(int productId)
        {
            var result = await _productService.GetProductByIdAsync(productId);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<ProductResponseDto>>> GetProductByName([FromQuery] string name)
        {
            var result = await _productService.GetProductByNameAsync(name);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ProductResponseDto>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("stock-level")]
        public async Task<ActionResult<List<Product>>> GetProductsByStockRange(
            [FromQuery] string min,
            [FromQuery] string max
        )
        {
            var products = await _productService.GetProductsByStockRangeAsync(
                int.Parse(min),
                int.Parse(max)
            );

            return Ok(products);

        }
        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct(CreateProductDto dto)
        {
            var result = await _productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new { productId = result.Id}, result);
        }

        [HttpPost("{id}/add-to-stock/{quantity}")]
        public async Task<ActionResult<ProductResponseDto>> IncrementProductStock(int id, int quantity)
        {
            var result = await _productService.IncrementProductStockAsync(id, quantity);
            return Ok(result);
        }

        [HttpPost("{id}/decrement-stock/{quantity}")]
        public async Task<ActionResult<ProductResponseDto>> DecrementProductStock(int id, int quantity)
        {
            var result = await _productService.DecrementProductStockAsync(id, quantity);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponseDto>> UpdateProduct(int id, UpdateProductDto product)
        {
            var result = await _productService.UpdateProductAsync(id, product);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductResponseDto>> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            return Ok(result);
        }
    }
}