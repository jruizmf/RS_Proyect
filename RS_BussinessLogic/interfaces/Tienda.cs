using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MLGBussinesLogic.interfaces
{
    public interface ITiendaRepository
    {
        Task<List<MLGDataAccessLayer.models.TiendaModelo>> GetAll();
        Task<MLGDataAccessLayer.models.TiendaModelo> GetOne(Guid Id);
        Task<Guid> Add(MLGDataAccessLayer.models.TiendaModelo Tienda);
        Task<string> Update(Guid Id, MLGDataAccessLayer.models.TiendaModelo Tienda);
        Task<string> Delete(Guid Id);
    }
}
