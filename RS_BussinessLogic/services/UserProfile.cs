using Microsoft.EntityFrameworkCore;
using MLGBussinesLogic.models.dto;
using MLGBussinessLogic.helpers;
using MLGBussinessLogic.interfaces;
using MLGBussinessLogic.middleware;
using MLGDataAccessLayer;
using MLGDataAccessLayer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLGBussinessLogic.services
{

    public class UsuarioClienteRepository : IUsuarioClienteRepository
    {
        public readonly AppDBContext _dbcontext;
        private HashMiddleware _hashMiddleware;
        public UsuarioClienteRepository(AppDBContext dbcontext, HashMiddleware hashMiddleware)
        {
            _dbcontext = dbcontext;
            _hashMiddleware = hashMiddleware;
        }

        public async Task<List<UsuarioClienteModelo>> GetAll()
        {
            var usuarioCliente = await _dbcontext.UsuarioClientes.Include(d => d.Usuario).Include(d => d.Cliente).ToListAsync<UsuarioClienteModelo>();
            return usuarioCliente;
        }
        public async Task<UsuarioClienteModelo>GetOne(Guid Id)
        {
            var usuarioCliente = await _dbcontext.UsuarioClientes.Include(d => d.Usuario).Include(d => d.Cliente).Where(u => u.Id == Id).FirstOrDefaultAsync();
            if (usuarioCliente == null) {
                usuarioCliente = await _dbcontext.UsuarioClientes.Include(d => d.Usuario).Include(d => d.Cliente).Where(u => u.UsuarioId == Id).FirstOrDefaultAsync();
            }

            return usuarioCliente;
        }

        public async Task<Guid> Add(UsuarioDto usuarioCliente)
        {
            
            if (usuarioCliente.ClienteId == null)
            {
                var _cliente = new ClienteModelo() { Nombre = usuarioCliente.Nombre, Apellidos = usuarioCliente.Apellidos, Direccion = usuarioCliente.Direccion };

                await _dbcontext.Clientes.AddAsync(_cliente);

                usuarioCliente.ClienteId = _cliente.Id;
            }
            if (usuarioCliente.UsuarioId == null ) {
               
                byte[] passwordHash, passwordSalt;

                _hashMiddleware.CreatePasswordHash(usuarioCliente.Password, out passwordHash, out passwordSalt);

                UsuarioModelo _usuario = new UsuarioModelo()
                {
                    UsuarioNombre = usuarioCliente.UsuarioNombre,
                };
                _usuario.status = 1;
                _usuario.password = passwordHash;
                _usuario.PasswordSalt = passwordSalt;

                await _dbcontext.Usuarios.AddAsync(_usuario);
                usuarioCliente.UsuarioId = _usuario.Id;
            }

            _dbcontext.UsuarioClientes.Add(new UsuarioClienteModelo()
            {
                UsuarioId = usuarioCliente.UsuarioId.Value,
                ClienteId = usuarioCliente.ClienteId.Value,
                fecha = new DateTime()
            });
            await _dbcontext.SaveChangesAsync();

            return usuarioCliente.Id;
        }
        public async Task<string> Update(Guid id, UsuarioDto usuarioCliente)
        {
            var _usuarioCliente = await _dbcontext.UsuarioClientes.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (_usuarioCliente == null)
                throw new AppException("Usuario no encontrado");

            if (usuarioCliente.ClienteId != null)
            {
                var _cliente = new ClienteModelo() { Nombre = usuarioCliente.Nombre, Apellidos = usuarioCliente.Apellidos, Direccion = usuarioCliente.Direccion };
                _cliente.Id = usuarioCliente.ClienteId.Value;

                _dbcontext.Clientes.Update(_cliente);
            }
            if (usuarioCliente.UsuarioId != null)
            {
                UsuarioModelo _usuario = new UsuarioModelo()
                {
                    Id = usuarioCliente.UsuarioId.Value,
                    UsuarioNombre = usuarioCliente.UsuarioNombre,
                };

                if (!String.IsNullOrWhiteSpace(usuarioCliente.Password)) {
                    byte[] passwordHash, passwordSalt;

                    _hashMiddleware.CreatePasswordHash(usuarioCliente.Password, out passwordHash, out passwordSalt);


                    _usuario.password = passwordHash;
                    _usuario.PasswordSalt = passwordSalt;
                }
               
                _usuario.status = 1;

            }
            await _dbcontext.SaveChangesAsync();
            return "Usuario modificado exitosamente";
        }
        public async Task<string> Delete(Guid id)
        {
            var _usuarioClientes = _dbcontext.UsuarioClientes.Where(u => u.Id == id).FirstOrDefault();
            if (_usuarioClientes == null)
            {
                return "El usuario no existe";
            }
            var _usuarios = _dbcontext.Usuarios.Where(u => u.Id == _usuarioClientes.UsuarioId).FirstOrDefault();
            if (_usuarios == null)
            {
                return "El usuario no existe";
            }
            var _clientes = _dbcontext.Clientes.Where(u => u.Id == _usuarioClientes.ClienteId).FirstOrDefault();
            if (_clientes == null)
            {
                return "El usuario no existe";
            }
            _dbcontext.UsuarioClientes.Remove(_usuarioClientes);
            _dbcontext.Usuarios.Remove(_usuarios);
            _dbcontext.Clientes.Remove(_clientes);

            await _dbcontext.SaveChangesAsync();

            return "Usuario eliminado exitosamente";
        }
    }
}
