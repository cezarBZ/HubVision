using HubVision.Application.DTOs;

namespace HubVision.Application.Services;

public interface IAdSetService
{
    Task<IReadOnlyList<AdSetDto>> GetAllAsync();
    Task<IReadOnlyList<AdSetDto>> GetByAgencyIdAsync(Guid agencyId);
    Task<IReadOnlyList<AdSetDto>> GetByCampaignIdAsync(Guid campaignId);
    Task<AdSetDto?> GetByIdAsync(Guid id);
    Task<AdSetDto> CreateAsync(CreateAdSetDto dto);
    Task<AdSetDto?> UpdateAsync(Guid id, UpdateAdSetDto dto);
    Task<bool> DeleteAsync(Guid id);
}
