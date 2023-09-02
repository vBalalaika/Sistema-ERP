using ERP.Language;
using ERP.Web.Areas.Logistics.Models.DeliveryNote;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Logistics.Validators.DeliveryNote
{
    public class DeliveryNoteDetailValidator : AbstractValidator<DeliveryNoteDetailViewModel>
    {
        public DeliveryNoteDetailValidator(IStringLocalizer<SharedResource> localizer)
        {
        }
    }
}
