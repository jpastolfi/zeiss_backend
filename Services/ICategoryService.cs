using zeiss_api.Models;

namespace zeiss_api.Services
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetCategoriesAsync();
    }
}