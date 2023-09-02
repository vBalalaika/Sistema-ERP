using AutoMapper;
using ERP.Application.DTOs.Entities.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using ERP.Web.Areas.Logistics.Models.DeliveryNote;
using ERP.Web.Areas.Production.Models;
using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.Logistics.Mappings.DeliveryNote
{
    public class DeliveryNoteDetailProfile : Profile
    {
        public DeliveryNoteDetailProfile()
        {
            CreateMap<DeliveryNoteDetailDTO, DeliveryNoteDetailViewModel>().ReverseMap();

            CreateMap<DeliveryNoteHeader, DeliveryNoteHeaderViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Structure, StructureViewModel>().ReverseMap();
            CreateMap<WorkActivity, WorkActivityViewModel>().ReverseMap();
        }
    }
}
