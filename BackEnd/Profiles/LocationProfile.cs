using AutoMapper;
using BackEnd.DTOs.Location;
using BackEnd.Models;

namespace BackEnd.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile() 
        {
            CreateMap<GetLocation, MsStorageLocation>();
            CreateMap<MsStorageLocation, GetLocation>();
        }
    }
}
