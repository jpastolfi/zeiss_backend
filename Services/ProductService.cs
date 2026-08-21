using Microsoft.EntityFrameworkCore;
using zeiss_api.Data;
using zeiss_api.DTOs;
using zeiss_api.Exceptions;
using zeiss_api.Models;
using zeiss_api.Services;

namespace zeiss_api.Services
{
    public class ProductService(ApplicationDbContext context) : IProductService
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto product)
        {
            Category category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Title == product.CategoryName) ?? 
                throw new CategoryNotFoundException(product.CategoryName);
            Product newProduct = new()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Category = category,
                CategoryId = category.Id,
            };
            _context.Products.Add(newProduct);
            for (int i = 0; i < 10; i++)
            {
                newProduct.Id = GenerateProductId();
                try
                {
                    await _context.SaveChangesAsync();
                    Console.WriteLine("It worked!");
                    ProductResponseDto response = new()
                    {
                        Id = newProduct.Id,
                        Name = newProduct.Name,
                        Price = newProduct.Price,
                        Stock = newProduct.Stock,
                        CategoryName = newProduct.Category.Title,
                    };
                    return response;
                }
                catch (DbUpdateException)
                { }
            }
            throw new IdGenerationException();
        }

        public Task<ProductResponseDto> DecrementProductStockAsync(int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponseDto> DeleteProductAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponseDto> GetProductByIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductResponseDto>> GetProductByNameAsync(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductResponseDto>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductResponseDto>> GetProductsByStockRangeAsync(int minStock, int maxStock)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponseDto> IncrementProductStockAsync(int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponseDto> UpdateProductAsync(UpdateProductDto product)
        {
            throw new NotImplementedException();
        }

        private static int GenerateProductId()
        {
            Random rnd = new();
            int candidate_id = rnd.Next(100000, 999999);
            return candidate_id;
        }
    }
}