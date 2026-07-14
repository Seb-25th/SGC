using AutoMapper;
using CitadoDev.Data.DTOs;
using CitadoDev.Data.Entities;

namespace CitadoDev.Data.Mappers.Mappers
{
    public class OfficeDtoMappingProfile : Profile
    {
        public OfficeDtoMappingProfile()
        {
            CreateMap<Office, OfficeDto>();
        }
    }
}
