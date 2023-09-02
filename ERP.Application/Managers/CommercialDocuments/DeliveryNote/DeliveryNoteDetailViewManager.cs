using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.CommercialDocuments.DeliveryNote;
using ERP.Application.Interfaces.Services.CommercialDocuments.DeliveryNote;
using ERP.Application.Interfaces.ViewManagers.CommercialDocuments.DeliveryNote;
using ERP.Application.Specification;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.CommercialDocuments.DeliveryNote
{
    public class DeliveryNoteDetailViewManager : ViewManager<DeliveryNoteDetail, DeliveryNoteDetailDTO>, IDeliveryNoteDetailViewManager
    {
        private readonly IDeliveryNoteDetailService _deliveryNoteDetailService;
        private readonly IMapper _mapper;

        public DeliveryNoteDetailViewManager(IDeliveryNoteDetailService deliveryNoteDetailService, IMapper mapper) : base(deliveryNoteDetailService, mapper)
        {
            _deliveryNoteDetailService = deliveryNoteDetailService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<DeliveryNoteDetailDTO>>> FindBySpecification(ISpecification<DeliveryNoteDetail> specification)
        {
            var deliveryNoteDetails = await _deliveryNoteDetailService.FindWithSpecificationPattern(specification);
            var deliveryNoteDetailsDTO = _mapper.Map<IReadOnlyList<DeliveryNoteDetailDTO>>(deliveryNoteDetails);
            return Result<IReadOnlyList<DeliveryNoteDetailDTO>>.Success(deliveryNoteDetailsDTO);
        }
    }
}
