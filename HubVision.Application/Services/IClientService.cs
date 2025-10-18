using HubVision.Application.DTOs;

namespace HubVision.Application.Services;

public interface IClientService
{
    Task<IReadOnlyList<ClientDto>> GetAllAsync();
    Task<IReadOnlyList<ClientDto>> GetByAgencyIdAsync(Guid agencyId);
    Task<ClientDto?> GetByIdAsync(Guid id);
    Task<ClientDto> CreateAsync(CreateClientDto dto);
    Task<ClientDto?> UpdateAsync(Guid id, UpdateClientDto dto);
    Task<bool> DeleteAsync(Guid id);
}
