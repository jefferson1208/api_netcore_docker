using System;
using System.Linq;
using System.Linq.Expressions;

namespace App.Docker.Domain.Queries
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                                    Expression<Func<T, bool>> expr2)
        {
            var invoke = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                (Expression.AndAlso(expr1.Body, invoke), expr1.Parameters);
        }
    }
}
