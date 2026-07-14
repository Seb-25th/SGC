
using AutoMapper;
using CitadoDev.Data.DTOs;
using CitadoDev.Data.Entities;
using CitadoDev.Data.Interfaces.Repositories;
using CitadoDev.Data.Interfaces.Services;

namespace CitadoDev.Data.Services
{
    public class SpecialtyServices : BaseServices<Specialty, SpecialtyDto>, ISpecialtyServices
    {
        public SpecialtyServices(IMapper mapper, ISpecialtyRepository repository) : base(mapper,repository)
        {
        }
    }
}
