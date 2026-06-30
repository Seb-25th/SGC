using AutoMapper;
using CitadoDev.Data.DTOs;
using CitadoDev.Data.Entities;
using CitadoDev.Data.Interfaces.Repositories;
using CitadoDev.Data.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadoDev.Test
{

    public class PatientServicesTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IBaseRepository<Patient>> _repoMock;
        private readonly BaseServices<Patient, PatientDto> _service;

        public PatientServicesTests()
        {
            _mapperMock = new Mock<IMapper>();
            _repoMock = new Mock<IBaseRepository<Patient>>();
            _service = new BaseServices<Patient, PatientDto>(_mapperMock.Object, _repoMock.Object);
        }

        private static Patient FakePatient(int id = 1) => new()
        {
            Id = id,
            FirstName = "Juan",
            LastName = "Perez",
            Email = "juan.perez@mail.com",
            Phone = "8095551234",
            DateOfBirth = new DateTime(1990, 5, 10),
            IdentityDocument = "00112345678",
            CreatedAt = DateTime.UtcNow
        };

        private static PatientDto FakeDto(int id = 1) => new()
        {
            Id = id,
            FirstName = "Juan",
            LastName = "Perez",
            Email = "juan.perez@mail.com",
            Phone = "8095551234",
            DateOfBirth = new DateTime(1990, 5, 10),
            IdentityDocument = "00112345678",
            CreatedAt = DateTime.UtcNow
        };

        [Fact]
        public async Task GetAllListDto_ShouldReturnList()
        {
            var entities = new List<Patient> { FakePatient() };
            var dtos = new List<PatientDto> { FakeDto() };

            _repoMock.Setup(r => r.GetAllListAsync()).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<List<PatientDto>>(entities)).Returns(dtos);

            var result = await _service.GetAllListDto();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetDtoById_WhenExists_ShouldReturnDto()
        {
            var entity = FakePatient();
            var dto = FakeDto();

            _repoMock.Setup(r => r.GetEntityByIdAsync(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<PatientDto>(entity)).Returns(dto);

            var result = await _service.GetDtoById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetDtoById_WhenNotFound_ShouldReturnNull()
        {
            _repoMock.Setup(r => r.GetEntityByIdAsync(99)).ReturnsAsync((Patient?)null);

            var result = await _service.GetDtoById(99);

            Assert.Null(result);
        }

        [Fact]
        public async Task SaveDtoAsync_ShouldReturnSavedDto()
        {
            var inputDto = FakeDto(0);
            var entity = FakePatient();
            var outputDto = FakeDto();

            _mapperMock.Setup(m => m.Map<Patient>(inputDto)).Returns(entity);
            _repoMock.Setup(r => r.SaveEntityAsync(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<PatientDto>(entity)).Returns(outputDto);

            var result = await _service.SaveDtoAsync(inputDto);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateDtoAsync_WhenExists_ShouldReturnUpdatedDto()
        {
            var inputDto = FakeDto();
            var entity = FakePatient();
            var outputDto = FakeDto();

            _mapperMock.Setup(m => m.Map<Patient>(inputDto)).Returns(entity);
            _repoMock.Setup(r => r.UpdateEntityAsync(1, entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<PatientDto>(entity)).Returns(outputDto);

            var result = await _service.UpdateDtoAsync(inputDto, 1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateDtoAsync_WhenNotFound_ShouldReturnNull()
        {
            var inputDto = FakeDto(99);
            var entity = FakePatient(99);

            _mapperMock.Setup(m => m.Map<Patient>(inputDto)).Returns(entity);
            _repoMock.Setup(r => r.UpdateEntityAsync(99, entity)).ReturnsAsync((Patient?)null);

            var result = await _service.UpdateDtoAsync(inputDto, 99);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteHardDtoAsync_ShouldReturnTrue()
        {
            _repoMock.Setup(r => r.RemoveAsync(1)).Returns(Task.CompletedTask);

            var result = await _service.DeleteHardDtoAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteHardDtoAsync_WhenExceptionThrown_ShouldReturnFalse()
        {
            _repoMock.Setup(r => r.RemoveAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            var result = await _service.DeleteHardDtoAsync(1);

            Assert.False(result);
        }
    }
}