using AutoMapper;
using CitadoDev.Data.DTOs;
using CitadoDev.Data.Entities;
namespace CitadoDev.Data.Mappers.Mappers
{
    public class AppointmentDtoMappingProfile : Profile
    {
        public AppointmentDtoMappingProfile()
        {
            CreateMap<Appointment, AppointmentDto>();
        }       
    }
}
