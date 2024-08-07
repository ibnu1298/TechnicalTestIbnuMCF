using BackEnd.DTOs;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services
{
    public class LocationFacade : ILocation
    {
        private readonly DataContext _context;
        public LocationFacade(DataContext context)
        {
            _context = context;
        }
        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MsStorageLocation>> GetAll()
        {
            try
            {
                var results = await _context.MsStorageLocations.OrderBy(x => x.LocationName).ToListAsync();
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public Task<MsStorageLocation> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MsStorageLocation>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> Insert(MsStorageLocation obj)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> Update(MsStorageLocation obj)
        {
            throw new NotImplementedException();
        }
    }
}
