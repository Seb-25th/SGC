using CitadoDev.Data.Context;
using CitadoDev.Data.Entities;

namespace CitadoDev.Data.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(DbContextCitadoDev context) : base(context)
        {
        }
    }
}
