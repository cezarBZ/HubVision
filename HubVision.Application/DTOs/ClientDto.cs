namespace HubVision.Application.DTOs;

public record ClientDto(
    Guid Id,
    Guid AgencyId,
    string Name,
    string Email,
    string? Phone,
    string? PhotoUrl,
    string? Company,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? LastLoginAt
);

public record CreateClientDto(
    Guid AgencyId,
    string Name,
    string Email,
    string Password,
    string? Phone,
    string? Company
);

public record UpdateClientDto(
    string Name,
    string Email,
    string? Phone,
    string? PhotoUrl,
    string? Company,
    bool IsActive
);
