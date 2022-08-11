using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class ResetPasswordModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Otp is required")]
        public string? otp { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        public string? newPassword { get; set; }
    }
}
