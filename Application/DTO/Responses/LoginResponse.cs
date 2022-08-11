using Microsoft.AspNetCore.Identity;

namespace Application.DTO.Responses
{
    public class LoginResponse
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public SignInResult? Errors { get; set; }
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
