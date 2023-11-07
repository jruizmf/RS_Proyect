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
using RS_DataAccess;
using RS_DataAccess.models;
using RS_BussinessLogic.models.dto;

namespace RS_BussinessLogic.services
{

    public class UserRepository : IUserRepository
    {
        public readonly AppDBContext _dbcontext;
        private HashMiddleware _hashMiddleware;
        public UserRepository(AppDBContext dbcontext, HashMiddleware hashMiddleware)
        {
            _dbcontext = dbcontext;
            _hashMiddleware = hashMiddleware;
        }

        public async Task<List<User>> GetAll()
        {
            var usuarios = await _dbcontext.Users.ToListAsync<User>();
            return usuarios;
        }
        public async Task<User> GetOne(Guid Id)
        {
            var usuarios = await _dbcontext.Users.Where(u => u.Id == Id).FirstOrDefaultAsync();
            return usuarios;
        }
        public async Task<string> Add(UserDto user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new AppException("la contraseña es requerida");

            if (_dbcontext.Users.Any(x => x.UserName == user.UserName))
                throw new AppException("El usuario: \"" + user.UserName + "\" ya ha sido utilizado");

            byte[] passwordHash, passwordSalt;

            _hashMiddleware.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            User _user = new User() { 

            };
            _user.UserName = user.UserName;
            _user.Status = 1;
            _user.Password = passwordHash;

            _dbcontext.Users.Add(_user);
            await _dbcontext.SaveChangesAsync();


            return user.Id.ToString();
        }
        public async Task<string> Update(Guid id, UserDto user)
        {
            var _user = await _dbcontext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (_user == null)
                throw new AppException("El usuario no existe");


            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(user.UserName) && user.UserName != _user.UserName)
            {
                // throw error if the new username is already taken
                if (_dbcontext.Users.Any(x => x.UserName == user.UserName))
                    throw new AppException($"El usuario {user.UserName} ya existe");

                _user.UserName = user.UserName;
            }

            // update password if provided
            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                byte[] passwordHash, passwordSalt;
                _hashMiddleware.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

                _user.Password = passwordHash;
            }

            _dbcontext.Users.Update(_user);
            _dbcontext.SaveChanges();
            await _dbcontext.SaveChangesAsync();
            return "Usuario modificado exitosamente";
        }
        public async Task<string> Delete(Guid id)
        {
            var _usuario = _dbcontext.Users.Where(empid => empid.Id == id).FirstOrDefault();
            if (_usuario == null)
            {
                return "El usuario no existe";
            }
            _dbcontext.Users.Remove(_usuario);

            await _dbcontext.SaveChangesAsync();

            return "Usuario eliminado exitosamente";
        }
    }
}
