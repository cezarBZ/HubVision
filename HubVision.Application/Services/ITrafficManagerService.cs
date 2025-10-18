using HubVision.Application.DTOs;

namespace HubVision.Application.Services;

public interface ITrafficManagerService
{
    Task<IReadOnlyList<TrafficManagerDto>> GetAllAsync();
    Task<IReadOnlyList<TrafficManagerDto>> GetByAgencyIdAsync(Guid agencyId);
    Task<TrafficManagerDto?> GetByIdAsync(Guid id);
    Task<TrafficManagerDto> CreateAsync(CreateTrafficManagerDto dto);
    Task<TrafficManagerDto?> UpdateAsync(Guid id, UpdateTrafficManagerDto dto);
    Task<bool> DeleteAsync(Guid id);
}
