using AutoMapper;
using CitadoDev.Data.DTOs;
using CitadoDev.Data.Entities;

namespace CitadoDev.Data.Mappers.Mappers
{
    public class MedicalRecordDtoMappingProfile : Profile
    {
        public MedicalRecordDtoMappingProfile()
        {
            CreateMap<MedicalRecord, MedicalRecordDto>();
        }
    }
}
