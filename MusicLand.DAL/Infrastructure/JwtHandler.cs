using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MusicLand.DAL.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicLand.DAL.Infrastructure
{
    public class JwtHandler
    {
        private readonly string secretKey;

        public JwtHandler(IConfiguration configuration)
        {
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = GetClaims(user!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public List<Claim> GetClaims(User user, string role = "User")
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Email, user.Email),
                new (ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new (ClaimTypes.Role, role)
            };

            return claims;
        }
    }
}
