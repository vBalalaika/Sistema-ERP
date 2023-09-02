using ERP.Application.Constants;
using ERP.Application.Enums;
using ERP.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdminUser
    {
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
                }
            }
        }

        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());

            await roleManager.AddPermissionClaim(adminRole, "Users");


            #region Clients
            await roleManager.AddPermissionClaim(adminRole, "Clients");
            #endregion

            await roleManager.AddPermissionClaim(adminRole, "Countries");
            await roleManager.AddPermissionClaim(adminRole, "States");
            await roleManager.AddPermissionClaim(adminRole, "Cities");

            #region Purchases

            #region Providers

            await roleManager.AddPermissionClaim(adminRole, "Providers");

            #endregion

            #region MissingProducts

            await roleManager.AddPermissionClaim(adminRole, "MissingProducts");

            #endregion

            #region PurchaseOrders

            await roleManager.AddPermissionClaim(adminRole, "PurchaseOrder");

            #endregion

            #region QuoteRequests

            await roleManager.AddPermissionClaim(adminRole, "QuoteRequest");

            #endregion

            #endregion

            #region Products
            await roleManager.AddPermissionClaim(adminRole, "Products");
            await roleManager.AddPermissionClaim(adminRole, "Categories");
            await roleManager.AddPermissionClaim(adminRole, "SubCategories");
            await roleManager.AddPermissionClaim(adminRole, "Stations");
            await roleManager.AddPermissionClaim(adminRole, "Structure");
            await roleManager.AddPermissionClaim(adminRole, "CodeGenerator");
            await roleManager.AddPermissionClaim(adminRole, "FunctionalArea");
            await roleManager.AddPermissionClaim(adminRole, "Archives");
            #endregion

            #region Sales
            await roleManager.AddPermissionClaim(adminRole, "SaleOrder");
            await roleManager.AddPermissionClaim(adminRole, "ProductionOrder");
            #endregion

            #region Production
            await roleManager.AddPermissionClaim(adminRole, "WorkOrders");
            await roleManager.AddPermissionClaim(adminRole, "WorkOrderItems");
            await roleManager.AddPermissionClaim(adminRole, "WorkOrderHeaders");
            await roleManager.AddPermissionClaim(adminRole, "WorkActivities");
            #endregion

            #region Logistics
            await roleManager.AddPermissionClaim(adminRole, "Logistic");
            #endregion

            #region Quality
            await roleManager.AddPermissionClaim(adminRole, "Quality");
            #endregion

        }

        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User Admin
            var defaultUser = new ApplicationUser
            {
                UserName = "sdavila27",
                Email = "sdavila27@gmail.com",
                FirstName = "Sebastian",
                LastName = "Davila",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "$eba$T!an.1980");
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
                await roleManager.SeedClaimsForSuperAdmin();
            }
        }
    }
}