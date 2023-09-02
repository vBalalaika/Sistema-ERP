using AspNetCoreHero.ToastNotification.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ERP.Web.Abstractions
{
    public class BasePageModel<T> : PageModel where T : class
    {
        private IMediator _mediator;

        private ILogger<T> _logger;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ILogger<T> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();

        private INotyfService notyf;

        protected INotyfService _notyf => notyf ??= HttpContext.RequestServices.GetService<INotyfService>();
    }
}