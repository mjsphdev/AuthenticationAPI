using Domain;

namespace Application.Authentication.Common
{
    public record AuthenticationResult(User user, string Token);
}
