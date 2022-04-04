using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Commen.Helper
{
    public static class TokenHelper
    {
        public static string GenerateToken(Dictionary<string, string> identityData,string secret,string issuer, int expiredMinutes = 10080)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identityData.ToList().ForEach(x => claimsIdentity.AddClaim(new Claim(x.Key, x.Value)));
            var expireTime = DateTime.UtcNow.AddMinutes(expiredMinutes);
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer,
                string.Empty,
                claimsIdentity.Claims,
                null,
                expireTime,
                signinCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
