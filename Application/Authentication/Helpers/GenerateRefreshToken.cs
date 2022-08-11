using System.Security.Cryptography;
using System.Text;


namespace Application.Authentication.Helpers
{
    public class GenerateRefreshToken
    {
        public string GetRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
