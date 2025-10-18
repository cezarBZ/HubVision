using HubVision.Application.DTOs;

namespace HubVision.Application.Services;

public interface IPlatformAccountService
{
    Task<IReadOnlyList<PlatformAccountDto>> GetAllAsync();
    Task<IReadOnlyList<PlatformAccountDto>> GetByAgencyIdAsync(Guid agencyId);
    Task<PlatformAccountDto?> GetByIdAsync(Guid id);
    Task<PlatformAccountDto> CreateAsync(CreatePlatformAccountDto dto);
    Task<PlatformAccountDto?> UpdateAsync(Guid id, UpdatePlatformAccountDto dto);
    Task<bool> DeleteAsync(Guid id);
}
