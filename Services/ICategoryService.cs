using zeiss_api.DTOs;
using zeiss_api.Models;

namespace zeiss_api.Services
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetCategoriesAsync();
        public Task<Category> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    }
}