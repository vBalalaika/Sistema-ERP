using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.Lists;
using ERP.Application.Interfaces.ViewManagers.Lists;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Lists.Models;
using ERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Lists.Controllers
{
    [Area("Lists")]
    public class CityController : BaseController<CityController>
    {
        private readonly ICityViewManager _viewManager;
        private readonly IStateViewManager _stateViewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CityController(ICityViewManager viewManager, IStateViewManager stateViewManager, IStringLocalizer<SharedResource> localizer)
        {
            _viewManager = viewManager;
            _stateViewManager = stateViewManager;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var model = new CityViewModel();
            return View(model);
        }

        public async Task<IActionResult> GetCitiesRecords(DatatableParamsViewModel param, string sSearch_2, string sSearch_3)
        {
            var response = await _viewManager.LoadAll();
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CityViewModel>>(response.Data);
                IEnumerable<CityViewModel> cities = viewModel;

                cities = cities.Where(x => x.Name != "");

                if (!string.IsNullOrEmpty(sSearch_2))
                {
                    cities = cities.Where(x => x.Name.ToLower().Contains(sSearch_2.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(sSearch_3))
                {
                    cities = cities.Where(x => x.ZipCode.ToLower().Contains(sSearch_3.ToLower())).ToList();
                }

                var sortColumnIndex = param.iSortCol_0;
                var sortDirection = param.sSortDir_0;

                if (sortColumnIndex == 2)
                {
                    cities = sortDirection == "asc" ? cities.OrderBy(c => c.Name) : cities.OrderByDescending(c => c.Name);
                }
                else if (sortColumnIndex == 3)
                {
                    cities = sortDirection == "asc" ? cities.OrderBy(c => c.ZipCode) : cities.OrderByDescending(c => c.ZipCode);
                }

                var displayResult = cities.Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength).ToList();

                var totalRecords = cities.Count();

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = displayResult
                });
            }
            else
            {
                return null;
            }
        }

        [Authorize(Policy = Permissions.Cities.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var statesResponse = await _stateViewManager.LoadAll();

            if (id == 0)
            {
                var cityViewModel = new CityViewModel();

                if (statesResponse.Succeeded)
                {
                    var stateViewModel = _mapper.Map<List<StateDTO>>(statesResponse.Data);
                    cityViewModel.States = new SelectList(stateViewModel, nameof(StateViewModel.Id), nameof(StateViewModel.Name), null, null);
                }

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", cityViewModel) });
            }
            else
            {
                var response = await _viewManager.GetByIdAsync(id);
                if (response.Succeeded)
                {
                    var cityViewModel = _mapper.Map<CityViewModel>(response.Data);

                    if (statesResponse.Succeeded)
                    {
                        var stateViewModel = _mapper.Map<List<StateViewModel>>(statesResponse.Data);
                        cityViewModel.States = new SelectList(stateViewModel, nameof(StateViewModel.Id), nameof(StateViewModel.Name), null, null);
                    }

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", cityViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, CityViewModel city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        // Create
                        var cityDTO = _mapper.Map<CityDTO>(city);
                        var result = await _viewManager.CreateAsync(cityDTO);
                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Success(_localizer["City with ID {0} created.", result.Data]);
                        }
                        else
                        {
                            _notify.Error(result.Message);
                        }
                    }
                    else
                    {
                        // Update
                        var cityDTO = _mapper.Map<CityDTO>(city);
                        var result = await _viewManager.UpdateAsync(cityDTO);
                        if (result.Succeeded) _notify.Information(_localizer["City with ID {0} update.", result.Data]);
                    }

                    var html = await _viewRenderer.RenderViewToStringAsync("Index", city);
                    return new JsonResult(new { isValid = true, html });
                }
                else
                {
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", city);
                    return new JsonResult(new { isValid = false, html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", city);
                return new JsonResult(new { isValid = false, html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _viewManager.DeleteAsync(id);
            if (deleteCommand.Succeeded)
            {
                _notify.Information(_localizer["City with ID {0} deleted.", id]);
                return new JsonResult(true);
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return new JsonResult(false);
            }
        }
    }
}
