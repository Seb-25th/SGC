using CitadoDev.Data.Context;
using CitadoDev.Data.Entities;

namespace CitadoDev.Data.Repositories
{
    public class MedicalRecordRepository : BaseRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(DbContextCitadoDev context) : base(context)
        {
        }
    }
}
