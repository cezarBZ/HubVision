using HubVision.Application.DTOs;

namespace HubVision.Application.Services;

public interface IAgencyService
{
    Task<IReadOnlyList<AgencyDto>> GetAllAsync();
    Task<AgencyDto?> GetByIdAsync(Guid id);
    Task<AgencyDto> CreateAsync(CreateAgencyDto dto);
    Task<AgencyDto?> UpdateAsync(Guid id, UpdateAgencyDto dto);
    Task<bool> DeleteAsync(Guid id);
}
