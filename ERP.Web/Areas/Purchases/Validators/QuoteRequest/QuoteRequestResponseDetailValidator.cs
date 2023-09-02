using ERP.Language;
using ERP.Web.Areas.Purchases.Models.QuoteRequest;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Purchases.Validators.QuoteRequest
{
    public class QuoteRequestResponseDetailValidator : AbstractValidator<QuoteRequestResponseDetailViewModel>
    {
        public QuoteRequestResponseDetailValidator(IStringLocalizer<SharedResource> localizer)
        {

        }
    }
}
