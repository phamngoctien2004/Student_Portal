using Domain.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Extensions
{
    public static class QueryableEtx
    {
        //public static IQueryable<T> Equal<T, TValue>(this IQueryable<T> query, Expression<Func<T, TValue>> expression, TValue value)
        //{
        //    if (value == null) return query;
        //    var function = expression.Compile();
        //    return query.Where(x => function(x).Equals(value));
        //}
        //public static IQueryable<T> Contain<T>(this IQueryable<T> query, Expression<Func<T, string>> expression, string value)
        //{
        //    if(value == null) return query;
        //    var function = expression.Compile();
        //    return query.Where(x => function(x).Contains(value));
        //}
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
            {
                return query;
            }
            // kiểm tra cột hợp lệ
            var propertyInfo = typeof(T).GetProperty(sortColumn,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                return query;
            }

            // dynamic linq
            var sortExpression = $"{propertyInfo.Name} {(sortDirection == "desc" ? "descending" : "ascending")}";
            return query.OrderBy(sortExpression);
        }
    }
}
