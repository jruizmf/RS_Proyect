using MLGDataAccessLayer.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MLGBussinessLogic.interfaces
{
    public interface ITiendaArticuloRepository
    {
        Task<List<TiendaArticuloModelo>> GetAll();
        Task<TiendaArticuloModelo> GetOne(Guid Id);
        Task<List<MLGDataAccessLayer.models.TiendaArticuloModelo>> GetByUser(Guid id);
        Task<Guid> Add(TiendaArticuloModelo Tienda);
        Task<string> Update(Guid Id, TiendaArticuloModelo Tienda);
        Task<string> Delete(Guid Id);
    }
}
