using HubVision.Application.DTOs;
using HubVision.Domain.AggregatesModel.CampaingAggregate;
using HubVision.Domain.Core.Data;

namespace HubVision.Application.Services;

public class CampaignService : ICampaignService
{
    private readonly IRepository<Campaign, Guid> _repository;

    public CampaignService(IRepository<Campaign, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CampaignDto>> GetAllAsync()
    {
        var campaigns = await _repository.GetAllAsync(null);
        return campaigns.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<CampaignDto>> GetByAgencyIdAsync(Guid agencyId)
    {
        var campaigns = _repository.Get(c => c.AgencyId == agencyId);
        return campaigns.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<CampaignDto>> GetByAdAccountIdAsync(Guid adAccountId)
    {
        var campaigns = _repository.Get(c => c.AdAccountId == adAccountId);
        return campaigns.Select(MapToDto).ToList();
    }

    public async Task<CampaignDto?> GetByIdAsync(Guid id)
    {
        var campaign = await _repository.GetByIdAsync(id);
        return campaign != null ? MapToDto(campaign) : null;
    }

    public async Task<CampaignDto> CreateAsync(CreateCampaignDto dto)
    {
        var campaign = (Campaign)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(Campaign));

        typeof(Campaign).GetProperty("Id")?.SetValue(campaign, Guid.NewGuid());
        typeof(Campaign).GetProperty(nameof(Campaign.AgencyId))?.SetValue(campaign, dto.AgencyId);
        typeof(Campaign).GetProperty(nameof(Campaign.AdAccountId))?.SetValue(campaign, dto.AdAccountId);
        typeof(Campaign).GetProperty(nameof(Campaign.Name))?.SetValue(campaign, dto.Name);
        typeof(Campaign).GetProperty(nameof(Campaign.Objective))?.SetValue(campaign, dto.Objective);
        typeof(Campaign).GetProperty(nameof(Campaign.Status))?.SetValue(campaign, dto.Status);
        typeof(Campaign).GetProperty(nameof(Campaign.DailyBudget))?.SetValue(campaign, dto.DailyBudget);
        typeof(Campaign).GetProperty(nameof(Campaign.LifetimeBudget))?.SetValue(campaign, dto.LifetimeBudget);
        typeof(Campaign).GetProperty(nameof(Campaign.StartTime))?.SetValue(campaign, dto.StartTime);
        typeof(Campaign).GetProperty(nameof(Campaign.EndTime))?.SetValue(campaign, dto.EndTime);
        typeof(Campaign).GetProperty(nameof(Campaign.CreatedAt))?.SetValue(campaign, DateTime.UtcNow);
        typeof(Campaign).GetProperty(nameof(Campaign.UpdatedAt))?.SetValue(campaign, DateTime.UtcNow);

        await _repository.AddAsync(campaign);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(campaign);
    }

    public async Task<CampaignDto?> UpdateAsync(Guid id, UpdateCampaignDto dto)
    {
        var campaign = await _repository.GetByIdAsync(id);
        if (campaign == null) return null;

        typeof(Campaign).GetProperty(nameof(Campaign.Name))?.SetValue(campaign, dto.Name);
        typeof(Campaign).GetProperty(nameof(Campaign.Objective))?.SetValue(campaign, dto.Objective);
        typeof(Campaign).GetProperty(nameof(Campaign.Status))?.SetValue(campaign, dto.Status);
        typeof(Campaign).GetProperty(nameof(Campaign.DailyBudget))?.SetValue(campaign, dto.DailyBudget);
        typeof(Campaign).GetProperty(nameof(Campaign.LifetimeBudget))?.SetValue(campaign, dto.LifetimeBudget);
        typeof(Campaign).GetProperty(nameof(Campaign.StartTime))?.SetValue(campaign, dto.StartTime);
        typeof(Campaign).GetProperty(nameof(Campaign.EndTime))?.SetValue(campaign, dto.EndTime);
        typeof(Campaign).GetProperty(nameof(Campaign.UpdatedAt))?.SetValue(campaign, DateTime.UtcNow);

        _repository.Update(campaign);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(campaign);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var campaign = await _repository.GetByIdAsync(id);
        if (campaign == null) return false;

        _repository.Delete(campaign);
        await _repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    private static CampaignDto MapToDto(Campaign campaign) => new(
        campaign.Id,
        campaign.AgencyId,
        campaign.AdAccountId,
        campaign.Name,
        campaign.Objective,
        campaign.Status,
        campaign.DailyBudget,
        campaign.LifetimeBudget,
        campaign.StartTime,
        campaign.EndTime,
        campaign.CreatedAt,
        campaign.UpdatedAt
    );
}
