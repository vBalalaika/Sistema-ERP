using ERP.Language;
using ERP.Web.Areas.Purchases.Models.QuoteRequest;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Purchases.Validators.QuoteRequest
{
    public class QuoteRequestHeaderValidator : AbstractValidator<QuoteRequestHeaderViewModel>
    {
        public QuoteRequestHeaderValidator(IStringLocalizer<SharedResource> localizer)
        {

        }
    }
}
