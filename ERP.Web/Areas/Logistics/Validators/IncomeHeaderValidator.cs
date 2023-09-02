using ERP.Language;
using ERP.Web.Areas.Logistics.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Logistics.Validators
{
    public class IncomeHeaderValidator : AbstractValidator<IncomeHeaderViewModel>
    {
        public IncomeHeaderValidator(IStringLocalizer<SharedResource> localizer)
        {

        }
    }
}
