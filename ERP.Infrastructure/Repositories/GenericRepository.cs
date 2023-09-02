using ERP.Application.Interfaces.Repositories;
using ERP.Application.Specification;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IReadOnlyList<T>> GetListAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }

        private void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);

        }

        public async Task DeleteAsync(int id)
        {
            var entityToDelete = await GetByIdAsync(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        public virtual Task<T> GetByIdFullAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<T>> FindAsync(ISpecification<T> specification = null)
        {
            return Task.FromResult(FindWithSpecificationPattern(specification));
        }

        private IQueryable<T> FindWithSpecificationPattern(ISpecification<T> specification = null)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), specification).AsNoTracking();
        }

    }
}
