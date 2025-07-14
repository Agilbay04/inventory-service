using System.ComponentModel.DataAnnotations;

namespace InventoryService.Models
{
    public class Inventory : BaseModel
    {
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(6)]
        public int Stock { get; set; }

        public bool IsPublish { get; set; }
    }
}