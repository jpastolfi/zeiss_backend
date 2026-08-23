using System.ComponentModel.DataAnnotations;

namespace zeiss_api.DTOs
{
    public class UpdateProductDto
    {
        [MinLength(3)]
        public string? Name { get;  set; }
        [Range(0.01, double.MaxValue)]
        public decimal? Price { get;  set; }
        public string? CategoryName { get;  set; }
    }
}