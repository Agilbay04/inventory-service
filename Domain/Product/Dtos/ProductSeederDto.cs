using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Domain.Inventory.Dtos
{
    public class ProductSeederDto
    {
        public string Name { get; set; }
        public bool IsPublish { get; set; }
        public string Code { get; set; }
        public string Key { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}