using AutoMapper;
using CitadoDev.Data.DTOs;
using CitadoDev.Data.Entities;
using CitadoDev.Data.Interfaces.Repositories;
using CitadoDev.Data.Interfaces.Services;
using System.Security.Cryptography.X509Certificates;

namespace CitadoDev.Data.Services
{
    public class OfficeServices : BaseServices<Office, OfficeDto>, IOfficeServices
    {
        public OfficeServices(IMapper mapper, IOfficeRepository repo) : base(mapper, repo)
        {
        }
    }
}
