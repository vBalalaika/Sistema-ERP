using ERP.Language;
using ERP.Web.Areas.Purchases.Models.Providers;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Purchases.Validators.Providers
{
    public class ProviderViewModelValidator : AbstractValidator<ProviderViewModel>
    {
        public ProviderViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(c => c.BusinessName)
                .NotNull()
                .NotEmpty().WithMessage(localizer["Property is required."]);
            RuleFor(s => s.CountryId)
                .InclusiveBetween(0, 5000).WithMessage(localizer["Property is required."]);
            //RuleFor(s => s.StateId)
            //    .InclusiveBetween(0, 5000).WithMessage(localizer["Property is required."]);
            RuleFor(s => s.DocumentTypeId)
                .InclusiveBetween(0, 5000).WithMessage(localizer["Property is required."]);
            RuleFor(s => s.IVAConditionId)
                .InclusiveBetween(0, 5000).WithMessage(localizer["Property is required."]);
        }
    }
}
