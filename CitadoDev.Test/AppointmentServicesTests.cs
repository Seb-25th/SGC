using AutoMapper;
using CitadoDev.Data.DTOs;
using CitadoDev.Data.Entities;
using CitadoDev.Data.Entities.Enums;
using CitadoDev.Data.Interfaces.Repositories;
using CitadoDev.Data.Mappers.Mappers;
using CitadoDev.Data.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CitadoDev.Test
{
    public class AppointmentServicesTests
    {
        private readonly Mock<IAppointmentRepository> _repoMock;
        private readonly AppointmentServices _sut;

        public AppointmentServicesTests()
        {
            _repoMock = new Mock<IAppointmentRepository>();

            // Real mapping profile, so AppointmentDtoMappingProfile is actually exercised,
            // not just mocked away.
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AppointmentDtoMappingProfile>();
            }, NullLoggerFactory.Instance);
            var mapper = config.CreateMapper();

            _sut = new AppointmentServices(mapper, _repoMock.Object);
        }

        // ------------------------------------------------------------------
        // Fake data helpers
        // ------------------------------------------------------------------

        private static Appointment FakeEntity(
            int id = 1,
            int patientId = 10,
            int doctorId = 20,
            int officeId = 30,
            AppointmentStatus status = AppointmentStatus.Scheduled,
            string? reason = "Routine check-up",
            DateTime? scheduledAt = null,
            DateTime? createdAt = null)
        {
            return new Appointment
            {
                Id = id,
                PatientId = patientId,
                DoctorId = doctorId,
                OfficeId = officeId,
                ScheduledAt = scheduledAt ?? new DateTime(2026, 7, 1, 9, 0, 0),
                DurationMinutes = 30,
                Status = status,
                Reason = reason,
                CreatedAt = createdAt ?? new DateTime(2026, 6, 20, 12, 0, 0)
            };
        }

        private static AppointmentDto FakeDto(
            int id = 1,
            int patientId = 10,
            int doctorId = 20,
            int officeId = 30,
            AppointmentStatus status = AppointmentStatus.Scheduled,
            string? reason = "Routine check-up",
            DateTime? scheduledAt = null,
            DateTime? createdAt = null)
        {
            return new AppointmentDto
            {
                Id = id,
                PatientId = patientId,
                DoctorId = doctorId,
                OfficeId = officeId,
                ScheduledAt = scheduledAt ?? new DateTime(2026, 7, 1, 9, 0, 0),
                DurationMinutes = 30,
                Status = status,
                Reason = reason,
                CreatedAt = createdAt ?? new DateTime(2026, 6, 20, 12, 0, 0)
            };
        }

        private static List<Appointment> FakeEntityList()
        {
            return new List<Appointment>
            {
                FakeEntity(id: 1, patientId: 10, doctorId: 20, officeId: 30, status: AppointmentStatus.Scheduled, reason: "Routine check-up"),
                FakeEntity(id: 2, patientId: 11, doctorId: 21, officeId: 30, status: AppointmentStatus.Completed, reason: "Follow-up"),
                FakeEntity(id: 3, patientId: 12, doctorId: 20, officeId: 31, status: AppointmentStatus.Cancelled, reason: null)
            };
        }

        // ------------------------------------------------------------------
        // AutoMapper profile sanity check
        // ------------------------------------------------------------------

        [Fact]
        public void MappingConfiguration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentDtoMappingProfile>(), NullLoggerFactory.Instance);
            config.AssertConfigurationIsValid();
        }

        // ------------------------------------------------------------------
        // GetDtoById
        // ------------------------------------------------------------------

        [Fact]
        public async Task GetDtoById_ExistingId_ReturnsMappedDto()
        {
            var entity = FakeEntity(id: 1);
            _repoMock.Setup(r => r.GetEntityByIdAsync(1)).ReturnsAsync(entity);

            var result = await _sut.GetDtoById(1);

            Assert.NotNull(result);
            Assert.Equal(entity.Id, result!.Id);
            Assert.Equal(entity.PatientId, result.PatientId);
            Assert.Equal(entity.DoctorId, result.DoctorId);
            Assert.Equal(entity.OfficeId, result.OfficeId);
            Assert.Equal(entity.Status, result.Status);
            Assert.Equal(entity.Reason, result.Reason);
            Assert.Equal(entity.ScheduledAt, result.ScheduledAt);
            Assert.Equal(entity.DurationMinutes, result.DurationMinutes);
            Assert.Equal(entity.CreatedAt, result.CreatedAt);

            _repoMock.Verify(r => r.GetEntityByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetDtoById_NonExistingId_ReturnsNull()
        {
            _repoMock.Setup(r => r.GetEntityByIdAsync(It.IsAny<int>())).ReturnsAsync((Appointment?)null);

            var result = await _sut.GetDtoById(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetDtoById_RepositoryThrows_ReturnsNull()
        {
            // BaseServices.GetDtoById swallows exceptions and returns null.
            _repoMock.Setup(r => r.GetEntityByIdAsync(It.IsAny<int>()))
                      .ThrowsAsync(new InvalidOperationException("DB unavailable"));

            var result = await _sut.GetDtoById(1);

            Assert.Null(result);
        }

        // ------------------------------------------------------------------
        // GetAllListDto
        // ------------------------------------------------------------------

        [Fact]
        public async Task GetAllListDto_ReturnsAllMappedDtos()
        {
            var entities = FakeEntityList();
            _repoMock.Setup(r => r.GetAllListAsync()).ReturnsAsync(entities);

            var result = await _sut.GetAllListDto();

            Assert.Equal(entities.Count, result.Count);
            Assert.All(result, dto => Assert.IsType<AppointmentDto>(dto));
            Assert.Equal(entities.Select(e => e.Id), result.Select(d => d.Id));
        }

        [Fact]
        public async Task GetAllListDto_NoAppointments_ReturnsEmptyList()
        {
            _repoMock.Setup(r => r.GetAllListAsync()).ReturnsAsync(new List<Appointment>());

            var result = await _sut.GetAllListDto();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllListDto_RepositoryThrows_ReturnsEmptyList()
        {
            // BaseServices.GetAllListDto swallows exceptions and returns an empty list.
            _repoMock.Setup(r => r.GetAllListAsync())
                      .ThrowsAsync(new InvalidOperationException("DB unavailable"));

            var result = await _sut.GetAllListDto();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // ------------------------------------------------------------------
        // SaveDtoAsync
        // ------------------------------------------------------------------

        [Fact]
        public async Task SaveDtoAsync_ValidDto_MapsAndPersistsEntity()
        {
            var fakeDto = FakeDto(id: 0, patientId: 15, doctorId: 25, officeId: 35, reason: "New patient consult");
            Appointment? capturedEntity = null;

            _repoMock
                .Setup(r => r.SaveEntityAsync(It.IsAny<Appointment>()))
                .Callback<Appointment>(e =>
                {
                    capturedEntity = e;
                    e.Id = 100; // simulate DB-generated id after SaveChangesAsync
                })
                .ReturnsAsync(() => capturedEntity!);

            var result = await _sut.SaveDtoAsync(fakeDto);

            Assert.NotNull(capturedEntity);
            Assert.Equal(fakeDto.PatientId, capturedEntity!.PatientId);
            Assert.Equal(fakeDto.DoctorId, capturedEntity.DoctorId);
            Assert.Equal(fakeDto.OfficeId, capturedEntity.OfficeId);
            Assert.Equal(fakeDto.Reason, capturedEntity.Reason);

            Assert.NotNull(result);
            Assert.Equal(100, result!.Id);
            _repoMock.Verify(r => r.SaveEntityAsync(It.IsAny<Appointment>()), Times.Once);
        }

        [Fact]
        public async Task SaveDtoAsync_RepositoryThrows_ReturnsNull()
        {
            var fakeDto = FakeDto(id: 0);
            _repoMock.Setup(r => r.SaveEntityAsync(It.IsAny<Appointment>()))
                      .ThrowsAsync(new InvalidOperationException("DB unavailable"));

            var result = await _sut.SaveDtoAsync(fakeDto);

            Assert.Null(result);
        }

        // ------------------------------------------------------------------
        // UpdateDtoAsync
        // ------------------------------------------------------------------

        [Fact]
        public async Task UpdateDtoAsync_ExistingId_UpdatesAndReturnsMappedDto()
        {
            var fakeDto = FakeDto(id: 1, status: AppointmentStatus.Completed, reason: "Updated reason");
            var updatedEntity = FakeEntity(id: 1, status: AppointmentStatus.Completed, reason: "Updated reason");

            _repoMock
                .Setup(r => r.UpdateEntityAsync(1, It.IsAny<Appointment>()))
                .ReturnsAsync(updatedEntity);

            var result = await _sut.UpdateDtoAsync(fakeDto, 1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
            Assert.Equal(AppointmentStatus.Completed, result.Status);
            Assert.Equal("Updated reason", result.Reason);

            _repoMock.Verify(r => r.UpdateEntityAsync(1, It.Is<Appointment>(e =>
                e.Status == AppointmentStatus.Completed &&
                e.Reason == "Updated reason")), Times.Once);
        }

        [Fact]
        public async Task UpdateDtoAsync_NonExistingId_ReturnsNull()
        {
            var fakeDto = FakeDto(id: 999);

            _repoMock
                .Setup(r => r.UpdateEntityAsync(999, It.IsAny<Appointment>()))
                .ReturnsAsync((Appointment?)null);

            var result = await _sut.UpdateDtoAsync(fakeDto, 999);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateDtoAsync_RepositoryThrows_ReturnsNull()
        {
            var fakeDto = FakeDto(id: 1);
            _repoMock.Setup(r => r.UpdateEntityAsync(1, It.IsAny<Appointment>()))
                      .ThrowsAsync(new InvalidOperationException("DB unavailable"));

            var result = await _sut.UpdateDtoAsync(fakeDto, 1);

            Assert.Null(result);
        }

        // ------------------------------------------------------------------
        // DeleteHardDtoAsync
        // ------------------------------------------------------------------

        [Fact]
        public async Task DeleteHardDtoAsync_ExistingId_ReturnsTrue()
        {
            _repoMock.Setup(r => r.RemoveAsync(1)).Returns(Task.CompletedTask);

            var result = await _sut.DeleteHardDtoAsync(1);

            Assert.True(result);
            _repoMock.Verify(r => r.RemoveAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteHardDtoAsync_RepositoryThrows_ReturnsFalse()
        {
            // BaseRepository.RemoveAsync silently no-ops on a missing id (no exception),
            // but BaseServices.DeleteHardDtoAsync also guards any unexpected exception
            // and returns false instead of propagating it.
            _repoMock.Setup(r => r.RemoveAsync(It.IsAny<int>()))
                      .ThrowsAsync(new InvalidOperationException("DB unavailable"));

            var result = await _sut.DeleteHardDtoAsync(999);

            Assert.False(result);
        }

        // ------------------------------------------------------------------
        // GetWithInclude
        // ------------------------------------------------------------------

        [Fact]
        public async Task GetWithInclude_RepositoryThrows_ReturnsEmptyList()
        {
            // GetWithInclude uses ProjectTo against an IQueryable from a real DbSet,
            // which can't easily be faked with Moq alone (EF Core query provider).
            // We verify the documented failure path: any exception yields an empty list.
            _repoMock.Setup(r => r.GetAllQueryWithInclude(It.IsAny<List<string>>()))
                      .Throws(new InvalidOperationException("Include failed"));

            var result = await _sut.GetWithInclude(new List<string> { "Patient", "Doctor" });

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // ------------------------------------------------------------------
        // GetAllQuery
        // ------------------------------------------------------------------

        [Fact]
        public async Task GetAllQuery_MapsQueryableEntitiesToDtos()
        {
            var entities = FakeEntityList().AsQueryable();
            _repoMock.Setup(r => r.GetAllQuery()).Returns(entities);

            var result = await _sut.GetAllQuery();

            Assert.Equal(3, result.Count);
            Assert.Equal(entities.Select(e => e.Id), result.Select(d => d.Id));
        }

        [Fact]
        public async Task GetAllQuery_RepositoryThrows_ReturnsEmptyList()
        {
            _repoMock.Setup(r => r.GetAllQuery())
                      .Throws(new InvalidOperationException("DB unavailable"));

            var result = await _sut.GetAllQuery();

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}