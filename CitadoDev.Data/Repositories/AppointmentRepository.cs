using CitadoDev.Data.Context;
using CitadoDev.Data.Entities;
using CitadoDev.Data.Interfaces.Repositories;

namespace CitadoDev.Data.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DbContextCitadoDev context) : base(context)
        {
        }
    }
}
