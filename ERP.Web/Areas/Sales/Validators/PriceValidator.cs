using ERP.Language;
using ERP.Web.Areas.Sales.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Sales.Validators
{
    public class PriceValidator : AbstractValidator<PriceViewModel>
    {
        public PriceValidator(IStringLocalizer<SharedResource> localizer)
        {

        }
    }
}
