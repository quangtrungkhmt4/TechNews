using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCore.Common.Helpers
{
    public static class EntityQueryFilterHelper
    {
        public static Func<IQueryable<T>, IQueryable<T>> Page<T>(int pageAt = 1, int pageSize = 20)
        {
            var myPage = pageAt < 1 ? 1 : pageAt;
            var myPageSize = pageSize <= 0 ? 20 : pageSize;
            return source => source.Skip((myPage - 1) * pageSize).Take(myPageSize);
        }

        public static Func<IQueryable<T>, IQueryable<T>> Sort<T, TKey>(Expression<Func<T, TKey>> sorter, bool descending = false)
        {
            return source => descending ? source.OrderByDescending(sorter) : source.OrderBy(sorter);
        }

        public static Func<IQueryable<T>, IQueryable<T>> CreateSort<T>(bool descending = false, params string[] sortExprs)
        {
            return source =>
            {
                if (sortExprs == null)
                {
                    return source;
                }

                var type = typeof(T);
                var parameter = Expression.Parameter(type, "p");
                var isFirst = true;
                MethodCallExpression resultExp = null;
                foreach (var sortExpr in sortExprs)
                {
                    var property = type.GetProperty(sortExpr);
                    if (property == null)
                    {
                        continue;
                    }
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);

                    if (isFirst)
                    {
                        resultExp = Expression.Call(typeof(Queryable), descending ? "OrderByDescending" : "OrderBy",
                                                    new[] { type, property.PropertyType }, source.Expression,
                                                    Expression.Quote(orderByExp));
                        isFirst = false;
                    }
                    else
                    {
                        resultExp = Expression.Call(typeof(Queryable), descending ? "ThenByDescending" : "ThenBy",
                                                    new[] { type, property.PropertyType }, resultExp,
                                                    Expression.Quote(orderByExp));
                    }
                }

                return resultExp == null ? source : source.Provider.CreateQuery<T>(resultExp);
            };
        }
    }
}
