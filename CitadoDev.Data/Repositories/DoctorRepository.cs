using CitadoDev.Data.Context;
using CitadoDev.Data.Entities;
using CitadoDev.Data.Interfaces.Repositories;

namespace CitadoDev.Data.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(DbContextCitadoDev context) : base(context)
        {
        }
    }
}
