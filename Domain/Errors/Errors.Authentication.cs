using ErrorOr;

namespace Domain.Errors
{
    public static partial class Errors
    {
        public static Error InvalidCredentials => Error.Validation(
                code: "Auth.InvalidCredentials",
                description: "Invalid Credentials"
            );
    }
}
