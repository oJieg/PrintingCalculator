using printing_calculator.Singletones.Interfases;
using System.Security.Cryptography;

namespace printing_calculator.Singletones
{
    public class SecurityCryptographyTokenGenerator : ITokenGenerator
    {
        private const int DEFAULT_TOKEN_SIZE = 32; //TODO вынести в конфиг

        public string GenerateRandomToken()
        {
            byte[] randomByets = new byte[DEFAULT_TOKEN_SIZE];
            RandomNumberGenerator.Fill(randomByets);
            return Convert.ToBase64String(randomByets)
                .Replace("+", "-")
                .Replace("/", "_")
                .TrimEnd('=');
        }
    }
}
