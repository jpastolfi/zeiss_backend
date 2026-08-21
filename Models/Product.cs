using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zeiss_api.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get;  set; }
        [Required]
        public string Name { get;  set; } = null!;
        public decimal Price { get;  set; }
        public int Stock { get;  set; }
        public int CategoryId { get;  set; }
        public Category Category { get;  set; } = null!;
    }
}