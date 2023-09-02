using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.Exceptions;
using ERP.Application.Interfaces.Services;
using ERP.Application.Interfaces.ViewManagers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers
{
    public class ViewManager<T, TDto> : IViewManager<T, TDto> where T : class
    {
        private readonly IGenericService<T, TDto> _genericService;
        private readonly IMapper _mapper;

        public ViewManager(IGenericService<T, TDto> genericService, IMapper mapper)
        {
            _genericService = genericService;
            _mapper = mapper;
        }

        public async Task<Result<int>> CreateAsync(TDto entityToCreate)
        {
            int idCreated = await _genericService.CreateAsync(entityToCreate);
            return Result<int>.Success(idCreated);
        }

        public async Task<Result<int>> DeleteAsync(int id)
        {
            try
            {
                var idDeleted = await _genericService.DeleteAsync(id);
                return Result<int>.Success(idDeleted);
            }
            catch (ElementNotFoundException ex)
            {
                return Result<int>.Fail(ex.Message);
            }
        }

        public async Task<Result<TDto>> GetByIdAsync(int id)
        {
            T entity = await _genericService.GetByIdAsync(id);
            TDto entityDto = _mapper.Map<TDto>(entity);
            return Result<TDto>.Success(entityDto);
        }

        public async Task<Result<IReadOnlyList<TDto>>> LoadAll()
        {
            IReadOnlyList<T> entities = await _genericService.GetListAsync();
            IReadOnlyList<TDto> entitiesDtos = _mapper.Map<IReadOnlyList<TDto>>(entities);
            return Result<IReadOnlyList<TDto>>.Success(entitiesDtos);
        }

        public async Task<Result<int>> UpdateAsync(TDto entityToUpdate)
        {
            int result = await _genericService.UpdateAsync(entityToUpdate);
            return Result<int>.Success(result);
        }
    }
}
