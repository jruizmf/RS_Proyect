using RS_BussinessLogic.models;
using RS_BussinessLogic.models.dto;
using RS_DataAccess.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RS_BussinessLogic.interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetOne(Guid Id);
        Task<string> Add(UserDto user);
        Task<string> Update(Guid Id, UserDto user);
        Task<string> Delete(Guid Id);
    }
}
