using HubVision.Application.DTOs;
using HubVision.Domain.AggregatesModel.TrafficManagerAggregate;
using HubVision.Domain.Core.Data;
using System.Security.Cryptography;
using System.Text;

namespace HubVision.Application.Services;

public class TrafficManagerService : ITrafficManagerService
{
    private readonly IRepository<TrafficManager, Guid> _repository;

    public TrafficManagerService(IRepository<TrafficManager, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TrafficManagerDto>> GetAllAsync()
    {
        var managers = await _repository.GetAllAsync(null);
        return managers.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<TrafficManagerDto>> GetByAgencyIdAsync(Guid agencyId)
    {
        var managers = _repository.Get(tm => tm.AgencyId == agencyId);
        return managers.Select(MapToDto).ToList();
    }

    public async Task<TrafficManagerDto?> GetByIdAsync(Guid id)
    {
        var manager = await _repository.GetByIdAsync(id);
        return manager != null ? MapToDto(manager) : null;
    }

    public async Task<TrafficManagerDto> CreateAsync(CreateTrafficManagerDto dto)
    {
        if (!Enum.TryParse<TrafficManagerRole>(dto.Role, out var role))
            throw new ArgumentException($"Invalid role: {dto.Role}");

        var passwordHash = HashPassword(dto.Password);
        var manager = TrafficManager.Create(dto.AgencyId, dto.Name, dto.Email, passwordHash, role);

        if (!string.IsNullOrEmpty(dto.Phone))
            typeof(TrafficManager).GetProperty(nameof(TrafficManager.Phone))?.SetValue(manager, dto.Phone);

        await _repository.AddAsync(manager);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(manager);
    }

    public async Task<TrafficManagerDto?> UpdateAsync(Guid id, UpdateTrafficManagerDto dto)
    {
        var manager = await _repository.GetByIdAsync(id);
        if (manager == null) return null;

        if (!Enum.TryParse<TrafficManagerRole>(dto.Role, out var role))
            throw new ArgumentException($"Invalid role: {dto.Role}");

        typeof(TrafficManager).GetProperty(nameof(TrafficManager.Name))?.SetValue(manager, dto.Name);
        typeof(TrafficManager).GetProperty(nameof(TrafficManager.Email))?.SetValue(manager, dto.Email);
        typeof(TrafficManager).GetProperty(nameof(TrafficManager.Phone))?.SetValue(manager, dto.Phone);
        typeof(TrafficManager).GetProperty(nameof(TrafficManager.PhotoUrl))?.SetValue(manager, dto.PhotoUrl);
        typeof(TrafficManager).GetProperty(nameof(TrafficManager.Role))?.SetValue(manager, role);
        typeof(TrafficManager).GetProperty(nameof(TrafficManager.IsActive))?.SetValue(manager, dto.IsActive);

        _repository.Update(manager);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(manager);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var manager = await _repository.GetByIdAsync(id);
        if (manager == null) return false;

        _repository.Delete(manager);
        await _repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private static TrafficManagerDto MapToDto(TrafficManager manager) => new(
        manager.Id,
        manager.AgencyId,
        manager.Name,
        manager.Email,
        manager.Phone,
        manager.PhotoUrl,
        manager.Role.ToString(),
        manager.IsActive,
        manager.HiredAt,
        manager.LastLoginAt
    );
}
