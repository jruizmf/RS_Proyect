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

    public class TiendaArticuloRepository : ITiendaArticuloRepository
    {
        public readonly AppDBContext _dbcontext;
        private HashMiddleware _hashMiddleware;
        public TiendaArticuloRepository(AppDBContext dbcontext, HashMiddleware hashMiddleware)
        {
            _dbcontext = dbcontext;
            _hashMiddleware = hashMiddleware;
        }

        public async Task<List<TiendaArticuloModelo>> GetAll()
        {
            var articuloTienda = await _dbcontext.TiendaArticulos.Include(d => d.Tienda).Include(d => d.Articulo).ToListAsync<TiendaArticuloModelo>();
            return articuloTienda;
        }
        public async Task<List<MLGDataAccessLayer.models.TiendaArticuloModelo>> GetByUser(Guid id)
        {
            var clienteArticulos = await _dbcontext.TiendaArticulos.Include(d => d.Articulo).Include(d => d.Articulo)
                .Where(ca => ca.ArticuloId == id).ToListAsync();

            if (clienteArticulos.Count == 0)
            {
                clienteArticulos = await _dbcontext.TiendaArticulos.Include(d => d.Tienda).Include(d => d.Articulo)
                .Where(ca => ca.TiendaId == id).ToListAsync();
            }

            return clienteArticulos;
        }
        public async Task<TiendaArticuloModelo> GetOne(Guid Id)
        {
            var articuloTienda = await _dbcontext.TiendaArticulos.Include(d => d.Tienda).Include(d => d.Articulo).Where(u => u.Id == Id).FirstOrDefaultAsync();
           
            return articuloTienda;
        }
        public async Task<Guid> Add(TiendaArticuloModelo articuloTienda)
        {
            
            _dbcontext.TiendaArticulos.Add(articuloTienda);
            await _dbcontext.SaveChangesAsync();

            return articuloTienda.Id;
        }
        public async Task<string> Update(Guid id, TiendaArticuloModelo articuloTienda)
        {
            
            _dbcontext.TiendaArticulos.Update(new MLGDataAccessLayer.models.TiendaArticuloModelo()
            {
                Id = articuloTienda.Id,
                TiendaId = articuloTienda.TiendaId,
                ArticuloId = articuloTienda.ArticuloId,
            });

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
