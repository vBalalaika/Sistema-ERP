using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Managers.ProductMod
{
    public class PieceFormatViewManager : ViewManager<PieceFormat, PieceFormatDTO>, IPieceormatViewManager
    {
        private readonly IPieceFormatService _pieceformatservice;
        private readonly IMapper _mapper;

        public PieceFormatViewManager(IPieceFormatService pieceformatservice, IMapper mapper) :
           base(pieceformatservice, mapper)
        {
            _pieceformatservice = pieceformatservice;
            _mapper = mapper;
        }
    }
}