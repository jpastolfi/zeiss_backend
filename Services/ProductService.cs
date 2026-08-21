using System.Diagnostics;
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

        public async Task<ProductResponseDto> GetProductByIdAsync(int productId)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId) ??
                throw new ProductNotFoundException(productId);

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                CategoryName = product.Category.Title,
                Price = product.Price,
                Stock = product.Stock,
            };
        }

        public async Task<List<ProductResponseDto>> GetProductByNameAsync(string productName)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(productName))
                .ToListAsync();
            
            return products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category.Title,
            }).ToList();
        }

        public async Task<List<ProductResponseDto>> GetProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
            
            return products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category.Title,
            }).ToList();
        }

        public async Task<List<ProductResponseDto>> GetProductsByStockRangeAsync(int minStock, int maxStock)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Stock >= minStock && p.Stock <= maxStock)
                .ToListAsync();
            
            return products.Select(p => new ProductResponseDto
            {
              Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category.Title,  
            }).ToList();
        }

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
        public async Task<ProductResponseDto> IncrementProductStockAsync(int productId, int quantity)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId) ??
                throw new ProductNotFoundException(productId);

            if (int.MaxValue - quantity > product.Stock)
                throw new StockOverflowException(productId, product.Stock, quantity);
            product.Stock += quantity;
            await _context.SaveChangesAsync();
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                CategoryName = product.Category.Title,
            };
        }
        public async Task<ProductResponseDto> DecrementProductStockAsync(int productId, int quantity)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId) ??
                throw new ProductNotFoundException(productId);
            if (quantity > product.Stock)
                throw new InsufficientStockException(productId, quantity, product.Stock);
            product.Stock -= quantity;
            await _context.SaveChangesAsync();
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                CategoryName = product.Category.Title,
            };
        }

        public async Task<ProductResponseDto> UpdateProductAsync(int productId, UpdateProductDto dto)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId) ??
                throw new ProductNotFoundException(productId);
            if (dto.Name is not null)
            {
                product.Name = dto.Name;
            }

            if (dto.Price is not null)
            {
                product.Price = dto.Price.Value;
            }

            if (dto.CategoryName is not null)
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Title == dto.CategoryName)
                    ?? throw new CategoryNotFoundException(dto.CategoryName);
                product.Category = category;
                product.CategoryId = category.Id;

            }
            await _context.SaveChangesAsync();
            return new ProductResponseDto
            {
              Id = product.Id,  
              Name = product.Name,  
              Stock = product.Stock,  
              Price = product.Price,  
              CategoryName = product.Category.Title,
            };
        }
        public async Task<ProductResponseDto> DeleteProductAsync(int productId)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId) ??
                throw new ProductNotFoundException(productId);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                CategoryName = product.Category.Title,
            };
        }

        private static int GenerateProductId()
        {
            Random rnd = new();
            int candidate_id = rnd.Next(100000, 999999);
            return candidate_id;
        }
    }
}