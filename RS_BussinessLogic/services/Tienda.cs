using Microsoft.EntityFrameworkCore;
using MLGBussinesLogic.interfaces;
using MLGDataAccessLayer;
using MLGDataAccessLayer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLGBussinesLogic.services
{
    public class TiendaRepository : ITiendaRepository
    {
        private AppDBContext _dbcontext;
        public TiendaRepository(AppDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<List<MLGDataAccessLayer.models.TiendaModelo>> GetAll()
        {
            var clientes = await _dbcontext.Tiendas.ToListAsync<MLGDataAccessLayer.models.TiendaModelo>();
            return clientes;
        }
        public async Task<MLGDataAccessLayer.models.TiendaModelo>GetOne(Guid id)
        {
            var tienda = await _dbcontext.Tiendas.Where(empid => empid.Id == id).FirstOrDefaultAsync();
            return tienda;
        }
        public async Task<Guid> Add(MLGDataAccessLayer.models.TiendaModelo tienda)
        {
            _dbcontext.Tiendas.Add(tienda);
            await _dbcontext.SaveChangesAsync();

            return tienda.Id;
        }
        public async Task<string> Update(Guid id, MLGDataAccessLayer.models.TiendaModelo cliente)
        {
            var _tienda = await _dbcontext.Tiendas.Where(empid => empid.Id == id).FirstOrDefaultAsync();
            if (_tienda == null)
            {
                return "La tienda no existe";
            }

            _tienda.Sucursal = cliente.Sucursal;
            _tienda.Direccion = cliente.Direccion;


            await _dbcontext.SaveChangesAsync();
            return "Tienda modificada exitosamente";
        }
        public async Task<string> Delete(Guid id)
        {
            var _tienda = _dbcontext.Tiendas.Where(empid => empid.Id == id).FirstOrDefault();
            if (_tienda == null)
            {
                return "El tienda no existe";
            }
            _dbcontext.Tiendas.Remove(_tienda);

            await _dbcontext.SaveChangesAsync();

            return "Tienda eliminada exitosamente";
        }
    }
}
