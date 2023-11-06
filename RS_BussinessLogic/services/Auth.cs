using Microsoft.EntityFrameworkCore;
using MLGBussinesLogic.interfaces;
using MLGBussinesLogic.models.dto;
using MLGBussinessLogic.middleware;
using MLGDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLGBussinesLogic.services
{
    public class AuthRepository : IAuthRepository
    {
        private AppDBContext _dbcontext;
        private HashMiddleware _hashMiddleware;
        private JwtMiddleware _jwtMiddleware;
        public AuthRepository(AppDBContext dbcontext, HashMiddleware hashMiddleware, JwtMiddleware jwtMiddleware)
        {
            _dbcontext = dbcontext;
            _hashMiddleware = hashMiddleware;
            _jwtMiddleware = jwtMiddleware;
        }
        public async Task<TokenResultDto>Login(AuthDto auth)
        {
            var usuario = await _dbcontext.Usuarios.Where(u => u.UsuarioNombre == auth.UserName).Include(x => x.UsuarioCliente).FirstOrDefaultAsync();

            if (usuario == null)
                return null;


            // check if password is correct
            if (!_hashMiddleware.VerifyPasswordHash(auth.Password, usuario.password,usuario.PasswordSalt))
                return null;

            var token = _jwtMiddleware.CreateToken(usuario);

            return token;
        }
    }
}
