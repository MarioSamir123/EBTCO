using EBTCO.Core.Contract.Identity;
using EBTCO.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EBTCO.RDS.Identity
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public TokenGenerator(UserManager<User> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenerateWebToken(User User)
        {
            var roles = await _userManager.GetRolesAsync(User);
            var tokenHandler = new JwtSecurityTokenHandler();
            String TokenKey = _configuration.GetSection("JWTKey").Value ?? String.Empty;
            var key = Encoding.ASCII.GetBytes(TokenKey);

            var claims = new List<Claim>
            {
                new Claim("Sub", User.Id.ToString()),
                new Claim(ClaimTypes.Email, User.Email ?? String.Empty),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            int.TryParse(_configuration.GetSection("JWTexpiredTimeIsSeconds").Value, out int expiredTime);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(expiredTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            await _userManager.UpdateAsync(User);
            String jwt = tokenHandler.WriteToken(token);
            return jwt;
        }
    }
}
