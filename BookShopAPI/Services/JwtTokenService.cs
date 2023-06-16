using BookShopAPI.Abstract;
using BookShopAPI.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookShopAPI.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<UserEntity> _userManager;
        public JwtTokenService(IConfiguration configuration, UserManager<UserEntity> userManager)
        {
            _config = configuration;
            _userManager = userManager;
        }
        public async Task<string> CreateToken(UserEntity user)
        { 
            List<Claim> claims = new List<Claim>()
            {
                new Claim("email", user.Email)
            }; 
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<String>("JwtSecretKey")));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredentials,
                expires: DateTime.Now.AddDays(10),
                claims: claims
           );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
} 
