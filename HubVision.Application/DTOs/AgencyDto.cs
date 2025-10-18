namespace HubVision.Application.DTOs;

public record AgencyDto(
    Guid Id,
    string Name,
    string Slug,
    string Email,
    bool IsActive,
    string Plan,
    string Phone,
    string? LogoUrl,
    string? Website,
    DateTime CreatedAt,
    DateTime? TrialEndsAt
);

public record CreateAgencyDto(
    string Name,
    string Slug,
    string Email,
    string Plan,
    string Phone,
    string? LogoUrl,
    string? Website
);

public record UpdateAgencyDto(
    string Name,
    string Email,
    string Plan,
    string Phone,
    string? LogoUrl,
    string? Website,
    bool IsActive
);
