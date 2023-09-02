using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Commons
{
    public class DocumentTypeViewManager : IDocumentTypeViewManager
    {
        private readonly IDocumentTypeService _documentTypeService;
        private readonly IMapper _mapper;

        public DocumentTypeViewManager(IDocumentTypeService documentTypeService, IMapper mapper)
        {
            _documentTypeService = documentTypeService;
            _mapper = mapper;
        }

        public async Task<Result<DocumentTypeDTO>> GetByIdAsync(int id)
        {
            DocumentType entity = await _documentTypeService.GetByIdAsync(id);
            DocumentTypeDTO entityDto = _mapper.Map<DocumentTypeDTO>(entity);
            return Result<DocumentTypeDTO>.Success(entityDto);
        }

        public async Task<Result<IReadOnlyList<DocumentTypeDTO>>> LoadAll()
        {
            IReadOnlyList<DocumentType> entities = await _documentTypeService.GetListAsync();
            IReadOnlyList<DocumentTypeDTO> entitiesDtos = _mapper.Map<IReadOnlyList<DocumentTypeDTO>>(entities);
            return Result<IReadOnlyList<DocumentTypeDTO>>.Success(entitiesDtos);
        }
    }
}
