using HubVision.Application.DTOs;
using HubVision.Domain.AggregatesModel.AdAccountAggregate;
using HubVision.Domain.Core.Data;

namespace HubVision.Application.Services;

public class AdAccountService : IAdAccountService
{
    private readonly IRepository<AdAccount, Guid> _repository;

    public AdAccountService(IRepository<AdAccount, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<AdAccountDto>> GetAllAsync()
    {
        var accounts = await _repository.GetAllAsync(null);
        return accounts.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<AdAccountDto>> GetByAgencyIdAsync(Guid agencyId)
    {
        var accounts = await _repository.GetAsync(a => a.AgencyId == agencyId);
        return accounts.Select(MapToDto).ToList();
    }

    public async Task<AdAccountDto?> GetByIdAsync(Guid id)
    { 
        var account = await _repository.GetByIdAsync(id);
        return account != null ? MapToDto(account) : null;
    }

    public async Task<AdAccountDto> CreateAsync(CreateAdAccountDto dto)
    {
        var account = AdAccount.Create(
            dto.AgencyId,
            dto.PlatformAccountId,
            dto.AccountName,
            dto.Currency,
            dto.TimeZone
        );

        if (dto.ClientId.HasValue)
            typeof(AdAccount).GetProperty(nameof(AdAccount.ClientId))?.SetValue(account, dto.ClientId);

        await _repository.AddAsync(account);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(account);
    }

    public async Task<AdAccountDto?> UpdateAsync(Guid id, UpdateAdAccountDto dto)
    {
        var account = await _repository.GetByIdAsync(id);
        if (account == null) return null;

        if (!Enum.TryParse<AdAccountStatus>(dto.Status, out var status))
            throw new ArgumentException($"Invalid status: {dto.Status}");

        typeof(AdAccount).GetProperty(nameof(AdAccount.AccountName))?.SetValue(account, dto.AccountName);
        typeof(AdAccount).GetProperty(nameof(AdAccount.Currency))?.SetValue(account, dto.Currency);
        typeof(AdAccount).GetProperty(nameof(AdAccount.TimeZone))?.SetValue(account, dto.TimeZone);
        typeof(AdAccount).GetProperty(nameof(AdAccount.Status))?.SetValue(account, status);
        typeof(AdAccount).GetProperty(nameof(AdAccount.IsActive))?.SetValue(account, dto.IsActive);
        typeof(AdAccount).GetProperty(nameof(AdAccount.ClientId))?.SetValue(account, dto.ClientId);

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

    private static AdAccountDto MapToDto(AdAccount account) => new(
        account.Id,
        account.AgencyId,
        account.PlatformAccountId,
        account.AccountName,
        account.Currency,
        account.TimeZone,
        account.Status.ToString(),
        account.IsActive,
        account.ClientId,
        account.CreatedAt,
        account.LastSyncedAt
    );
}
