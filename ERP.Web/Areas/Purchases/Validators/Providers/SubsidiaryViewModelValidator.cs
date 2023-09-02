using ERP.Domain.Entities.Purchases.Providers;
using ERP.Language;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Purchases.Validators.Providers
{
    public class SubsidiaryViewModelValidator : AbstractValidator<Subsidiary>
    {
        public SubsidiaryViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(s => s.SubsidiaryNumber)
                .NotNull()
                .NotEmpty().WithMessage(localizer["Property is required."])
                .MaximumLength(50).WithMessage(localizer["Property must not exceed 50 characters."]);

            RuleFor(s => s.CountryId)
                .InclusiveBetween(0, 5000).WithMessage(localizer["Property is required."]);
            //RuleFor(s => s.StateId)
            //    .InclusiveBetween(0, 5000).WithMessage(localizer["Property is required."]);
        }


    }
}
