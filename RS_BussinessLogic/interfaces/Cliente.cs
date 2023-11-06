using MLGDataAccessLayer.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MLGBussinesLogic.interfaces
{
    public interface IClienteRepository
    {
        Task<List<ClienteModelo>> GetAll();
        Task<ClienteModelo> GetOne(Guid Id);
        Task<Guid>Add(ClienteModelo Cliente);
        Task<string> Update(Guid Id, ClienteModelo cliente);
        Task<string> Delete(Guid Id);
    }
}
