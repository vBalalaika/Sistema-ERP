using ERP.Language;
using ERP.Web.Areas.Clients.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Clients.Validators
{
    public class ReminderViewModelValidator : AbstractValidator<ReminderViewModel>
    {
        public ReminderViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(r => r.Comment)
              .MaximumLength(255).WithMessage(localizer["Property must not exceed 255 characters."]);

        }
    }
}
