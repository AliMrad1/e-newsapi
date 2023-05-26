using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BLC
{
    public class GenerateJWT
    {
        private IConfiguration _configuration;

        public GenerateJWT(IConfiguration _configuration) 
        {
            this._configuration = _configuration;
        }

        public string GenerateJwtToken(UserResponse user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string key_gen = GenerateRandomKey(256);
            var key = Encoding.ASCII.GetBytes(key_gen);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Role, user.role.role_name),
                new Claim(ClaimTypes.Email, user.email)
            }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(this._configuration["Jwt:TokenExpirationDays"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateJwtVerificationToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string key_gen = GenerateRandomKey(256);
            var key = Encoding.ASCII.GetBytes(key_gen);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Email, email)
            }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(this._configuration["Jwt:VerficationTokenExpirationDays"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        string GenerateRandomKey(int keySizeInBits)
        {
            byte[] keyBytes = new byte[keySizeInBits / 8];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }
            return Convert.ToBase64String(keyBytes);
        }


        public bool IsTokenExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = false,
                ValidateLifetime = true
            };

            try
            {
                // Parse and validate the JWT token
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);

                // Extract the expiration claim from the token's claims
                var expirationClaim = claimsPrincipal.FindFirst("exp");

                // Check if the expiration claim exists and if the expiration time is in the past
                if (expirationClaim != null && DateTime.TryParse(expirationClaim.Value, out var expirationDate))
                {
                    return expirationDate < DateTime.UtcNow;
                }

                throw new Exception("Invalid token format or missing expiration claim");

            }
            catch (Exception)
            {
                throw new Exception("Invalid token format or other error occurred");
            }
        }
    }

   
}
