using CitadoDev.Data.Context;
using CitadoDev.Data.Entities;
using CitadoDev.Data.Interfaces.Repositories;

namespace CitadoDev.Data.Repositories
{
    public class SpecialtyRepository : BaseRepository<Specialty>, ISpecialtyRepository
    {
        public SpecialtyRepository(DbContextCitadoDev context) : base(context)
        {
        }
    }
}
