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
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get;  set; }
        [Range(0, int.MaxValue)]
        public int Stock { get;  set; }
        public int CategoryId { get;  set; }
        public Category Category { get;  set; } = null!;
    }
}