
namespace Application.DTO.Responses
{
    public class RefreshTokenResponse
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public string AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
