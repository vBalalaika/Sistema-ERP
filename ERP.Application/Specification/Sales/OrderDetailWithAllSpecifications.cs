using ERP.Domain.Entities.Clients;
using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderDetailWithAllSpecifications : BaseSpecification<OrderDetail>
    {
        public OrderDetailWithAllSpecifications()
        {
            AddInclude(oh => oh.OrderState);
            AddInclude(oh => oh.Product);
            AddInclude(oh => oh.Structure);
            AddInclude(oh => oh.SupplyVoltage);
            AddInclude(oh => oh.OrderHeader);
            AddInclude($"{nameof(OrderDetail.OrderHeader)}.{nameof(OrderHeader.Client)}");
            AddInclude($"{nameof(OrderDetail.OrderHeader)}.{nameof(OrderHeader.Client)}.{nameof(Client.Country)}");
            AddInclude($"{nameof(OrderDetail.Product)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(Structure.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.WorkActions)}");
        }

        // For sale orders only
        public OrderDetailWithAllSpecifications(bool isSale) : base(x => (isSale == true ? x.OrderHeader.ClientId != null : x.OrderHeader.ClientId == null))
        {
            AddInclude(oh => oh.OrderState);
            AddInclude(oh => oh.Product);
            AddInclude(oh => oh.Structure);
            AddInclude(oh => oh.SupplyVoltage);
            AddInclude(oh => oh.OrderHeader);
            AddInclude(oh => oh.WorkOrder);
            AddInclude($"{nameof(OrderDetail.OrderHeader)}.{nameof(OrderHeader.Client)}");
            AddInclude($"{nameof(OrderDetail.OrderHeader)}.{nameof(OrderHeader.Client)}.{nameof(Client.Country)}");
            AddInclude($"{nameof(OrderDetail.Product)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(Structure.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.WorkActions)}");
            AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}");
            AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
        }

        // For production orders only; categoryId = 4 y InHouseManufacturing = true; Deben ser todas las ordenes cuya categoría de producto no sea "Componentes comprados" y sea fabricado por Simplemak
        //public OrderDetailWithAllSpecifications(int categoryId, bool InHouseManufacturing) : base(x => x.Product.SubCategory.CategoryId != categoryId && x.Product.ProductFeature.InHouseManufacturing == InHouseManufacturing)
        //{
        //    AddInclude(oh => oh.OrderState);
        //    AddInclude(oh => oh.Product);
        //    AddInclude(oh => oh.Structure);
        //    AddInclude(oh => oh.SupplyVoltage);
        //    AddInclude(oh => oh.OrderHeader);
        //    AddInclude($"{nameof(OrderDetail.OrderHeader)}.{nameof(OrderHeader.Client)}");
        //    AddInclude($"{nameof(OrderDetail.OrderHeader)}.{nameof(OrderHeader.Client)}.{nameof(Client.Country)}");
        //    AddInclude($"{nameof(OrderDetail.Product)}.{nameof(Product.SubCategory)}");
        //    AddInclude($"{nameof(Structure.Product)}.{nameof(Product.ProductFeature)}");
        //    AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.WorkActions)}");
        //    AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}");
        //    AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
        //}

        // TODO: validar que el numero de producto arranque con 03- 
        // For production orders only; categoryId = 4 y InHouseManufacturing = true; Deben ser todas las ordenes cuya categoría de producto no sea "Componentes comprados" y sea fabricado por Simplemak
        public OrderDetailWithAllSpecifications(int categoryId, bool InHouseManufacturing) : base(x => x.Product.SubCategory.CategoryId != categoryId && x.Product.ProductFeature.InHouseManufacturing == InHouseManufacturing && (x.ProductNumber.StartsWith("04") ? (x.Product.ProductFeature.StoredStock == true || x.Product.ProductFeature.StoredStock == false) : x.Product.ProductFeature.StoredStock == false) && x.OrderStateId != 6)
        {
            AddInclude(oh => oh.OrderState);
            AddInclude(oh => oh.Product);
            AddInclude(oh => oh.Structure);
            AddInclude(oh => oh.SupplyVoltage);
            AddInclude(oh => oh.OrderHeader);
            AddInclude(oh => oh.WorkOrder);
            AddInclude($"{nameof(OrderDetail.OrderHeader)}.{nameof(OrderHeader.Client)}");
            AddInclude($"{nameof(OrderDetail.OrderHeader)}.{nameof(OrderHeader.Client)}.{nameof(Client.Country)}");
            AddInclude($"{nameof(OrderDetail.Product)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(Structure.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.WorkActions)}");
            AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}");
            AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
        }

        public OrderDetailWithAllSpecifications(int categoryId, bool InHouseManufacturing, bool noStoredStock) : base(x => x.Product.SubCategory.CategoryId == categoryId && x.Product.ProductFeature.InHouseManufacturing == InHouseManufacturing && x.Product.ProductFeature.StoredStock == noStoredStock && (x.OrderStateId <= 3 || x.OrderStateId == 5))
        {
            AddInclude(oh => oh.OrderState);
            AddInclude(oh => oh.Product);
            AddInclude(oh => oh.Structure);
            AddInclude(oh => oh.SupplyVoltage);
            AddInclude(oh => oh.OrderHeader);
            //AddInclude($"{nameof(OrderDetail.OrderHeader)}.{nameof(OrderHeader.Client)}");
            //AddInclude($"{nameof(OrderDetail.OrderHeader)}.{nameof(OrderHeader.Client)}.{nameof(Client.Country)}");
            AddInclude($"{nameof(OrderDetail.Product)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(Structure.Product)}.{nameof(Product.ProductFeature)}");
            //AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.WorkActions)}");
            //AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}");
            //AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
        }

        public OrderDetailWithAllSpecifications(int id) : base(x => x.Id == id)
        {
            AddInclude(oh => oh.OrderState);
            AddInclude(oh => oh.Product);
            AddInclude(oh => oh.Structure);
            AddInclude(oh => oh.SupplyVoltage);
            AddInclude(oh => oh.OrderHeader);
            AddInclude($"{nameof(OrderDetail.OrderHeader)}.{nameof(OrderHeader.Client)}");
            AddInclude($"{nameof(OrderDetail.OrderHeader)}.{nameof(OrderHeader.Client)}.{nameof(Client.Country)}");
            AddInclude($"{nameof(OrderDetail.Product)}.{nameof(Product.UnitMeasure)}");
            AddInclude($"{nameof(OrderDetail.Product)}.{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");
            AddInclude($"{nameof(Structure.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(OrderDetail.Structure)}.{nameof(Structure.LastVersion)}");
            AddInclude($"{nameof(OrderDetail.WorkOrder)}.{nameof(WorkOrder.WorkOrderItems)}.{nameof(WorkOrderItem.WorkActivities)}.{nameof(WorkActivity.WorkActions)}");

        }
    }
}
