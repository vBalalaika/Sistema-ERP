using ERP.Language;
using ERP.Web.Areas.Purchases.Models.PurchaseOrder;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Purchases.Validators.PurchaseOrder
{
    public class PurchaseOrderDetailValidator : AbstractValidator<PurchaseOrderDetailViewModel>
    {
        public PurchaseOrderDetailValidator(IStringLocalizer<SharedResource> localizer)
        {

        }
    }
}
