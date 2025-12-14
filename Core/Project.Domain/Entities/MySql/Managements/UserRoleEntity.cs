using Project.Extensions.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Project.Domain.Entities.MySql.Managements
{
    [Table("UserRole")]
    public class UserRoleEntity
    {
        /// <summary>
        /// Id khóa chính
        /// </summary>
        [Key]
        public Guid UserRoleID { get; set; }

        /// <summary>
        /// Id người dùng
        /// </summary>
        [ForeignKey(nameof(User))]
        public int UserID { get; set; }

        /// <summary>
        /// Id vai trò
        /// </summary>
        [ForeignKey(nameof(Role))]
        public int RoleID { get; set; }

        /// <summary>
        /// Thời điểm gán vai trò cho người dùng
        /// </summary>
        public DateTime AssignedAt { get; set; } = GenerateExtentions.Now;

        // Navigation
        public UserEntity User { get; set; } = null!;
        public RoleEntity Role { get; set; } = null!;
    }
}
