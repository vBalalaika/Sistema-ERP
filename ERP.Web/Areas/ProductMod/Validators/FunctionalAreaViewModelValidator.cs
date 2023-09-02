using ERP.Language;
using ERP.Web.Areas.ProductMod.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.ProductMod.Validators
{
    public class FunctionalAreaViewModelValidator : AbstractValidator<FunctionalAreaViewModel>
    {
        public FunctionalAreaViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage(localizer["Property is required."])
                .NotNull()
                .MaximumLength(50).WithMessage(localizer["Property must not exceed 50 characters."]);
        }
    }
}
