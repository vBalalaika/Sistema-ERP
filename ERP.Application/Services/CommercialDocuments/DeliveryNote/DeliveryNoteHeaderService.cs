using AutoMapper;
using ERP.Application.DTOs.Entities.CommercialDocuments.DeliveryNote;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Application.Services.CommercialDocuments.DeliveryNote
{
    public class DeliveryNoteHeaderService : GenericService<DeliveryNoteHeader, DeliveryNoteHeaderDTO>, IDeliveryNoteHeaderService
    {
        public DeliveryNoteHeaderService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public override async Task<int> CreateAsync(DeliveryNoteHeaderDTO entityToCreateDTO)
        {
            await MapDetails(entityToCreateDTO);
            return await base.CreateAsync(entityToCreateDTO);
        }

        public async Task MapDetails(DeliveryNoteHeaderDTO deliveryNoteHeaderDTO)
        {
            var oldDetails = _unitOfWork.DeliveryNoteDetailRepository.GetAll().Result.Where(dnd => dnd.DeliveryNoteHeaderId == deliveryNoteHeaderDTO.Id).ToList();

            // To create new details
            foreach (var detail in deliveryNoteHeaderDTO.DeliveryNoteDetails.Where(dnd => dnd.Id == 0))
            {
                await _unitOfWork.DeliveryNoteDetailRepository.CreateAsync(detail);
            }

            // To update details
            foreach (var detail in deliveryNoteHeaderDTO.DeliveryNoteDetails.Where(dnd => dnd.Id != 0 && oldDetails.Any(dndOld => dndOld.Id == dnd.Id)))
            {
                _unitOfWork.DeliveryNoteDetailRepository.Update(detail);
            }

            // To delete details

        }
    }
}
