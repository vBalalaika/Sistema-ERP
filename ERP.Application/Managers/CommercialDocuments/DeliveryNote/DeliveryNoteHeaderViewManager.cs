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
    public class DeliveryNoteHeaderViewManager : ViewManager<DeliveryNoteHeader, DeliveryNoteHeaderDTO>, IDeliveryNoteHeaderViewManager
    {
        private readonly IDeliveryNoteHeaderService _deliveryNoteHeaderService;
        private readonly IMapper _mapper;

        public DeliveryNoteHeaderViewManager(IDeliveryNoteHeaderService deliveryNoteHeaderService, IMapper mapper) : base(deliveryNoteHeaderService, mapper)
        {
            _deliveryNoteHeaderService = deliveryNoteHeaderService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<DeliveryNoteHeaderDTO>>> FindBySpecification(ISpecification<DeliveryNoteHeader> specification)
        {
            var deliveryNoteHeaders = await _deliveryNoteHeaderService.FindWithSpecificationPattern(specification);
            var deliveryNoteHeadersDTO = _mapper.Map<IReadOnlyList<DeliveryNoteHeaderDTO>>(deliveryNoteHeaders);
            return Result<IReadOnlyList<DeliveryNoteHeaderDTO>>.Success(deliveryNoteHeadersDTO);
        }
    }
}
