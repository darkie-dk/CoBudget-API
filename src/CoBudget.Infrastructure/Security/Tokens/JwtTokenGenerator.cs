﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoBudget.Domain.Entities;
using CoBudget.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace CoBudget.Infrastructure.Security.Tokens;
public class JwtTokenGenerator(uint expirationTimeMinutes, string signingKey) : IAcessTokenGenerator
{
    private readonly uint _expirationTimeMinutes = expirationTimeMinutes;
    private readonly string _signingKey = signingKey;
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>()
        {
            new Claim("guid", user.UserId.ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_signingKey);

        return new SymmetricSecurityKey(key);
    }
}
