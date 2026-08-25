using zeiss_api.Models;
using zeiss_api.Data;
using Microsoft.EntityFrameworkCore;


namespace zeiss_api.Services
{
    public class CategoryService(ApplicationDbContext context) : ICategoryService
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}