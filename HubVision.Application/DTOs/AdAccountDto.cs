namespace HubVision.Application.DTOs;

public record AdAccountDto(
    Guid Id,
    Guid AgencyId,
    Guid PlatformAccountId,
    string AccountName,
    string Currency,
    string TimeZone,
    string Status,
    bool IsActive,
    Guid? ClientId,
    DateTime CreatedAt,
    DateTime? LastSyncedAt
);

public record CreateAdAccountDto(
    Guid AgencyId,
    Guid PlatformAccountId,
    string AccountName,
    string Currency,
    string TimeZone,
    Guid? ClientId
);

public record UpdateAdAccountDto(
    string AccountName,
    string Currency,
    string TimeZone,
    string Status,
    bool IsActive,
    Guid? ClientId
);
