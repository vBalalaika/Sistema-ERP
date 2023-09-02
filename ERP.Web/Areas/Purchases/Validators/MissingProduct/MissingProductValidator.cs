using ERP.Language;
using ERP.Web.Areas.Purchases.Models.MissingProduct;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Purchases.Validators.MissingProduct
{
    public class MissingProductValidator : AbstractValidator<MissingProductViewModel>
    {
        public MissingProductValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(mp => mp.Quantity).NotEmpty();
        }
    }
}
