namespace HubVision.Application.DTOs;

public record PlatformAccountDto(
    Guid Id,
    Guid AgencyId,
    Guid TrafficManagerId,
    string Platform,
    string PlatformUserId,
    string PlatformEmail,
    string DisplayName,
    bool IsActive,
    bool IsDefault,
    DateTime ConnectedAt,
    DateTime? LastSyncedAt,
    DateTime? TokenExpiresAt
);

public record CreatePlatformAccountDto(
    Guid AgencyId,
    Guid TrafficManagerId,
    string Platform,
    string PlatformUserId,
    string PlatformEmail,
    string DisplayName,
    string AccessToken,
    string RefreshToken,
    DateTime? TokenExpiresAt
);

public record UpdatePlatformAccountDto(
    string DisplayName,
    bool IsActive,
    bool IsDefault,
    string? AccessToken,
    string? RefreshToken,
    DateTime? TokenExpiresAt
);
