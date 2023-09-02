using ERP.Language;
using ERP.Web.Areas.Sales.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Sales.Validators
{
    public class OrderDetailValidator : AbstractValidator<OrderDetailViewModel>
    {
        public OrderDetailValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Quantity)
                .NotNull()
                .NotEqual(0).WithMessage(localizer["The quantity cannot be zero."])
                .GreaterThan(0).WithMessage(localizer["The quantity must be greater than zero."]);
        }
    }
}
