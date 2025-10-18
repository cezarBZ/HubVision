namespace HubVision.Application.DTOs;

public record TrafficManagerDto(
    Guid Id,
    Guid AgencyId,
    string Name,
    string Email,
    string? Phone,
    string? PhotoUrl,
    string Role,
    bool IsActive,
    DateTime HiredAt,
    DateTime? LastLoginAt
);

public record CreateTrafficManagerDto(
    Guid AgencyId,
    string Name,
    string Email,
    string Password,
    string Role,
    string? Phone
);

public record UpdateTrafficManagerDto(
    string Name,
    string Email,
    string? Phone,
    string? PhotoUrl,
    string Role,
    bool IsActive
);
