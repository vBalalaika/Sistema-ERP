using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Production.WorkActivitySpecifications
{
    public class WorkActivityWithAllSpecification : BaseSpecification<WorkActivity>
    {
        public WorkActivityWithAllSpecification()
        {
            AddInclude(x => x.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Product)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Structure)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
        }
        public WorkActivityWithAllSpecification(int activityId) : base(wa => wa.Id == activityId)
        {
            AddInclude(x => x.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
        }
        public WorkActivityWithAllSpecification(int activityId, int woiProductId, int stationId) : base(wa => wa.Id == activityId || (wa.ProductStation.StationId == stationId && wa.StateActivity == true && wa.WorkOrderItem.ProductId == woiProductId))
        {
            AddInclude(x => x.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
        }
        public WorkActivityWithAllSpecification(bool isActive) : base(x => x.StateActivity == isActive)
        {
            AddInclude(x => x.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Product)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Structure)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(WorkActivity.WorkActions)}");
        }

        public WorkActivityWithAllSpecification(bool isActive, int activityId) : base(x => x.StateActivity == isActive && x.Id == activityId)
        {
            AddInclude(x => x.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Product)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Structure)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(WorkActivity.WorkActions)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.OrderState)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Structure)}");

            AddInclude(x => x.OutsourcedProvider);
        }
        public WorkActivityWithAllSpecification(bool isActive, bool toShipments) : base(x =>
        x.StateActivity == isActive &&
        (toShipments ? (x.ToShipments.HasValue ? (x.ToShipments.Value == toShipments || x.ProductStation.Station.WorkOrderDisplayType == "Envios") : (x.ProductStation.Station.WorkOrderDisplayType == "Envios")) : (x.ToShipments.HasValue ? (x.ToShipments.Value == toShipments || x.ProductStation.Station.WorkOrderDisplayType != "Envios") : (x.ProductStation.Station.WorkOrderDisplayType != "Envios")))
        && x.ProductStation.Station.WorkOrderDisplayType != "Abastecimientos")
        {
            AddInclude(x => x.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Product)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Structure)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}.{nameof(Station.FunctionalArea)}");
            AddInclude($"{nameof(WorkActivity.WorkActions)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.OrderState)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Structure)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.RelProductStations)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");
        }

    }
}
