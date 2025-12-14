using Project.Extensions.Entities;
using Project.Extensions.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities.MySql.Managements
{
    [Table("User")]
    public class UserEntity : ICreationTime, ILastUpdateTime
    {
        /// <summary>
        /// Id người dùng
        /// </summary>
        [Key]
        public Guid UserID { get; set; }

        /// <summary>
        /// Tên đăng nhập, không trùng lặp
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Email, không trùng lặp
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Mật khẩu đã được mã hóa
        /// </summary>

        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Chuỗi ngẫu nhiên dùng để tăng cường bảo mật mật khẩu
        /// </summary>
        public string Salt { get; set; } = string.Empty;

        /// <summary>
        /// Họ tên đầy đủ của người dùng
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>

        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Trạng thái hoạt động của tài khoản (1: active, 0: inactive)
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Trạng thái xác thực email (1: đã xác thực, 0: chưa xác thực)
        /// </summary>
        public bool IsEmailVerified { get; set; } = false;

        /// <summary>
        /// Trạng thái xác thực số điện thoại (1: đã xác thực, 0: chưa xác thực)
        /// </summary>
        public bool IsPhoneVerified { get; set; } = false;

        /// <summary>
        /// Thời gian đăng nhập cuối cùng
        /// </summary>
        public DateTime UserLastLogin { get; set; } = GenerateExtentions.Now;

        public DateTime CreationTime { get; set; } = GenerateExtentions.Now;

        public DateTime LastUpdateTime{ get; set; } = GenerateExtentions.Now;

        // Navigation properties
        public ICollection<UserRoleEntity> UserRoles { get; set; } = new HashSet<UserRoleEntity>();
        public ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = new HashSet<RefreshTokenEntity>();
    }
}
