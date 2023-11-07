using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RS_BussinessLogic.interfaces;
using RS_BussinessLogic.models.dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using RS_BussinessLogic.models.common;
using System.Threading.Tasks;
using RS_BussinessLogic.models;
using Microsoft.Extensions.Configuration;
using RS_DataAccess.models;

namespace RS_BussinessLogic.middleware
{
    public class JwtMiddleware
    {
        private readonly IConfiguration _config;

        public JwtMiddleware(IConfiguration config)
        {
            _config = config;
        }
        public TokenResultDto CreateToken(User user) {

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config[ "JWT:secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new TokenResultDto
            {
                Id = user.Id,
                Profile= user.UserProfile,
                User = user.UserName,
                Status = user.Status,
                Token = tokenString
            };
        }
    }
}
