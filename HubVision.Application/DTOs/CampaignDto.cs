namespace HubVision.Application.DTOs;

public record CampaignDto(
    Guid Id,
    Guid AgencyId,
    Guid AdAccountId,
    string Name,
    string Objective,
    string Status,
    decimal? DailyBudget,
    decimal? LifetimeBudget,
    DateTime? StartTime,
    DateTime? EndTime,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record CreateCampaignDto(
    Guid AgencyId,
    Guid AdAccountId,
    string Name,
    string Objective,
    string Status,
    decimal? DailyBudget,
    decimal? LifetimeBudget,
    DateTime? StartTime,
    DateTime? EndTime
);

public record UpdateCampaignDto(
    string Name,
    string Objective,
    string Status,
    decimal? DailyBudget,
    decimal? LifetimeBudget,
    DateTime? StartTime,
    DateTime? EndTime
);
