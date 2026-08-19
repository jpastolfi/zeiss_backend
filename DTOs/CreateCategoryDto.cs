using System.ComponentModel.DataAnnotations;

namespace zeiss_api.DTOs
{
    public class CreateCategoryDto
    {
        [Required]
        public string CategoryName { get; set; } = null!;
    }
}