﻿using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class PieceFormatProfile : Profile
    {
        public PieceFormatProfile()
        {
            CreateMap<PieceFormat, PieceFormatDTO>().ReverseMap();
        }
    }
}
