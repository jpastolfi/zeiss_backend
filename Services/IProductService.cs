using zeiss_api.Models;

namespace zeiss_api.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetProductByNameAsync(string productName);
        public Task<Product> GetProductByIdAsync(int productId);
        public Task<List<Product>> GetProductsAsync();
        public Task<List<Product>> GetProductsByStockRangeAsync(int minStock, int maxStock);
        public Task<Product> CreateProductAsync(Product product);
        public Task<Product> IncrementProductStockAsync(int productId, int quantity);
        public Task<Product> DecrementProductStockAsync(int productId, int quantity);
        public Task<Product> UpdateProductAsync(Product product);
        public Task<Product> DeleteProductAsync(int productId);
    }
}