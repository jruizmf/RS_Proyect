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

    public class ClienteArticuloRepository : IClienteArticuloRepository
    {
        public readonly AppDBContext _dbcontext;
        private HashMiddleware _hashMiddleware;
        public ClienteArticuloRepository(AppDBContext dbcontext, HashMiddleware hashMiddleware)
        {
            _dbcontext = dbcontext;
            _hashMiddleware = hashMiddleware;
        }

        public async Task<List<MLGDataAccessLayer.models.ClienteArticuloModelo>> GetAll()
        {
            var clienteArticulos = await _dbcontext.ClienteArticulos.Include(d => d.Cliente).Include(d => d.Articulo).ToListAsync();
            return clienteArticulos;
        }

        public async Task<List<MLGDataAccessLayer.models.ClienteArticuloModelo>> GetByUser(Guid id)
        {
            var clienteArticulos = await _dbcontext.ClienteArticulos.Include(d => d.Cliente).Include(d => d.Articulo)
                .Where(ca => ca.ClienteId == id).ToListAsync();

            if(clienteArticulos == null)
            {
                clienteArticulos = await _dbcontext.ClienteArticulos.Include(d => d.Cliente).Include(d => d.Articulo)
                .Where(ca => ca.ArticuloId == id).ToListAsync();
            }

            return clienteArticulos;
        }
        public async Task<MLGDataAccessLayer.models.ClienteArticuloModelo> GetOne(Guid Id)
        {
            var clienteArticulos = await _dbcontext.ClienteArticulos.Include(d => d.Articulo).Include(d => d.Cliente).Where(u => u.Id == Id).FirstOrDefaultAsync();
            if (clienteArticulos == null) {
                clienteArticulos = await _dbcontext.ClienteArticulos.Include(d => d.Articulo).Include(d => d.Cliente).Where(u => u.ClienteId == Id).FirstOrDefaultAsync();
            }

            return clienteArticulos;
        }

        public async Task<string> Add(MLGBussinesLogic.models.dto.ClienteArticuloDto clienteArticulos)
        {
            var _articulo = await _dbcontext.Articulos.Where(a => a.Id == clienteArticulos.ArticuloId).FirstOrDefaultAsync();
            
            if (_articulo == null)
            {
                return "No hay productos en stock";
            }

            if (_articulo.Stock  <= 0)
            {
                return "No hay productos en stock";
            }

            _articulo.Stock = _articulo.Stock - clienteArticulos.Cantidad;


           

            _dbcontext.ClienteArticulos.Add(new ClienteArticuloModelo()
            {
                ClienteId = clienteArticulos.ClienteId,
                ArticuloId = clienteArticulos.ArticuloId,
                fecha = DateTime.Now
            });

            await _dbcontext.SaveChangesAsync();

            return "Articulo agregado exitosamente";
        }
        public async Task<string> Update(Guid id, MLGDataAccessLayer.models.ClienteArticuloModelo clienteArticulo)
        {
            var _clienteArticulo = await _dbcontext.ClienteArticulos.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (_clienteArticulo != null)
            {
                return "El articulo ya existe";
            }

            _clienteArticulo.ClienteId = clienteArticulo.ClienteId;
            _clienteArticulo.ArticuloId = clienteArticulo.ArticuloId;


            await _dbcontext.SaveChangesAsync();
            return "Articulo modificado exitosamente";
        }
        public async Task<string> Delete(Guid id)
        {
            var _clienteArticulo = _dbcontext.ClienteArticulos.Where(u => u.Id == id).FirstOrDefault();
            if (_clienteArticulo == null)
            {
                return "El Articulo no existe";
            }
           
            _dbcontext.ClienteArticulos.Remove(_clienteArticulo);
            await _dbcontext.SaveChangesAsync();

            return "Articulo eliminado exitosamente";
        }
    }
}
