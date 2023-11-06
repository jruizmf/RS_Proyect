using MLGBussinesLogic.models.dto;
using MLGDataAccessLayer.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MLGBussinessLogic.interfaces
{
    public interface IClienteArticuloRepository
    {
        Task<List<ClienteArticuloModelo>> GetAll();
        Task<ClienteArticuloModelo> GetOne(Guid Id);

        Task<List<ClienteArticuloModelo>> GetByUser(Guid usuario);
        Task<string> Add(ClienteArticuloDto ClienteArticulo);
        Task<string> Update(Guid Id, ClienteArticuloModelo ClienteArticulo);
        Task<string> Delete(Guid Id);
    }
}
