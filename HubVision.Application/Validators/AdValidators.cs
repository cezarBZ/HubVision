using FluentValidation;
using HubVision.Application.DTOs;

namespace HubVision.Application.Validators;

public class CreateAdDtoValidator : AbstractValidator<CreateAdDto>
{
    public CreateAdDtoValidator()
    {
        RuleFor(x => x.AgencyId)
            .NotEmpty().WithMessage("AgencyId is required");

        RuleFor(x => x.AdSetId)
            .NotEmpty().WithMessage("AdSetId is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .MaximumLength(50).WithMessage("Status must not exceed 50 characters");

        RuleFor(x => x.CreativeId)
            .NotEmpty().WithMessage("CreativeId is required")
            .MaximumLength(100).WithMessage("CreativeId must not exceed 100 characters");

        RuleFor(x => x.Headline)
            .MaximumLength(100).WithMessage("Headline must not exceed 100 characters")
            .When(x => !string.IsNullOrEmpty(x.Headline));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}

public class UpdateAdDtoValidator : AbstractValidator<UpdateAdDto>
{
    public UpdateAdDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .MaximumLength(50).WithMessage("Status must not exceed 50 characters");

        RuleFor(x => x.CreativeId)
            .NotEmpty().WithMessage("CreativeId is required")
            .MaximumLength(100).WithMessage("CreativeId must not exceed 100 characters");

        RuleFor(x => x.Headline)
            .MaximumLength(100).WithMessage("Headline must not exceed 100 characters")
            .When(x => !string.IsNullOrEmpty(x.Headline));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}
