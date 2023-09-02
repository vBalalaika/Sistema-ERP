using ERP.Language;
using ERP.Web.Areas.Clients.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Clients.Validators
{
    public class SaleOperationViewModelValidator : AbstractValidator<SaleOperationViewModel>
    {
        public SaleOperationViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {

        }
    }
}
