using ERP.Language;
using ERP.Web.Areas.Clients.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Clients.Validators
{
    public class ConsultedProductViewModelValidator : AbstractValidator<CommunicationViewModel>
    {
        public ConsultedProductViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {

        }
    }
}
