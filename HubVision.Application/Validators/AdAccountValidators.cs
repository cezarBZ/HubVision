using FluentValidation;
using HubVision.Application.DTOs;

namespace HubVision.Application.Validators;

public class CreateAdAccountDtoValidator : AbstractValidator<CreateAdAccountDto>
{
    public CreateAdAccountDtoValidator()
    {
        RuleFor(x => x.AgencyId)
            .NotEmpty().WithMessage("AgencyId is required");

        RuleFor(x => x.PlatformAccountId)
            .NotEmpty().WithMessage("PlatformAccountId is required");

        RuleFor(x => x.AccountName)
            .NotEmpty().WithMessage("AccountName is required")
            .MaximumLength(200).WithMessage("AccountName must not exceed 200 characters");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required")
            .MaximumLength(10).WithMessage("Currency must not exceed 10 characters");

        RuleFor(x => x.TimeZone)
            .NotEmpty().WithMessage("TimeZone is required")
            .MaximumLength(100).WithMessage("TimeZone must not exceed 100 characters");
    }
}

public class UpdateAdAccountDtoValidator : AbstractValidator<UpdateAdAccountDto>
{
    public UpdateAdAccountDtoValidator()
    {
        RuleFor(x => x.AccountName)
            .NotEmpty().WithMessage("AccountName is required")
            .MaximumLength(200).WithMessage("AccountName must not exceed 200 characters");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required")
            .MaximumLength(10).WithMessage("Currency must not exceed 10 characters");

        RuleFor(x => x.TimeZone)
            .NotEmpty().WithMessage("TimeZone is required")
            .MaximumLength(100).WithMessage("TimeZone must not exceed 100 characters");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .Must(s => Enum.TryParse<Domain.AggregatesModel.AdAccountAggregate.AdAccountStatus>(s, out _))
            .WithMessage("Invalid status");
    }
}
