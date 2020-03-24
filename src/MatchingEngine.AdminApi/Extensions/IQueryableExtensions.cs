using System;
using System.Linq;
using System.Linq.Expressions;

namespace MatchingEngine.AdminApi.Extensions
{
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> source, string propertyName, string direction)
        {
            if (direction == "asc")
                return source.OrderBy(ToLambda<T>(propertyName));

            return source.OrderByDescending(ToLambda<T>(propertyName));
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderBy(ToLambda<T>(propertyName));
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderByDescending(ToLambda<T>(propertyName));
        }

        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }
    }
}
