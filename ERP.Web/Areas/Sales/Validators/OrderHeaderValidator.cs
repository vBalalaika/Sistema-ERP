using ERP.Language;
using ERP.Web.Areas.Sales.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Sales.Validators
{
    public class OrderHeaderValidator : AbstractValidator<OrderHeaderViewModel>
    {
        public OrderHeaderValidator(IStringLocalizer<SharedResource> localizer)
        {

        }
    }
}
