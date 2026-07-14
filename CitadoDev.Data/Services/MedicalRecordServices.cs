
using AutoMapper;
using CitadoDev.Data.DTOs;
using CitadoDev.Data.Entities;
using CitadoDev.Data.Interfaces.Repositories;
using CitadoDev.Data.Interfaces.Services;

namespace CitadoDev.Data.Services
{
    public class MedicalRecordServices : BaseServices<MedicalRecord, MedicalRecordDto>, IMedicalRecordServices 
    {
        public MedicalRecordServices(IMapper mapper, IMedicalRecordRepository repo) : base(mapper, repo)
        {

        }
    }
}
