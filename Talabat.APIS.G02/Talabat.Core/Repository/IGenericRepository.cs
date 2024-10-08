﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task Add(T item);
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);

        public Task<IReadOnlyList<T>> GetAllWithSpecs(ISpecification<T> specs);

        public Task<T> GetByIdWithSpecs(ISpecification<T> specs);

    }
}
