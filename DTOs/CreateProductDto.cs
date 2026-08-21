using System.ComponentModel.DataAnnotations;

namespace zeiss_api.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get;  set; } = null!;
        [Range(0.01, double.MaxValue)]
        public decimal Price { get;  set; }
        [Range(0, int.MaxValue)]
        public int Stock { get;  set; }
        [Required]
        public string CategoryName { get;  set; } = null!;
    }
}