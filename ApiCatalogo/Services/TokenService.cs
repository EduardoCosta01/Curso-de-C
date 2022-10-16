using ApiCatalogo.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiCatalogo.Services
{
    public class TokenService : ITokenService
    {
        public string GerarToken(string Key, string issuer, string audience, UserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

            var credentials = new SigningCredentials(securitykey,
                                  SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer,
                                             audience: audience,
                                             claims: claims,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }

}
