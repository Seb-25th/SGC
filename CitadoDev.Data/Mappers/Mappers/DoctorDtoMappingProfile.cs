using AutoMapper;
using CitadoDev.Data.DTOs;
using CitadoDev.Data.Entities;

namespace CitadoDev.Data.Mappers.Mappers
{
    public class DoctorDtoMappingProfile : Profile
    {
        public DoctorDtoMappingProfile()
        {
            CreateMap<Doctor, DoctorDto>();
        }
    }
}
