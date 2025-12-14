using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Security;

namespace Project.Domain.Entities.MySql.Managements
{
    [Table("RolePermission")]
    public class RolePermissionEntity
    {
        /// <summary>
        /// Id khóa chính
        /// </summary>
        [Key]
        public Guid RolePermissionID { get; set; }

        /// <summary>
        /// Id vai trò
        /// </summary>
        [ForeignKey(nameof(Role))]
        public int RoleID { get; set; }

        /// <summary>
        /// Id quyền
        /// </summary>
        [ForeignKey(nameof(Permission))]
        public int PermissionID { get; set; }

        // Navigation
        public RoleEntity Role { get; set; } = null!;
        public PermissionEntity Permission { get; set; } = null!;
    }
}
