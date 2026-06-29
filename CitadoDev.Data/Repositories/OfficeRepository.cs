using CitadoDev.Data.Context;
using CitadoDev.Data.Entities;
using CitadoDev.Data.Interfaces.Repositories;

namespace CitadoDev.Data.Repositories
{
    public class OfficeRepository : BaseRepository<Office>, IOfficeRepository
    {
        public OfficeRepository(DbContextCitadoDev context) : base(context)
        {
        }
    }
}
