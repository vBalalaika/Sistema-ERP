using AutoMapper;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.Commons
{
    public class DocumentTypeService : IDocumentTypeService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public DocumentTypeService(IUnitOfWork unitOfWOrk, IMapper mapper)
        {
            _unitOfWork = unitOfWOrk;
            _mapper = mapper;
        }

        public async Task<DocumentType> GetByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<DocumentType>().GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<DocumentType>> GetListAsync()
        {
            return await _unitOfWork.GetRepository<DocumentType>().GetListAsync();
        }

    }
}
