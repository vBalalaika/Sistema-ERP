using AutoMapper;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.Commons
{
    public class CountryService : ICountryService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWOrk, IMapper mapper)
        {
            _unitOfWork = unitOfWOrk;
            _mapper = mapper;
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<Country>().GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<Country>> GetListAsync()
        {
            //For get all
            //return await _unitOfWork.GetRepository<Country>().GetListAsync();

            //For get all active
            return await _unitOfWork.CountryRepository.GetListActiveAsync();
        }

    }
}
