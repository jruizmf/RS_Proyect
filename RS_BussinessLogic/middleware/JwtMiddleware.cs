using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MLGBussinesLogic.interfaces;
using MLGBussinesLogic.models.dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using MLGBussinessLogic.models.common;
using System.Threading.Tasks;
using MLGBussinesLogic.models;
using Microsoft.Extensions.Configuration;

namespace MLGBussinessLogic.middleware
{
    public class JwtMiddleware
    {
        private readonly IConfiguration _config;

        public JwtMiddleware(IConfiguration config)
        {
            _config = config;
        }
        public  TokenResultDto CreateToken(MLGDataAccessLayer.models.UsuarioModelo usuario) {

            if (usuario == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>(
                "JWT:secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new TokenResultDto
            {
                Id = usuario.Id,
                Cliente= usuario.UsuarioCliente,
                Usuario = usuario.UsuarioNombre,
                Status = usuario.status,
                Token = tokenString
            };
        }
    }
}
