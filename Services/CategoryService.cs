using zeiss_api.Services;
using zeiss_api.DTOs;
using zeiss_api.Models;


namespace zeiss_api.Services
{
    public class CategoryService : ICategoryService
    {
        public Task<Category> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }
    }
}