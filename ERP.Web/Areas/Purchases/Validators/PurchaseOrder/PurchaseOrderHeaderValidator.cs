using ERP.Language;
using ERP.Web.Areas.Purchases.Models.PurchaseOrder;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Purchases.Validators.PurchaseOrder
{
    public class PurchaseOrderHeaderValidator : AbstractValidator<PurchaseOrderHeaderViewModel>
    {
        public PurchaseOrderHeaderValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Comments).MaximumLength(100).WithMessage(localizer["Property must not exceed 100 characters."]);
        }
    }
}
