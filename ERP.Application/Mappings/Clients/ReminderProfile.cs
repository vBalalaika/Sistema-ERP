using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Domain.Entities.Clients;

namespace ERP.Application.Mappings.Clients
{
    public class ReminderProfile : Profile
    {
        public ReminderProfile()
        {
            CreateMap<Reminder, ReminderDTO>().ReverseMap();
        }
    }
}
