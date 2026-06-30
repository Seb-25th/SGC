using AutoMapper;
using Moq;
using Xunit;
using CitadoDev.Data.Services;
using CitadoDev.Data.Interfaces.Repositories;
using CitadoDev.Data.Entities;
using CitadoDev.Data.DTOs;

namespace CitadoDev.Test;

public class MedicalRecordServicesTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IMedicalRecordRepository> _repoMock;
    private readonly MedicalRecordServices _service;

    public MedicalRecordServicesTests()
    {
        _mapperMock = new Mock<IMapper>();
        _repoMock   = new Mock<IMedicalRecordRepository>();
        _service    = new MedicalRecordServices(_mapperMock.Object, _repoMock.Object);
    }

    private static MedicalRecord FakeRecord(int id = 1) => new()
    {
        Id            = id,
        AppointmentId = 1,
        Diagnosis     = "Diagnosis test",
        Treatment     = "Treatment test"
    };

    private static MedicalRecordDto FakeDto(int id = 1) => new()
    {
        Id            = id,
        AppointmentId = 1,
        Diagnosis     = "Diagnosis test",
        Treatment     = "Treatment test"
    };

    [Fact]
    public async Task GetAllListDto_ShouldReturnList()
    {
        var entities = new List<MedicalRecord> { FakeRecord() };
        var dtos     = new List<MedicalRecordDto> { FakeDto() };

        _repoMock.Setup(r => r.GetAllListAsync()).ReturnsAsync(entities);
        _mapperMock.Setup(m => m.Map<List<MedicalRecordDto>>(entities)).Returns(dtos);

        var result = await _service.GetAllListDto();

        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetDtoById_WhenExists_ShouldReturnDto()
    {
        var entity = FakeRecord();
        var dto    = FakeDto();

        _repoMock.Setup(r => r.GetEntityByIdAsync(1)).ReturnsAsync(entity);
        _mapperMock.Setup(m => m.Map<MedicalRecordDto>(entity)).Returns(dto);

        var result = await _service.GetDtoById(1);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetDtoById_WhenNotFound_ShouldReturnNull()
    {
        _repoMock.Setup(r => r.GetEntityByIdAsync(99)).ReturnsAsync((MedicalRecord?)null);

        var result = await _service.GetDtoById(99);

        Assert.Null(result);
    }

    [Fact]
    public async Task SaveDtoAsync_ShouldReturnSavedDto()
    {
        var inputDto  = FakeDto(0);
        var entity    = FakeRecord();
        var outputDto = FakeDto();

        _mapperMock.Setup(m => m.Map<MedicalRecord>(inputDto)).Returns(entity);
        _repoMock.Setup(r => r.SaveEntityAsync(entity)).ReturnsAsync(entity);
        _mapperMock.Setup(m => m.Map<MedicalRecordDto>(entity)).Returns(outputDto);

        var result = await _service.SaveDtoAsync(inputDto);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteHardDtoAsync_ShouldReturnTrue()
    {
        _repoMock.Setup(r => r.RemoveAsync(1)).Returns(Task.CompletedTask);

        var result = await _service.DeleteHardDtoAsync(1);

        Assert.True(result);
    }
}