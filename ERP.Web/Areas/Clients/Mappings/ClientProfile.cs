using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Web.Areas.Clients.Models;

namespace ERP.Web.Areas.Clients.Mappings
{
    internal class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientDTO, ClientViewModel>().ReverseMap();
        }
    }
}
