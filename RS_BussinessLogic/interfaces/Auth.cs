using RS_BussinessLogic.models;
using RS_BussinessLogic.models.dto;
using System.Threading.Tasks;

namespace RS_BussinessLogic.interfaces
{
    public interface IAuthRepository
    {
        Task<TokenResultDto> Login(AuthDto auth);
    }
}
