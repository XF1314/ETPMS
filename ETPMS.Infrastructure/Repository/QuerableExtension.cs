using System;
using System.Linq;
using System.Linq.Expressions;

namespace ETPMS.Infrastructure.Repository
{
    public static class QuerableExtension
    {
        /// <summary>
        /// 扩展IQueryable的OrderBy方法
        /// </summary>
        /// <typeparam name="TEntity">泛型类T</typeparam>
        /// <param name="source">类集合</param>
        /// <param name="orderByProperty">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>排序后类集合</returns>
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool isAsc) where TEntity : class
        {
            string command = isAsc ? "OrderBy" : "OrderByDescending";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
