using ERP.Domain.Entities.Purchases.Providers;
using ERP.Language;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Purchases.Validators.Providers
{
    public class ContactViewModelValidator : AbstractValidator<Contact>
    {
        public ContactViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(c => c.Name)
                 .NotNull()
                 .NotEmpty().WithMessage(localizer["Property is required."])
                 .MaximumLength(50).WithMessage(localizer["Property must not exceed 50 characters."]);

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage(localizer["Invalid email address."]);
        }
    }
}
