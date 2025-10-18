using HubVision.Application.DTOs;

namespace HubVision.Application.Services;

public interface IAdAccountService
{
    Task<IReadOnlyList<AdAccountDto>> GetAllAsync();
    Task<IReadOnlyList<AdAccountDto>> GetByAgencyIdAsync(Guid agencyId);
    Task<AdAccountDto?> GetByIdAsync(Guid id);
    Task<AdAccountDto> CreateAsync(CreateAdAccountDto dto);
    Task<AdAccountDto?> UpdateAsync(Guid id, UpdateAdAccountDto dto);
    Task<bool> DeleteAsync(Guid id);
}
