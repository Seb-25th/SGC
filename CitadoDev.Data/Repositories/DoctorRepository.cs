using CitadoDev.Data.Context;
using CitadoDev.Data.Entities;

namespace CitadoDev.Data.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(DbContextCitadoDev context) : base(context)
        {
        }
    }
}
