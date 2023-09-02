using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Domain.Entities.Commons;

namespace ERP.Application.Mappings.Commons
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<DocumentType, DocumentTypeDTO>().ReverseMap();
        }
    }
}
