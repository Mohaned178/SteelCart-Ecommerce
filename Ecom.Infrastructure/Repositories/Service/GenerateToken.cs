using Ecom.Core.Entities;
using Ecom.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Ecom.infrastructure.Repositries.Service
{
    public class GenerateToken : IGenerateToken
    {
        private readonly IConfiguration configuration;

        public GenerateToken(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetAndCreateToken(AppUser user)
        {
            
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentException("User email is required");
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id), 
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email) 
            };

            string secret = configuration["Token:Secret"];
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentNullException("Token:Secret", "Token secret is not configured in appsettings.json");
            }

            byte[] key = Encoding.UTF8.GetBytes(secret);
            SigningCredentials credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            );

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = configuration["Token:Issuer"],
                SigningCredentials = credentials,
                NotBefore = DateTime.UtcNow
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            
            Console.WriteLine($"Generated JWT for user: {user.Email}");

            return tokenString;
        }
    }
}