using HubVision.Application.DTOs;
using HubVision.Domain.AggregatesModel.AdAggregate;
using HubVision.Domain.Core.Data;

namespace HubVision.Application.Services;

public class AdService : IAdService
{
    private readonly IRepository<Ad, Guid> _repository;

    public AdService(IRepository<Ad, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<AdDto>> GetAllAsync()
    {
        var ads = await _repository.GetAllAsync(null);
        return ads.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<AdDto>> GetByAgencyIdAsync(Guid agencyId)
    {
        var ads = _repository.Get(a => a.AgencyId == agencyId);
        return ads.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<AdDto>> GetByAdSetIdAsync(Guid adSetId)
    {
        var ads = _repository.Get(a => a.AdSetId == adSetId);
        return ads.Select(MapToDto).ToList();
    }

    public async Task<AdDto?> GetByIdAsync(Guid id)
    {
        var ad = await _repository.GetByIdAsync(id);
        return ad != null ? MapToDto(ad) : null;
    }

    public async Task<AdDto> CreateAsync(CreateAdDto dto)
    {
        var ad = new Ad
        {
            Name = dto.Name,
            Status = dto.Status,
            CreativeId = dto.CreativeId,
            AdSet = null!
        };

        typeof(Ad).GetProperty("Id")?.SetValue(ad, Guid.NewGuid());
        typeof(Ad).GetProperty(nameof(Ad.AgencyId))?.SetValue(ad, dto.AgencyId);
        typeof(Ad).GetProperty(nameof(Ad.AdSetId))?.SetValue(ad, dto.AdSetId);
        typeof(Ad).GetProperty(nameof(Ad.ImageUrl))?.SetValue(ad, dto.ImageUrl);
        typeof(Ad).GetProperty(nameof(Ad.VideoUrl))?.SetValue(ad, dto.VideoUrl);
        typeof(Ad).GetProperty(nameof(Ad.AdText))?.SetValue(ad, dto.AdText);
        typeof(Ad).GetProperty(nameof(Ad.Headline))?.SetValue(ad, dto.Headline);
        typeof(Ad).GetProperty(nameof(Ad.Description))?.SetValue(ad, dto.Description);
        typeof(Ad).GetProperty(nameof(Ad.CallToAction))?.SetValue(ad, dto.CallToAction);
        typeof(Ad).GetProperty(nameof(Ad.LinkUrl))?.SetValue(ad, dto.LinkUrl);
        typeof(Ad).GetProperty(nameof(Ad.CreatedAt))?.SetValue(ad, DateTime.UtcNow);
        typeof(Ad).GetProperty(nameof(Ad.UpdatedAt))?.SetValue(ad, DateTime.UtcNow);

        await _repository.AddAsync(ad);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(ad);
    }

    public async Task<AdDto?> UpdateAsync(Guid id, UpdateAdDto dto)
    {
        var ad = await _repository.GetByIdAsync(id);
        if (ad == null) return null;

        typeof(Ad).GetProperty(nameof(Ad.Name))?.SetValue(ad, dto.Name);
        typeof(Ad).GetProperty(nameof(Ad.Status))?.SetValue(ad, dto.Status);
        typeof(Ad).GetProperty(nameof(Ad.CreativeId))?.SetValue(ad, dto.CreativeId);
        typeof(Ad).GetProperty(nameof(Ad.ImageUrl))?.SetValue(ad, dto.ImageUrl);
        typeof(Ad).GetProperty(nameof(Ad.VideoUrl))?.SetValue(ad, dto.VideoUrl);
        typeof(Ad).GetProperty(nameof(Ad.AdText))?.SetValue(ad, dto.AdText);
        typeof(Ad).GetProperty(nameof(Ad.Headline))?.SetValue(ad, dto.Headline);
        typeof(Ad).GetProperty(nameof(Ad.Description))?.SetValue(ad, dto.Description);
        typeof(Ad).GetProperty(nameof(Ad.CallToAction))?.SetValue(ad, dto.CallToAction);
        typeof(Ad).GetProperty(nameof(Ad.LinkUrl))?.SetValue(ad, dto.LinkUrl);
        typeof(Ad).GetProperty(nameof(Ad.UpdatedAt))?.SetValue(ad, DateTime.UtcNow);

        _repository.Update(ad);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(ad);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var ad = await _repository.GetByIdAsync(id);
        if (ad == null) return false;

        _repository.Delete(ad);
        await _repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    private static AdDto MapToDto(Ad ad) => new(
        ad.Id,
        ad.AgencyId,
        ad.AdSetId,
        ad.Name,
        ad.Status,
        ad.CreativeId,
        ad.ImageUrl,
        ad.VideoUrl,
        ad.AdText,
        ad.Headline,
        ad.Description,
        ad.CallToAction,
        ad.LinkUrl,
        ad.CreatedAt,
        ad.UpdatedAt
    );
}
