using System.ComponentModel.DataAnnotations;

namespace zeiss_api.Models
{
    public class Category
    {
        public int Id { get;  set; }
        [Required]
        [MaxLength(100)]
        public string Title { get;  set; } = null!;
    }
}