using FluentValidation;
using HubVision.Application.DTOs;

namespace HubVision.Application.Validators;

public class CreateAdSetDtoValidator : AbstractValidator<CreateAdSetDto>
{
    public CreateAdSetDtoValidator()
    {
        RuleFor(x => x.AgencyId)
            .NotEmpty().WithMessage("AgencyId is required");

        RuleFor(x => x.CampaignId)
            .NotEmpty().WithMessage("CampaignId is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .MaximumLength(50).WithMessage("Status must not exceed 50 characters");

        RuleFor(x => x.DailyBudget)
            .GreaterThan(0).WithMessage("DailyBudget must be greater than 0");

        RuleFor(x => x.LifetimeBudget)
            .GreaterThan(0).WithMessage("LifetimeBudget must be greater than 0")
            .When(x => x.LifetimeBudget.HasValue);

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime).WithMessage("EndTime must be after StartTime")
            .When(x => x.EndTime.HasValue);
    }
}

public class UpdateAdSetDtoValidator : AbstractValidator<UpdateAdSetDto>
{
    public UpdateAdSetDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .MaximumLength(50).WithMessage("Status must not exceed 50 characters");

        RuleFor(x => x.DailyBudget)
            .GreaterThan(0).WithMessage("DailyBudget must be greater than 0");

        RuleFor(x => x.LifetimeBudget)
            .GreaterThan(0).WithMessage("LifetimeBudget must be greater than 0")
            .When(x => x.LifetimeBudget.HasValue);

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime).WithMessage("EndTime must be after StartTime")
            .When(x => x.EndTime.HasValue);
    }
}
