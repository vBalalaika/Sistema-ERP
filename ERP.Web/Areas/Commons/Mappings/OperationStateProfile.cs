using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Web.Areas.Commons.Models;

namespace ERP.Web.Areas.Commons.Mappings
{
    public class OperationStateProfile : Profile
    {
        public OperationStateProfile()
        {
            CreateMap<OperationStateDTO, OperationStateViewModel>().ReverseMap();
        }
    }
}
