using FluentValidation;
using HubVision.Application.DTOs;

namespace HubVision.Application.Validators;

public class CreateAgencyDtoValidator : AbstractValidator<CreateAgencyDto>
{
    public CreateAgencyDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug is required")
            .MaximumLength(20).WithMessage("Slug must not exceed 20 characters")
            .Matches("^[a-z0-9-]+$").WithMessage("Slug must contain only lowercase letters, numbers, and hyphens");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(200).WithMessage("Email must not exceed 200 characters");

        RuleFor(x => x.Plan)
            .NotEmpty().WithMessage("Plan is required")
            .Must(p => Enum.TryParse<Domain.AggregatesModel.AgencyAggregate.SubscriptionPlan>(p, out _))
            .WithMessage("Invalid subscription plan");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .MaximumLength(50).WithMessage("Phone must not exceed 50 characters");
    }
}

public class UpdateAgencyDtoValidator : AbstractValidator<UpdateAgencyDto>
{
    public UpdateAgencyDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(200).WithMessage("Email must not exceed 200 characters");

        RuleFor(x => x.Plan)
            .NotEmpty().WithMessage("Plan is required")
            .Must(p => Enum.TryParse<Domain.AggregatesModel.AgencyAggregate.SubscriptionPlan>(p, out _))
            .WithMessage("Invalid subscription plan");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .MaximumLength(50).WithMessage("Phone must not exceed 50 characters");
    }
}
