using Microsoft.IdentityModel.Tokens;
using NBK_API.Models.Entites;
using NBK_API.Models.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoodleBackEnd.Models.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext context;

        public TokenService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            this.context = context;
        }

        public (bool IsValid, string Token) GenerateToken(string Email, string password)
        {

            var userAccount = context.Users.SingleOrDefault(r => r.Email == Email);
            if (userAccount == null)
            {
                return (false, "");
            }

            var validPassword = BCrypt.Net.BCrypt.EnhancedVerify(password, userAccount.PasswordHash);
            if (!validPassword)
            {
                return (false, "");

            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // From here
            var claims = new[]
            {
        new Claim(TokenClaimsConstant.Username, Email),
        new Claim(TokenClaimsConstant.UserId, userAccount.Id.ToString()),

        };

            // To Here
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Expire
                signingCredentials: credentials);
            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return (true, generatedToken);
        }
    }
}
