using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.CommercialDocuments.DeliveryNote;
using ERP.Application.Specification;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.CommercialDocuments.DeliveryNote
{
    public interface IDeliveryNoteDetailViewManager : IViewManager<DeliveryNoteDetail, DeliveryNoteDetailDTO>
    {
        Task<Result<IReadOnlyList<DeliveryNoteDetailDTO>>> FindBySpecification(ISpecification<DeliveryNoteDetail> specification);
    }
}
