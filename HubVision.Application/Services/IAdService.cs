using HubVision.Application.DTOs;

namespace HubVision.Application.Services;

public interface IAdService
{
    Task<IReadOnlyList<AdDto>> GetAllAsync();
    Task<IReadOnlyList<AdDto>> GetByAgencyIdAsync(Guid agencyId);
    Task<IReadOnlyList<AdDto>> GetByAdSetIdAsync(Guid adSetId);
    Task<AdDto?> GetByIdAsync(Guid id);
    Task<AdDto> CreateAsync(CreateAdDto dto);
    Task<AdDto?> UpdateAsync(Guid id, UpdateAdDto dto);
    Task<bool> DeleteAsync(Guid id);
}
