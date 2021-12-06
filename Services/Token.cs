using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Models = Domain.Models;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Services
{
  public class Token : IToken
  {
    private readonly IConfiguration _config;

    public Token(IConfiguration config)
    {
      _config = config;
    }

    public string GenerateToken(Models.User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_config["JwtConfiguration:SecretKey"]);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.AccessLevel.ToString()),
                    new Claim("user_login", user.Login)
          }),
        Expires = DateTime.UtcNow.AddHours(1),
        Issuer = _config["JwtConfiguration:Issuer"],
        Audience = _config["JwtConfiguration:Audience"],
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}