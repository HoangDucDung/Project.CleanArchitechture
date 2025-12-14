using Project.Extensions.Entities;
using Project.Extensions.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities.MySql.Managements
{
    [Table("Permissions")]
    public class PermissionEntity : ICreationTime, ILastUpdateTime
    {
        /// <summary>
        /// Id khóa chính
        /// </summary>
        [Key]
        public Guid PermissionID { get; set; }

        /// <summary>
        /// Tên quyền, không trùng lặp
        /// </summary>
        public string PermissionName { get; set; } = string.Empty;

        /// <summary>
        /// Mô tả chi tiết về quyền
        /// </summary>
        public string? Description { get; set; }

        public DateTime CreationTime { get; set; } = GenerateExtentions.Now;
        public DateTime LastUpdateTime { get; set; } = GenerateExtentions.Now;

        // Navigation
        public ICollection<RolePermissionEntity> RolePermissions { get; set; } = new HashSet<RolePermissionEntity>();
    }
}
