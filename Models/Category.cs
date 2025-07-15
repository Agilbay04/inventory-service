using System.ComponentModel.DataAnnotations;

namespace InventoryService.Models
{
    public class Category : BaseModel
    {
        public string Key { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}