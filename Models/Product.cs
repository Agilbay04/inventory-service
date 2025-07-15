using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Models
{
    public class Product : BaseModel
    {
        public string Code { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(6)]
        public int Stock { get; set; }

        public bool IsPublish { get; set; }

        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}