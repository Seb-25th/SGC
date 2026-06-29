
using AutoMapper;
using CitadoDev.Data.DTOs;
using CitadoDev.Data.Entities;
using CitadoDev.Data.Interfaces.Repositories;
using CitadoDev.Data.Interfaces.Services;

namespace CitadoDev.Data.Services
{
    public class AppointmentServices : BaseServices<Appointment, AppointmentDto>, IAppointmentServices
    {
        public AppointmentServices(IMapper mapper, IAppointmentRepository repo) : base(mapper, repo)
        {
        }

    }
}
