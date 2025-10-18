using HubVision.Application.DTOs;
using HubVision.Domain.AggregatesModel.PlatformAccountAggregate;
using HubVision.Domain.Core.Data;

namespace HubVision.Application.Services;

public class PlatformAccountService : IPlatformAccountService
{
    private readonly IRepository<PlatformAccount, Guid> _repository;

    public PlatformAccountService(IRepository<PlatformAccount, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<PlatformAccountDto>> GetAllAsync()
    {
        var accounts = await _repository.GetAllAsync(null);
        return accounts.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<PlatformAccountDto>> GetByAgencyIdAsync(Guid agencyId)
    {
        var accounts = _repository.Get(pa => pa.AgencyId == agencyId);
        return accounts.Select(MapToDto).ToList();
    }

    public async Task<PlatformAccountDto?> GetByIdAsync(Guid id)
    {
        var account = await _repository.GetByIdAsync(id);
        return account != null ? MapToDto(account) : null;
    }

    public async Task<PlatformAccountDto> CreateAsync(CreatePlatformAccountDto dto)
    {
        if (!Enum.TryParse<PlatformType>(dto.Platform, out var platform))
            throw new ArgumentException($"Invalid platform: {dto.Platform}");

        var account = PlatformAccount.Create(
            dto.AgencyId,
            dto.TrafficManagerId,
            platform,
            dto.PlatformUserId,
            dto.PlatformEmail,
            dto.DisplayName,
            dto.AccessToken,
            dto.RefreshToken,
            dto.TokenExpiresAt
        );

        await _repository.AddAsync(account);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(account);
    }

    public async Task<PlatformAccountDto?> UpdateAsync(Guid id, UpdatePlatformAccountDto dto)
    {
        var account = await _repository.GetByIdAsync(id);
        if (account == null) return null;

        typeof(PlatformAccount).GetProperty(nameof(PlatformAccount.DisplayName))?.SetValue(account, dto.DisplayName);
        typeof(PlatformAccount).GetProperty(nameof(PlatformAccount.IsActive))?.SetValue(account, dto.IsActive);
        typeof(PlatformAccount).GetProperty(nameof(PlatformAccount.IsDefault))?.SetValue(account, dto.IsDefault);

        if (!string.IsNullOrEmpty(dto.AccessToken))
            typeof(PlatformAccount).GetProperty(nameof(PlatformAccount.AccessToken))?.SetValue(account, dto.AccessToken);
        if (!string.IsNullOrEmpty(dto.RefreshToken))
            typeof(PlatformAccount).GetProperty(nameof(PlatformAccount.RefreshToken))?.SetValue(account, dto.RefreshToken);
        if (dto.TokenExpiresAt.HasValue)
            typeof(PlatformAccount).GetProperty(nameof(PlatformAccount.TokenExpiresAt))?.SetValue(account, dto.TokenExpiresAt);

        _repository.Update(account);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(account);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var account = await _repository.GetByIdAsync(id);
        if (account == null) return false;

        _repository.Delete(account);
        await _repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    private static PlatformAccountDto MapToDto(PlatformAccount account) => new(
        account.Id,
        account.AgencyId,
        account.TrafficManagerId,
        account.Platform.ToString(),
        account.PlatformUserId,
        account.PlatformEmail,
        account.DisplayName,
        account.IsActive,
        account.IsDefault,
        account.ConnectedAt,
        account.LastSyncedAt,
        account.TokenExpiresAt
    );
}
