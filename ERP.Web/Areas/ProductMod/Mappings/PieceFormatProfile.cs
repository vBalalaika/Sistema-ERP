using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.ProductMod.Mappings
{
    internal class PieceFormatProfile : Profile
    {
        public PieceFormatProfile()
        {
            CreateMap<PieceFormatDTO, PieceFormatViewModel>().ReverseMap();
        }
    }
}
