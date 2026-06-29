using CitadoDev.Data.Context;
using CitadoDev.Data.Entities;

namespace CitadoDev.Data.Repositories
{
    public class OfficeRepository : BaseRepository<Office>, IOfficeRepository
    {
        public OfficeRepository(DbContextCitadoDev context) : base(context)
        {
        }
    }
}
