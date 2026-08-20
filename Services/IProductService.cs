using zeiss_api.DTOs;
using zeiss_api.Models;

namespace zeiss_api.Services
{
    public interface IProductService
    {
        public Task<List<ProductResponseDto>> GetProductByNameAsync(string productName);
        public Task<ProductResponseDto> GetProductByIdAsync(int productId);
        public Task<List<ProductResponseDto>> GetProductsAsync();
        public Task<List<ProductResponseDto>> GetProductsByStockRangeAsync(int minStock, int maxStock);
        public Task<ProductResponseDto> CreateProductAsync(CreateProductDto product);
        public Task<ProductResponseDto> IncrementProductStockAsync(int productId, int quantity);
        public Task<ProductResponseDto> DecrementProductStockAsync(int productId, int quantity);
        public Task<ProductResponseDto> UpdateProductAsync(UpdateProductDto product);
        public Task<ProductResponseDto> DeleteProductAsync(int productId);
    }
}