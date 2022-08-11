using Microsoft.AspNetCore.Identity;

namespace Application.DTO
{
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<IdentityError>? Errors { get; set; }
    }
}
