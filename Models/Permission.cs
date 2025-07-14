using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Models
{
    public class Permission : BaseModel
    {
        [MaxLength(150)]
        public string Name { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(150)]
        public string Key { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
