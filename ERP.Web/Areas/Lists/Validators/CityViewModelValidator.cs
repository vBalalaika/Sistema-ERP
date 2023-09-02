using ERP.Language;
using ERP.Web.Areas.Lists.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Lists.Validators
{
    public class CityViewModelValidator : AbstractValidator<CityViewModel>
    {
        public CityViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(p => p.Name)
              .NotEmpty().WithMessage(localizer["Property is required."])
              .NotNull()
              .MaximumLength(50).WithMessage(localizer["Property must not exceed 50 characters."]);
        }
    }
}
