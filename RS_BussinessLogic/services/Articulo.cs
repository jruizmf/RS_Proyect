using Microsoft.EntityFrameworkCore;
using MLGBussinesLogic.interfaces;
using MLGBussinesLogic.models;
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

    public class ArticuloRepository : IArticuloRepository
    {
        private AppDBContext _dbcontext;
        private HashMiddleware _hashMiddleware;
        public ArticuloRepository(AppDBContext dbcontext, HashMiddleware hashMiddleware)
        {
            _dbcontext = dbcontext;
            _hashMiddleware = hashMiddleware;
        }

        public async Task<List<MLGDataAccessLayer.models.ArticuloModelo>> GetAll()
        {
            var articulos = await _dbcontext.Articulos.ToListAsync<MLGDataAccessLayer.models.ArticuloModelo>();
            return articulos;
        }
        public async Task<MLGDataAccessLayer.models.ArticuloModelo> GetOne(Guid Id)
        {
            var articulo = await _dbcontext.Articulos.Where(u => u.Id == Id).FirstOrDefaultAsync();
            return articulo;
        }
        public async Task<ArticuloModelo> Add(MLGDataAccessLayer.models.ArticuloModelo articulo)
        {
            var _articulo = await _dbcontext.Articulos.Where(a => a.Codigo == articulo.Codigo).FirstOrDefaultAsync();
            if (_articulo != null)
            {
                return null;
            }
            _dbcontext.Articulos.Add(articulo);
            await _dbcontext.SaveChangesAsync();

            return articulo;
        }
        public async Task<string> Update(Guid id, MLGDataAccessLayer.models.ArticuloModelo articulo)
        {
            var _articulo = await _dbcontext.Articulos.Where(a => a.Codigo == articulo.Codigo).FirstOrDefaultAsync();
            if (_articulo != null)
            {
                return "El articulo ya existe";
            }

            _articulo.Codigo = articulo.Codigo;
            _articulo.Descripcion = articulo.Descripcion;
            _articulo.Precio = articulo.Precio;
            _articulo.Imagen = articulo.Imagen;
            _articulo.Stock = articulo.Stock;


            await _dbcontext.SaveChangesAsync();
            return "Articulo modificado exitosamente";
        }
        public async Task<string> Delete(Guid id)
        {
            var _articulo = _dbcontext.Articulos.Where(a => a.Id == id).FirstOrDefault();


            if (_articulo == null)
            {
                return "El articulo no existe";
            }
            _dbcontext.Articulos.Remove(_articulo);

            await _dbcontext.SaveChangesAsync();

            return "Articulo eliminado exitosamente";
        }
    }
}
