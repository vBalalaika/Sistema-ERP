using ERP.Language;
using ERP.Web.Areas.ProductMod.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.ProductMod.Validators
{
    public class CodeGeneratorViewModelValidator : AbstractValidator<CodeGeneratorViewModel>
    {
        public CodeGeneratorViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(p => p.Quantity)
                .GreaterThan(0).WithMessage(localizer["Property must be greater than 0"]);

            RuleFor(p => p.SubCategoryId)
               .NotEmpty().WithMessage(localizer["Property is required."])
               .NotNull();

        }
    }
}

