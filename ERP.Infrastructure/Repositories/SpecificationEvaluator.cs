using ERP.Application.Specification;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ERP.Infrastructure.Repositories
{
    public class SpecificationEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            // fetch a Queryable that includes all expression-based includes
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            // modify the IQueryable to include any string-based include statements
            query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

            if (spec.GroupBy != null)
            {
                query = query.GroupBy(spec.GroupBy).SelectMany(x => x);
            }

            // Apply paging if enabled
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip)
                             .Take(spec.Take);
            }
            return query;
        }
    }
}
