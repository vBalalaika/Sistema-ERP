using ERP.Infrastructure.Identity.Models;
using ERP.Web.Areas.Admin.Models;
using AutoMapper;

namespace ERP.Web.Areas.Admin.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}