using AutoMapper;
using CitadoDev.Data.DTOs;
using CitadoDev.Data.Entities;

namespace CitadoDev.Data.Mappers.Mappers
{
    public class PatientDtoMappingProfile : Profile
    {
        public PatientDtoMappingProfile()
        {
            CreateMap<Patient, PatientDto>();
        }
    }
}
