using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.CommercialDocuments.DeliveryNote
{
    public interface IDeliveryNoteDetailRepository : IGenericRepository<DeliveryNoteDetail>
    {
        Task<IReadOnlyList<DeliveryNoteDetail>> GetAll();
    }
}
