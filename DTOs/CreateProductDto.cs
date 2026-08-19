using System.ComponentModel.DataAnnotations;

namespace zeiss_api.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get;  set; } = null!;
        public decimal Price { get;  set; }
        public int Stock { get;  set; }
        [Required]
        public string CategoryName { get;  set; } = null!;
    }
}