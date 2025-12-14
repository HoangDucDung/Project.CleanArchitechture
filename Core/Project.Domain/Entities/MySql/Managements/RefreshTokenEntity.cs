using Project.Extensions.Entities;
using Project.Extensions.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities.MySql.Managements
{
    [Table("RefreshToken")]
    public class RefreshTokenEntity : ICreationTime
    {
        /// <summary>
        /// Id khóa chính
        /// </summary>
        [Key]
        public Guid TokenID { get; set; }

        /// <summary>
        /// Id người dùng
        /// </summary>
        [ForeignKey(nameof(User))]
        public int UserID { get; set; }

        /// <summary>
        /// Giá trị token
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Thời điểm hết hạn của token
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Trạng thái thu hồi (1: Đã thu hồi, 0: Chưa thu hồi)
        /// </summary>
        public bool IsRevoked { get; set; } = false;

        /// <summary>
        /// Trạng thái sử dụng (1: Đã sử dụng, 0: Chưa sử dụng)
        /// </summary>
        public bool IsUsed { get; set; } = false;

        /// <summary>
        /// Thời điểm thu hồi token
        /// </summary>
        public DateTime? RevokedAt { get; set; }

        /// <summary>
        /// Token thay thế khi token hiện tại bị thu hồi
        /// </summary>
        public string? ReplacedByToken { get; set; }

        public DateTime CreationTime { get; set; } = GenerateExtentions.Now;

        // Navigation
        public UserEntity User { get; set; } = null!;
        
    }
}
