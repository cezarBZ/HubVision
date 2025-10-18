using HubVision.Application.DTOs;
using HubVision.Domain.AggregatesModel.AdSetAggregate;
using HubVision.Domain.Core.Data;

namespace HubVision.Application.Services;

public class AdSetService : IAdSetService
{
    private readonly IRepository<AdSet, Guid> _repository;

    public AdSetService(IRepository<AdSet, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<AdSetDto>> GetAllAsync()
    {
        var adSets = await _repository.GetAllAsync(null);
        return adSets.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<AdSetDto>> GetByAgencyIdAsync(Guid agencyId)
    {
        var adSets = _repository.Get(a => a.AgencyId == agencyId);
        return adSets.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<AdSetDto>> GetByCampaignIdAsync(Guid campaignId)
    {
        var adSets = _repository.Get(a => a.CampaignId == campaignId);
        return adSets.Select(MapToDto).ToList();
    }

    public async Task<AdSetDto?> GetByIdAsync(Guid id)
    {
        var adSet = await _repository.GetByIdAsync(id);
        return adSet != null ? MapToDto(adSet) : null;
    }

    public async Task<AdSetDto> CreateAsync(CreateAdSetDto dto)
    {
        var adSet = (AdSet)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(AdSet));

        typeof(AdSet).GetProperty("Id")?.SetValue(adSet, Guid.NewGuid());
        typeof(AdSet).GetProperty(nameof(AdSet.AgencyId))?.SetValue(adSet, dto.AgencyId);
        typeof(AdSet).GetProperty(nameof(AdSet.CampaignId))?.SetValue(adSet, dto.CampaignId);
        typeof(AdSet).GetProperty(nameof(AdSet.Name))?.SetValue(adSet, dto.Name);
        typeof(AdSet).GetProperty(nameof(AdSet.Status))?.SetValue(adSet, dto.Status);
        typeof(AdSet).GetProperty(nameof(AdSet.DailyBudget))?.SetValue(adSet, dto.DailyBudget);
        typeof(AdSet).GetProperty(nameof(AdSet.LifetimeBudget))?.SetValue(adSet, dto.LifetimeBudget);
        typeof(AdSet).GetProperty(nameof(AdSet.StartTime))?.SetValue(adSet, dto.StartTime);
        typeof(AdSet).GetProperty(nameof(AdSet.EndTime))?.SetValue(adSet, dto.EndTime);
        typeof(AdSet).GetProperty(nameof(AdSet.TargetingJson))?.SetValue(adSet, dto.TargetingJson);
        typeof(AdSet).GetProperty(nameof(AdSet.OptimizationGoal))?.SetValue(adSet, dto.OptimizationGoal);
        typeof(AdSet).GetProperty(nameof(AdSet.BillingEvent))?.SetValue(adSet, dto.BillingEvent);
        typeof(AdSet).GetProperty(nameof(AdSet.CreatedAt))?.SetValue(adSet, DateTime.UtcNow);
        typeof(AdSet).GetProperty(nameof(AdSet.UpdatedAt))?.SetValue(adSet, DateTime.UtcNow);

        await _repository.AddAsync(adSet);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(adSet);
    }

    public async Task<AdSetDto?> UpdateAsync(Guid id, UpdateAdSetDto dto)
    {
        var adSet = await _repository.GetByIdAsync(id);
        if (adSet == null) return null;

        typeof(AdSet).GetProperty(nameof(AdSet.Name))?.SetValue(adSet, dto.Name);
        typeof(AdSet).GetProperty(nameof(AdSet.Status))?.SetValue(adSet, dto.Status);
        typeof(AdSet).GetProperty(nameof(AdSet.DailyBudget))?.SetValue(adSet, dto.DailyBudget);
        typeof(AdSet).GetProperty(nameof(AdSet.LifetimeBudget))?.SetValue(adSet, dto.LifetimeBudget);
        typeof(AdSet).GetProperty(nameof(AdSet.StartTime))?.SetValue(adSet, dto.StartTime);
        typeof(AdSet).GetProperty(nameof(AdSet.EndTime))?.SetValue(adSet, dto.EndTime);
        typeof(AdSet).GetProperty(nameof(AdSet.TargetingJson))?.SetValue(adSet, dto.TargetingJson);
        typeof(AdSet).GetProperty(nameof(AdSet.OptimizationGoal))?.SetValue(adSet, dto.OptimizationGoal);
        typeof(AdSet).GetProperty(nameof(AdSet.BillingEvent))?.SetValue(adSet, dto.BillingEvent);
        typeof(AdSet).GetProperty(nameof(AdSet.UpdatedAt))?.SetValue(adSet, DateTime.UtcNow);

        _repository.Update(adSet);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(adSet);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var adSet = await _repository.GetByIdAsync(id);
        if (adSet == null) return false;

        _repository.Delete(adSet);
        await _repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    private static AdSetDto MapToDto(AdSet adSet) => new(
        adSet.Id,
        adSet.AgencyId,
        adSet.CampaignId,
        adSet.Name,
        adSet.Status,
        adSet.DailyBudget,
        adSet.LifetimeBudget,
        adSet.StartTime,
        adSet.EndTime,
        adSet.TargetingJson,
        adSet.OptimizationGoal,
        adSet.BillingEvent,
        adSet.CreatedAt,
        adSet.UpdatedAt
    );
}
