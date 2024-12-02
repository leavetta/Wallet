using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Auth
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _options;
        
        public JwtService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_options.Key);
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            Claim[] claims = [new("userId", user.Id.ToString())];

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
