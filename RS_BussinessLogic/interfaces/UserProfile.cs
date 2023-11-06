using RS_BussinessLogic.models.dto;
using RS_BussinessLogic.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RS_BussinessLogic.interfaces
{
    public interface IUserProfileRepository
    {
        Task<List<UserProfile>> GetAll();
        Task<UserProfile> GetOne(Guid Id);
        Task<Guid> Add(UserProfile Profile);
        Task<string> Update(Guid Id, UserProfile Profile);
        Task<string> Delete(Guid Id);
    }
}
