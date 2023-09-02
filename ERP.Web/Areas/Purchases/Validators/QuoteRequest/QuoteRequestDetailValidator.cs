using ERP.Language;
using ERP.Web.Areas.Purchases.Models.QuoteRequest;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Purchases.Validators.QuoteRequest
{
    public class QuoteRequestDetailValidator : AbstractValidator<QuoteRequestDetailViewModel>
    {
        public QuoteRequestDetailValidator(IStringLocalizer<SharedResource> localizer)
        {

        }
    }
}
