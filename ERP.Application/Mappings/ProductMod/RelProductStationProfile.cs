﻿using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class RelProductStationProfile : Profile
    {
        public RelProductStationProfile()
        {
            CreateMap<RelProductStation, RelProductStationDTO>().ReverseMap();
        }
    }
}
