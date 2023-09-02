﻿using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Mappings.Purchases.Providers
{
    public class RelProviderProductProfile : Profile
    {
        public RelProviderProductProfile()
        {
            CreateMap<RelProviderProduct, RelProviderProductDTO>().ReverseMap();
        }
    }
}
