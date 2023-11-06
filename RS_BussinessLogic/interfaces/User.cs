using RS_BussinessLogic.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RS_BussinessLogic.interfaces
{
    public interface IUseroRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetOne(Guid Id);
        Task<Guid> Add(RS_BussinessLogic.models.User Tienda);
        Task<string> Update(Guid Id, RS_BussinessLogic.models.User Tienda);
        Task<string> Delete(Guid Id);
    }
}
