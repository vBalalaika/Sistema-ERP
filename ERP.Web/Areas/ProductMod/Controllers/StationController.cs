using ERP.Application.Constants;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Infrastructure.DbContexts;
using ERP.Infrastructure.Identity.Models;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Admin.Models;
using ERP.Web.Areas.ProductMod.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.ProductMod.Controllers
{
    [Area("ProductMod")]
    public class StationController : BaseController<StationController>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStationViewManager _viewManager;
        private readonly IFunctionalAreaViewManager _functionalareaviewManager;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private IdentityContext _identityContext;
        public StationController(IStationViewManager viewManager, IFunctionalAreaViewManager functionalareaviewManager, UserManager<ApplicationUser> userManager, IdentityContext identityContext, IStringLocalizer<SharedResource> localizer)
        {
            _viewManager = viewManager;
            _functionalareaviewManager = functionalareaviewManager;
            _userManager = userManager;
            _localizer = localizer;
            _identityContext = identityContext;
        }

        public IActionResult Index()
        {
            var model = new StationViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _viewManager.LoadAllWithFunctionalArea();
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<StationViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel.OrderBy(st => st.FunctionalArea.Order).ToList());
            }
            return null;
        }

        [Authorize(Policy = Permissions.Stations.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            try
            {
                var functionalareaResponse = await _functionalareaviewManager.GetAllFunctionalAreas();
                var usersViewModel = _mapper.Map<List<UserViewModel>>(_identityContext.Users.Where(user => user.IsActive == true).ToList());
                if (id == 0)
                {
                    var stationViewModel = new StationViewModel();
                    stationViewModel.UsersSelectList = new SelectList(usersViewModel, nameof(UserViewModel.Id), nameof(UserViewModel.FirstAndLastName), null, null);
                    if (functionalareaResponse.Succeeded)
                    {
                        var functionalareasViewModel = _mapper.Map<List<FunctionalAreaDTO>>(functionalareaResponse.Data);
                        stationViewModel.FunctionalAreas = new SelectList(functionalareasViewModel, nameof(FunctionalAreaViewModel.Id), nameof(FunctionalAreaViewModel.Description), null, null);
                    }
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", stationViewModel) });
                }
                else
                {
                    var response = await _viewManager.GetByIdAsync(id);
                    if (response.Succeeded)
                    {
                        var stationViewModel = _mapper.Map<StationViewModel>(response.Data);
                        if (stationViewModel.Users != null)
                        {
                            stationViewModel.UsersIds = stationViewModel.Users.Split(',');
                        }
                        stationViewModel.UsersSelectList = new SelectList(usersViewModel, nameof(UserViewModel.Id), nameof(UserViewModel.FirstAndLastName), null, null);
                        if (functionalareaResponse.Succeeded)
                        {
                            var functionalareasViewModel = _mapper.Map<List<FunctionalAreaDTO>>(functionalareaResponse.Data);
                            stationViewModel.FunctionalAreas = new SelectList(functionalareasViewModel, nameof(FunctionalAreaViewModel.Id), nameof(FunctionalAreaViewModel.Description), null, null);
                        }
                        return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", stationViewModel) });
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, StationViewModel station, string[] UsersIds)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (station.DefaultUser != null && !UsersIds.Any(id => id.Equals(station.DefaultUser)))
                    {
                        List<string> list = UsersIds.ToList();
                        list.Add(station.DefaultUser);
                        UsersIds = list.ToArray();
                    }
                    var concantIds = string.Join(',', UsersIds);
                    if (id == 0)
                    {
                        station.Users = concantIds;
                        var stationDTO = _mapper.Map<StationDTO>(station);
                        var result = await _viewManager.CreateAsync(stationDTO);
                        if (result.Succeeded)
                        {
                            id = result.Data;
                            _notify.Success(_localizer["Station with ID {0} Created.", result.Data]);
                        }
                        else _notify.Error(result.Message);
                    }
                    else
                    {
                        station.Users = concantIds;
                        var stationDTO = _mapper.Map<StationDTO>(station);
                        var result = await _viewManager.UpdateAsync(stationDTO);
                        if (result.Succeeded) _notify.Information(_localizer["Station with ID {0} Update.", result.Data]);
                    }

                    var response = await _viewManager.LoadAllWithFunctionalArea();
                    if (response.Succeeded)
                    {
                        var viewModel = _mapper.Map<List<StationViewModel>>(response.Data);
                        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                        return new JsonResult(new { isValid = true, html = html });
                    }
                    else
                    {
                        _notify.Error(response.Message);
                        return null;
                    }
                }
                else
                {
                    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", station);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", station);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _viewManager.DeleteAsync(id);
            if (deleteCommand.Succeeded)
            {
                _notify.Information(_localizer["Station with ID {0} Deleted.", id]);
                var response = await _viewManager.LoadAllWithFunctionalArea();
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<StationViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return new JsonResult(new { isValid = false });
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return new JsonResult(new { isValid = false });
            }
        }
    }
}
