using HubVision.Application.DTOs;
using HubVision.Domain.AggregatesModel.ClientAggregate;
using HubVision.Domain.Core.Data;
using System.Security.Cryptography;
using System.Text;

namespace HubVision.Application.Services;

public class ClientService : IClientService
{
    private readonly IRepository<Client, Guid> _repository;

    public ClientService(IRepository<Client, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ClientDto>> GetAllAsync()
    {
        var clients = await _repository.GetAllAsync(null);
        return clients.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<ClientDto>> GetByAgencyIdAsync(Guid agencyId)
    {
        var clients = _repository.Get(c => c.AgencyId == agencyId);
        return clients.Select(MapToDto).ToList();
    }

    public async Task<ClientDto?> GetByIdAsync(Guid id)
    {
        var client = await _repository.GetByIdAsync(id);
        return client != null ? MapToDto(client) : null;
    }

    public async Task<ClientDto> CreateAsync(CreateClientDto dto)
    {
        var passwordHash = HashPassword(dto.Password);
        var client = Client.Create(dto.AgencyId, dto.Name, dto.Email, passwordHash);

        if (!string.IsNullOrEmpty(dto.Phone))
            typeof(Client).GetProperty(nameof(Client.Phone))?.SetValue(client, dto.Phone);
        if (!string.IsNullOrEmpty(dto.Company))
            typeof(Client).GetProperty(nameof(Client.Company))?.SetValue(client, dto.Company);

        await _repository.AddAsync(client);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(client);
    }

    public async Task<ClientDto?> UpdateAsync(Guid id, UpdateClientDto dto)
    {
        var client = await _repository.GetByIdAsync(id);
        if (client == null) return null;

        typeof(Client).GetProperty(nameof(Client.Name))?.SetValue(client, dto.Name);
        typeof(Client).GetProperty(nameof(Client.Email))?.SetValue(client, dto.Email);
        typeof(Client).GetProperty(nameof(Client.Phone))?.SetValue(client, dto.Phone);
        typeof(Client).GetProperty(nameof(Client.PhotoUrl))?.SetValue(client, dto.PhotoUrl);
        typeof(Client).GetProperty(nameof(Client.Company))?.SetValue(client, dto.Company);
        typeof(Client).GetProperty(nameof(Client.IsActive))?.SetValue(client, dto.IsActive);

        _repository.Update(client);
        await _repository.UnitOfWork.SaveChangesAsync();

        return MapToDto(client);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var client = await _repository.GetByIdAsync(id);
        if (client == null) return false;

        _repository.Delete(client);
        await _repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private static ClientDto MapToDto(Client client) => new(
        client.Id,
        client.AgencyId,
        client.Name,
        client.Email,
        client.Phone,
        client.PhotoUrl,
        client.Company,
        client.IsActive,
        client.CreatedAt,
        client.LastLoginAt
    );
}
