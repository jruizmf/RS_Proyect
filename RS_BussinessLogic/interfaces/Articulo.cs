using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MLGBussinesLogic.interfaces
{
    public interface IArticuloRepository
    {
        Task<List<MLGDataAccessLayer.models.ArticuloModelo>> GetAll();
        Task<MLGDataAccessLayer.models.ArticuloModelo> GetOne(Guid Id);
        Task<MLGDataAccessLayer.models.ArticuloModelo> Add(MLGDataAccessLayer.models.ArticuloModelo articulo);
        Task<string> Update(Guid Id, MLGDataAccessLayer.models.ArticuloModelo articulo);
        Task<string> Delete(Guid Id);
    }
}
