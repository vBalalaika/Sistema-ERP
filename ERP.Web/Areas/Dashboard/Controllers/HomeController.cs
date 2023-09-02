using ERP.Language;
using ERP.Web.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ERP.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : BaseController<HomeController>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        public HomeController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            _notify.Information(_localizer["Welcome"]);
            return View();
        }
    }
}