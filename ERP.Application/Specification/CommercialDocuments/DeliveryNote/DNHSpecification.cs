using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.CommercialDocuments.DeliveryNote
{
    public class DNHSpecification : BaseSpecification<DeliveryNoteHeader>
    {
        public DNHSpecification()
        {

        }

        public DNHSpecification(int id) : base(dnh => dnh.Id == id)
        {
            AddInclude(dnh => dnh.DeliveryNoteDetails);
            AddInclude(dnh => dnh.Provider);
            AddInclude(dnh => dnh.TransportProvider);
            AddInclude($"{nameof(DeliveryNoteHeader.Provider)}.{nameof(Provider.RelProviderStations)}");
            AddInclude($"{nameof(DeliveryNoteHeader.DeliveryNoteDetails)}.{nameof(DeliveryNoteDetail.ProductItem)}");
            AddInclude($"{nameof(DeliveryNoteHeader.DeliveryNoteDetails)}.{nameof(DeliveryNoteDetail.ProductItem)}.{nameof(Product.UnitMeasure)}");
            AddInclude($"{nameof(DeliveryNoteHeader.DeliveryNoteDetails)}.{nameof(DeliveryNoteDetail.Product)}");
            AddInclude($"{nameof(DeliveryNoteHeader.DeliveryNoteDetails)}.{nameof(DeliveryNoteDetail.Configuration)}");
            AddInclude($"{nameof(DeliveryNoteHeader.DeliveryNoteDetails)}.{nameof(DeliveryNoteDetail.WorkActivity)}");
            AddInclude($"{nameof(DeliveryNoteHeader.DeliveryNoteDetails)}.{nameof(DeliveryNoteDetail.WorkActivity)}.{nameof(WorkActivity.ProductStation)}");
            AddInclude($"{nameof(DeliveryNoteHeader.DeliveryNoteDetails)}.{nameof(DeliveryNoteDetail.WorkActivity)}.{nameof(WorkActivity.WorkOrderItem)}");
            AddInclude($"{nameof(DeliveryNoteHeader.DeliveryNoteDetails)}.{nameof(DeliveryNoteDetail.WorkActivity)}.{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}");
            AddInclude($"{nameof(DeliveryNoteHeader.DeliveryNoteDetails)}.{nameof(DeliveryNoteDetail.WorkActivity)}.{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.WorkOrderHeader)}");
            AddInclude($"{nameof(DeliveryNoteHeader.DeliveryNoteDetails)}.{nameof(DeliveryNoteDetail.WorkActivity)}.{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}");
            AddInclude($"{nameof(DeliveryNoteHeader.DeliveryNoteDetails)}.{nameof(DeliveryNoteDetail.WorkActivity)}.{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}");
            AddInclude($"{nameof(DeliveryNoteHeader.DeliveryNoteDetails)}.{nameof(DeliveryNoteDetail.WorkActivity)}.{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Product)}");
            AddInclude($"{nameof(DeliveryNoteHeader.DeliveryNoteDetails)}.{nameof(DeliveryNoteDetail.WorkActivity)}.{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.WorkOrder)}.{nameof(WorkOrder.OrderDetail)}.{nameof(OrderDetail.Structure)}");
        }
    }
}
