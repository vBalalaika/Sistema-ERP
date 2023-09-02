using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.Production.WorkActivitySpecifications
{
    public class WorkActivitiesByWorkOrderItemIdSpecification : BaseSpecification<WorkActivity>
    {
        public WorkActivitiesByWorkOrderItemIdSpecification(bool isActive, int woiId) : base(wa => wa.StateActivity == isActive && wa.WorkOrderItemId == woiId)
        {
            AddInclude(x => x.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.RelProductStations)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");

            AddInclude(x => x.WorkActions);
        }

        /* Fran 4/4/2023: Se quito filtro para no tener en cuenta las estaciones de envios ni abastecimientos: wa.ProductStation.Station.WorkOrderDisplayType != "Envios" && wa.ProductStation.Station.WorkOrderDisplayType != "Abastecimientos" */
        /* Fran 15/6/2023: Se agrega filtro para no tener en cuenta las estaciones de abastecimientos: wa.ProductStation.Station.WorkOrderDisplayType != "Abastecimientos" */
        public WorkActivitiesByWorkOrderItemIdSpecification(int woiId) : base(wa => wa.WorkOrderItemId == woiId && !wa.WorkOrderItem.Product.ProductFeature.StoredStock && wa.ProductStation.Station.WorkOrderDisplayType != "Abastecimientos")
        {
            AddInclude(x => x.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.RelProductStations)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");

            AddInclude(x => x.WorkActions);
        }

        /* Fran 4/4/2023: Se quito filtro para no tener en cuenta las estaciones de envios ni abastecimientos: wa.ProductStation.Station.WorkOrderDisplayType != "Envios" && wa.ProductStation.Station.WorkOrderDisplayType != "Abastecimientos" */
        /* Fran 15/6/2023: Se agrega filtro para no tener en cuenta las estaciones de abastecimientos: wa.ProductStation.Station.WorkOrderDisplayType != "Abastecimientos" */
        public WorkActivitiesByWorkOrderItemIdSpecification(int woiId, bool forMontaje) : base(wa => wa.WorkOrderItemId == woiId && !wa.WorkOrderItem.Product.ProductFeature.StoredStock && wa.ProductStation.Station.WorkOrderDisplayType != "Abastecimientos")
        {
            AddInclude(x => x.WorkOrderItem);
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.RelProductStations)}");
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");

            AddInclude(x => x.WorkActions);
        }
    }
}
