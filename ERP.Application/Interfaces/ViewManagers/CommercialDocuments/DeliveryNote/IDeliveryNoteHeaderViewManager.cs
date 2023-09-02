using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.CommercialDocuments.DeliveryNote;
using ERP.Application.Specification;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.CommercialDocuments.DeliveryNote
{
    public interface IDeliveryNoteHeaderViewManager : IViewManager<DeliveryNoteHeader, DeliveryNoteHeaderDTO>
    {
        Task<Result<IReadOnlyList<DeliveryNoteHeaderDTO>>> FindBySpecification(ISpecification<DeliveryNoteHeader> specification);
    }
}
