using System.ComponentModel.DataAnnotations;

namespace Zeiss_Api.Models
{
    public class Category
    {
        public int Id { get;  set; }
        [Required]
        [MaxLength(100)]
        public string Title { get;  set; } = null!;
    }
}