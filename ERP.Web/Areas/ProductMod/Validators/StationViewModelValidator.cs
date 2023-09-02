using ERP.Language;
using ERP.Web.Areas.ProductMod.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.ProductMod.Validators
{
    public class StationViewModelValidator : AbstractValidator<StationViewModel>
    {
        public StationViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(s => s.Description)
                .NotEmpty().WithMessage(localizer["Property is required."])
                .NotNull()
                .MaximumLength(50).WithMessage(localizer["Property must not exceed 50 characters."]);

            RuleFor(s => s.Abbreviation)
           .NotEmpty().WithMessage(localizer["Property is required."])
           .NotNull()
           .MaximumLength(50).WithMessage(localizer["Property must not exceed 50 characters."]);


            RuleFor(sc => sc.FunctionalAreaId)
                .NotEmpty().WithMessage(localizer["Property is required."])
                .NotNull();
        }
    }
}
