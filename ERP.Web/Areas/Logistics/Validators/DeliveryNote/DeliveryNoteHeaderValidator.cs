using ERP.Language;
using ERP.Web.Areas.Logistics.Models.DeliveryNote;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Logistics.Validators.DeliveryNote
{
    public class DeliveryNoteHeaderValidator : AbstractValidator<DeliveryNoteHeaderViewModel>
    {
        public DeliveryNoteHeaderValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(dnh => dnh.Number)
                .NotNull()
                .NotEmpty().WithMessage(localizer["Property is required."]);
        }
    }
}
