using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletAspNetCore.Auth
{
    public  class JwtParser
    {
        public JwtParser()
        {
            
        }

        public Guid? ExtractIdUser(string authHeader)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.ReadToken(authHeader) as JwtSecurityToken;
            Guid.TryParse(securityToken.Claims.First(claim => claim.Type == "userId").Value, out Guid userId);

            return userId;
        }
    }
}
