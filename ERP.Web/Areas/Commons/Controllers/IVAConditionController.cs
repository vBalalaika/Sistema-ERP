using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Commons.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Commons.Controllers
{
    [Area("Commons")]
    public class IVAConditionController : BaseController<IVAConditionController>
    {
        private readonly IIVAConditionViewManager _viewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public IVAConditionController(IIVAConditionViewManager viewManager, IStringLocalizer<SharedResource> localizer)
        {
            _viewManager = viewManager;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var model = new IVAConditionViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _viewManager.LoadAll();
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<IVAConditionViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
    }
}
