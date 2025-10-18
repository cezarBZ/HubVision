namespace HubVision.Application.DTOs;

public record AdSetDto(
    Guid Id,
    Guid AgencyId,
    Guid CampaignId,
    string Name,
    string Status,
    decimal DailyBudget,
    decimal? LifetimeBudget,
    DateTime StartTime,
    DateTime? EndTime,
    string? TargetingJson,
    string? OptimizationGoal,
    string? BillingEvent,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record CreateAdSetDto(
    Guid AgencyId,
    Guid CampaignId,
    string Name,
    string Status,
    decimal DailyBudget,
    decimal? LifetimeBudget,
    DateTime StartTime,
    DateTime? EndTime,
    string? TargetingJson,
    string? OptimizationGoal,
    string? BillingEvent
);

public record UpdateAdSetDto(
    string Name,
    string Status,
    decimal DailyBudget,
    decimal? LifetimeBudget,
    DateTime StartTime,
    DateTime? EndTime,
    string? TargetingJson,
    string? OptimizationGoal,
    string? BillingEvent
);
