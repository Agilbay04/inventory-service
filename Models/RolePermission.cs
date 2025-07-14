using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Models
{
    public class RolePermission : BaseModel
    {
        public Guid RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }

        public Guid PermissionId { get; set; }

        [ForeignKey(nameof(PermissionId))]
        public virtual Permission Permission { get; set; }
    }
}