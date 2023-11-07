using RS_DataAccess.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RS_BussinessLogic.interfaces
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAll();
        Task<Property> GetOne(Guid Id);
        Task<string> Add(Property Tienda);
        Task<string> Update(Guid Id, Property Tienda);
        Task<string> Delete(Guid Id);
    }
}
