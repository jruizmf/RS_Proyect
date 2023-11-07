using Microsoft.EntityFrameworkCore;
using RS_BussinessLogic.models.dto;
using RS_BussinessLogic.helpers;
using RS_BussinessLogic.interfaces;
using RS_BussinessLogic.middleware;
using RS_BussinessLogic;
using RS_BussinessLogic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS_DataAccess;
using RS_DataAccess.models;

namespace RS_BussinessLogic.services
{

    public class UserProfileRepository : IUserProfileRepository
    {
        public readonly AppDBContext _dbcontext;
        private HashMiddleware _hashMiddleware;
        public UserProfileRepository(AppDBContext dbcontext, HashMiddleware hashMiddleware)
        {
            _dbcontext = dbcontext;
            _hashMiddleware = hashMiddleware;
        }

        public async Task<List<UserProfile>> GetAll()
        {
            var usuarioCliente = await _dbcontext.UserProfiles.Include(d => d.User).ToListAsync<UserProfile>();
            return usuarioCliente;
        }
        public async Task<UserProfile>GetOne(Guid Id)
        {
            var _userProfile = await _dbcontext.UserProfiles.Include(d => d.User).Where(u => u.Id == Id).FirstOrDefaultAsync();
            if (_userProfile == null) {
                _userProfile = await _dbcontext.UserProfiles.Include(d => d.User).Where(u => u.User.Id == Id).FirstOrDefaultAsync();
            }

            return _userProfile;
        }

        public async Task<Guid> Add(UserProfile userProfile)
        {
           
            _dbcontext.UserProfiles.Add(userProfile);
            await _dbcontext.SaveChangesAsync();

            return userProfile.Id;
        }
        public async Task<string> Update(Guid id, UserProfile userProfile)
        {
            var _userProfile = await _dbcontext.UserProfiles.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (_userProfile == null)
                throw new AppException("Usuario no encontrado");

            _userProfile.AddressNumber = userProfile.AddressNumber;
            _userProfile.AddressStreet = userProfile.AddressStreet;
            _userProfile.AddressNeighborhood = userProfile.AddressNeighborhood;
            _userProfile.ZIP = userProfile.ZIP;
            _userProfile.CityId = userProfile.CityId;
            _userProfile.StateId = userProfile.StateId;
            _userProfile.CountryId = userProfile.CountryId;
            _userProfile.Latitude = userProfile.Latitude;
            _userProfile.MunicipalityId = userProfile.MunicipalityId;
            _userProfile.Longitude = userProfile.Longitude;

            await _dbcontext.SaveChangesAsync();
            return "Perfil modificado exitosamente";
        }
        public async Task<string> Delete(Guid id)
        {
            var _userProfiles = _dbcontext.UserProfiles.Where(u => u.Id == id).FirstOrDefault();
            if (_userProfiles == null)
            {
                return "El usuario no existe";
            }
            _dbcontext.UserProfiles.Remove(_userProfiles);

            await _dbcontext.SaveChangesAsync();

            return "Usuario eliminado exitosamente";
        }
    }
}
