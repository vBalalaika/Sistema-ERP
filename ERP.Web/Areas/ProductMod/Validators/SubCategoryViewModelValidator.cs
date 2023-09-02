using ERP.Language;
using ERP.Web.Areas.ProductMod.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.ProductMod.Validators
{
    public class SubCategoryViewModelValidator : AbstractValidator<SubCategoryViewModel>
    {
        public SubCategoryViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(sc => sc.Description)
                .NotEmpty().WithMessage(localizer["Property is required."])
                .NotNull()
                .MaximumLength(50).WithMessage(localizer["Property must not exceed 50 characters."]);

            RuleFor(sc => sc.Prefix)
                .NotEmpty().WithMessage(localizer["Property is required."])
                .NotNull()
                .MaximumLength(2).WithMessage(localizer["Property must not exceed 2 characters."])
                .Matches("^[A-Za-z]{2}").WithMessage(localizer["Property must have 2 alphabetic characters."])
                ;

            RuleFor(sc => sc.CategoryId)
                .NotEmpty().WithMessage(localizer["Property is required."])
                .NotNull();
        }
    }
}