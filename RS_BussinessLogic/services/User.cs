using Microsoft.EntityFrameworkCore;
using RS_BussinessLogic.models;
using RS_BussinessLogic.helpers;
using RS_BussinessLogic.interfaces;
using RS_BussinessLogic.middleware;
using RS_BussinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLGBussinessLogic.services
{

    public class UserRepository : IUserRepository
    {
        public readonly AppDBContext _dbcontext;
        private HashMiddleware _hashMiddleware;
        public UsuarioRepository(AppDBContext dbcontext, HashMiddleware hashMiddleware)
        {
            _dbcontext = dbcontext;
            _hashMiddleware = hashMiddleware;
        }

        public async Task<List<MLGDataAccessLayer.models.UsuarioModelo>> GetAll()
        {
            var usuarios = await _dbcontext.Usuarios.ToListAsync<MLGDataAccessLayer.models.UsuarioModelo>();
            return usuarios;
        }
        public async Task<MLGDataAccessLayer.models.UsuarioModelo> GetOne(Guid Id)
        {
            var usuarios = await _dbcontext.Usuarios.Where(u => u.Id == Id).FirstOrDefaultAsync();
            return usuarios;
        }
        public async Task<Guid> Add(UsuarioModelo _usuario)
        {
            if (string.IsNullOrWhiteSpace(_usuario.password))
                throw new AppException("la contraseña es requerida");

            if (_dbcontext.Usuarios.Any(x => x.UsuarioNombre == _usuario.UsuarioNombre))
                throw new AppException("El usuario: \"" + _usuario.UsuarioNombre + "\" ya ha sido utilizado");

            byte[] passwordHash, passwordSalt;

            _hashMiddleware.CreatePasswordHash(_usuario.password, out passwordHash, out passwordSalt);

            MLGDataAccessLayer.models.UsuarioModelo usuario = new MLGDataAccessLayer.models.UsuarioModelo() { 

            };
            usuario.UsuarioNombre = _usuario.UsuarioNombre;
            usuario.status = 1;
            usuario.password = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            _dbcontext.Usuarios.Add(usuario);
            await _dbcontext.SaveChangesAsync();


            return usuario.Id;
        }
        public async Task<string> Update(Guid id, UsuarioModelo usuario)
        {
            var _usuario = await _dbcontext.Usuarios.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (_usuario == null)
                throw new AppException("El usuario no existe");


            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(usuario.UsuarioNombre) && usuario.UsuarioNombre != _usuario.UsuarioNombre)
            {
                // throw error if the new username is already taken
                if (_dbcontext.Usuarios.Any(x => x.UsuarioNombre == usuario.UsuarioNombre))
                    throw new AppException($"El usuario {usuario.UsuarioNombre} ya existe");

                _usuario.UsuarioNombre = usuario.UsuarioNombre;
            }

            // update password if provided
            if (!string.IsNullOrWhiteSpace(usuario.password))
            {
                byte[] passwordHash, passwordSalt;
                _hashMiddleware.CreatePasswordHash(usuario.password, out passwordHash, out passwordSalt);

                _usuario.password = passwordHash;
                _usuario.PasswordSalt = passwordSalt;
            }

            _dbcontext.Usuarios.Update(_usuario);
            _dbcontext.SaveChanges();
            await _dbcontext.SaveChangesAsync();
            return "Usuario modificado exitosamente";
        }
        public async Task<string> Delete(Guid id)
        {
            var _usuario = _dbcontext.Usuarios.Where(empid => empid.Id == id).FirstOrDefault();
            if (_usuario == null)
            {
                return "El usuario no existe";
            }
            _dbcontext.Usuarios.Remove(_usuario);

            await _dbcontext.SaveChangesAsync();

            return "Usuario eliminado exitosamente";
        }
    }
}
