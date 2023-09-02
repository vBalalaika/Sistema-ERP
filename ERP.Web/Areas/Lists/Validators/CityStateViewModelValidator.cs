﻿using ERP.Language;
using ERP.Web.Areas.Lists.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Lists.Validators
{
    public class CityStateViewModelValidator : AbstractValidator<CityStateViewModel>
    {
        public CityStateViewModelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(p => p.City_Name)
                .NotEmpty().WithMessage(localizer["Property is required."])
                .NotNull()
                .MaximumLength(50).WithMessage(localizer["Property must not exceed 50 characters."]);
            RuleFor(p => p.State_Name)
                .NotEmpty().WithMessage(localizer["Property is required."])
                .NotNull()
                .MaximumLength(50).WithMessage(localizer["Property must not exceed 50 characters."]);
        }
    }
}
