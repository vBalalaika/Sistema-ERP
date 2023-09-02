using ERP.Domain.Entities.Purchases.Providers;
using ERP.Language;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Purchases.Validators.Providers
{
    public class FinancialInformationViewModelValidator : AbstractValidator<FinancialInformation>
    {
        public FinancialInformationViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {

        }
    }
}
