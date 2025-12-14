using System.ComponentModel.DataAnnotations;

namespace Project.Application.Contract.Models.Auths.Registers
{
    public class ReqRegisterDto
    {
        [Required(ErrorMessage = "Không được để trống.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Không được để trống.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Không được để trống.")]
        public string Password { get; set; } = string.Empty;

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
