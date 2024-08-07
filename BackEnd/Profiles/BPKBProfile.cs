using AutoMapper;
using BackEnd.DTOs.BPKB;
using BackEnd.Models;

namespace BackEnd.Profiles
{
    public class BPKBProfile : Profile
    {
        public BPKBProfile()
        {
            CreateMap<GetBPKBDTO, TrBpkb>();
            CreateMap<TrBpkb, GetBPKBDTO>();

            CreateMap<InsertUpdateBPKBDTO, TrBpkb>();
            CreateMap<TrBpkb, InsertUpdateBPKBDTO>();
        }
    }
}
