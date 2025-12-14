using Project.Extensions.Entities;
using Project.Extensions.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.MySql.Managements
{
    [Table("Role")]
    public class RoleEntity : ICreationTime, ILastUpdateTime
    {
        /// <summary>
        /// Id khóa chính
        /// </summary>
        [Key]
        public Guid RoleID { get; set; }

        /// <summary>
        /// Tên vai trò, không trùng lặp
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// Mô tả chi tiết về vai trò
        /// </summary>
        public string? Description { get; set; }

        public DateTime CreationTime { get; set; } = GenerateExtentions.Now;
        public DateTime LastUpdateTime { get; set; } = GenerateExtentions.Now;

        // Navigation
        public ICollection<UserRoleEntity> UserRoles { get; set; } = new HashSet<UserRoleEntity>();
        public ICollection<RolePermissionEntity> RolePermissions { get; set; } = new HashSet<RolePermissionEntity>();
    }
}
