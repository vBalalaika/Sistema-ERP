using System.Collections.Generic;

namespace ERP.Application.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

        #region Dashboard
        public static class Dashboard
        {
            public const string View = "Permissions.Dashboard.View";
            public const string Create = "Permissions.Dashboard.Create";
            public const string Edit = "Permissions.Dashboard.Edit";
            public const string Delete = "Permissions.Dashboard.Delete";
        }
        #endregion

        #region Users
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }
        #endregion

        #region Products
        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
        }

        public static class Archives
        {
            public const string View = "Permissions.Archives.View";
            public const string Create = "Permissions.Archives.Create";
            public const string Edit = "Permissions.Archives.Edit";
            public const string Delete = "Permissions.Archives.Delete";
        }

        public static class Stations
        {
            public const string View = "Permissions.Stations.View";
            public const string Create = "Permissions.Stations.Create";
            public const string Edit = "Permissions.Stations.Edit";
            public const string Delete = "Permissions.Stations.Delete";
        }

        public static class Categories
        {
            public const string View = "Permissions.Categories.View";
            public const string Create = "Permissions.Categories.Create";
            public const string Edit = "Permissions.Categories.Edit";
            public const string Delete = "Permissions.Categories.Delete";
        }
        public static class SubCategories
        {
            public const string View = "Permissions.SubCategories.View";
            public const string Create = "Permissions.SubCategories.Create";
            public const string Edit = "Permissions.SubCategories.Edit";
            public const string Delete = "Permissions.SubCategories.Delete";
        }
        public static class Structure
        {
            public const string View = "Permissions.Structure.View";
            public const string Create = "Permissions.Structure.Create";
            public const string Edit = "Permissions.Structure.Edit";
            public const string Delete = "Permissions.Structure.Delete";
        }
        public static class CodeGenerator
        {
            public const string View = "Permissions.CodeGenerator.View";
            public const string Create = "Permissions.CodeGenerator.Create";
        }
        public static class FunctionalArea
        {
            public const string View = "Permissions.FunctionalArea.View";
            public const string Create = "Permissions.FunctionalArea.Create";
            public const string Edit = "Permissions.FunctionalArea.Edit";
            public const string Delete = "Permissions.FunctionalArea.Delete";
        }
        #endregion

        #region Clients
        public static class Clients
        {
            public const string View = "Permissions.Clients.View";
            public const string Create = "Permissions.Clients.Create";
            public const string Edit = "Permissions.Clients.Edit";
            public const string Delete = "Permissions.Clients.Delete";
        }
        #endregion

        #region Lists
        public static class States
        {
            public const string View = "Permissions.States.View";
            public const string Create = "Permissions.States.Create";
            public const string Edit = "Permissions.States.Edit";
            public const string Delete = "Permissions.States.Delete";
        }
        public static class Cities
        {
            public const string View = "Permissions.Cities.View";
            public const string Create = "Permissions.Cities.Create";
            public const string Edit = "Permissions.Cities.Edit";
            public const string Delete = "Permissions.Cities.Delete";
        }
        #endregion

        #region Purchases

        #region Providers
        public static class Providers
        {
            public const string View = "Permissions.Providers.View";
            public const string Create = "Permissions.Providers.Create";
            public const string Edit = "Permissions.Providers.Edit";
            public const string Delete = "Permissions.Providers.Delete";
        }
        #endregion

        #region MissingProducts
        public static class MissingProducts
        {
            public const string View = "Permissions.MissingProducts.View";
            public const string Create = "Permissions.MissingProducts.Create";
            public const string Edit = "Permissions.MissingProducts.Edit";
            public const string Delete = "Permissions.MissingProducts.Delete";
        }
        #endregion

        #region PurchaseOrders
        public static class PurchaseOrder
        {
            public const string View = "Permissions.PurchaseOrder.View";
            public const string Create = "Permissions.PurchaseOrder.Create";
            public const string Edit = "Permissions.PurchaseOrder.Edit";
            public const string Delete = "Permissions.PurchaseOrder.Delete";
        }
        #endregion

        #region QuoteRequests
        public static class QuoteRequest
        {
            public const string View = "Permissions.QuoteRequest.View";
            public const string Create = "Permissions.QuoteRequest.Create";
            public const string Edit = "Permissions.QuoteRequest.Edit";
            public const string Delete = "Permissions.QuoteRequest.Delete";
        }
        #endregion

        #endregion

        #region Sales
        public static class SaleOrder
        {
            public const string View = "Permissions.SaleOrder.View";
            public const string Create = "Permissions.SaleOrder.Create";
            public const string Edit = "Permissions.SaleOrder.Edit";
            public const string Delete = "Permissions.SaleOrder.Delete";
        }
        public static class ProductionOrder
        {
            public const string View = "Permissions.ProductionOrder.View";
            public const string Create = "Permissions.ProductionOrder.Create";
            public const string Edit = "Permissions.ProductionOrder.Edit";
            public const string Delete = "Permissions.ProductionOrder.Delete";
        }
        #endregion

        #region Production
        public static class WorkOrders
        {
            public const string View = "Permissions.WorkOrders.View";
            public const string Create = "Permissions.WorkOrders.Create";
            public const string Edit = "Permissions.WorkOrders.Edit";
            public const string Delete = "Permissions.WorkOrders.Delete";
        }
        public static class WorkOrderItems
        {
            public const string View = "Permissions.WorkOrderItems.View";
            public const string Create = "Permissions.WorkOrderItems.Create";
            public const string Edit = "Permissions.WorkOrderItems.Edit";
            public const string Delete = "Permissions.WorkOrderItems.Delete";
        }
        public static class WorkActivities
        {
            public const string View = "Permissions.WorkActivities.View";
            public const string Create = "Permissions.WorkActivities.Create";
            public const string Edit = "Permissions.WorkActivities.Edit";
            public const string Delete = "Permissions.WorkActivities.Delete";
        }
        public static class WorkOrderHeaders
        {
            public const string View = "Permissions.WorkOrderHeaders.View";
            public const string Create = "Permissions.WorkOrderHeaders.Create";
            public const string Edit = "Permissions.WorkOrderHeaders.Edit";
            public const string Delete = "Permissions.WorkOrderHeaders.Delete";
        }
        #endregion

        #region Logistics
        public static class Logistic
        {
            public const string View = "Permissions.Logistic.View";
            public const string Create = "Permissions.Logistic.Create";
            public const string Edit = "Permissions.Logistic.Edit";
            public const string Delete = "Permissions.Logistic.Delete";
        }
        #endregion

        #region Quality
        public static class Quality
        {
            public const string View = "Permissions.Quality.View";
            public const string Create = "Permissions.Quality.Create";
            public const string Edit = "Permissions.Quality.Edit";
            public const string Delete = "Permissions.Quality.Delete";
        }
        #endregion

        #region CommercialDocuments
        public static class DeliveryNote
        {
            public const string View = "Permissions.DeliveryNote.View";
            public const string Create = "Permissions.DeliveryNote.Create";
            public const string Edit = "Permissions.DeliveryNote.Edit";
            public const string Delete = "Permissions.DeliveryNote.Delete";
        }
        #endregion
    }
}