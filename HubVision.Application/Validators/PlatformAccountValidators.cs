using FluentValidation;
using HubVision.Application.DTOs;

namespace HubVision.Application.Validators;

public class CreatePlatformAccountDtoValidator : AbstractValidator<CreatePlatformAccountDto>
{
    public CreatePlatformAccountDtoValidator()
    {
        RuleFor(x => x.AgencyId)
            .NotEmpty().WithMessage("AgencyId is required");

        RuleFor(x => x.TrafficManagerId)
            .NotEmpty().WithMessage("TrafficManagerId is required");

        RuleFor(x => x.Platform)
            .NotEmpty().WithMessage("Platform is required")
            .Must(p => Enum.TryParse<Domain.AggregatesModel.PlatformAccountAggregate.PlatformType>(p, out _))
            .WithMessage("Invalid platform");

        RuleFor(x => x.PlatformUserId)
            .NotEmpty().WithMessage("PlatformUserId is required")
            .MaximumLength(200).WithMessage("PlatformUserId must not exceed 200 characters");

        RuleFor(x => x.PlatformEmail)
            .NotEmpty().WithMessage("PlatformEmail is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(200).WithMessage("PlatformEmail must not exceed 200 characters");

        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("DisplayName is required")
            .MaximumLength(200).WithMessage("DisplayName must not exceed 200 characters");

        RuleFor(x => x.AccessToken)
            .NotEmpty().WithMessage("AccessToken is required");

        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("RefreshToken is required");
    }
}

public class UpdatePlatformAccountDtoValidator : AbstractValidator<UpdatePlatformAccountDto>
{
    public UpdatePlatformAccountDtoValidator()
    {
        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("DisplayName is required")
            .MaximumLength(200).WithMessage("DisplayName must not exceed 200 characters");
    }
}
