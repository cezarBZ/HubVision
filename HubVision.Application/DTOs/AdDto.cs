namespace HubVision.Application.DTOs;

public record AdDto(
    Guid Id,
    Guid AgencyId,
    Guid AdSetId,
    string Name,
    string Status,
    string CreativeId,
    string? ImageUrl,
    string? VideoUrl,
    string? AdText,
    string? Headline,
    string? Description,
    string? CallToAction,
    string? LinkUrl,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record CreateAdDto(
    Guid AgencyId,
    Guid AdSetId,
    string Name,
    string Status,
    string CreativeId,
    string? ImageUrl,
    string? VideoUrl,
    string? AdText,
    string? Headline,
    string? Description,
    string? CallToAction,
    string? LinkUrl
);

public record UpdateAdDto(
    string Name,
    string Status,
    string CreativeId,
    string? ImageUrl,
    string? VideoUrl,
    string? AdText,
    string? Headline,
    string? Description,
    string? CallToAction,
    string? LinkUrl
);
