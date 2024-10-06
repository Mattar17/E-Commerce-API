using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {

        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specs)
        {
            var Query = inputQuery;
            if (specs.Criteria != null)
            {
                Query = Query.Where(specs.Criteria);
            }

            if (specs.OrderBy != null)
            {
                Query = Query.OrderBy(specs.OrderBy);
            }

            if (specs.OrderByDescending != null)
            {
                Query = Query.OrderByDescending(specs.OrderByDescending);
            }

            if (specs.IsPaginationEnabled)
            {
                Query = Query.Skip(specs.Skip).Take(specs.Take);
            }

            Query = specs.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

            return Query;
        }
    }
}
