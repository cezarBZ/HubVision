using HubVision.Application.DTOs;

namespace HubVision.Application.Services;

public interface ICampaignService
{
    Task<IReadOnlyList<CampaignDto>> GetAllAsync();
    Task<IReadOnlyList<CampaignDto>> GetByAgencyIdAsync(Guid agencyId);
    Task<IReadOnlyList<CampaignDto>> GetByAdAccountIdAsync(Guid adAccountId);
    Task<CampaignDto?> GetByIdAsync(Guid id);
    Task<CampaignDto> CreateAsync(CreateCampaignDto dto);
    Task<CampaignDto?> UpdateAsync(Guid id, UpdateCampaignDto dto);
    Task<bool> DeleteAsync(Guid id);
}
