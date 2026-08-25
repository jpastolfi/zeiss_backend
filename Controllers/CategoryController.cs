using Microsoft.AspNetCore.Mvc;
using zeiss_api.Models;
using zeiss_api.Services;

namespace zeiss_api.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoryController(ICategoryService service) : ControllerBase
    {
        private readonly ICategoryService _service = service;
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var result = await _service.GetCategoriesAsync();
            return Ok(result);
        }
    }
}