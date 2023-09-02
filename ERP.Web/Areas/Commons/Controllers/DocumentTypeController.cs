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
    public class DocumentTypeController : BaseController<DocumentTypeController>
    {
        private readonly IDocumentTypeViewManager _viewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DocumentTypeController(IDocumentTypeViewManager viewManager, IStringLocalizer<SharedResource> localizer)
        {
            _viewManager = viewManager;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var model = new DocumentTypeViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _viewManager.LoadAll();
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<DocumentTypeViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

    }
}
