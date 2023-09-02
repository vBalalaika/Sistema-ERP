using ERP.Language;
using ERP.Web.Areas.Clients.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Clients.Validators
{
    public class ClientViewModelValidator : AbstractValidator<ClientViewModel>
    {
        public ClientViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(c => c.BusinessName)
                .NotNull()
                .NotEmpty().WithMessage(localizer["Property is required."])
                .MaximumLength(500).WithMessage(localizer["Property must not exceed 500 characters."]);

            //RuleFor(c => c.Email)
            //    .EmailAddress().WithMessage(localizer["Invalid email address."]);
        }
    }
}
