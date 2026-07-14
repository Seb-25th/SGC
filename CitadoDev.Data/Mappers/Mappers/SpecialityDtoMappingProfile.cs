using AutoMapper;
using CitadoDev.Data.DTOs;
using CitadoDev.Data.Entities;

namespace CitadoDev.Data.Mappers.Mappers
{
    public class SpecialityDtoMappingProfile : Profile
    {
        public SpecialityDtoMappingProfile()
        {
            CreateMap<Specialty, SpecialtyDto>();
        }
    }
}
