using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly G02DbContext _dbContext;

        public GenericRepository(G02DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _dbContext.Products.Include(p => p.ProductBrand)
                .Include(p => p.ProductType).ToListAsync();
            }
            return await _dbContext.Set<T>().ToListAsync(); 
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllWithSpecs(ISpecification<T> specs)
        {
            return await ApplySpecification(specs).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecs(ISpecification<T> specs)
        {
            return await ApplySpecification(specs).FirstOrDefaultAsync();
        }

        public IQueryable<T> ApplySpecification(ISpecification<T> specs)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), specs);
        }

        public async Task Add(T item)
        {
             await _dbContext.Set<T>().AddAsync(item);
        }
    }
}
