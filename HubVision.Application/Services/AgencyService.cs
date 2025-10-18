using HubVision.Application.DTOs;
using HubVision.Domain.AggregatesModel.AgencyAggregate;
using HubVision.Domain.Core.Data;

namespace HubVision.Application.Services;

public class AgencyService : IAgencyService 
{
    private readonly IRepository<Agency, Guid> _repository;

    public AgencyService(IRepository<Agency, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<AgencyDto>> GetAllAsync()
    {
        var agencies = await _repository.GetAllAsync(null);
        return agencies.Select(MapToDto).ToList();
    }

    public async Task<AgencyDto?> GetByIdAsync(Guid id)
    {
        var agency = await _repository.GetByIdAsync(id);
        return agency != null ? MapToDto(agency) : null;
    }

    public async Task<AgencyDto> CreateAsync(CreateAgencyDto dto)
    {
        if (!Enum.TryParse<SubscriptionPlan>(dto.Plan, out var plan))
            throw new ArgumentException($"Invalid subscription plan: {dto.Plan}");

        var agency = Agency.Create(dto.Name, dto.Slug, dto.Email, plan);

        if (!string.IsNullOrEmpty(dto.Phone))
            typeof(Agency).GetProperty(nameof(Agency.Phone))?.SetValue(agency, dto.Phone);
        if (!string.IsNullOrEmpty(dto.LogoUrl))
            typeof(Agency).GetProperty(nameof(Agency.LogoUrl))?.SetValue(agency, dto.LogoUrl);
        if (!string.IsNullOrEmpty(dto.Website))
            typeof(Agency).GetProperty(nameof(Agency.Website))?.SetValue(agency, dto.Website);

        await _repository.AddAsync(agency);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(agency);
    }

    public async Task<AgencyDto?> UpdateAsync(Guid id, UpdateAgencyDto dto)
    {
        var agency = await _repository.GetByIdAsync(id);
        if (agency == null) return null;

        if (!Enum.TryParse<SubscriptionPlan>(dto.Plan, out var plan))
            throw new ArgumentException($"Invalid subscription plan: {dto.Plan}");

        typeof(Agency).GetProperty(nameof(Agency.Name))?.SetValue(agency, dto.Name);
        typeof(Agency).GetProperty(nameof(Agency.Email))?.SetValue(agency, dto.Email);
        typeof(Agency).GetProperty(nameof(Agency.Plan))?.SetValue(agency, plan);
        typeof(Agency).GetProperty(nameof(Agency.Phone))?.SetValue(agency, dto.Phone);
        typeof(Agency).GetProperty(nameof(Agency.LogoUrl))?.SetValue(agency, dto.LogoUrl);
        typeof(Agency).GetProperty(nameof(Agency.Website))?.SetValue(agency, dto.Website);
        typeof(Agency).GetProperty(nameof(Agency.IsActive))?.SetValue(agency, dto.IsActive);

        _repository.Update(agency);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(agency);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var agency = await _repository.GetByIdAsync(id);
        if (agency == null) return false;

        _repository.Delete(agency);
        await _repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    private static AgencyDto MapToDto(Agency agency) => new(
        agency.Id,
        agency.Name,
        agency.Slug,
        agency.Email,
        agency.IsActive,
        agency.Plan.ToString(),
        agency.Phone,
        agency.LogoUrl,
        agency.Website,
        agency.CreatedAt,
        agency.TrialEndsAt
    );
}
