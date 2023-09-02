using ERP.Language;
using ERP.Web.Areas.ProductMod.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.ProductMod.Validators
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage(localizer["Property is required."])
                .NotNull()
                .MaximumLength(50).WithMessage(localizer["Property must not exceed 50 characters."]);

            RuleFor(p => p.Observation)
                .MaximumLength(500).WithMessage(localizer["Property must not exceed 500 characters."]);

            RuleFor(p => p.Code)
                .NotEmpty().WithMessage(localizer["Property is required."])
                .NotNull()
                .Matches("^[A-Z]{2}[0-9]{4}").WithMessage(localizer["Property must have 2 alphabetic characters and 4 numbers."]);


            RuleFor(p => p.SubCategoryId)
               .NotEmpty().WithMessage(localizer["Property is required."])
               .NotNull();

            When(p => p.StockUnitMeasureId == 31, () =>
            {
                RuleFor(p => p.StockWidth)
                    .NotEmpty().WithMessage(localizer["Property is required."])
                    .NotNull()
                    .NotEqual(0).WithMessage(localizer["Property must be different than zero."]);
                RuleFor(p => p.StockLength)
                    .NotEmpty().WithMessage(localizer["Property is required."])
                    .NotNull()
                    .NotEqual(0).WithMessage(localizer["Property must be different than zero."]);
            });

            When(p => p.StockUnitMeasureId == 32 || p.StockUnitMeasureId == 102 || p.StockUnitMeasureId == 103, () =>
            {
                RuleFor(p => p.StockLength)
                    .NotEmpty().WithMessage(localizer["Property is required."])
                    .NotNull()
                    .NotEqual(0).WithMessage(localizer["Property must be different than zero."]);
            });

        }
    }
}