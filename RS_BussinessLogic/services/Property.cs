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

    public class PropertyRepository : IPropertyRepository
    {
        public readonly AppDBContext _dbcontext;
        private HashMiddleware _hashMiddleware;
        public PropertyRepository(AppDBContext dbcontext, HashMiddleware hashMiddleware)
        {
            _dbcontext = dbcontext;
            _hashMiddleware = hashMiddleware;
        }

        public async Task<List<Property>> GetAll()
        {
            var clienteArticulos = await _dbcontext.Properties.Include(d => d.PropertyImages).ToListAsync();
            return clienteArticulos;
        }

    
        public async Task<Property> GetOne(Guid Id)
        {
            var clienteArticulos = await _dbcontext.Properties.Include(d => d.PropertyImages).Include(d => d.User).Where(u => u.Id == Id).FirstOrDefaultAsync();
            if (clienteArticulos == null) {
                clienteArticulos = await _dbcontext.Properties.Include(d => d.PropertyImages).Include(d => d.User).Where(u => u.User.Id == Id).FirstOrDefaultAsync();
            }

            return clienteArticulos;
        }

        public async Task<string> Add(Property property)
        {
            try
            {
                _dbcontext.Properties.Add(property);

                await _dbcontext.SaveChangesAsync();

                return "La propiedad agregado exitosamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> Update(Guid id, Property property)
        {
            var _property = await _dbcontext.Properties.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (_property != null)
            {
                return "La propiedad ya existe";
            }

            _property.Title = property.Title;
            _property.Description = property.Description;
            _property.Price = property.Price;
            _property.Tags = property.Tags;
            _property.AddressNumber = property.AddressNumber;
            _property.AddressStreet = property.AddressStreet;
            _property.AddressNeighborhood = property.AddressNeighborhood;
            _property.ZIP = property.ZIP;
            _property.CityId = property.CityId;
            _property.StateId = property.StateId;
            _property.CountryId = property.CountryId;
            _property.Latitude = property.Latitude;
            _property.MunicipalityId = property.MunicipalityId;
            _property.Longitude = property.Longitude;


            await _dbcontext.SaveChangesAsync();
            return "La propiedad modificado exitosamente";
        }
        public async Task<string> Delete(Guid id)
        {
            var _property = _dbcontext.Properties.Where(u => u.Id == id).FirstOrDefault();
            if (_property == null)
            {
                return "El Articulo no existe";
            }
           
            _dbcontext.Properties.Remove(_property);
            await _dbcontext.SaveChangesAsync();

            return "Propiedad eliminada exitosamente";
        }
    }
}
