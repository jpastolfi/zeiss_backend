using System.ComponentModel.DataAnnotations;

namespace zeiss_api.DTOs
{
    public class UpdateProductDto
    {
        public string? Name { get;  set; } = null!;
        public decimal? Price { get;  set; }
        public string? CategoryName { get;  set; } = null!;
    }
}