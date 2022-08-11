using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class SendOTP
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? ResetEmail { get; set; }
    }
}
