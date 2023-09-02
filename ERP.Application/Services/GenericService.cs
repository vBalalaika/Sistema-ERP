using AutoMapper;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services;
using ERP.Application.Specification;
using ERP.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services
{
    public class GenericService<T, TDTO> : IGenericService<T, TDTO> where T : class, IAuditableBaseEntity where TDTO : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public GenericService(IUnitOfWork unitOfWOrk, IMapper mapper)
        {
            _unitOfWork = unitOfWOrk;
            _mapper = mapper;

        }

        public virtual async Task<int> CreateAsync(TDTO entityToCreateDTO)
        {
            T entityToCreate = MapToEntity(entityToCreateDTO);
            T entity = await _unitOfWork.GetRepository<T>().CreateAsync(entityToCreate);

            await _unitOfWork.Commit();
            return entity.Id;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<T>().GetByIdAsync(id);
        }

        public virtual async Task<TDTO> GetByIdFullAsync(int id)
        {
            T entity = await _unitOfWork.GetRepository<T>().GetByIdFullAsync(id);
            return MapToDto(entity);
        }

        public virtual async Task<int> UpdateAsync(TDTO entityDtoToUpdate)
        {
            T entity = MapToEntity(entityDtoToUpdate);

            _unitOfWork.GetRepository<T>().Update(entity);

            await _unitOfWork.Commit();
            return entity.Id;
        }
        public virtual async Task<int> DeleteAsync(int id)
        {
            T entityToDelete = await _unitOfWork.GetRepository<T>().GetByIdAsync(id);

            await _unitOfWork.GetRepository<T>().DeleteAsync(id);
            await _unitOfWork.Commit();
            return id;
        }
        protected virtual T MapToEntity(TDTO entityDTO)
        {
            return _mapper.Map<T>(entityDTO);
        }
        protected virtual TDTO MapToDto(T entity)
        {
            return _mapper.Map<TDTO>(entity);
        }

        public async Task<IReadOnlyList<T>> GetListAsync()
        {
            return await _unitOfWork.GetRepository<T>().GetListAsync();
        }
        public async Task<IEnumerable<T>> FindWithSpecificationPattern(ISpecification<T> specification = null)
        {
            return await _unitOfWork.GetRepository<T>().FindAsync(specification);
        }
    }
}
