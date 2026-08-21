using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;
using zeiss_api.DTOs;
using zeiss_api.Services;

[ApiController]
[Route("/api/products")]
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> CreateProduct(CreateProductDto dto)
    {
        var result = await _productService.CreateProductAsync(dto);
        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductResponseDto>> GetProductById(int productId)
    {
        var result = await _productService.GetProductByIdAsync(productId);
        return Ok(result);
    }
}