using ERP.Application.Constants;
using ERP.Infrastructure.Identity.Models;
using ERP.Language;
using ERP.Web.Abstractions;
using ERP.Web.Areas.Admin.Models;
using ERP.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PermissionController : BaseController<PermissionController>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public PermissionController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IStringLocalizer<SharedResource> localizer)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _localizer = localizer;
        }

        public async Task<ActionResult> Index(string roleId)
        {
            var model = new PermissionViewModel();
            var allPermissions = new List<RoleClaimsViewModel>();

            allPermissions.GetPermissions(typeof(Permissions.Dashboard), roleId);

            allPermissions.GetPermissions(typeof(Permissions.Users), roleId);

            allPermissions.GetPermissions(typeof(Permissions.Products), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Stations), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Categories), roleId);
            allPermissions.GetPermissions(typeof(Permissions.SubCategories), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Structure), roleId);
            allPermissions.GetPermissions(typeof(Permissions.CodeGenerator), roleId);
            allPermissions.GetPermissions(typeof(Permissions.FunctionalArea), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Archives), roleId);

            allPermissions.GetPermissions(typeof(Permissions.Clients), roleId);

            allPermissions.GetPermissions(typeof(Permissions.States), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Cities), roleId);

            allPermissions.GetPermissions(typeof(Permissions.Providers), roleId);
            allPermissions.GetPermissions(typeof(Permissions.MissingProducts), roleId);
            allPermissions.GetPermissions(typeof(Permissions.PurchaseOrder), roleId);
            allPermissions.GetPermissions(typeof(Permissions.QuoteRequest), roleId);

            allPermissions.GetPermissions(typeof(Permissions.SaleOrder), roleId);
            allPermissions.GetPermissions(typeof(Permissions.ProductionOrder), roleId);

            allPermissions.GetPermissions(typeof(Permissions.WorkActivities), roleId);
            allPermissions.GetPermissions(typeof(Permissions.WorkOrderItems), roleId);
            allPermissions.GetPermissions(typeof(Permissions.WorkOrders), roleId);
            allPermissions.GetPermissions(typeof(Permissions.WorkOrderHeaders), roleId);

            allPermissions.GetPermissions(typeof(Permissions.Logistic), roleId);

            allPermissions.GetPermissions(typeof(Permissions.Quality), roleId);

            var role = await _roleManager.FindByIdAsync(roleId);
            model.RoleId = roleId;
            var claims = await _roleManager.GetClaimsAsync(role);
            var claimsModel = _mapper.Map<List<RoleClaimsViewModel>>(claims);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claimsModel.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            model.RoleClaims = _mapper.Map<List<RoleClaimsViewModel>>(allPermissions);
            ViewData["Title"] = _localizer["Permissions for {0} Role", role.Name];
            ViewData["Caption"] = _localizer["Manage {0} Role Permissions", role.Name];

            _notify.Success(_localizer["Updated Claims / Permissions for Role {0}", role.Name]);
            return View(model);
        }

        public async Task<IActionResult> Update(PermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            //Remove all Claims First
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddPermissionClaim(role, claim.Value);
            }
            _notify.Success(_localizer["Updated Claims / Permissions for Role {0}", role.Name]);
            //var user = await _userManager.GetUserAsync(User);
            //await _signInManager.RefreshSignInAsync(user);

            return RedirectToAction("Index", new { roleId = model.RoleId });
        }
    }
}