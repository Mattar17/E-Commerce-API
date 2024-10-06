using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecification()
        {

        }

        public BaseSpecification(Expression<Func<T, bool>> _criteria)
        {
            Criteria = _criteria;
        }

        public void AddOrderBy(Expression<Func<T, object>> OrderbyExpression)
        {
            OrderBy = OrderbyExpression;
        }

        public void AddOrderByDescending(Expression<Func<T, object>> OrderByDescendingExpression)
        {
            OrderByDescending = OrderByDescendingExpression;
        }

        public void ApplyPagination(int take, int skip)
        {
            IsPaginationEnabled = true;
            Take = take;
            Skip = skip;
        }
    }
}
